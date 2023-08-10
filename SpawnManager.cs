using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacle;
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private float spawnDelay;
    [SerializeField] private float spawnRate;
    private PlayerController playerControllerScript;
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacles", spawnDelay, spawnRate);

    }

    void SpawnObstacles()
    {
        if (playerControllerScript.gameStarts==true)
        {
            int randomObstacles = Random.Range(0, 12);
            if (playerControllerScript.gameOver == false)
            {
                Instantiate(obstacle[randomObstacles], spawnPos, obstacle[randomObstacles].transform.rotation);
            }
        }
    }
}