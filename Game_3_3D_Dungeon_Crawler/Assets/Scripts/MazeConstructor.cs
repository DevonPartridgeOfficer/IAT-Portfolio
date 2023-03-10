using UnityEngine;

public class MazeConstructor : MonoBehaviour
{
    public bool showDebug;
    public float placementThreshold = 0.1f;   // chance of empty space
    public float hallWidth { get; private set; } //Places player/treasure in middle of hallway
    public int goalRow { get; private set; } //Determines location of treasure/enemy
    public int goalCol { get; private set; } //Determines location of treasure/enemy
    public Node[,] graph;

    [SerializeField] private Material mazeMat1;
    [SerializeField] private Material mazeMat2;
    [SerializeField] private Material startMat;
    [SerializeField] private Material treasureMat;

    private MazeMeshGenerator meshGenerator;

    public int[,] data
    {
        get; private set;
    }

    //Creates a basic maze
    void Awake()
    {
        meshGenerator = new MazeMeshGenerator();
        hallWidth = meshGenerator.width;

        // default to walls surrounding a single empty cell
        data = new int[,]
        {
            {1, 1, 1},
            {1, 0, 1},
            {1, 1, 1}
        };
    }

    public void GenerateNewMaze(int sizeRows, int sizeCols, TriggerEventHandler treasureCallback)
    {
        DisposeOldMaze();

        if (sizeRows % 2 == 0 && sizeCols % 2 == 0)
            Debug.LogError("Odd numbers work better for dungeon size.");

        data = FromDimensions(sizeRows, sizeCols);
        goalRow = data.GetUpperBound(0) - 1;
        goalCol = data.GetUpperBound(1) - 1;

        graph = new Node[sizeRows, sizeCols];

        for (int i = 0; i < sizeRows; i++)
            for (int j = 0; j < sizeCols; j++)
                graph[i, j] = data[i, j] == 0 ? new Node(i, j, true) : new Node(i, j, false);

        DisplayMaze();
        PlaceGoal(treasureCallback);
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

    private void DisplayMaze()
    {
        GameObject go = new GameObject();
        go.transform.position = Vector3.zero;
        go.name = "Procedural Maze";
        go.tag = "Generated";

        MeshFilter mf = go.AddComponent<MeshFilter>();
        mf.mesh = meshGenerator.FromData(data);

        MeshCollider mc = go.AddComponent<MeshCollider>();
        mc.sharedMesh = mf.mesh;

        MeshRenderer mr = go.AddComponent<MeshRenderer>();
        mr.materials = new Material[2] { mazeMat1, mazeMat2 };
    }

    //Remove previous maze objects (tag = generated)
    public void DisposeOldMaze()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Generated");
        foreach (GameObject go in objects)
        {
            Destroy(go);
        }
    }

    private void PlaceGoal(TriggerEventHandler treasureCallback)
    {
        GameObject treasure = GameObject.CreatePrimitive(PrimitiveType.Cube);
        treasure.transform.position = new Vector3(goalCol * hallWidth, .5f, goalRow * hallWidth);
        treasure.name = "Treasure";
        treasure.tag = "Generated";

        treasure.GetComponent<BoxCollider>().isTrigger = true;
        treasure.GetComponent<MeshRenderer>().sharedMaterial = treasureMat;

        TriggerEventRouter tc = treasure.AddComponent<TriggerEventRouter>();
        tc.callback = treasureCallback;
    }
}