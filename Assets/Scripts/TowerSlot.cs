using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class TowerSlot : MonoBehaviour
{
    ObjectPooler _objectPooler;
    private bool _hasTower = false;
    
    [Inject]
    public void Construct (ObjectPooler objectPooler) {
        _objectPooler = objectPooler;
    }

    void OnMouseDown(){
        if(_hasTower != true){
            _objectPooler.SpawnObject(ObjectPooler.PoolType.Tower,transform.position,Quaternion.identity, this.transform);
            _hasTower = true;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }else{
            UpgradeTower();
        }
    }

    void UpgradeTower(){
        
    }
    

}
