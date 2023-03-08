using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//2D array of TileData
//Holds information about tile position on board and any pieces on the tile
public class TileData
{
    private Vector2 position = Vector2.zero;
    public Vector2 Position
    {
        get{ return position; }
    }

    private ChessPiece currentPiece = null;
    public ChessPiece CurrentPiece
    {
        get{ return currentPiece; }
        set{ currentPiece = value; }
    }

    public TileData(int x, int y)
    {
        position.x = x;
        position.y = y;

        if (y == 0 || y == 1 || y == 6 || y == 7)        
            currentPiece = GameObject.Find("[" + x.ToString() + "," + y.ToString() + "]").GetComponent<ChessPiece>();        
    }

    public void SwapFakePieces(ChessPiece newPiece)
    {
        currentPiece = newPiece;
    }
}
