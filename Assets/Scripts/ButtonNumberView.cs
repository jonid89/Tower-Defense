using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class ButtonNumberView : MonoBehaviour, IDisposable
{
    [SerializeField] private Text text; 
    [SerializeField] private Button button;

    public Button myButton => button; 

    private void Start()
    {
        
    }


    public void UpdateNumber(string number){
        text.text = number;
    }

    public void SetPosition(Vector2 positionInGrid){
        this.transform.position = positionInGrid + new Vector2 (Screen.width * 0.5f, Screen.height * 0.5f);
    }

    public void Dispose(){

    }
    
    public class Pool : MemoryPool<ButtonNumberView>
    {
        
    }
    
}
