using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameramotion : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cam.Priority = 10;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cam.Priority = 0;
        }
    }
}
