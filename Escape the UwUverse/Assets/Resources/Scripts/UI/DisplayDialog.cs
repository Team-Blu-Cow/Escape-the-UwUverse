using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisplayDialog : MonoBehaviour
{
    //public MasterInput controls;

    TextMeshProUGUI tmp;
    Queue<string> sentances;
    Queue<float> delays;

    bool typing = false;
    string currentSentance;

    // Start is called before the first frame update
    void Start()
    {
        sentances = new Queue<string>();
        delays = new Queue<float>();
        tmp = GetComponentInChildren<TextMeshProUGUI>();

        string[] text = new string[1];
        float[] delay = new float[1];

        text[0] = "test";
        delay[0] = 0.5f;

        StartDialog(text, delay);
    }

    private void Awake()
    {
        //controls = new MasterInput();
        //Keyboard.current.spaceKey;
        //controls.PlayerControls.Dialogue.performed += ctx => DialougeSkip();
    }

    private void OnEnable()
    {
        //controls.Enable();
    }

    private void OnDisable()
    {
        //controls.Disable();
    }

    public void StartDialog(string[] customDialog, float[] textDelay)
    {
        sentances.Clear();

        LeanTween.move(gameObject, new Vector3(transform.position.x, -100,0), 1);

        foreach(string sentance in customDialog)
        {
            sentances.Enqueue(sentance);
        }
        
        foreach(float delay in textDelay)
        {
            delays.Enqueue(delay);
        }

        while (delays.Count < sentances.Count)
        {
            delays.Enqueue(textDelay[0]);
        }

        DisplayNextSentance();
    }

    void DisplayNextSentance()
    {
        if (sentances.Count == 0)
        {
            EndDialog();
        }
        else
        {
            currentSentance = sentances.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentance(currentSentance, delays.Dequeue()));
        }
    }

    void DialougeSkip()
    {
        if (typing)
        {
            StopAllCoroutines();
            tmp.text = currentSentance;
            typing = false;
        }
        else
        {
            DisplayNextSentance();
        }
    }

    void EndDialog()
    {
        LeanTween.move(gameObject, new Vector3(transform.position.x, -490, 0), 1);
    }

    IEnumerator TypeSentance(string sentance, float delay)
    {
        typing = true;
        tmp.text = "";
        foreach(char letter in sentance.ToCharArray())
        {
            tmp.text += letter;
            yield return new WaitForSeconds(delay);
        }
        typing = false;
    }
}
