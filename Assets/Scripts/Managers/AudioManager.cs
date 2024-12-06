using UnityEngine;
using Zenject;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    private SignalBus signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }

    private void OnEnable()
    {
        signalBus.Subscribe<PlaySoundSignal>(OnPlaySound);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<PlaySoundSignal>(OnPlaySound);
    }

    private void OnPlaySound(PlaySoundSignal signal)
    {
        audioSource.PlayOneShot(signal.Sound);
    }
}