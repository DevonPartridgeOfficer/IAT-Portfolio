/*  Filename: Rotate.cs
 *   Purpose: Rotates x,y,z axis values for windmill blades by a constant value
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotationSpeed;

    // Updates all axis with new rotation if 'value > 0' is set in editor
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
