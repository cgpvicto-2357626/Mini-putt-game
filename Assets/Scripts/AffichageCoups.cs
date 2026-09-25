using UnityEngine;
using TMPro;

public class AffichageCoups : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCoups;

    private void OnEnable()
    {
        GameEvents.onCoupEffectue += MettreAjourAffiche;
    }
    private void OnDisable()
    {
        GameEvents.onCoupEffectue -= MettreAjourAffiche;
    }

    private void MettreAjourAffiche(int coups)
    {
        textCoups.text = "Coups : " + coups;
    }
}
