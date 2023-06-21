/*  Filename: TweenScale.cs
 *   Purpose: 'Animates' sheep heart object between two sizes over time
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenScale : MonoBehaviour
{
    public float targetScale; 
    public float timeToReachTarget; 
    private float startScale;  
    private float percentScaled;

    // Sets starting scale to the current scale of the heart
    void Start()
    {
        startScale = transform.localScale.x;
    }

    // Scales to target amount over time
    void Update()
    {
        if (percentScaled < 1f) 
        {
            percentScaled += Time.deltaTime / timeToReachTarget; 
            float scale = Mathf.Lerp(startScale, targetScale, percentScaled); 
            transform.localScale = new Vector3(scale, scale, scale); 
        }
    }
}
