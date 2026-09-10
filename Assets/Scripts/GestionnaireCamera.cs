using UnityEngine;
using Unity.Cinemachine;
using System;

/// <summary>
/// Gérer le changement entre les deux caméras, utile pour quelle caméra doit étre active à chaque moment du jeu
/// </summary>
public class GestionnaireCamera : MonoBehaviour
{
    [SerializeField] private CinemachineCamera camPlacement;
    [SerializeField] private CinemachineCamera camSuivi;
    [SerializeField] private MonoBehaviour controlesPlacement;

    void Start()
    {
        ActiverModePlacement();
    }

    private void OnEnable()
    {
        GameEvents.OnFrappeCommencee += ActiverModeSuivi;
        GameEvents.OnBalleArretee += ActiverModePlacement;
    }

    private void OnDisable()
    {
        GameEvents.OnFrappeCommencee -= ActiverModeSuivi;
        GameEvents.OnBalleArretee -= ActiverModePlacement;
    }

    public void ActiverModePlacement()
    {
        Debug.Log("[CAMERA] → Mode PLACEMENT");
        if (camPlacement != null)
        {
            camPlacement.enabled = true;
        }
        if (camSuivi != null)
        {
            camSuivi.enabled = false;
        }

        if (controlesPlacement != null)
        {
            controlesPlacement.enabled = true;
        }
    }

    public void ActiverModeSuivi()
    {
        Debug.Log("[CAMERA] → Mode SUIVI");
        if (camPlacement != null)
        {
            camPlacement.enabled =false ;
        }

        if(camSuivi != null)
        {
            camSuivi .enabled = true;
        }

        if (controlesPlacement != null)
        {
            controlesPlacement.enabled = false;
        }
    }
}