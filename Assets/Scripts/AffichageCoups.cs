using UnityEngine;
using TMPro;

/// <summary>
/// Affiche le nombre de coups du joueur a l'ecran
/// </summary>
public class AffichageCoups : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCoups;

    /// <summary>
    /// quand le script s'active, on écoute l'évenement du coup
    /// </summary>
    private void OnEnable()
    {
        GameEvents.onCoupEffectue += MettreAjourAffiche;
    }

    /// <summary>
    /// quand le script se desactive, on arrete d'écouter l'évenement du coup
    /// </summary>
    private void OnDisable()
    {
        GameEvents.onCoupEffectue -= MettreAjourAffiche;
    }

    /// <summary>
    /// Change le texte pour afficher le nouveau nombre de coups
    /// </summary>
    /// <param name="coups">Le nombre de coups fait par le joueur</param>
    private void MettreAjourAffiche(int coups)
    {
        textCoups.text = "Coups : " + coups;
    }
}