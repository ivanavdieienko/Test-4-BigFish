using UnityEngine;

public class CentralForce : MonoBehaviour
{
    [SerializeField]
    private Transform center;

    [SerializeField]
    private float attractionStrength = 5f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.attachedRigidbody != null)
        {
            Vector3 direction = (center.position - other.transform.position).normalized;
            other.attachedRigidbody.AddForce(direction * attractionStrength);
        }
    }
}
