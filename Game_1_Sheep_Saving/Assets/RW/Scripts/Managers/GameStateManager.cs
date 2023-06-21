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

    // Start a new GameStateManager instance
    void Awake()
    {
        Instance = this;
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

    //Stops all sheep spawning and removes all remaining sheep objects
    //Show gameover screen
    private void GameOver()
    {
        sheepSpawner.canSpawn = false;
        sheepSpawner.DestroyAllSheep();
        UIManager.Instance.ShowGameOverWindow();
    }
}
