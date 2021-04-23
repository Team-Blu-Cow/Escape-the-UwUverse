using UnityEngine;

public class Pause : MonoBehaviour
{
    private Canvas m_canvas;

    [SerializeField] private Canvas m_optionsCanvas;

    private void Start()
    {
        m_canvas = GetComponent<Canvas>();
        m_canvas.enabled = false;
    }

    public void TogglePause()
    {
        // Get if the options are already open or not
        bool open = m_canvas.isActiveAndEnabled;

        if (open)
        {
            m_canvas.enabled = false;
        }
        else
        {
            m_canvas.enabled = true;
        }
    }

    public void OpenOptions()
    {
        m_optionsCanvas.enabled = true;
    }

    public void CloseOptions()
    {
        m_optionsCanvas.enabled = false;
    }
}