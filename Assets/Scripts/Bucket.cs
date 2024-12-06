using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bucket : MonoBehaviour
{
    [SerializeField]
    private TextMesh textField;

    [SerializeField]
    private SpriteRenderer bucket;

    public event Action<Ball, float> OnCatchBall;

    private HashSet<Collider2D> processedColliders;

    private float winMultiplier;

    public BallType ColorType { get; private set; }

    public void Initialize(BallType type, Color color, float amount, Vector3 position)
    {
        ColorType = type;
        bucket.color = color;
        winMultiplier = amount;
        textField.text = amount.ToString();
        transform.localPosition = position;
        processedColliders = new HashSet<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!processedColliders.Contains(other) &&
            other.TryGetComponent<Ball>(out Ball ball) &&
            ball.BallType == ColorType)
        {
            processedColliders.Add(other);

            OnCatchBall?.Invoke(ball, winMultiplier);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        processedColliders.Remove(other);
    }
}

public class BucketFactory : PlaceholderFactory<Bucket> { }
