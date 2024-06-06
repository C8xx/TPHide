using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationAndSceneChange : MonoBehaviour
{
    public Animation anim; // Asigna tu Animation aqu� en el Inspector
    public string newSceneName; // Asigna el nombre de la nueva escena aqu� en el Inspector
    public float delaySeconds = 5f; // Asigna el retraso aqu� en el Inspector

    void Start()
    {
        StartCoroutine(PlayAnimationAndChangeScene());
    }

    IEnumerator PlayAnimationAndChangeScene()
    {
        yield return new WaitForSeconds(delaySeconds);

        // Aseg�rate de que el nombre de la animaci�n es correcto
        anim.Play("panel");

        // Espera hasta que la animaci�n termine
        while (anim.isPlaying)
        {
            yield return null;
        }

        // Cambia a la nueva escena
        SceneManager.LoadScene(newSceneName);
    }
}
