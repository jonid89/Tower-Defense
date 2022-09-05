using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class ObjectSpawner : IDisposable
{
    private int listSize = 9;

    private List<ButtonNumberView> buttonNumberViewList = new List<ButtonNumberView>();

    public ObjectSpawner(ButtonNumberView.Factory buttonNumberViewFactory, ButtonNumberController.Factory buttonNumberControllerFactory/*, ButtonGrid buttonGrid*/){
        for(int i=0; i < listSize; i++){
            var buttonNumberView = buttonNumberViewFactory.Create();
            Vector2 position = new Vector2(-250+250*(i%3),-50+50*(i/3));
            buttonNumberView.SetPosition(position);
            buttonNumberControllerFactory.Create(buttonNumberView);
            buttonNumberViewList.Add(buttonNumberView);
            
        }
       
        
    }


    public void Dispose(){

    }

    /*private void MakeGrid(){
        for(int i=0; i < listSize; i++){
            buttonNumberViewList[i].gameObject.GetComponent<RectTransform>().localPosition = new Vector2;
        }

    }*/

}
