using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    [Header("Entered Text")]
    [TextArea(3, 10)]
    [SerializeField] private string[] customDialog;

    [SerializeField] private float[] textDelay;

    [Header("Speaker")]
    [SerializeField] private string speakerName;
    [SerializeField] private bool randomPerson;

    [Header("Display Box")]
    [SerializeField] private UwUverse.DisplayDialog displayText;

    private Sprite[] people;
    private int personIndex;

    private void Start()
    {
        people = Resources.LoadAll<Sprite>("GFX/DialogueSprites");
        personIndex = Random.Range(0, people.Length);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (randomPerson)
            {
                displayText.StartDialog(customDialog, textDelay, people[personIndex].name);
            }
            else if (name != "")
            {
                displayText.StartDialog(customDialog, textDelay, speakerName);
            }
            else
            {
                displayText.StartDialog(customDialog, textDelay);
            }
        }
    }
}