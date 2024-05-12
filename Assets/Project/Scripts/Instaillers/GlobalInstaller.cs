using Zenject;
public class GlobalInstaller : MonoInstaller<GlobalInstaller>
{
    public override void InstallBindings()
    {
        PlayerInput playerInput = new();
        playerInput.Enable();

        Container.Bind<PlayerInput>().FromInstance(playerInput).AsSingle();
        Container.Bind<ThirdPersonController>().AsSingle();
    }
}
