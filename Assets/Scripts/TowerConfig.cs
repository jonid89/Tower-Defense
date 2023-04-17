using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerConfigData", menuName = "ScriptableObjects/TowerData")]
public class TowerConfig : ScriptableObject
{
    [SerializeField] public float _fireInterval = 4f;
    [SerializeField] public float _projectilePathDuration = 1f;
    [SerializeField] public int _projectileDamage = 5;

}
