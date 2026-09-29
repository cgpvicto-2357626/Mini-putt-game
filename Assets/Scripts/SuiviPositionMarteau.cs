using System;
using UnityEngine;

/// <summary>
/// Des que la balle est arreter on met la marteau deriére elle
/// </summary>
public class SuiviPositionMarteau : MonoBehaviour
{
    [SerializeField] private Transform balle;
    private float hauteury;
    private Collider[] ColliderMarteau;
    private MeshRenderer meshMarteau;

    /// <summary>
    /// On garde la hauteur de depart et on va chercher le mesh et les colliders du marteau
    /// </summary>
    void Start()
    {
        hauteury = transform.position.y;
        meshMarteau = GetComponent<MeshRenderer>();
        ColliderMarteau = GetComponents<Collider>();
        DesactiverColliders();
    }

    /// <summary>
    /// quand le script s'active, on écoute les évenements
    /// </summary>
    private void OnEnable()
    {
        GameEvents.OnFrappeCommencee += CacherMarteau;
        GameEvents.OnBalleArretee += AfficherMarteau;
        GameEvents.OnModeFixeActive += ActiverColliders;
    }

    /// <summary>
    /// quand le script s'active, on arrete d'écoute les évenements
    /// </summary>
    private void OnDisable()
    {
        GameEvents.OnFrappeCommencee -= CacherMarteau;
        GameEvents.OnBalleArretee -= AfficherMarteau;
        GameEvents.OnModeFixeActive -= ActiverColliders;
    }

    /// <summary>
    /// on met la marteau visible en l'activant des que la balle est arréter
    /// </summary>
    private void AfficherMarteau()
    {
        meshMarteau.enabled = true;
        DesactiverColliders();
    }

    /// <summary>
    /// on met la marteau invisible en l'activant des que la frappe est commencer
    /// </summary>
    private void CacherMarteau()
    {
        meshMarteau.enabled = false;
    }

    /// <summary>
    /// Active tous les colliders du marteau pour qu'il puisse frapper la balle
    /// </summary>
    private void ActiverColliders()
    {
        for (int i = 0; i < ColliderMarteau.Length; i++)
        {
            ColliderMarteau[i].enabled = true;
        }
    }

    /// <summary>
    /// Desactive tous les colliders du marteau pour qu'il ne touche pas la balle
    /// </summary>
    private void DesactiverColliders()
    {
        for (int i = 0; i < ColliderMarteau.Length; i++)
        {
            ColliderMarteau[i].enabled = false;
        }
    }

    /// <summary>
    /// il faut suivre la position du balle en LateUpdate aprés avoir bouger la balle en update
    /// </summary>
    void LateUpdate()
    {
        transform.position = new Vector3(balle.position.x, hauteury, balle.position.z);
    }
}