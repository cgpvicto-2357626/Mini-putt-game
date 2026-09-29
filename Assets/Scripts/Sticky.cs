using UnityEngine;

/// <summary>
/// Ralentit la balle quand elle entre dans la zone collante
/// </summary>
public class Sticky : MonoBehaviour
{
    [SerializeField] private float vitesseRalentisssement;

    /// <summary>
    /// Quand un objet entre dans la zone, on ralentit la balle
    /// </summary>
    /// <param name="other">Le collider de l'objet qui entre dans la zone</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = rb.linearVelocity * vitesseRalentisssement;
            }
        }
    }
}