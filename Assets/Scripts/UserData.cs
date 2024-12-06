using System;
using UnityEngine;
using Zenject;

public class UserData: IInitializable, IDisposable
{
    private SignalBus signalBus;

    private float bet = 0.3f;
    private int balance = 300000;

    public float Bet
    {
        get => bet;
        set
        {
            if (bet != value)
            {
                bet = value;
                signalBus.Fire(new BetChangedSignal() { Bet = value });
            }
        }
    }
    public int Balance
    {
        get => balance;
        set
        {
            if (balance != value)
            {
                balance = value;
                signalBus.Fire(new BalanceChangedSignal() { Balance = value });
            }
        }
    }

    public UserData(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }

    public void Initialize()
    {
        Bet = PlayerPrefs.GetFloat(nameof(Bet));
        Balance = PlayerPrefs.GetInt(nameof(Balance));
    }

    public void Dispose()
    {
        PlayerPrefs.SetFloat(nameof(Bet), Bet);
        PlayerPrefs.SetInt(nameof(Balance), Balance);
    }
}