using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInteraction
{
    public Color highlightColor = new Color(0f, 200f, 0f);
    public static bool selected;

    // highlight the selected object
    public void highlightObject(GameObject go)
    {
        if (!selected)
        {
            Renderer rend = go.GetComponent<Renderer>();
            rend.material.color += highlightColor;
            selected = true;
            foreach (Renderer r in go.GetComponentsInChildren<Renderer>())
            {
                r.material.color += highlightColor;
            }
        }
    }

    public void unHighlightObject(GameObject go)
    {
        if(selected == true)
        {
            Renderer rend = go.GetComponent<Renderer>();
            rend.material.color -= highlightColor;
            selected = false;
            foreach (Renderer r in go.GetComponentsInChildren<Renderer>())
            {
                r.material.color -= highlightColor;
            }
        }
        
    }

    public bool getStatus()
    {
        return selected;
    }

    public void setStatus(bool stat)
    {
        selected = stat;
    }
}
