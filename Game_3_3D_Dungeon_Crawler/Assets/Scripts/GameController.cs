using System;
using UnityEngine;

[RequireComponent(typeof(MazeConstructor))]

public class GameController : MonoBehaviour
{
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
    }
}
