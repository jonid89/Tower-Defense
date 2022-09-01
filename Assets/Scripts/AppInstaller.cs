using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class AppInstaller : MonoInstaller
{
    [SerializeField] GameObject _buttonNumberView;
    

    public override void InstallBindings()
    {        
        Container.Bind<ObjectSpawner>().AsSingle().NonLazy();
        ButtonInstaller.Install(Container);
        Container.BindFactory<ButtonNumberView, ButtonNumberView.Factory>().FromComponentInNewPrefab(_buttonNumberView).UnderTransformGroup("Canvas").NonLazy();
    }

}