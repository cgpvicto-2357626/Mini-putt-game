using UnityEngine;

/// <summary>
/// le cercle suit la position de la balle sans copier sa rotation
/// </summary>
public class suiviPostionCircle : MonoBehaviour
{
    [SerializeField] private Transform balle;

    void LateUpdate()
    {
        transform.position = balle.position;
    }
}
