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
    [SerializeField] private DirectionCoups directionCoups;
    [SerializeField] private float delaiAvantVerification = 0.2f;

    [Header("Paramétres")]
    [SerializeField] private float vitesseArret = 0.05f;

    private InputAction actionFrapper;
    private bool enMove = false;
    private bool pretAFrapper = false;
    private float tempsDerniereFrappe = -1f;
    private int nombreCoups = 0;


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
        rb.Sleep();
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

    private void OnEnable()
    {
        GameEvents.OnBalleAuTrou += ReinitialiserCoups;
    }

    private void OnDisable()
    {
        GameEvents.OnBalleAuTrou -= ReinitialiserCoups;
    }

    private void ReinitialiserCoups()
    {
        nombreCoups = 0;
        GameEvents.TriggerCoupEffectue(nombreCoups);
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
    private void OnTriggerEnter(Collider other)
    {
        if (pretAFrapper == true && other.gameObject.CompareTag("Marteau"))
        {
            pretAFrapper = false;
            nombreCoups++;
            GameEvents.TriggerCoupEffectue(nombreCoups);
            enMove = true;

            tempsDerniereFrappe = Time.time;
            GameEvents.TriggerFrappeCommencee();

            rb.WakeUp();
            Vector3 direction = directionCoups.GetDirection();
            float force = directionCoups.GetForce();
            rb.AddForce(direction * force, ForceMode.Impulse);

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

        //une suggestion de Claude pour le probléme de suivi à la deuxiéme fois
        if (Time.time - tempsDerniereFrappe < delaiAvantVerification)
        {
            return;
        }

        if (enMove && rb.linearVelocity.sqrMagnitude < vitesseArret * vitesseArret)
        {
            enMove = false;
            pretAFrapper = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            
            GameEvents.TriggerBalleArretee();
        }
    }

}