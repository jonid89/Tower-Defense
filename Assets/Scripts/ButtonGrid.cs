using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class ButtonGrid
{
    private int listSize = 9;
    private List<ButtonNumberView> _buttonNumberViewList = new List<ButtonNumberView>();

    public ButtonGrid(List<ButtonNumberView> buttonNumberViewList){
        _buttonNumberViewList = buttonNumberViewList;

    }

    public void MakeGrid(){
         for(int i=0; i < listSize; i++){
            _buttonNumberViewList[i].gameObject.GetComponent<RectTransform>().localPosition = new Vector2(-150+150*(listSize/(i+1)),-50+50*(listSize%(i+1)));
        }
    }
    
}
