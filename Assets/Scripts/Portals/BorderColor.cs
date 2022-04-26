using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderColor : MonoBehaviour
{

    public Material border;
    public float colorSpeed;

    private bool red;
    private bool redReverse;
    private bool green;
    private bool greenReverse;
    private bool blue;
    private bool blueReverse;

    // Start is called before the first frame update
    void Start()
    {
        red = true;
        redReverse = false;
        green = false;
        greenReverse = false;
        blue = false;
        blueReverse = false;

        border.SetColor("_EmissionColor", new Color(0.5f, 0f, 1f));
    }

    private void FixedUpdate()
    {
        if (red)
        {
            if (!redReverse)
            {
                border.SetColor("_EmissionColor", new Color(border.GetColor("_EmissionColor").r + colorSpeed/10f, 0f, 1f));
            }
            else
            {
                border.SetColor("_EmissionColor", new Color(1f, 0f, border.GetColor("_EmissionColor").b - colorSpeed / 10f));
            }

            if (border.GetColor("_EmissionColor").r >= 1f && !redReverse)
            {
                redReverse = true;
            }
            else if (border.GetColor("_EmissionColor").b <= 0f && redReverse)
            {
                green = true;
                red = false;
                redReverse = false;
            }
        }
        else if (green)
        {
            if (!greenReverse)
            {
                border.SetColor("_EmissionColor", new Color(1f, border.GetColor("_EmissionColor").g + colorSpeed / 10f, 0f));
            }
            else
            {
                border.SetColor("_EmissionColor", new Color(border.GetColor("_EmissionColor").r - colorSpeed / 10f, 1f, 0f));
            }

            if (border.GetColor("_EmissionColor").g >= 1f && !greenReverse)
            {
                greenReverse = true;
            }
            else if (border.GetColor("_EmissionColor").r <= 0f && greenReverse)
            {
                blue = true;
                green = false;
                greenReverse = false;
            }
        }
        else if (blue)
        {
            if (!blueReverse)
            {
                border.SetColor("_EmissionColor", new Color(0f, 1f, border.GetColor("_EmissionColor").b + colorSpeed / 10f));
            }
            else
            {
                border.SetColor("_EmissionColor", new Color(0f, border.GetColor("_EmissionColor").g - colorSpeed / 10f, 1f));
            }

            if (border.GetColor("_EmissionColor").b >= 1f && !blueReverse)
            {
                blueReverse = true;
            }
            else if (border.GetColor("_EmissionColor").g <= 0f && blueReverse)
            {
                red = true;
                blue = false;
                blueReverse = false;
            }
        }
    }
}
