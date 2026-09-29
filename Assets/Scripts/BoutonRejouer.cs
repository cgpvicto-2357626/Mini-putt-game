using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// script qui rejoue la jeu en cliquant sur le bouton rejouer 
/// </summary>
public class BoutonRejouer : MonoBehaviour
{
    /// <summary>
    /// Appelée quand on clique sur le bouton Rejouer
    /// source de sceneManager: https://docs.unity3d.com/560/Documentation/ScriptReference/SceneManagement.SceneManager.html
    /// </summary>
    public void Rejouer() {   
        int sceneActive = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneActive);
    }
}
