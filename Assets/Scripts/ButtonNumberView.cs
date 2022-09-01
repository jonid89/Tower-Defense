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

    [Inject]
    public void Construct()
    {
        Reset(0);
    }

    void Reset(int resetNumber){
        string _resetNumber = resetNumber.ToString();
        text.text = _resetNumber;
    }

    

    private void Start()
    {
        this.transform.position = new Vector3 (Screen.width * 0.5f, Screen.height * 0.5f, 0);
    }


    public void UpdateNumber(string number){
        text.text = number;
    }

    
    public class Pool : MonoMemoryPool<int, ButtonNumberView>
    {
        protected override void Reinitialize(int resetNumber, ButtonNumberView buttonNumberView){
            buttonNumberView.Reset(resetNumber);
        }
    }
    
}
