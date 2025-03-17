using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProを利用するための名前空間
using UnityEngine.UI;

public class PlacementPlayerPopUp : MonoBehaviour
{
    [Header("ScriptableObject参照")]
    //プレイヤー（主人公）を含むOfficerDataのリストを保持しているScriptableObject
    public VagaOfficerDataSO vagaOfficerDataSO;

    //meritValueに応じてOfficerRankを更新するためのクラス
    public OfficerRankUpDater rankUpDater;

    [Header("UI参照")]
    //「功績値追加ボタン」
    public Button btnPlayerMVUp;

    // 画面に表示する功績値用テキスト（TextMeshPro）
    public TMP_Text txtMeritValue;

    // 画面に表示する主人公階級用テキスト（TextMeshPro）
    public TMP_Text txtPlayerRank;

    // 主人公のスプライトを表示するUI Image
    public Image ImgPlayer;

    // 主人公の名前を表示するテキスト
    public TMP_Text txtPlayerName;

    // 主人公のステータスを表示するテキスト
    public TMP_Text txtPlayerStatus;

    // 主人公の攻撃力を表示するテキスト
    public TMP_Text txtPlayerAttackPower;

    // 主人公の防御力を表示するテキスト
    public TMP_Text txtPlayerDeffensePower;

    [Header("プレイヤーのOfficerDataリスト上のインデックス")]
    //主人公（プレイヤー）がvagaOfficerDataSO.vagaOfficerDataListの何番目に格納されているか
    public int playerIndex = 0;

    //内部で扱う主人公のOfficerData参照
    private OfficerData playerOfficerData;

    private void Start()
    {
        // ScriptableObject が正しくアサインされているか確認
        if (vagaOfficerDataSO == null)
        {
            Debug.Log("VagaOfficerDataSOがアサインされていません。");
            return; //処理を終了する
        }
        // rankUpDater が正しくアサインされているか確認
        if (rankUpDater == null)
        {
            Debug.Log("OfficerRankUpDataがアサインされていません。");
            return; //処理を終了する
        }
        // playerIndex の範囲がリストの範囲内か確認
        if (playerIndex < 0 || playerIndex >= vagaOfficerDataSO.vagaOfficerDataList.Count) //playerIndexが０以下もしくはリストの数値以上であれば
        {
            Debug.Log("playerIndexがリストの範囲外です。");
            return; //処理を終了する
        }

        //主人公のOfficerDataを取得
        playerOfficerData = vagaOfficerDataSO.vagaOfficerDataList[playerIndex]; //vagaOfficerDataListからplayerIndexを取得（０番目）

        //初期表示をセット
        txtMeritValue.text = playerOfficerData.meritValue.ToString();　//プレイヤーの功績値の初期値をリストプレイヤーから設定。ToString型にして読めるようにする
        txtPlayerRank.text = playerOfficerData.officerRank.ToString();　//プレイヤーの階級の初期値をリストプレイヤーから設定。ToString型は必要らしい。
        ImgPlayer.sprite = playerOfficerData.OfficerSprite;
        txtPlayerName.text = playerOfficerData.OfficerName;
        txtPlayerStatus.text = playerOfficerData.OfficerStatus;
        txtPlayerAttackPower.text = playerOfficerData.attackPower.ToString();
        txtPlayerDeffensePower.text = playerOfficerData.defensePower.ToString();

        // ボタンクリック時の処理を登録
        btnPlayerMVUp.onClick.AddListener(OnClickPlayerMVUp); //AddListenerによりOnClickPlayerMVUpメソッドが追加される
    }

    // 「功績値追加」ボタンが押されたときの処理
    private void OnClickPlayerMVUp()
    {
        // 主人公の功績値を50加算
        playerOfficerData.meritValue += 50;

        // 全将校のランクを再計算（OfficerRankUpDater を利用）
        rankUpDater.UpdateOfficerRanks();

        // 再計算後の主人公データを UI に反映
        txtMeritValue.text = playerOfficerData.meritValue.ToString();
        txtPlayerRank.text = playerOfficerData.officerRank.ToString();
    }



}

