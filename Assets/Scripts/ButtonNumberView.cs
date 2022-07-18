using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class ButtonNumberView : MonoBehaviour
{
    [SerializeField] private Text text; 
    ButtonNumberController _buttonNumberController;

    [Inject]
    public void Construct (ButtonNumberController buttonNumberController) {
        _buttonNumberController = buttonNumberController;
    }


    private void Start()
    {
        this.GetComponent<Button>().OnClickAsObservable()
            .Subscribe(_ => _buttonNumberController.ButtonClick());
    }


    public void UpdateNumber(int number){
        Debug.Log(number);
        text.text = number.ToString();
    }
    
}
