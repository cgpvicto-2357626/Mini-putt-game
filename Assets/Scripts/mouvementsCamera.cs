using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Gérer les mouvements de la cible de caméra
/// </summary>
public class mouvementsCamera : MonoBehaviour
{
    [Header("Paramètres de déplacement")]
    [SerializeField]
    private float vitesseDeplacement;

    [SerializeField]
    private float vitesseRotation;

    [SerializeField]
    private float vitesseInclinaison;

    [SerializeField]
    private float vitesseZoom;

    [SerializeField]
    private Vector2 limitesInclinaison;

    [SerializeField]
    private Vector2 limitesZoom;

    [Header("Références aux objets de jeu")]

    [SerializeField]
    private BoxCollider volumeCamera;

    [SerializeField]
    private PlayerInput controles;
    private Vector2 deplacement;
    private float rotation;
    private float inclinaison;
    private float zoom;


    /// <summary>
    /// On va chercher chaque action du joueur et on s'abonne pour savoir quand elle commence et quand elle arrete
    /// </summary>
    void Start()
    {
        InputAction actionDeplacement = controles.actions.FindAction("player/DeplacerCamera");
        actionDeplacement.performed += CommencerDeplacement;
        actionDeplacement.canceled += TerminerDeplacement;

        InputAction actionRetation = controles.actions.FindAction("player/TournerCamera");
        actionRetation.performed += CommencerRotation;
        actionRetation.canceled += TerminerRotation;

        InputAction actionInclinaison = controles.actions.FindAction("player/InclinerCamera");
        actionInclinaison.performed += CommencerInclinaison;
        actionInclinaison.canceled += TerminerInclinaison;

        InputAction actionZoom = controles.actions.FindAction("player/ZoomerCamera");
        actionZoom.performed += CommencerZoom;
        actionZoom.canceled += TerminerZoom;
    }

    /// <summary>
    /// A chaque frame on applique les quatre mouvements de la caméra
    /// </summary>
    void Update()
    {
        deplacerCamera();
        TournerCamera();
        InclinerCamera();
        AppliquerZoom();
    }

    /// <summary>
    /// Quand l'objet est détruit, on se désabonne des actions pour éviter les erreurs
    /// </summary>
    private void OnDestroy()
    {
        if (controles == null || controles.actions == null)
        {
            return;
        }

        InputAction actionDeplacement = controles.actions.FindAction("player/DeplacerCamera");
        actionDeplacement.performed -= CommencerDeplacement;
        actionDeplacement.canceled -= TerminerDeplacement;

        InputAction actionRotation = controles.actions.FindAction("player/TournerCamera");
        actionRotation.performed -= CommencerRotation;
        actionRotation.canceled -= TerminerRotation;

        InputAction actionInclinaison = controles.actions.FindAction("player/InclinerCamera");
        actionInclinaison.performed -= CommencerInclinaison;
        actionInclinaison.canceled -= TerminerInclinaison;

        InputAction actionZoom = controles.actions.FindAction("player/ZoomerCamera");
        actionZoom.performed -= CommencerZoom;
        actionZoom.canceled -= TerminerZoom;
    }

    #region Déplacements
    /// <summary>
    /// Commence le déplacement de la caméra
    /// </summary>
    /// <param name="contexte">Information du callback de l'action</param>
    private void CommencerDeplacement(InputAction.CallbackContext contexte)
    {
        deplacement = vitesseDeplacement * contexte.ReadValue<Vector2>();
    }

    /// <summary>
    /// Termine le déplacement de la caméra
    /// </summary>
    /// <param name="contexte">Information du callback de l'action</param>
    private void TerminerDeplacement(InputAction.CallbackContext contexte)
    {
        deplacement = Vector2.zero;
    }

    /// <summary>
    /// Gère le déplacement de la caméra en fonction de l'input du joueur et des limites du volume de la caméra
    /// </summary>
    private void deplacerCamera()
    {
        if (deplacement.sqrMagnitude > 0.0f)
        {
            Vector3 prochainePosition = transform.position + transform.right * deplacement.x * Time.deltaTime + transform.forward * deplacement.y * Time.deltaTime;
            prochainePosition.y = transform.position.y;

            if (volumeCamera.bounds.Contains(prochainePosition))
            {
                transform.position = prochainePosition;
            }
        }

    }
    #endregion

    #region Rotation

    /// <summary>
    /// Commence la rotation de la caméra
    /// </summary>
    /// <param name="contexte">Information du callback de l'action</param>
    private void CommencerRotation(InputAction.CallbackContext contexte)
    {
        rotation = vitesseRotation * contexte.ReadValue<float>();
    }

    /// <summary>
    /// Termine la rotation de la caméra
    /// </summary>
    /// <param name="contexte">Information du callback de l'action</param>
    private void TerminerRotation(InputAction.CallbackContext contexte)
    {
        rotation = 0.0f;
    }

    /// <summary>
    /// Fait tourner la caméra sur l'axe Y du monde
    /// </summary>
    private void TournerCamera()
    {
        transform.Rotate(new Vector3(0.0f, rotation * Time.deltaTime, 0.0f), Space.World);
    }

    #endregion

    #region inclinaison
    /// <summary>
    /// Commence l'inclinaison de la caméra
    /// </summary>
    /// <param name="contexte">Information du callback de l'action</param>
    private void CommencerInclinaison(InputAction.CallbackContext contexte)
    {
        inclinaison = vitesseInclinaison * contexte.ReadValue<float>();
    }

    /// <summary>
    /// Termine l'inclinaison de la caméra
    /// </summary>
    /// <param name="contexte">Information du callback de l'action</param>
    private void TerminerInclinaison(InputAction.CallbackContext contexte)
    {
        inclinaison = 0.0f;
    }

    /// <summary>
    /// Incline la caméra vers le haut ou vers le bas en restant dans les limites permises
    /// </summary>
    private void InclinerCamera()
    {
        float angle = (transform.localEulerAngles.x + inclinaison * Time.deltaTime) % 360;

        if (angle >= limitesInclinaison.x || angle <= limitesInclinaison.y)
        {
            transform.Rotate(new Vector3(inclinaison * Time.deltaTime, 0.0f, 0.0f), Space.Self);
        }
    }
    #endregion

    #region Zoom

    /// <summary>
    /// Commence le zoom de la caméra
    /// </summary>
    /// <param name="contexte">Information du callback de l'action</param>
    private void CommencerZoom(InputAction.CallbackContext contexte)
    {
        zoom = contexte.ReadValue<float>() * vitesseZoom;
    }

    /// <summary>
    /// Termine le zoom de la caméra
    /// </summary>
    /// <param name="contexte">Information du callback de l'action</param>
    private void TerminerZoom(InputAction.CallbackContext contexte)
    {
        zoom = 0f;
    }

    /// <summary>
    /// Avance ou recule la caméra pour faire le zoom
    /// </summary>
    private void AppliquerZoom()
    {
        /*
        CinemachinePositionComposer positionComposer = cameraGeree.GetComponent<CinemachinePositionComposer>();
        Vector3 offsetCamera = positionComposer.TargetOffset + positionComposer.TargetOffset.normalized * zoom;
        float distanceCamera = offsetCamera.magnitude;

        if (distanceCamera >= limitesZoom.x && distanceCamera <= limitesZoom.y)
        {   
            positionComposer.TargetOffset = offsetCamera;
        }
        */
        if (zoom == 0f)
            return;

        // bouger la camera vers l'avant ou l'arriere
        transform.position += transform.forward * zoom * Time.deltaTime;
    }
    #endregion
}