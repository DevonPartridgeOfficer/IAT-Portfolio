using UnityEngine;

public class MazeConstructor : MonoBehaviour
{
    public bool showDebug;
    public float placementThreshold = 0.1f;   // chance of empty space

    [SerializeField] private Material mazeMat1;
    [SerializeField] private Material mazeMat2;
    [SerializeField] private Material startMat;
    [SerializeField] private Material treasureMat;

    public int[,] data
    {
        get; private set;
    }

    //Creates a basic maze
    void Awake()
    {
        // default to walls surrounding a single empty cell
        data = new int[,]
        {
            {1, 1, 1},
            {1, 0, 1},
            {1, 1, 1}
        };
    }

    public void GenerateNewMaze(int sizeRows, int sizeCols)
    {
        if (sizeRows % 2 == 0 && sizeCols % 2 == 0)
            Debug.LogError("Odd numbers work better for dungeon size.");

        data = FromDimensions(sizeRows, sizeCols);
    }

    //Generates 2D array of int for rows/cols
    public int[,] FromDimensions(int sizeRows, int sizeCols)
    {
        int[,] maze = new int[sizeRows, sizeCols];
        //Gets the upper indices of the maze (dimensions)
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++) //Loop over rows
            for (int j = 0; j <= cMax; j++) //Loop over columns
                if (i == 0 || j == 0 || i == rMax || j == cMax) //Check maze boundaries, min/max values must be the outside == walls
                    maze[i, j] = 1;
                else if (i % 2 == 0 && j % 2 == 0 && Random.value > placementThreshold) //To choose wall or blank space.
                {
                    maze[i, j] = 1;

                    int a = Random.value < .5 ? 0 : (Random.value < .5 ? -1 : 1);  
                    int b = a != 0 ? 0 : (Random.value < .5 ? -1 : 1);
                    maze[i + a, j + b] = 1;
                }
        return maze;
    }

    //Prints the maze to the scene for debugging
    void OnGUI()
    {
        if (!showDebug)
            return;

        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        string msg = "";

        for (int i = rMax; i >= 0; i--)
        {
            for (int j = 0; j <= cMax; j++)
                msg += maze[i, j] == 0 ? "...." : "==";
            msg += "\n";
        }

        GUI.Label(new Rect(20, 20, 500, 500), msg);
    }
}