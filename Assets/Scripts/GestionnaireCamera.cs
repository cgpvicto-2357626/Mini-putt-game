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



    void Start()
    {
        ActiverModePlacement();
    }

    private void OnEnable()
    {
        GameEvents.OnFrappeCommencee += ActiverModeSuivi;
        GameEvents.OnBalleArretee += ActiverModePlacement;
        GameEvents.OnModeFixeActive += ActiverModeFixe;
    }

    private void OnDisable()
    {
        GameEvents.OnFrappeCommencee -= ActiverModeSuivi;
        GameEvents.OnBalleArretee -= ActiverModePlacement;
        GameEvents.OnModeFixeActive -= ActiverModeFixe;

    }

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