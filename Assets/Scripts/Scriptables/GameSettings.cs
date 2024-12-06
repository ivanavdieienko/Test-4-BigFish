using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    [SerializeField]
    private float[] greenLineWinAmounts = { 11, 3.2f, 1.6f, 1.2f, 1.1f, 1f, 0.5f };

    [SerializeField]
    private float[] yellowLineWinAmounts = { 25, 8, 3.1f, 1.7f, 1.2f, 0.7f, 0.3f };

    [SerializeField]
    private float[] redLineWinAmounts = { 141, 25, 8.1f, 2.3f, 0.7f, 0.2f, 0 };

    [SerializeField]
    private float[] betValues = { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 1.2f, 2, 4, 10, 20, 50, 100 };

    public float[] RedLineWinAmounts => redLineWinAmounts;
    public float[] YellowLineWinAmounts => yellowLineWinAmounts;
    public float[] GreenLineWinAmounts => greenLineWinAmounts;

    public float[] BetValues => betValues;
}
