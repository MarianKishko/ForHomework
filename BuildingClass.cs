using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buildings", menuName ="Building")]
public class BuildingClass : ScriptableObject
{
    public List<BuildingData> buildings;
}

[Serializable]
public class BuildingData
{
    [field: SerializeField] public GameObject BuildingPrefab;
    [field: SerializeField] public int ID;
    [field: SerializeField] public string BuildingName;
    [field: SerializeField] public int width;
    [field: SerializeField] public int height;
}
