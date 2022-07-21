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
    [SerializeField] private Button button;
    public Button myButton => button; 


    private void Start()
    {
        
    }


    public void UpdateNumber(string number){
        Debug.Log(number);
        text.text = number;
    }

    
    
}
