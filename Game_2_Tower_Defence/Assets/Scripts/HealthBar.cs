/*  Filename: HealthBar.cs
 *   Purpose: Sets health for all enemies and updates if shot by player monster
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 100;
    private float originalScale;

    // Starts healthbar with a default scale for the gameobject (full health)
    void Start()
    {
        originalScale = gameObject.transform.localScale.x;
    }

    // Updates the healthbar with new health % and displays
    void Update()
    {
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x = currentHealth / maxHealth * originalScale;
        gameObject.transform.localScale = tmpScale;
    }
}
