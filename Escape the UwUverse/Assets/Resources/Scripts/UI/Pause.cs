using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private Canvas canvas;

    [SerializeField] private Canvas optionsCanvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void TogglePause()
    {
        // Get if the options are already open or not
        bool open = canvas.isActiveAndEnabled;

        if (open)
        {
            canvas.enabled = false;
        }
        else
        {
            canvas.enabled = true;
        }
    }

    public void OpenOptions()
    {
        optionsCanvas.enabled = true;
    }

    public void CloseOptions()
    {
        optionsCanvas.enabled = false;
    }
}