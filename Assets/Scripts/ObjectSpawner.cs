using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class ObjectSpawner
{
    
    public ObjectSpawner(ButtonNumberView.Pool buttonNumberViewPool, ButtonNumberController.Pool buttonNumberControllerPool){
        var buttonNumberView = buttonNumberViewPool.Spawn(0);
        buttonNumberControllerPool.Spawn(buttonNumberView);
    }

}
