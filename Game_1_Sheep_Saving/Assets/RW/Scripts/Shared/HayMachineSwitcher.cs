/*  Filename: HayMachineSwitcher.cs
 *   Purpose: Changes current machine with titlescreen click
 *            Sets selected machine as new colour for gameplay
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class HayMachineSwitcher : MonoBehaviour, IPointerClickHandler
{
    //Haymachine objects on titlescreen
    public GameObject blueHayMachine;
    public GameObject yellowHayMachine;
    public GameObject redHayMachine;

    private int selectedIndex;

    // From the titlescreen
    public void OnPointerClick(PointerEventData eventData)
    {
        //Cycles through colour options
        selectedIndex++;
        selectedIndex %= Enum.GetValues(typeof(HayMachineColor)).Length;
        GameSettings.hayMachineColor = (HayMachineColor)selectedIndex; //Updates current machine colour for in-game

        //Sets one machine active at a time to view
        switch (GameSettings.hayMachineColor)
        {
            case HayMachineColor.Blue:
                blueHayMachine.SetActive(true);
                yellowHayMachine.SetActive(false);
                redHayMachine.SetActive(false);
                break;

            case HayMachineColor.Yellow:
                blueHayMachine.SetActive(false);
                yellowHayMachine.SetActive(true);
                redHayMachine.SetActive(false);
                break;

            case HayMachineColor.Red:
                blueHayMachine.SetActive(false);
                yellowHayMachine.SetActive(false);
                redHayMachine.SetActive(true);
                break;

        }
    }
}
