using UnityEngine;
using Zenject;

public class BucketManager : MonoBehaviour
{
    [SerializeField]
    private BallType colorType;

    [SerializeField]
    private Color winColor;

    [SerializeField]
    private Color looseColor;

    [SerializeField]
    private float bucketSpacing = 0.6f;

    private Bucket[] buckets;

    [Inject]
    private BucketFactory bucketFactory;
    [Inject]
    private SignalBus signalBus;
    [Inject]
    private GameSettings settings;
    [Inject]
    private GameSounds sounds;

    private float[] winAmounts;

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i].OnCatchBall += OnCatchBall;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i].OnCatchBall -= OnCatchBall;
        }
    }

    private void Initialize()
    {
        switch (colorType)
        {
            case BallType.Red:
                winAmounts = settings.RedLineWinAmounts; break;
            case BallType.Yellow:
                winAmounts = settings.YellowLineWinAmounts; break;
            case BallType.Green:
                winAmounts = settings.GreenLineWinAmounts; break;
        }

        var position = Vector3.zero;
        var bucketsCount = (winAmounts.Length - 1) * 2 + 1;

        buckets = new Bucket[bucketsCount];

        for (int i = 0; i < winAmounts.Length - 1; i++)
        {
            buckets[i] = CreateBucket($"bucket{i}", winAmounts[i], position);

            position.x += bucketSpacing;
        }

        for (int i = winAmounts.Length - 1; i >= 0; i--)
        {
            buckets[bucketsCount - i - 1] = CreateBucket($"bucket{bucketsCount - i - 1}", winAmounts[i], position);

            position.x += bucketSpacing;
        }
    }

    private void OnCatchBall(Ball ball, float winMultiplier)
    {
        signalBus.Fire(new CollectWinWithMultiplierSignal() { Multiplier = winMultiplier });
        signalBus.Fire(new RemoveBallSignal() { Ball = ball });

        if (winMultiplier < 1)
        {
            signalBus.Fire(new PlaySoundSignal() { Sound = sounds.SoundLost });
        }
        else
        {
            signalBus.Fire(new PlaySoundSignal() { Sound = sounds.SoundWin });
        }
    }

    private Bucket CreateBucket(string bucketName, float winAmount, Vector3 position)
    {
        var color = winAmount < 1 ? looseColor : winColor;
        var bucket = bucketFactory.Create();
        bucket.name = bucketName;
        bucket.transform.parent = transform;
        bucket.Initialize(colorType, color, winAmount, position);

        return bucket;
    }
}
