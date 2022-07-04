using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class EnemyMoveController : MonoBehaviour
{
    [SerializeField] private GameObject enemyWaypoints;

    /*#region Singleton
    public static EnemyMoveController Instance;
    private void Awake()
    {
        Instance = this;
    }

    #endregion*/

    private List<Transform> waypoints;
    private List<Vector3> waypointsPositions = new List<Vector3>();
    
    private GameObject target;

    private int nextWaypointIndex;

    

    private void OnEnable()
    {
        waypoints = enemyWaypoints.GetComponentsInChildren<Transform>().ToList();
        
        waypoints.RemoveAt(index:0);
        
        foreach (Transform waypoint in waypoints)
        {
            waypointsPositions.Add(waypoint.position);
        }
    }

    // Update is called once per frame
    public List<Vector3> getWaypoints()
    {
        return waypointsPositions;
    }
    



}
