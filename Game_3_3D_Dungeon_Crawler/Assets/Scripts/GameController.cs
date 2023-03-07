using System;
using UnityEngine;

[RequireComponent(typeof(MazeConstructor))]

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab; //Add a player with assigned prefab
    public GameObject monsterPrefab; //Add a monster with assigned prefab

    private MazeConstructor constructor;
    
    //To set the size of the maze
    [SerializeField] private int rows;
    [SerializeField] private int cols;

    void Awake()
    {
        constructor = GetComponent<MazeConstructor>();
    }

    void Start()
    {
        constructor.GenerateNewMaze(rows, cols);
        CreatePlayer();
        CreateMonster();
    }

    //Setup for player character
    //Player will starrt at the beginning of the maze [1,1]
    private void CreatePlayer()
    {
        Vector3 playerStartPosition = new Vector3(constructor.hallWidth, 1, constructor.hallWidth);
        GameObject player = Instantiate(playerPrefab, playerStartPosition, Quaternion.identity);
        player.tag = "Generated";
    }

    //Creates the monster at the final cell of the world
    private void CreateMonster()
    {
        Vector3 monsterPosition = new Vector3(constructor.goalCol * constructor.hallWidth, 0f, constructor.goalRow * constructor.hallWidth);
        GameObject monster = Instantiate(monsterPrefab, monsterPosition, Quaternion.identity);
        monster.tag = "Generated";
    }
}
