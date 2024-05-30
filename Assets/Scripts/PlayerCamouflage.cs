using UnityEngine;
using System.Collections; // A�adido para IEnumerator

public class PlayerCamouflage : MonoBehaviour
{
    [Header("Referencias de Objetos")]
    public GameObject playerObject;
    public GameObject camouflagedObject;

    [Header("Configuraci�n de Entrada")]
    public KeyCode camouflageKey = KeyCode.C;

    [Header("Duraci�n del Camuflaje")]
    public float camouflageDuration = 5.0f;

    private bool isCamouflaged = false;
    private Coroutine camouflageCoroutine;

    void Update()
    {
        if (Input.GetKeyDown(camouflageKey))
        {
            ToggleCamouflage();
        }
    }

    private void ToggleCamouflage()
    {
        if (isCamouflaged)
        {
            StopCamouflage();
        }
        else
        {
            StartCamouflage();
        }
    }

    private void StartCamouflage()
    {
        isCamouflaged = true;
        playerObject.SetActive(false);
        camouflagedObject.SetActive(true);

        if (camouflageCoroutine != null)
        {
            StopCoroutine(camouflageCoroutine);
        }
        camouflageCoroutine = StartCoroutine(CamouflageTimer());
    }

    private void StopCamouflage()
    {
        isCamouflaged = false;
        playerObject.SetActive(true);
        camouflagedObject.SetActive(false);

        if (camouflageCoroutine != null)
        {
            StopCoroutine(camouflageCoroutine);
            camouflageCoroutine = null;
        }
    }

    private IEnumerator CamouflageTimer()
    {
        yield return new WaitForSeconds(camouflageDuration);
        StopCamouflage();
    }
}
