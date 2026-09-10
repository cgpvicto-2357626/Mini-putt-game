using UnityEngine;
using System;

/// <summary>
/// Détecte quand la balle entre dans le trou.
/// </summary>
public class DetecteurTrou : MonoBehaviour
{
    [Header("Réferences")]
    [SerializeField] private Transform pointDepart;
    /// <summary>
    /// Quand un objet avec le tag "Ball" entre dans le trigger, on détecte le trou.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("balle dans le trou !");
            TeleporterBalle(other);
        }
    }

    /// <summary>
    /// Replace la balle au point de départ et arrête sa vitesse
    /// pour qu'elle ne garde pas son élan.
    /// </summary>
    private void TeleporterBalle(Collider ballCollider)
    {
        Rigidbody rb = ballCollider.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        rb.position = pointDepart.position;
        rb.rotation = pointDepart.rotation;

        GameEvents.TriggerBalleArretee();
    }
}