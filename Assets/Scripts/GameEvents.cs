using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    private void Awake()
    {
        Instance = this;
    }

    /*public event Action onDoorwayTriggerEnter;
    public void DoorwayTriggerEnter()
    {
        if(onDoorwayTriggerEnter!=null)
        {
            onDoorwayTriggerEnter();
        }
    }
    */

}
