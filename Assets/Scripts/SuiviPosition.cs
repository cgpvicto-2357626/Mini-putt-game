using UnityEngine;

// une suggestion inspiré de Claude pour avoir une proxy entre la camera qui suivent et la ball
public class SuiviPosition : MonoBehaviour
{
    [SerializeField]
    private Transform cible;

    /// <summary>
    /// s'execute apres tous les Update() du frame, donc la position copiee est toujours la position finale de la balle
    /// </summary>
    void LateUpdate()
    {
        transform.position = cible.position;
    }
}
