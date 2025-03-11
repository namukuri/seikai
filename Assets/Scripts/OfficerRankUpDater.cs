using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficerRankUpDater : MonoBehaviour
{
    // V_OfficerDataSOをInspectorからアサインできるようにする
    public V_OfficerDataSO officerDataSO;

    // meritValueに応じたランクを更新するメソッド
    public void UpdateOfficerRanks()
    {
        if (officerDataSO == null)
        {
            Debug.LogWarning("OfficerDataSOがアサインされていません");
            return;
        }

         foreach (V_OfficerData officer in officerDataSO.v_OfficerDataList)
        {
            if (officer.meritValue >= 12800)
            {
                officer.officerRank = OfficerRank.元帥;
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
        UpdateOfficerRanks();
    }

    
}
