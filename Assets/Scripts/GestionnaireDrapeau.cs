using UnityEngine;

/// <summary>
/// cache le drapeau des que la balle approche de trou
/// </summary>
public class GestionnaireDrapeau : MonoBehaviour
{
    [SerializeField] private Transform balle;
    [SerializeField] private GameObject flag;
    [SerializeField] private float distanceBalle = 2f;

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, balle.position);

        if (distance < distanceBalle)
        {
            if (flag.activeSelf == true)
            {
                flag.SetActive(false);
            }
        }
        else
        {
            if (flag.activeSelf == false)
            {
                flag.SetActive(true);
            }
        }
    }
}
