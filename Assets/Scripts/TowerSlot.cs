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
            GameObject tower = objectPooler.SpawnFromPool("Tower",transform.position,Quaternion.identity, this.transform.parent, new Vector3(0,0,0));
            hasTower = true;
        }
    }

    
    /*public void OnPointerClick(PointerEventData pointerEventData)
    {
        if(hasTower != true){
            GameObject tower = objectPooler.SpawnFromPool("Tower",transform.position,Quaternion.identity, this.transform.parent, new Vector3(0,0,0));
            hasTower = true;
        }
        Debug.Log(name + " Game Object Clicked!");
    }
    */
}
