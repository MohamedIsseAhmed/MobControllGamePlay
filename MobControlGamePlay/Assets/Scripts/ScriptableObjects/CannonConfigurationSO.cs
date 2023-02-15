using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CannonConfigurationSO", menuName = "CannonConfigurationSO/CannonConfigurationSO", order =1)]
public class CannonConfigurationSO : ScriptableObject
{
    [field: SerializeField] public float CannonMoveSpeed { get; private set; } = 5;
    [field: SerializeField] public float CannonMoveSidesSpeed { get; private set; } = 5;
    [field: SerializeField] public float SpawnTimeMax { get; private set; } = 0.5f;
    [field: SerializeField] public float SpawnTimeOffset { get; private set; } = 0.15f;
    [field: SerializeField] public float SpawnLerpSpeed { get; private set; } = 1f;
    [field: SerializeField] public float MinUVCannonScale { get; private set; } = -0.5f;
    [field: SerializeField] public float MaxUVCannonScale { get; private set; } = 1.34f;
    [field: SerializeField] public float MinX { get; private set; } = 0;
    [field: SerializeField] public float MaxX { get; private set; } = 2;

    [field: SerializeField] public bool IsEnemyConnon { get; private set; } = false;
}
