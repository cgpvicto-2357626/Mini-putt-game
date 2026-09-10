using UnityEngine;

// une suggestion inspiré de Claude pour avoir une proxy entre la camera qui suivent et la ball
public class SuiviPosition : MonoBehaviour
{
    [SerializeField]
    private Transform cible;

    /// <summary>
    /// apprés que tous les méthodes updates soient terminés, on execute cette méthode
    /// </summary>
    void Update()
    {
        transform.position = cible.position;
    }
}
