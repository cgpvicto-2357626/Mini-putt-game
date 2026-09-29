using UnityEngine;
using Unity.Cinemachine;
using System;

/// <summary>
/// Gérer le changement entre les trois caméras, utile pour quelle caméra doit étre active à chaque moment du jeu
/// </summary>
public class GestionnaireCamera : MonoBehaviour
{
    [SerializeField] private GameObject camPlacement;
    [SerializeField] private GameObject camSuivi;
    [SerializeField] private GameObject camFixe;
    [SerializeField] private MonoBehaviour controlesPlacement;

    /// <summary>
    /// Au debut du jeu on commence avec la camera de placement
    /// </summary>
    void Start()
    {
        ActiverModePlacement();
    }

    /// <summary>
    /// quand le script s'active, on écoute les évenements
    /// </summary>
    private void OnEnable()
    {
        GameEvents.OnFrappeCommencee += ActiverModeSuivi;
        GameEvents.OnBalleArretee += ActiverModePlacement;
        GameEvents.OnModeFixeActive += ActiverModeFixe;
    }

    /// <summary>
    /// quand le script se desactive, on arrete d'écouter les évenements
    /// </summary>
    private void OnDisable()
    {
        GameEvents.OnFrappeCommencee -= ActiverModeSuivi;
        GameEvents.OnBalleArretee -= ActiverModePlacement;
        GameEvents.OnModeFixeActive -= ActiverModeFixe;
    }

    /// <summary>
    /// camera de placement quand le joueur vise son coup, on active aussi les controles
    /// </summary>
    public void ActiverModePlacement()
    {
        if (camPlacement != null)
        {
            camPlacement.SetActive(true);
        }

        if (camFixe != null)
        {
            camFixe.SetActive(false);
        }

        if (camSuivi != null)
        {
            camSuivi.SetActive(false);
        }

        if (controlesPlacement != null)
        {
            controlesPlacement.enabled = true;
        }
    }

    /// <summary>
    /// camera fixe qui ne bouge pas, les controles de placement sont fermer
    /// </summary>
    public void ActiverModeFixe()
    {
        if (camPlacement != null)
        {
            camPlacement.SetActive(false);
        }
        if (camFixe != null)
        {
            camFixe.SetActive(true);
        }
        if (camSuivi != null)
        {
            camSuivi.SetActive(false);
        }
        if (controlesPlacement != null)
        {
            controlesPlacement.enabled = false;
        }
    }

    /// <summary>
    /// camera suivi quand la balle roule
    /// </summary>
    public void ActiverModeSuivi()
    {
        if (camPlacement != null)
        {
            camPlacement.SetActive(false);
        }

        if (camFixe != null)
        {
            camFixe.SetActive(false);
        }

        if (camSuivi != null)
        {
            camSuivi.SetActive(true);
        }

        if (controlesPlacement != null)
        {
            controlesPlacement.enabled = false;
        }
    }
}