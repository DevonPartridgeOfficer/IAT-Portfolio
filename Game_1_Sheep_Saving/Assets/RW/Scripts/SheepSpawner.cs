/*  Filename: SheepSpawner.cs
 *   Purpose: Manages the creation/deletion of sheep objects
 *            Starts a coroutine to spawn sheep until gameover flag (from GameStateManager.cs)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public GameObject sheepPrefab;
    public bool canSpawn = true; //Sets sheep to constantly spawn during gameplay
    public float timeBetweenSpawns;
    public List<Transform> sheepSpawnPositions = new List<Transform>();
    private List<GameObject> sheepList = new List<GameObject>();

    // Starts sheep spawning routine
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    //Instantiates sheep to a list of possible positions
    //Adds sheep to list of 'alive' sheep
    private void SpawnSheep()
    {
        Vector3 randomPosition = sheepSpawnPositions[Random.Range(0, sheepSpawnPositions.Count)].position;
        GameObject sheep = Instantiate(sheepPrefab, randomPosition, sheepPrefab.transform.rotation);
        sheepList.Add(sheep);
        sheep.GetComponent<Sheep>().SetSpawner(this);
    }

    //Spawns sheep after an interval
    //canSpawn will be false after GameOver() in GameStateManager.cs
    private IEnumerator SpawnRoutine()
    {
        while (canSpawn)
        {
            SpawnSheep();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    //Removes sheep from 'alive' list after hit/drop
    public void RemoveSheepFromList(GameObject sheep)
    {
        sheepList.Remove(sheep);
    }

    //Remove all sheep objects from current game after GameOver()
    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList)
        {
            Destroy(sheep);
        }

        sheepList.Clear();
    }
}
