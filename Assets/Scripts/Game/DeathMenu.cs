using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject panelDeath;
    public string scene;
   // public Player player;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
      //  if (player.vida <= 0)
       // {
           // player.anim.SetTrigger("Die");
            //player.speed = 0;
            //Invoke("Die", 1.5f);
       // }

    }
    public void Die()
    {
        panelDeath.SetActive(true);
    }
    public void Restart()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 lastPlayerPosition = CheckPointSystem.instance.UltimaPos;
            player.transform.position = lastPlayerPosition;
        }
       SceneManager.LoadScene(scene);
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Cerrar()
    {
        Application.Quit();

    }

}
