using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class TowerSlot : MonoBehaviour
{
    ObjectPooler _objectPooler;
    private bool hasTower = false;
    
    private GameObject tower;

    [Inject]
    public void Construct (ObjectPooler objectPooler) {
        _objectPooler = objectPooler;
    }



    void OnMouseDown(){
        if(hasTower != true){
            _objectPooler.SpawnObject(ObjectPooler.PoolType.Tower,transform.position,Quaternion.identity, this.transform);
            hasTower = true;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }else{
            UpgradeTower();
        }
    }

    void UpgradeTower(){
        
    }
    

}
