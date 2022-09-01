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
        Container.BindMemoryPool<ButtonNumberView, ButtonNumberView.Pool>().WithInitialSize(10).FromComponentInNewPrefab(_buttonNumberView).UnderTransformGroup("Canvas").NonLazy();
    }

}