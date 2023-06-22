/*  Filename: MonsterData.cs
 *   Purpose: Used to group the cost (in gold) and the visual representation for a specific monster level
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

//Group gold cost and Monitization. Serializable to change all values in Level class while game is running
[System.Serializable]

public class MonsterLevel
{
    public int cost;
    public GameObject visualisation;
    public GameObject bullet;
    public float fireRate;
}

//Handles get/set of levels, updates UI visualisation
public class MonsterData : MonoBehaviour
{
    public List<MonsterLevel> levels;
    private MonsterLevel currentLevel;

    public MonsterLevel CurrentLevel
    {
        get { return currentLevel; }
        set 
        { 
            currentLevel = value;
            int currentLevelIndex = levels.IndexOf(currentLevel);
            GameObject levelVisualisation = levels[currentLevelIndex].visualisation;

            for (int i = 0; i < levels.Count; i++)
            {
                if (levelVisualisation != null)
                {
                    if (i == currentLevelIndex)
                    { levels[i].visualisation.SetActive(true); }
                    else
                    { levels[i].visualisation.SetActive(false); }
                }
            }
        }
    }

    //Starts monsters at lowest level (1) when placed
    void OnEnable()
    {
        CurrentLevel = levels[0];
    }

    //Checks if current monster can be upgraded (>maxLevel), otherwise no upgrade
    public MonsterLevel GetNextLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        int maxLevelIndex = levels.Count - 1;
        if (currentLevelIndex < maxLevelIndex)
        {
            return levels[currentLevelIndex+1];
        }
        else
        {
            return null;
        }
    }

    //Increments the level of the selected monster
    public void IncreaseLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        if (currentLevelIndex < levels.Count - 1)
        {
            CurrentLevel = levels[currentLevelIndex+1];
        }
    }
}
