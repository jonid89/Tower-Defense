using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class EnemyMoveController : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private GameObject enemyWaypoints;

    [SerializeField] private float duration = 10f;

    private List<Transform> waypoints;
    private List<Vector3> waypointsPositions = new List<Vector3>();
    
    private int nextWaypointIndex;

    private void OnEnable()
    {
        waypoints = enemyWaypoints.GetComponentsInChildren<Transform>().ToList();
        
        waypoints.RemoveAt(index:0);
        
        foreach (Transform waypoint in waypoints)
        {
            waypointsPositions.Add(waypoint.position);
        }
        MoveToThroughWaypoints();
    }

    // Update is called once per frame
    private void MoveToThroughWaypoints()
    {
        target.DOPath(waypointsPositions.ToArray(), duration, PathType.Linear, PathMode.Full3D).SetEase(Ease.Linear);
    }
}
