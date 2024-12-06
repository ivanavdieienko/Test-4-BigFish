using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BetOption : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject activeBackground;

    [SerializeField]
    private TextMeshProUGUI betText;

    public event Action<float> OnSelectBet;

    private float bet;

    public void SetValue(float value)
    {
        bet = value;
        betText.text = bet.ToString(UiManager.NumberFormat);
    }

    public void SetActive(bool value)
    {
        activeBackground.SetActive(value);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSelectBet?.Invoke(bet);
    }
}
