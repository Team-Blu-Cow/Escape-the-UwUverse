using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DisplayDialog : MonoBehaviour
{
    //public MasterInput controls;

    TextMeshProUGUI tmp;
    Queue<string> sentances;
    Queue<float> delays;

    bool typing = false;
    string currentSentance;

    [SerializeField] int maxHeight;
    [SerializeField] int minHeight;

    [SerializeField] TextMeshProUGUI activeName;
    [SerializeField] Image characterSprite;
    [SerializeField] Image finishedMark;

    // Start is called before the first frame update
    void Start()
    {
        sentances = new Queue<string>();
        delays = new Queue<float>();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            string[] text = new string[1];
            float[] delay = new float[1];

            text[0] = "test";
            delay[0] = 0.1f;

            StartDialog(text, delay, "Jack");
        }

        if (Keyboard.current.enterKey.isPressed)
        {
            EndDialog();
        }
    }
    private void Awake()
    {
        //controls = new MasterInput();
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
        activeName.transform.parent.gameObject.SetActive(false);
        characterSprite.enabled = false;

        sentances.Clear();
        delays.Clear();

        foreach (string sentance in customDialog)
        {
            sentances.Enqueue(sentance);
        }

        foreach (float delay in textDelay)
        {
            delays.Enqueue(delay);
        }

        while (delays.Count < sentances.Count)
        {
            delays.Enqueue(textDelay[0]);
        }

        LeanTween.move(gameObject, new Vector3(transform.position.x, maxHeight, 0), 1);

        DisplayNextSentance();
    }
    
    public void StartDialog(string[] customDialog, float[] textDelay, string name)
    {
        activeName.transform.parent.gameObject.SetActive(true);
        characterSprite.enabled = true;

        activeName.text = name;
        string path = "GFX/DialogueSprites/" + name;
        characterSprite.sprite = Resources.Load<Sprite>(path);

        if (characterSprite.sprite == null)
        {
            Debug.LogWarning("Sprite at " + path + " not loaded");
            characterSprite.enabled = false;
        }

        sentances.Clear();
        delays.Clear();
        
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

        LeanTween.move(gameObject, new Vector3(transform.position.x, maxHeight, 0), 1).setOnComplete(DisplayNextSentance);
            
        //DisplayNextSentance();
    }

    void DisplayNextSentance()
    {
        if (sentances.Count == 0)
        {
            //EndDialog();
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
        LeanTween.move(gameObject, new Vector3(transform.position.x, minHeight, 0), 1);
    }

    IEnumerator TypeSentance(string sentance, float delay)
    {
        typing = true;
        finishedMark.enabled = false;
        tmp.text = "";
        foreach(char letter in sentance.ToCharArray())
        {
            tmp.text += letter;
            yield return new WaitForSeconds(delay);
        }
        finishedMark.enabled = true;
        typing = false;
    }
}
