using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class ButtonNumberController
{
    
    ButtonNumberView _buttonNumberView;

    private int _clicks;

    public ButtonNumberController(ButtonNumberView buttonNumberView){
        Reset(buttonNumberView);
        _buttonNumberView = buttonNumberView;
        _buttonNumberView.myButton.OnClickAsObservable()
            .Subscribe(_ => ButtonClick());
    }

    void Reset(ButtonNumberView buttonNumberView){
        _clicks = 0;
        _buttonNumberView = buttonNumberView;
    }
        

    public void ButtonClick(){
        _clicks ++;
        _buttonNumberView.UpdateNumber(_clicks.ToString());

    }

    public class Pool : MemoryPool<ButtonNumberView, ButtonNumberController>
    {
        protected override void Reinitialize(ButtonNumberView buttonNumberView, ButtonNumberController buttonNumberController){
            buttonNumberController.Reset(buttonNumberView);
        }
    }
}
