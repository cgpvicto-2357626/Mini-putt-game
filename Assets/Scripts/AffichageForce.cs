using UnityEngine;
using UnityEngine.UI;

public class AffichageForce : MonoBehaviour
{
    [SerializeField] private DirectionCoups directionCoups;
    [SerializeField] private Slider sliderForce;
    [SerializeField] private Image remplissage;

    void Update()
    {
        MettreAJourBarre();
    }

    private void MettreAJourBarre()
    {
        float force = directionCoups.GetForce();
        float forceMin = 0.2f;
        float forceMax = 1f;

        float pourcentage = (force - forceMin) / (forceMax - forceMin);
        pourcentage = Mathf.Clamp01(pourcentage);

        sliderForce.value = pourcentage;
    }
}