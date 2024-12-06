using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BetTooltip : MonoBehaviour
{
    [SerializeField]
    Button closeButton;

    [SerializeField]
    GameObject betOptionPrefab;

    [SerializeField]
    Transform betOptionsContainer;

    private List<BetOption> betOptions = new();

    private float[] betValues;
    private UserData userData;
    private SignalBus signalBus;

    [Inject]
    private void Construct(UserData userData, SignalBus signalBus, GameSettings gameSettings)
    {
        this.userData = userData;
        this.signalBus = signalBus;
        betValues = gameSettings.BetValues;
    }

    private void Awake()
    {
        for (int i = 0; i < betValues.Length; i++)
        {
            var betGameObject = Instantiate(betOptionPrefab, betOptionsContainer);
            if (betGameObject.TryGetComponent<BetOption>(out var betOption))
            {
                betOption.SetValue(betValues[i]);
                betOptions.Add(betOption);
            }
        }
    }

    private void OnEnable()
    {
        closeButton.onClick.AddListener(OcCloseClick);

        for (int i = 0; i < betOptions.Count; i++)
        {
            bool isActive = userData.Bet.Equals(betValues[i]);

            betOptions[i].OnSelectBet += OnClickBet;
        }
        UpdateActiveBet();
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(OcCloseClick);

        for (int i = 0; i < betOptions.Count; i++)
        {
            bool isActive = userData.Bet.Equals(betValues[i]);

            betOptions[i].OnSelectBet -= OnClickBet;
        }
    }

    private void OcCloseClick()
    {
        gameObject.SetActive(false);
    }

    private void OnClickBet(float bet)
    {
        if (!bet.Equals(userData.Bet))
        {
            signalBus.Fire(new ChangeBetSignal() { Bet = bet });

            UpdateActiveBet();
        }
    }

    private void UpdateActiveBet()
    {
        for (int i = 0; i < betOptions.Count; i++)
        {
            bool isActive = userData.Bet.Equals(betValues[i]);

            betOptions[i].SetActive(isActive);
        }
    }
}