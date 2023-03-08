using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//What team is playing - white or black
public enum PlayerTeam
{
    NONE = -1,
    WHITE,
    BLACK,
};

public class GameManager : MonoBehaviour
{
    BoardManager board;
    Minimax minimax;
    public PlayerTeam playerTurn; //Whose turn it currently is
    bool kingDead = false; //Flag for gameover
    public GameObject fromHighlight;
    public GameObject toHighlight;

    //Singleton instance - access other script
    private static GameManager instance; 
    public static GameManager Instance
    {
        get { return instance; }
    }

    private bool isCoroutineExecuting = false; //Delay coroutine for piece move

    //Set instance to this singleton
    private void Awake()
    {
        if (instance == null)        
            instance = this;        
        else if (instance != this)        
            Destroy(this);    
    }    

    //Sets up the board
    void Start()
    {
        board = BoardManager.Instance;
        minimax = Minimax.Instance;
        board.SetupBoard();
    }

    //Starts a new move coroutine
    private void Update()
    {
        StartCoroutine(DoAIMove());
    }

    //Runs the game
    IEnumerator DoAIMove()
    {       
        if(isCoroutineExecuting) //Stops if already running
            yield break;

        isCoroutineExecuting = true;

        //Gameover check
        if (kingDead)
            Debug.Log(playerTurn + " wins!");
        else if (!kingDead) //Move if game still running
        {
            MoveData move = minimax.GetMove();

            RemoveObject("Highlight"); //Clear previous move highlight so that a new one can be shown
            ShowMove(move);

            yield return new WaitForSeconds(1); //Delay for moves showing
            
            
            SwapPieces(move); //Moves the pieces 
            if (!kingDead) //Keeps game running and swap team if no game over               
                UpdateTurn();     

            isCoroutineExecuting = false; //Let another coroutine run after this                                                                                                        
        }
    }

    public void SwapPieces(MoveData move)
    {
        TileData firstTile = move.firstPosition;
        TileData secondTile = move.secondPosition;        

        firstTile.CurrentPiece.MovePiece(new Vector2(secondTile.Position.x, secondTile.Position.y));

        CheckDeath(secondTile);
                        
        secondTile.CurrentPiece = move.pieceMoved;
        firstTile.CurrentPiece = null;
        secondTile.CurrentPiece.chessPosition = secondTile.Position;
        secondTile.CurrentPiece.HasMoved = true;            
    }   

    private void UpdateTurn()
    {     
        playerTurn = playerTurn == PlayerTeam.WHITE ? PlayerTeam.BLACK : PlayerTeam.WHITE;        
    }

    void CheckDeath(TileData _secondTile)
    {
        if (_secondTile.CurrentPiece != null)        
            if (_secondTile.CurrentPiece.Type == ChessPiece.PieceType.KING)           
                kingDead = true;                           
            else
                Destroy(_secondTile.CurrentPiece.gameObject);        
    }

    void ShowMove(MoveData move)
    {
        GameObject GOfrom = Instantiate(fromHighlight);
        GOfrom.transform.position = new Vector2(move.firstPosition.Position.x, move.firstPosition.Position.y);
        GOfrom.transform.parent = transform;

        GameObject GOto = Instantiate(toHighlight);
        GOto.transform.position = new Vector2(move.secondPosition.Position.x, move.secondPosition.Position.y);
        GOto.transform.parent = transform;
    }

    public void RemoveObject(string text)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(text);
        foreach (GameObject GO in objects)
            Destroy(GO);        
    }
}
