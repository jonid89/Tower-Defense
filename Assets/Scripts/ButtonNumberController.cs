using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class ButtonNumberController : IDisposable
{
    ButtonNumberView _buttonNumberView;

    private int number = 0;

    public ButtonNumberController(ButtonNumberView buttonNumberView) {
        _buttonNumberView = buttonNumberView;
        _buttonNumberView.myButton.OnClickAsObservable()
            .Subscribe(_ => ButtonClick());
            //.AddTo(this);
    }

    public void ButtonClick(){
        //Debug.Log("clicked");
        number ++;
        _buttonNumberView.UpdateNumber(number.ToString());

    }

    public void Dispose(){

    }

    public class Factory : PlaceholderFactory<ButtonNumberView, ButtonNumberController>
    {
    }
}
