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
    public GameObject gameOverWindow;

    // Start a new UIManager instance
    void Awake()
    {
        Instance = this;
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
    
    public void ShowGameOverWindow()
    {
        gameOverWindow.SetActive(true);
    }
}