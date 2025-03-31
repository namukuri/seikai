using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �V���O���g���p�^�[��
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
