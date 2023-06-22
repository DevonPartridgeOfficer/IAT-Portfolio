/*  Filename: ChessPiece.cs
 *   Purpose: Holds information for piece type, position and where its moving
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Holds position info for pieces and where it is moving to
public class ChessPiece : MonoBehaviour
{
    public enum PieceType
    {
        NONE = -1,
        PAWN,
        BISHOP,
        KNIGHT,
        ROOK,
        QUEEN,
        KING,
    };    

    [SerializeField] private PieceType type = PieceType.NONE;
    [SerializeField] private PlayerTeam team = PlayerTeam.NONE;

    public PieceType Type
    {
        get{ return type; }
    }
    public PlayerTeam Team
    {
        get{ return team; }
    }
    public Vector2 chessPosition;
    private Vector2 moveTo;

    private bool hasMoved = false;
    public bool HasMoved
    {
        get{ return hasMoved; }
        set{ hasMoved = value; }
    }
        
    void Start()
    {
        transform.position = chessPosition;
        moveTo = transform.position;
    }

    void Update()
    {
        transform.position = moveTo;
    }

    public void MovePiece(Vector2 position)
    {
        moveTo = position;
    }
}
