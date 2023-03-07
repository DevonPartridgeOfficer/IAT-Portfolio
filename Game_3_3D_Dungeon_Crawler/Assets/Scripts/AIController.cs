using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 140;

    private Node[,] graph;
    public Node[,] Graph
    {
        get { return graph; }
        set { graph = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Calculate distance from start/end goal
    private int CalculateDistanceCost(Node a, Node b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = xDistance - yDistance;
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    //Loop through list and find lowest fCost
    private Node GetLowestFCostNode(List<Node> pathNodeList)
    {
        Node lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
                lowestFCostNode = pathNodeList[i];

        return lowestFCostNode;
    }

    //Find neighbours for a node
    private List<Node> GetNeighbourList(Node currentNode)
    {
        List<Node> neighbourList = new List<Node>();

        if (currentNode.x - 1 >= 0)
        {
            neighbourList.Add(graph[currentNode.x - 1, currentNode.y]);

            if (currentNode.y - 1 >= 0)
                neighbourList.Add(graph[currentNode.x - 1, currentNode.y - 1]);
            if (currentNode.y + 1 < graph.GetLength(1))
                neighbourList.Add(graph[currentNode.x - 1, currentNode.y + 1]);
        }

        if (currentNode.x + 1 < graph.GetLength(0))
        {
            neighbourList.Add(graph[currentNode.x + 1, currentNode.y]);

            if (currentNode.y - 1 >= 0)
                neighbourList.Add(graph[currentNode.x + 1, currentNode.y - 1]);
            if (currentNode.y + 1 < graph.GetLength(1))
                neighbourList.Add(graph[currentNode.x + 1, currentNode.y + 1]);
        }

        if (currentNode.y - 1 >= 0)
            neighbourList.Add(graph[currentNode.x, currentNode.y - 1]);
        if (currentNode.y + 1 < graph.GetLength(1))
            neighbourList.Add(graph[currentNode.x, currentNode.y + 1]);

        return neighbourList;
    }

    //Once the end is reaches, walk back through the path
    private List<Node> CalculatePath(Node endNode)
    {
        List<Node> path = new List<Node>();
        path.Add(endNode);
        Node currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    public List<Node> FindPath(int startX, int startY, int endX, int endY)
    {
        Node startNode = graph[startX, startY]; //Get start of path
        Node endNode = graph[endX, endY];       //Get end of path

        List<Node> openList = new List<Node> { startNode }; //Nodes not visited
        List<Node> closedList = new List<Node>();           //Nodes visited

        int graphWidth = graph.GetLength(0); //Outer bounds of graph
        int graphHeight = graph.GetLength(1);//Outer bounds of graph

        //Walk through the path from the start and attribute values
        //Unknown gCost set to max possible value (updates later)
        for (int x = 0; x < graphWidth; x++)
            for (int y = 0; y < graphHeight; y++)
            {
                Node pathNode = graph[x, y];
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }

        //Set values from the startNode, get distance to end
        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        //While there are still unchecked nodes
        while (openList.Count > 0) 
        {
            Node currentNode = GetLowestFCostNode(openList); //Get lowest fCost
            if (currentNode == endNode)
                return CalculatePath(endNode); //Trace back the path if at end

            //Update node to visited list
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            //Look over neighbours
            foreach (Node neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue; //Has node already been checked

                //Check if neighbour is a wall (wall => closedList)
                if (!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode); //Add gCost and cost of moving to neighbour
                
                //Look for available neighbours and add to openList
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                        openList.Add(neighbourNode);
                }
            }
        }

        //No more nodes on the open list
        return null;
    }
}
