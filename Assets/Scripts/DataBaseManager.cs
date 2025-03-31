using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シングルトンパターン
/// </summary>
public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;

    public VagaOfficerDataSO vagaOfficerDataSO;
    public WarshipDataSO warshipDataSO;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
}
