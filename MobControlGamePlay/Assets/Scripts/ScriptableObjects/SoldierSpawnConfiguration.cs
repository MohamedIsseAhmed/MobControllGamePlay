using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SoldierSpawnConfiguration",fileName = "SoldierSpawnConfiguration/SoldierSpawnData",order =2)]
public class SoldierSpawnConfiguration : ScriptableObject
{
    [field: SerializeField] public GameObject soldierPrefab;
    [field: SerializeField] public float SpwnThrowForce { get; private set; } = 100;
    [field: SerializeField] public bool IsThisSpawner { get; private set; } = false;
}
