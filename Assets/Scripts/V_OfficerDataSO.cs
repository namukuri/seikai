using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "V_OfficerDataSO", menuName = "Create V_OfficerDataSO")]


public class V_OfficerDataSO : ScriptableObject
{
    public List<V_OfficerData> v_OfficerDataList = new List<V_OfficerData>();
}

