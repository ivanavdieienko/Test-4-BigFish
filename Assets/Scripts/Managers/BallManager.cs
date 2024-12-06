using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class BallManager: IInitializable, IDisposable
{
    private const float BallMinValueX = -0.499f;
    private const float BallMaxValueX = 0.4999f;

    private Dictionary<BallType, MonoMemoryPool<Ball>> poolHub;

    private UserData userData;

    private SignalBus signalBus;

    private int fallingBallsCount;

    public BallManager(GreenPool greenPool, YellowPool yellowPool, RedPool redPool, UserData userData, SignalBus signalBus)
    {
        poolHub = new Dictionary<BallType, MonoMemoryPool<Ball>>
        {
            { BallType.Green, greenPool },
            { BallType.Yellow, yellowPool },
            { BallType.Red, redPool }
        };

        this.userData = userData;
        this.signalBus = signalBus;
    }

    public void Initialize()
    {
        signalBus.Subscribe<ThrowBallSignal>(ThrowBall);
        signalBus.Subscribe<RemoveBallSignal>(RemoveBall);
    }

    public void Dispose()
    {
        signalBus.Unsubscribe<ThrowBallSignal>(ThrowBall);
        signalBus.Unsubscribe<RemoveBallSignal>(RemoveBall);
    }

    public void ThrowBall(ThrowBallSignal signal)
    {
        if (userData.Balance - (userData.Bet * 100) < 0)
        {
            signalBus.Fire<ShowAddMoneyPopupSignal>();
            return;
        }

        if (fallingBallsCount == 0)
        {
            signalBus.Fire<LockBetChangeSignal>();
        }

        var ball = poolHub[signal.BallType].Spawn();
        ball.transform.localPosition = Vector3.right * Random.Range(BallMinValueX, BallMaxValueX);

        signalBus.Fire<PlaceBetSignal>();

        fallingBallsCount++;
    }

    public void RemoveBall(RemoveBallSignal signal)
    {
        poolHub[signal.Ball.BallType].Despawn(signal.Ball);

        fallingBallsCount--;

        if (fallingBallsCount == 0)
        {
            signalBus.Fire<UnlockBetChangeSignal>();
        }
    }
}