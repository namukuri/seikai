using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WarshipDataSO", menuName = "Create WarshipDataSO" )]

public class WarshipDataSO : ScriptableObject
{
    public List<WarshipData> warshipDataList = new List<WarshipData>();
}
