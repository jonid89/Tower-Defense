using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class ObjectSpawner
{
    

    public ObjectSpawner(ButtonNumberView.Factory buttonNumberViewFactory, ButtonNumberController.Factory buttonNumberControllerFactory){
        var buttonNumberView = buttonNumberViewFactory.Create();
        buttonNumberControllerFactory.Create(buttonNumberView);
    }

}
