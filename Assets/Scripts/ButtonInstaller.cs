using UnityEngine;
using Zenject;

public class ButtonInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ButtonNumberController>().AsSingle();
    }
}