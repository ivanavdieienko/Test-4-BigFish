using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField]
    private BallType type;

    public BallType BallType => type;

    private Rigidbody2D body;

    public void Start()
    {
        body = GetComponent<Rigidbody2D>();

        gameObject.SetActive(true);
    }

    public void Reset()
    {
        body.velocity = Vector3.zero;
        body.angularVelocity = 0f;
        body.rotation = 0f;
        gameObject.SetActive(false);
    }
}