/*  Filename: MoveData.cs
 *   Purpose: Holds data on piece movement
 *            What tile they move from, move to, and whether to destroy an enemy piece
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveData
{
    public TileData firstPosition = null;
    public TileData secondPosition = null;
    public ChessPiece pieceMoved = null;
    public ChessPiece pieceKilled = null;
    public int score = int.MinValue;
}
