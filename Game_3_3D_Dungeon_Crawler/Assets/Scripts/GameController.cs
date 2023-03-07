using System;
using UnityEngine;

[RequireComponent(typeof(MazeConstructor))]

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab; //Add a player with assigned prefab
    public GameObject monsterPrefab; //Add a monster with assigned prefab

    private MazeConstructor constructor;
    private AIController aIController;

    //To set the size of the maze
    [SerializeField] private int rows;
    [SerializeField] private int cols;

    void Awake()
    {
        constructor = GetComponent<MazeConstructor>();
        aIController = GetComponent<AIController>();
    }

    void Start()
    {
        constructor.GenerateNewMaze(rows, cols);
        aIController.Graph = constructor.graph;
        aIController.Player = CreatePlayer();
        aIController.Monster = CreateMonster();
        aIController.HallWidth = constructor.hallWidth;
        aIController.StartAI();
    }

    //Setup for player character
    //Player will starrt at the beginning of the maze [1,1]
    private GameObject CreatePlayer()
    {
        Vector3 playerStartPosition = new Vector3(constructor.hallWidth, 1, constructor.hallWidth);
        GameObject player = Instantiate(playerPrefab, playerStartPosition, Quaternion.identity);
        player.tag = "Generated";

        return player;
    }

    //Creates the monster at the final cell of the world
    private GameObject CreateMonster()
    {
        Vector3 monsterPosition = new Vector3(constructor.goalCol * constructor.hallWidth, 0f, constructor.goalRow * constructor.hallWidth);
        GameObject monster = Instantiate(monsterPrefab, monsterPosition, Quaternion.identity);
        monster.tag = "Generated";

        return monster;
    }
}
