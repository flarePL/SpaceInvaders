using SpaceInvaders.Elementy;
using SpaceInvaders.UIElementy;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<UIGamePlay>().FromComponentInHierarchy().AsSingle();
    }
}