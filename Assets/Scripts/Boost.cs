using UnityEngine;

/// <summary>
/// un scirpt pour augumenter la vitesse de la ball quand elle entre en coliston avec le gameobject boost
/// </summary>
public class Boost : MonoBehaviour
{
    [SerializeField] private float vitesseBoost;

    /// <summary>
    /// Quand la balle entre dans la zone, on la pousse plus vite dans la meme direction
    /// </summary>
    /// <param name="other">Le collider de l'objet qui entre dans la zone</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = rb.linearVelocity.normalized;
                rb.AddForce(direction * vitesseBoost, ForceMode.Impulse);
            }
        }
    }
}