/*  Filename: ChangeColorOnMouseOver.cs
 *   Purpose: Used on titlescreen to indicate mouse location
 *            Updates object colour with selected hoverColor while mouse is active
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeColorOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MeshRenderer model;
    public Color normalColor;
    public Color hoverColor;

    // Starts with normal colour for the object
    void Start()
    {
        model.material.color = normalColor;
    }

    // On mouse over, update with new hoverColor
    public void OnPointerEnter(PointerEventData eventData)
    {
        model.material.color = hoverColor;
    }

    //Return to normal after mouse leaves object
    public void OnPointerExit(PointerEventData eventData)
    {
        model.material.color = normalColor;
    }
}
