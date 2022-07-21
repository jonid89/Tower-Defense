using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class AppInstaller : MonoInstaller
{
    [SerializeField] ButtonNumberView _buttonNumberView;

    public override void InstallBindings()
    {        
        Container.Bind<ButtonNumberController>().AsSingle().NonLazy();
        Container.Bind<ButtonNumberView>().FromInstance(_buttonNumberView).AsSingle();
        
    }

}