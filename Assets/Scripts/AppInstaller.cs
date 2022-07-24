using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class AppInstaller : MonoInstaller
{
    [SerializeField] GameObject _buttonNumberView;
    //[SerializeField] GameObject _canvas;
    

    public override void InstallBindings()
    {        
        
        //Container.Bind<Canvas>().FromComponentInNewPrefab(_canvas).AsSingle();
        //Container.Bind<ButtonNumberView>().FromComponentInNewPrefab(_buttonNumberView).UnderTransform(_canvas.transform).AsSingle();
        Container.BindFactory<ButtonNumberView, ButtonNumberView.Factory>().FromComponentInNewPrefab(_buttonNumberView).AsSingle().NonLazy();
        Container.Bind<ButtonNumberController>().AsSingle().NonLazy();
    }

}