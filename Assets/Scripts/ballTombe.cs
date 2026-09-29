using UnityEngine;

/// <summary>
/// remet la balle au point de départ si elle sort de la piste
/// </summary>
public class ballTombe : MonoBehaviour
{
    [SerializeField] private Transform pointDepart;

    /// <summary>
    /// Quand la balle sort de la zone, on arrete sa vitesse et on la remet au depart
    /// </summary>
    /// <param name="other">Le collider de l'objet qui sort de la zone</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.position = pointDepart.position;
                rb.Sleep();
            }
            GameEvents.TriggerBalleReinitialisee();
            GameEvents.TriggerBalleArretee();
        }
    }
}