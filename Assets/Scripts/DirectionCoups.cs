using UnityEngine;
using UnityEngine.InputSystem;

public class DirectionCoups : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private Transform origineFleche;
    [SerializeField] private PlayerInput controles;

    [Header("Direction")]
    [SerializeField] private float vitesseRotation = 50f;

    [Header("Force")]
    [SerializeField] private float forceMin = 0.1f;
    [SerializeField] private float forceMax = 1f;
    [SerializeField] private float vitesseForce = .2f;

    private InputAction actionForce;
    private InputAction actionDirection;
    private float angleActuel = 0f;
    private float forceActuelle;

    void Start()
    {
        actionDirection = controles.actions.FindAction("player/Direction");
        actionForce = controles.actions.FindAction("player/Force");
        forceActuelle = forceMin;
    }

    // Update is called once per frame
    void Update()
    {
        TournerFleche();
        AjusterForce();
    }

    private void AjusterForce()
    {
        if (actionForce == null)
        {
            return;
        }
        float ajustementForce = actionForce.ReadValue<float>();
        forceActuelle += ajustementForce * vitesseForce * Time.deltaTime;
        forceActuelle = Mathf.Clamp(forceActuelle, forceMin, forceMax); // on resteint de sortir de la marge de min et max
    }

    private void TournerFleche()
    {
        if(actionDirection == null)
        {
            return;
        }
        float rotation = actionDirection.ReadValue<float>();
        angleActuel += rotation * vitesseRotation * Time.deltaTime;
        origineFleche.localRotation = Quaternion.Euler(0f, angleActuel, 0f);
    }

    public Vector3 GetDirection()
    {
        return -origineFleche.forward;
    }

    public float GetForce()
    {
        return forceActuelle;
    }
    public float GetRoation()
    {
        return angleActuel;
    }
}
