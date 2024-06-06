using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationAndSceneChange : MonoBehaviour
{
    public Animation anim; // Asigna tu Animation aquí en el Inspector
    public string newSceneName; // Asigna el nombre de la nueva escena aquí en el Inspector
    public float delaySeconds = 5f; // Asigna el retraso aquí en el Inspector

    void Start()
    {
        StartCoroutine(PlayAnimationAndChangeScene());
    }

    IEnumerator PlayAnimationAndChangeScene()
    {
        yield return new WaitForSeconds(delaySeconds);

        // Asegúrate de que el nombre de la animación es correcto
        anim.Play("panel");

        // Espera hasta que la animación termine
        while (anim.isPlaying)
        {
            yield return null;
        }

        // Cambia a la nueva escena
        SceneManager.LoadScene(newSceneName);
    }
}
