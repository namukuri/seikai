using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficerRankUpDater : MonoBehaviour
{
    // OfficerDataSOをInspectorからアサインできるようにする
    public VagaOfficerDataSO vagaofficerDataSO;

    // meritValueに応じたランクを更新するメソッド
    public void UpdateOfficerRanks() //OfficerDataListのmeritValueに応じて階級を変更する
    {
        if (vagaofficerDataSO == null) //officerDataSOがなければ
        {
            Debug.LogWarning("vagaOfficerDataSOがアサインされていません");　// Debug.LogWarningで警告つきの出力　Debug.Logでもよい？
            return; //以下の処理を実行せず終了する。
        }

         foreach (OfficerData officer in vagaofficerDataSO.vagaOfficerDataList)　//foreachで全要素を処理。このうちvagaofficerDataSOは定義されたOfficerDataSO型の変数であることを確認。教材のとおりfor分で書くのなら以下の通りになる
            //for(int i = 0; i < vagaofficerDataSO.vagaOfficerDataList.Cout; i++)
        {
            //OfficerData officer = vagaofficerDataSO.vagaOfficerDataList[i];
            if (officer.meritValue >= 12800) //もしv_OfficerDataListでつくられたV_OfficerDataの変数meritValueが12800以上なら
            {
                officer.officerRank = OfficerRank.元帥;　//v_OfficerDataListでつくられた変数officerRankをOfficerRnakで作った通りの元帥にしなさい。※以下略
            }
            else if (officer.meritValue >= 6400)
            {
                officer.officerRank = OfficerRank.大将;
            }
            else if (officer.meritValue >= 3200)
            {
                officer.officerRank = OfficerRank.中将;
            }
            else if (officer.meritValue >= 1600)
            {
                officer.officerRank = OfficerRank.少将;
            }
            else if (officer.meritValue >= 800)
            {
                officer.officerRank = OfficerRank.大佐;
            }
            else if (officer.meritValue >= 400)
            {
                officer.officerRank = OfficerRank.中佐;
            }
            else if (officer.meritValue >= 200)
            {
                officer.officerRank = OfficerRank.少佐;
            }
            else if (officer.meritValue >= 100)
            {
                officer.officerRank = OfficerRank.大尉;
            }
            else if (officer.meritValue >= 50)
            {
                officer.officerRank = OfficerRank.中尉;
            }
            else
            {
                // 50未満の場合は初期状態として少尉に設定
                officer.officerRank = OfficerRank.少尉;
            }
        }

        Debug.Log("全将校のランクを更新しました");
    }
    // Start is called before the first frame update
    void Start()
    {
       // UpdateOfficerRanks();
    }

    /// <summary>
    /// 戻り値のあるメソッド(voidと書いてない）retunがないとエラーになる
    /// </summary>
    /// <param name="meritValue"></param>
    /// <returns></returns>
    public OfficerRank UpdateOfficerRank(int meritValue)
    {
        Debug.Log("meritValu" + meritValue);
        switch (meritValue)
        {
            case int value when value >= 12800:
                return OfficerRank.元帥;
            case int value when value >= 6400:
                return OfficerRank.大将;
            case int value when value >= 3200:
                return OfficerRank.中将;
            case int value when value >= 1600:
                return OfficerRank.少将;
            case int value when value >= 800:
                return OfficerRank.大佐;
            case int value when value >= 400:
                return OfficerRank.中佐;
            case int value when value >= 200:
                return OfficerRank.少佐;
            case int value when value >= 100:
                return OfficerRank.大尉;
            case int value when value >= 50:
                return OfficerRank.中尉;
            default:
                return OfficerRank.少尉;
        }
    }
}
