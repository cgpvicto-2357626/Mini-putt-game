using UnityEngine;

/// <summary>
/// le cercle suit la position de la balle sans copier sa rotation
/// </summary>
public class suiviPostionCircle : MonoBehaviour
{
    [SerializeField] private Transform balle;

    /// <summary>
    /// On place le cercle sur la balle aprés que la balle a bouger
    /// </summary>
    void LateUpdate()
    {
        transform.position = balle.position;
    }
}