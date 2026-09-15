using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Script pour controller la balle en la frappant avec space
/// </summary>
public class ControleBalle : MonoBehaviour
{
    [Header("Réeferences")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerInput controles;
    [SerializeField] private Animator animatorMarteau;
    [SerializeField] private Transform cibleCamera;
    [SerializeField] private float delaiAvantVerification = 0.2f;

    [Header("Paramétres")]
    [SerializeField] private float forceFrappe = 1.5f;
    [SerializeField] private float vitesseArret = 0.05f;

    private InputAction actionFrapper;
    private bool enMove = false;
    private bool pretAFrapper = false;

    private float tempsDerniereFrappe = -1f;

    /// <summary>
    /// prépare la touche espace pour faire avancer la balle
    /// </summary>
    void Start()
    {
        actionFrapper = controles.actions.FindAction("player/FrapperBall");
        if (actionFrapper != null)
        {
            actionFrapper.performed += DetecteFrappe;
        }
    }

    /// <summary>
    /// on vérifie si la balle est arrétée
    /// </summary>
    private void Update()
    {
        VerifierArretBalle();
    }

    /// <summary>
    /// enléver l'evenement de la touche quand le script et détruit
    /// </summary>
    void OnDestroy()
    {
        if (actionFrapper != null)
        {
            actionFrapper.performed -= DetecteFrappe;
        }
    }

    /// <summary>
    /// qunad on appuie sur espace, la balle est frappée sauf il est pas en move
    /// </summary>
    /// <param name="contexte"></param>
    private void DetecteFrappe(InputAction.CallbackContext contexte)
    {
        if (!enMove && !pretAFrapper)
        {
            pretAFrapper = true;
            GameEvents.TriggerModeFixeActive();

            if (animatorMarteau != null)
            {
                animatorMarteau.SetTrigger("Frapper");
            }
        }
    }

    /// <summary>
    /// si il detecte une collistion:
    /// mets pret a frapper a false, la balle est en movement
    /// réveille la balle ;
    ///lui donne une force vers la droite ;
    /// indique que la balle bouge ;
    /// met la caméra en mode suivi.
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (pretAFrapper == true && collision.gameObject.CompareTag("Marteau"))
        {
            pretAFrapper = false;
            enMove = true;

            tempsDerniereFrappe = Time.time;
            GameEvents.TriggerFrappeCommencee();

            rb.WakeUp();
            Vector3 direction = new Vector3(1f, 0f, 0f);
            rb.AddForce(direction * forceFrappe, ForceMode.Impulse);

        }
    }

    /// <summary>
    /// Déplace en continu la cible de la caméra sur X et Z pour suivre la balle en mouvement.
    /// </summary>


    /// <summary>
    /// on vérfie si la balle est arréter
    /// si balle ne bouge pas, on fait rien
    /// si leur vitesse est presque 0, la balle est arreter 
    /// revient en mode placment
    /// </summary>
    private void VerifierArretBalle()
    {
        if (!enMove)
            return;

        if (Time.time - tempsDerniereFrappe < delaiAvantVerification)
        {
            return;
        }

        if (enMove && rb.linearVelocity.sqrMagnitude < vitesseArret * vitesseArret)
        {
            enMove = false;
            pretAFrapper = false;

            GameEvents.TriggerBalleArretee();
        }
    }

}