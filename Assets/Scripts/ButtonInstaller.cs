using UnityEngine;
using Zenject;

public class ButtonInstaller : Installer<ButtonInstaller>
{
    public override void InstallBindings()
    {
        Container.BindFactory<ButtonNumberView, ButtonNumberController, ButtonNumberController.Factory>();
        //Container.BindInterfacesTo<ButtonNumberController>().AsSingle();
    }
}