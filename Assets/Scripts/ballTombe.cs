using UnityEngine;

/// <summary>
/// remet la balle au point de départ si elle sort de la piste
/// </summary>
public class ballTombe : MonoBehaviour
{
    [SerializeField] private Transform pointDepart;

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
            GameEvents.TriggerBalleArretee();
        }
    }
}