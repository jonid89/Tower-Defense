using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ButtonNumberController
{
    ButtonNumberView _buttonNumberView;

    private int number = 0;

    public ButtonNumberController(ButtonNumberView buttonNumberView) {
        _buttonNumberView = buttonNumberView;
    }

    public void ButtonClick(){
        number ++;
        _buttonNumberView.UpdateNumber(number);

    }
}
