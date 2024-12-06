using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UiManager : MonoBehaviour
{
    public const string NumberFormat = "N2";

    [SerializeField]
    Button buttonGreen;

    [SerializeField]
    Button buttonYellow;

    [SerializeField]
    Button buttonRed;

    [SerializeField]
    Button buttonPlus;

    [SerializeField]
    Button buttonMinus;

    [SerializeField]
    Button buttonBetMenu;

    [SerializeField]
    GameObject betTooltip;

    [SerializeField]
    TextMeshProUGUI textBet;

    [SerializeField]
    TextMeshProUGUI textBalance;

    private GameSettings gameSettings;
    private SignalBus signalBus;
    private UserData userData;

    [Inject]
    public void Construct(GameSettings gameSettings, SignalBus signalBus, UserData userData)
    {
        this.gameSettings = gameSettings;
        this.signalBus = signalBus;
        this.userData = userData;
    }

    private void InitBetTooltip()
    {
        betTooltip.SetActive(true);

        

        betTooltip.SetActive(false);
    }

    private void Start()
    {
        textBet.text = userData.Bet.ToString(NumberFormat);
        textBalance.text = (userData.Balance / 100.0).ToString(NumberFormat);

        InitBetTooltip();
    }

    private void OnEnable()
    {
        buttonGreen.onClick.AddListener(OnGreenClick);
        buttonYellow.onClick.AddListener(OnYellowClick);
        buttonRed.onClick.AddListener(OnRedClick);

        buttonPlus.onClick.AddListener(OnPlusClick);
        buttonMinus.onClick.AddListener(OnMinusClick);
        buttonBetMenu.onClick.AddListener(OnBetMenuClick);

        signalBus.Subscribe<BetChangedSignal>(OnBetChanged);
        signalBus.Subscribe<LockBetChangeSignal>(OnBetChangeLocked);
        signalBus.Subscribe<UnlockBetChangeSignal>(OnBetChangeUnlocked);
        signalBus.Subscribe<BalanceChangedSignal>(OnBalanceChanged);
    }

    private void OnDisable()
    {
        buttonGreen.onClick.RemoveListener(OnGreenClick);
        buttonYellow.onClick.RemoveListener(OnYellowClick);
        buttonRed.onClick.RemoveListener(OnRedClick);

        buttonPlus.onClick.RemoveListener(OnPlusClick);
        buttonMinus.onClick.RemoveListener(OnMinusClick);
        buttonBetMenu.onClick.RemoveListener(OnBetMenuClick);

        signalBus.Unsubscribe<BetChangedSignal>(OnBetChanged);
        signalBus.Unsubscribe<LockBetChangeSignal>(OnBetChangeLocked);
        signalBus.Unsubscribe<UnlockBetChangeSignal>(OnBetChangeUnlocked);
        signalBus.Unsubscribe<BalanceChangedSignal>(OnBalanceChanged);
    }

    private void OnGreenClick()
    {
        signalBus.Fire(new ThrowBallSignal() { BallType = BallType.Green });
    }

    private void OnYellowClick()
    {
        signalBus.Fire(new ThrowBallSignal() { BallType = BallType.Yellow });
    }

    private void OnRedClick()
    {
        signalBus.Fire(new ThrowBallSignal() { BallType = BallType.Red });
    }

    private void OnMinusClick()
    {
        int index = Array.IndexOf(gameSettings.BetValues, userData.Bet) - 1;
        if (index < 0)
        {
            index = 0;
        }

        float bet = gameSettings.BetValues[index];

        signalBus.Fire(new ChangeBetSignal() { Bet = bet });
    }

    private void OnPlusClick()
    {
        int index = Array.IndexOf(gameSettings.BetValues, userData.Bet) + 1;
        if (index < 0)
        {
            index = 0;
        }
        else if (index >= gameSettings.BetValues.Length)
        {
            index = gameSettings.BetValues.Length - 1;
        }

        float bet = gameSettings.BetValues[index];

        signalBus.Fire(new ChangeBetSignal() { Bet = bet });
    }

    private void OnBetMenuClick()
    {
        betTooltip.SetActive(!betTooltip.activeSelf);
    }

    private void OnBetChanged(BetChangedSignal signal)
    {
        textBet.text = signal.Bet.ToString(NumberFormat);
    }

    private void OnBalanceChanged(BalanceChangedSignal signal)
    {
        textBalance.text = (signal.Balance / 100.0).ToString(NumberFormat);
    }

    private void OnBetChangeLocked()
    {
        buttonPlus.enabled = false;
        buttonMinus.enabled = false;
        buttonBetMenu.enabled = false;
    }

    private void OnBetChangeUnlocked()
    {
        buttonPlus.enabled = true;
        buttonMinus.enabled = true;
        buttonBetMenu.enabled = true;
    }
}
