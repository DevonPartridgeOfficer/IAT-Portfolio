/*  Filename: DestoryTimer.cs
 *   Purpose: Delays a 'Destroy' call for the specified amount of time
 *            Used to destroy sheep heart objects after 1.5 seconds
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float timeToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
