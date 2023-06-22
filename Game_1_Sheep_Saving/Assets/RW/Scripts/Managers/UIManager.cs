/*  Filename: UIManager.cs
 *   Purpose: Reads and displays current saved/lost sheep to screen
 *            Toggles gameover screen
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text sheepSavedText;
    public Text sheepDroppedText;
    public Text highScoreText;
    public GameObject gameOverWindow;

    // Start a new UIManager instance
    void Awake()
    {
        Instance = this;
        UpdateHighScore();
    }

    //Updates saved with value set by GameStateManager
    public void UpdateSheepSaved() 
    {
        sheepSavedText.text = GameStateManager.Instance.sheepSaved.ToString();
    }

    //Updates dropped with value set by GameStateManager
    public void UpdateSheepDropped() 
    {
        sheepDroppedText.text = GameStateManager.Instance.sheepDropped.ToString();
    }

    //Updates highscore from gamesettings value
    public void UpdateHighScore()
    {
        highScoreText.text = "High Score: " + GameSettings.highScore.ToString();
    }
    
    public void ShowGameOverWindow()
    {
        gameOverWindow.SetActive(true);
    }
}