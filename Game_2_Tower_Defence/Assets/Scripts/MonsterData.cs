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
}

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
            int currentLevelIndex = levles.IndexOf(CurrentLevel);
            GameObject levelVisualisation = levels[currentLevelIndex].visualisation;

            for (int i = 0; i < levles.Count; i++)
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

    void OnEnable()
    {
        CurrentLevel = levels[0];
    }

    public MonsterLevel GetNextLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        int maxLevelIndex = levels.Count - 1;
        if (currentLevelIndex < maxLevelIndex) //Only level up if less than maxlevel
        {
            return levels[currentLevelIndex + 1];
        }
        else
        {
            return null;
        }
    }

    public void IncreaseLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        if (currentLevelIndex < levels.Count - 1)
        {
            CurrentLevel = levels[currentLevelIndex + 1];
        }
    }
}
