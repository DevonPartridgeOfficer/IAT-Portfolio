/*  Filename: GameStateManager.cs
 *   Purpose: Sets values for current scores
 *            Triggers gameover
 *            Allows exiting with esc during gameplay
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    [HideInInspector]
    public int sheepSaved;
    
    [HideInInspector]
    public int sheepDropped;

    public int sheepDroppedBeforeGameOver; //Gameover flag
    public SheepSpawner sheepSpawner;
    public int highScore;

    // Start a new GameStateManager instance
    void Awake()
    {
        Instance = this;
        highScore = GameSettings.highScore;
    }

    // Check if player has pressed esc to exit game
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }
    }

    //Increment the UI display for saved
    public void SavedSheep()
    {
        sheepSaved++;
        UIManager.Instance.UpdateSheepSaved();
        CheckScore(); //Checks if saved sheep is higher than current highscore and updates during gameplay
    }

    //Increment the UI display for losses
    //Check if threshold reached and trigger gameover
    public void DroppedSheep()
    {
        sheepDropped++;
        UIManager.Instance.UpdateSheepDropped();

        if (sheepDropped == sheepDroppedBeforeGameOver)
        {
            GameOver();
        }
    }

    //Updates score if better this playthrough
    //Happens during gameplay and gameover
    private void CheckScore()
    {
        if (sheepSaved > highScore)
        {
            GameSettings.highScore = sheepSaved;
        }
        UIManager.Instance.UpdateHighScore();
    }

    //Stops all sheep spawning and removes all remaining sheep objects
    //Show gameover screen and updates highscore if necessary
    private void GameOver()
    {
        sheepSpawner.canSpawn = false;
        sheepSpawner.DestroyAllSheep();
        UIManager.Instance.ShowGameOverWindow();
        CheckScore(); //Checks at end if score is accurate
    }
}
