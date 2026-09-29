using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Gére la direction de la fleche et la force du coup choisie par le joueur
/// </summary>
public class DirectionCoups : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private Transform origineFleche;
    [SerializeField] private PlayerInput controles;

    [Header("Direction")]
    [SerializeField] private float vitesseRotation = 50f;

    [Header("Force")]
    [SerializeField] private float forceMin = 0.1f;
    [SerializeField] private float forceMax = 1f;
    [SerializeField] private float vitesseForce = .2f;

    private InputAction actionForce;
    private InputAction actionDirection;
    private float angleActuel = 0f;
    private float forceActuelle;

    /// <summary>
    /// On va chercher les actions du joueur et on met la force au minimum
    /// </summary>
    void Start()
    {
        actionDirection = controles.actions.FindAction("player/Direction");
        actionForce = controles.actions.FindAction("player/Force");
        forceActuelle = forceMin;
    }

    /// <summary>
    /// A chaque frame on tourne la fleche et on ajuste la force
    /// </summary>
    void Update()
    {
        TournerFleche();
        AjusterForce();
    }

    /// <summary>
    /// Augmente ou diminue la force en restant entre le minimum et le maximum
    /// </summary>
    private void AjusterForce()
    {
        if (actionForce == null)
        {
            return;
        }
        float ajustementForce = actionForce.ReadValue<float>();
        forceActuelle += ajustementForce * vitesseForce * Time.deltaTime;
        forceActuelle = Mathf.Clamp(forceActuelle, forceMin, forceMax); // on resteint de sortir de la marge de min et max
    }

    /// <summary>
    /// Tourne la fleche a gauche ou a droite selon l'input du joueur
    /// </summary>
    private void TournerFleche()
    {
        if (actionDirection == null)
        {
            return;
        }
        float rotation = actionDirection.ReadValue<float>();
        angleActuel += rotation * vitesseRotation * Time.deltaTime;
        origineFleche.localRotation = Quaternion.Euler(0f, angleActuel, 0f);
    }

    /// <summary>
    /// Donne la direction ou la balle doit partir
    /// </summary>
    /// <returns>La direction du coup</returns>
    public Vector3 GetDirection()
    {
        return -origineFleche.forward;
    }

    /// <summary>
    /// Donne la force choisie par le joueur
    /// </summary>
    /// <returns>La force du coup</returns>
    public float GetForce()
    {
        return forceActuelle;
    }

    /// <summary>
    /// Donne l'angle actuel de la fleche
    /// </summary>
    /// <returns>L'angle en degres</returns>
    public float GetRoation()
    {
        return angleActuel;
    }
}