using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Affiche la force du coup dans une barre a l'ecran
/// </summary>
public class AffichageForce : MonoBehaviour
{
    [SerializeField] private DirectionCoups directionCoups;
    [SerializeField] private Slider sliderForce;
    [SerializeField] private Image remplissage;

    /// <summary>
    /// A chaque frame on met la barre a jour
    /// </summary>
    void Update()
    {
        MettreAJourBarre();
    }

    /// <summary>
    /// Transforme la force en pourcentage et le met dans le slider
    /// </summary>
    private void MettreAJourBarre()
    {
        float force = directionCoups.GetForce();
        float forceMin = 0.2f;
        float forceMax = 1f;

        float pourcentage = (force - forceMin) / (forceMax - forceMin);
        pourcentage = Mathf.Clamp01(pourcentage); //version rapidee d'une marge de 0 a 1 

        sliderForce.value = pourcentage;
    }
}