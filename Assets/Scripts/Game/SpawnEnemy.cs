using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject lifeBarPrefab;
    public float summonOffSet;
    private GameObject lifeBarInstance;

    public void SummonEnemies()
    {
      //  GameObject canvasGO = GameObject.FindGameObjectWithTag("CanvasEnemigos");
       // Canvas canvas = canvasGO.GetComponent<Canvas>();

       // if (canvas != null)
       // {
         //   lifeBarInstance = Instantiate(lifeBarPrefab);
         //   lifeBarInstance.transform.SetParent(canvas.transform);
       // }
        GameObject enemy = Instantiate(enemyPrefab, transform.position - Vector3.down * summonOffSet, enemyPrefab.transform.rotation);
      //  enemy.GetComponent<HpSystem>().Slider(lifeBarInstance);
    }
}
