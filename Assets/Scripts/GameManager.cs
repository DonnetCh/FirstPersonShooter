using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Enemyprefab;
    public Transform[] spawnPosEnemy;
    // Numero de Spawns
    public int randomSpawn;
    public float TiempodeSpawn = 3.0f;
    private float UltimoTiempodeSpawn = 0f;
    public int EnemyLimit = 5;
    public GameObject[] enemiesInScene;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemiesInScene.Length <= EnemyLimit)
        {
            if (Time.time > UltimoTiempodeSpawn + TiempodeSpawn)
            {

                //Crear un objeto
                // GameObject newObject = Instantiate(NPCprefab, SpawnPoint.position, Quaternion.identity);
                CrearEnemy();
                RandomSpawn();
                //Reiniciar el tiempo
                UltimoTiempodeSpawn = Time.time;
            }
        }
    }

    void CrearEnemy()
    {
        Instantiate(Enemyprefab, spawnPosEnemy[randomSpawn].transform.position, Enemyprefab.transform.rotation);
    }

    void RandomSpawn()
    {
        randomSpawn = Random.Range(0, spawnPosEnemy.Length);
    }
}
