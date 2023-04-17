using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnerConfigData", menuName = "ScriptableObjects/EnemySpawnerData")]
public class EnemySpawnerConfig : ScriptableObject
{
    public int _enemiesAmount = 3;
    public float _enemiesSpawnRate = 3;

}
