/*  Filename: QuitButton.cs
 *   Purpose: Handles the closing of the game from the titlescreen
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit(); //Only works in builds of the game
    }
}
