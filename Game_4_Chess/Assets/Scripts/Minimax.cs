using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimax : MonoBehaviour
{
    //Singleton references
    BoardManager board;
    GameManager gameManager;
    MoveData bestMove; //return from Minimax algorithm

    //Used in evaluation for moves
    int myScore = 0;
    int opponentScore = 0;

    int maxDepth; //Flag for recursive to stop

    //List for algorithm fake plays
    List<TileData> myPieces = new List<TileData>();
    List<TileData> opponentPieces = new List<TileData>();

    Stack<MoveData> moveStack = new Stack<MoveData>(); //Stack for storing moves
    MoveHeuristic weight = new MoveHeuristic(); //piece weighting

    //Singleton instance for this class
    public static Minimax instance;
    public static Minimax Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Called from GameManager to run algorithm
    public MoveData GetMove()
    {
        board = BoardManager.Instance;
        gameManager = GameManager.Instance;
        bestMove = CreateMove(board.GetTileFromBoard(new Vector2(0, 0)), board.GetTileFromBoard(new Vector2(0, 0))); //Default move of [0,0] to overwrite

        maxDepth = 3; //How many moves ahead to check against (affects performance)
        CalculateMinMax(maxDepth, true);

        return bestMove;
    }

    //Creates a hypothetical move
    //Where from, where to, piece moving, piece killed check
    MoveData CreateMove(TileData from, TileData to)
    {
        MoveData tempMove = new MoveData
        {
            firstPosition = from,
            pieceMoved = from.CurrentPiece,
            secondPosition = to
        };

        if (to.CurrentPiece != null)
            tempMove.pieceKilled = to.CurrentPiece;

        return tempMove;
    }

    //Get all available moves for the player
    //Iterates through all available player pieces and makes a list of legal moves (MoveData)
    List<MoveData> GetMoves(PlayerTeam team)
    {
        List<MoveData> turnMove = new List<MoveData>();
        List<TileData> pieces = (team == gameManager.playerTurn) ? myPieces : opponentPieces;

        foreach (TileData tile in pieces)
        {
            MoveFunction movement = new MoveFunction(board);
            List<MoveData> pieceMoves = movement.GetMoves(tile.CurrentPiece, tile.Position);

            foreach (MoveData move in pieceMoves)
            {
                MoveData newMove = CreateMove(move.firstPosition, move.secondPosition);
                turnMove.Add(newMove);
            }
        }
        return turnMove;
    }

    //Performs the fake moves
    void DoFakeMove(TileData currentTile, TileData targetTile)
    {
        targetTile.SwapFakePieces(currentTile.CurrentPiece);
        currentTile.CurrentPiece = null;
    }

    //Undo the fake moves (moving back up the 'tree')
    void UndoFakeMove()
    {
        MoveData tempMove = moveStack.Pop();
        TileData movedTo = tempMove.secondPosition;
        TileData movedFrom = tempMove.firstPosition;
        ChessPiece pieceKilled = tempMove.pieceKilled;
        ChessPiece pieceMoved = tempMove.pieceMoved;

        movedFrom.CurrentPiece = movedTo.CurrentPiece;
        movedTo.CurrentPiece = (pieceKilled != null) ? pieceKilled : null;
    }

    //Evaluate score(piece) difference
    int Evaluate()
    {
        int pieceDifference = myScore - opponentScore;
        return pieceDifference;
    }

    //Check over board
    //Assign pieces to myPieces or opponentPieces and calculates score
    void GetBoardState()
    {
        myPieces.Clear();
        opponentPieces.Clear();
        myScore = 0;
        opponentScore = 0;

        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                TileData tile = board.GetTileFromBoard(new Vector2(x, y));
                if (tile.CurrentPiece != null && tile.CurrentPiece.Type != ChessPiece.PieceType.NONE)
                {
                    if (tile.CurrentPiece.Team == gameManager.playerTurn)
                    {
                        myScore += weight.GetPieceWeight(tile.CurrentPiece.Type);
                        myPieces.Add(tile);
                    }
                    else
                    {
                        opponentScore += weight.GetPieceWeight(tile.CurrentPiece.Type);
                        opponentPieces.Add(tile);
                    }
                }
            }
        }
    }

    int CalculateMinMax(int depth, bool max)
    {
        GetBoardState();

        if (depth == 0)
            return Evaluate();

        if (max)
        {
            int maxScore = int.MinValue;
            List<MoveData> allMoves = GetMoves(gameManager.playerTurn);
            allMoves = Shuffle(allMoves);
            foreach (MoveData move in allMoves)
            {
                moveStack.Push(move);

                DoFakeMove(move.firstPosition, move.secondPosition);
                int score = CalculateMinMax(depth - 1, false);
                UndoFakeMove();

                if (score > maxScore)
                    maxScore = score;

                if (score > bestMove.score && depth == maxDepth)
                {
                    move.score = score;
                    bestMove = move;
                }
            }
            return maxScore;
        }
        else
        {
            PlayerTeam opponent = gameManager.playerTurn == PlayerTeam.WHITE ? PlayerTeam.BLACK : PlayerTeam.WHITE;
            int minScore = int.MaxValue;
            List<MoveData> allMoves = GetMoves(opponent);
            allMoves = Shuffle(allMoves);
            foreach (MoveData move in allMoves)
            {
                moveStack.Push(move);

                DoFakeMove(move.firstPosition, move.secondPosition);
                int score = CalculateMinMax(depth - 1, true);
                UndoFakeMove();

                if (score < minScore)
                    minScore = score;
            }
            return minScore;
        }
    }

    //Shuffles any given list
    public List<T> Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }
}
