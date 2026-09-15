using UnityEngine;

public class Sticky : MonoBehaviour
{
    [SerializeField] private float vitesseRalentisssement;

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
