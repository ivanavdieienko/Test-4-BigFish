using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    private const string BallsParent = "GameField/Pins";
    private const int PoolInitialSize = 10;

    [SerializeField]
    private Ball redBall;

    [SerializeField]
    private Ball yellowBall;

    [SerializeField]
    private Ball greenBall;

    [SerializeField]
    private Bucket bucketPrefab;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<CollectWinWithMultiplierSignal>();
        Container.DeclareSignal<BalanceChangedSignal>();
        Container.DeclareSignal<BetChangedSignal>();
        Container.DeclareSignal<ChangeBetSignal>();
        Container.DeclareSignal<LockBetChangeSignal>();
        Container.DeclareSignal<PlaceBetSignal>();
        Container.DeclareSignal<PlaySoundSignal>();
        Container.DeclareSignal<RemoveBallSignal>();
        Container.DeclareSignal<ThrowBallSignal>();
        Container.DeclareSignal<UnlockBetChangeSignal>();

        Container.BindFactory<Bucket, BucketFactory>()
            .FromComponentInNewPrefab(bucketPrefab);

        Container.BindMemoryPool<Ball, GreenPool>()
            .WithInitialSize(PoolInitialSize)
            .FromComponentInNewPrefab(greenBall)
            .UnderTransformGroup(BallsParent);

        Container.BindMemoryPool<Ball, YellowPool>()
            .WithInitialSize(PoolInitialSize)
            .FromComponentInNewPrefab(yellowBall)
            .UnderTransformGroup(BallsParent);

        Container.BindMemoryPool<Ball, RedPool>()
            .WithInitialSize(PoolInitialSize)
            .FromComponentInNewPrefab(redBall)
            .UnderTransformGroup(BallsParent);

        Container.Bind<UserData>().AsSingle();

        Container.BindInterfacesAndSelfTo<AudioManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<BallManager>().AsSingle();
        Container.Bind<BucketManager>().AsTransient();

        Container.Bind<BetOption>().AsTransient();

        Container.Bind<CollectWinWithMultiplierCommand>().AsTransient();
        Container.Bind<ChangeBetCommand>().AsTransient();
        Container.Bind<PlaceBetCommand>().AsTransient();

        Container.BindSignal<ChangeBetSignal>()
            .ToMethod<ChangeBetCommand>((cmd, signal) => cmd.Execute(signal))
            .FromResolve();

        Container.BindSignal<CollectWinWithMultiplierSignal>()
            .ToMethod<CollectWinWithMultiplierCommand>((cmd, signal) => cmd.Execute(signal))
            .FromResolve();

        Container.BindSignal<PlaceBetSignal>()
            .ToMethod<PlaceBetCommand>((cmd, signal) => cmd.Execute())
            .FromResolve();
    }
}