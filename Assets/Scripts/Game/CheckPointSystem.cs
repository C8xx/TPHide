using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSystem : MonoBehaviour
{
    public static CheckPointSystem instance;

    Vector3 ultimaPosicionPlayer;

    public Vector3 UltimaPos
    {
        get { return ultimaPosicionPlayer; }
    }

    private void Awake()
    {
        if (CheckPointSystem.instance != null)
            Destroy(gameObject);
        else
        {
            // Cargar la última posición del jugador desde PlayerPrefs
            float lastPlayerPosX = PlayerPrefs.GetFloat("LastPlayerPosX", 0);
            float lastPlayerPosY = PlayerPrefs.GetFloat("LastPlayerPosY", 0);
            ultimaPosicionPlayer = new Vector3(lastPlayerPosX, lastPlayerPosY, 0);
        }
            CheckPointSystem.instance = this;
    }

    public void ActualizarUltimaPos(Vector3 pos)
    {
        ultimaPosicionPlayer = pos;
    }
}
