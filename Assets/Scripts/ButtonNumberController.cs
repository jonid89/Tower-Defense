using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class ButtonNumberController
{
    ButtonNumberView _buttonNumberView;

    private int number = 0;

    public ButtonNumberController(ButtonNumberView buttonNumberView) {
        _buttonNumberView = buttonNumberView;
        _buttonNumberView.myButton.OnClickAsObservable()
            .Subscribe(_ => ButtonClick());
    }

    public void ButtonClick(){
        Debug.Log("clicked");
        number ++;
        _buttonNumberView.UpdateNumber(number.ToString());

    }
}
