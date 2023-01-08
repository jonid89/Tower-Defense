using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] private GameObject _enemyWaypoints;

    private List<Transform> _waypoints;
    private List<Vector3> _waypointsPositions = new List<Vector3>();
    
    

    private void OnEnable()
    {
        _waypoints = _enemyWaypoints.GetComponentsInChildren<Transform>().ToList();
        
        _waypoints.RemoveAt(index:0);
        
        foreach (Transform waypoint in _waypoints)
        {
            _waypointsPositions.Add(waypoint.position);
        }
    }

    public List<Vector3> getWaypoints()
    {
        return _waypointsPositions;
    }
    



}
