using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public PlayerInputSettings inputSettings;

    public override void InstallBindings()
    {
        Container.Bind<PlayerInputSettings>().FromInstance(inputSettings).AsSingle();
    }
}
