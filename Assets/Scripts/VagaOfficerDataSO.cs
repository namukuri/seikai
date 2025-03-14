using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VagaOfficerDataSO", menuName = "Create VagaOfficerDataSO")]　//CreateAssetMenu 属性の機能。VagaOfficerDataSO スクリプタブル・オブジェクトを作成


public class VagaOfficerDataSO : ScriptableObject //ScriptableObjectとしてVagaOfficerDataSOを作成
{
    public List<OfficerData> vagaOfficerDataList = new List<OfficerData>(); //List 型にして、１つの変数内に複数の OfficerData が管理されていることによって、１つのデータ群として利用できるようにしている
}

