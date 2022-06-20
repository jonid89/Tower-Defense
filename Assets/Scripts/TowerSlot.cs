using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSlot : MonoBehaviour
{
    ObjectPooler objectPooler;
    private bool hasTower = false;
    public void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    void OnMouseDown(){
        if(hasTower != true){
            GameObject tower = objectPooler.SpawnFromPool("Tower",transform.position,Quaternion.identity, this.transform.parent);
            hasTower = true;
        }
    }

    

}
