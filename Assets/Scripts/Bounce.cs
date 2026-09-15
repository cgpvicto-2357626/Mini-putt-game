using UnityEngine;

/// <summary>
/// Projette la balle vers le haut quand elle passe au centre
/// </summary>
public class Bounce : MonoBehaviour
{
    [SerializeField] private float forceBounce = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // projeter vers le haut
                rb.AddForce(Vector3.up * forceBounce, ForceMode.Impulse);
            }
        }
    }
}