using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [Header("Entered Text")]
    [TextArea(3, 10)]
    public string[] customDialog;
    public float[] textDelay;

    public DisplayDialog displayText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.CompareTag("Player"))
        {
            displayText.StartDialog(customDialog, textDelay);
        }
    }
}
