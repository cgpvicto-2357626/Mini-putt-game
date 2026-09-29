using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

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

    private Coroutine coroutineArret;
    private InputAction actionFrapper;
    private bool enMove = false;
    private bool pretAFrapper = false;
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
    /// quand le script s'active, on écoute les évenements du jeu
    /// </summary>
    private void OnEnable()
    {
        GameEvents.OnBalleAuTrou += ReinitialiserCoups;
        GameEvents.OnBalleReinitialisee += ArreterSuivi;
    }

    /// <summary>
    /// quand le script se desactive, on arrete d'écouter les évenements du jeu
    /// </summary>
    private void OnDisable()
    {
        GameEvents.OnBalleAuTrou -= ReinitialiserCoups;
        GameEvents.OnBalleReinitialisee -= ArreterSuivi;
    }

    /// <summary>
    /// reinitialise le text du coups dans le canvas
    /// </summary>
    private void ReinitialiserCoups()
    {
        nombreCoups = 0;
        GameEvents.TriggerCoupEffectue(nombreCoups);
    }

    /// <summary>
    /// arrete le suivi de la balle quand elle est replacee par un autre script
    /// (hors piste ou dans le trou), pour eviter un double arret
    /// </summary>
    private void ArreterSuivi()
    {
        if (coroutineArret != null)
        {
            StopCoroutine(coroutineArret);
            coroutineArret = null;
        }
        enMove = false;
        pretAFrapper = false;
    }

    /// <summary>
    /// qunad on appuie sur espace, la balle est frappée sauf il est pas en move
    /// </summary>
    /// <param name="contexte">Information du callback de l'action</param>
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
    /// si il detecte une collision:
    /// mets pret a frapper a false, la balle est en movement
    /// réveille la balle ;
    ///lui donne une force vers la droite ;
    /// indique que la balle bouge ;
    /// met la caméra en mode suivi.
    /// </summary>
    /// <param name="other">Le collider de l'objet qui touche la balle</param>
    private void OnTriggerEnter(Collider other)
    {
        if (pretAFrapper == true && other.gameObject.CompareTag("Marteau"))
        {
            pretAFrapper = false;
            nombreCoups++;
            GameEvents.TriggerCoupEffectue(nombreCoups);
            enMove = true;

            GameEvents.TriggerFrappeCommencee();

            rb.WakeUp();
            Vector3 direction = directionCoups.GetDirection();
            float force = directionCoups.GetForce();
            rb.AddForce(direction * force, ForceMode.Impulse);
            coroutineArret = StartCoroutine(AttendreArretBalle());
        }
    }

    /// <summary>
    /// attend un peu, puis attend que la balle arrete de bouger
    /// </summary>
    /// <returns>La coroutine qui attend l'arret de la balle</returns>
    private IEnumerator AttendreArretBalle()
    {
        yield return new WaitForSeconds(delaiAvantVerification);

        while (rb.linearVelocity.sqrMagnitude >= vitesseArret * vitesseArret)
        {
            yield return null;
        }
        enMove = false;
        pretAFrapper = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        GameEvents.TriggerBalleArretee();
    }
}