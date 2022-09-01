using UnityEngine;
using Zenject;

public class ButtonInstaller : Installer<ButtonInstaller>
{
    public override void InstallBindings()
    {
        Container.BindMemoryPool<ButtonNumberController, ButtonNumberController.Pool>();
    }
}