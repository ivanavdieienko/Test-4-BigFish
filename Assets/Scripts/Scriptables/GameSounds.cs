using UnityEngine;

[CreateAssetMenu(fileName = "GameSounds", menuName = "ScriptableObjects/GameSounds", order = 1)]
public class GameSounds : ScriptableObject
{
    [SerializeField]
    private AudioClip win;

    [SerializeField]
    private AudioClip lost;

    public AudioClip SoundWin => win;
    public AudioClip SoundLost => lost;
}