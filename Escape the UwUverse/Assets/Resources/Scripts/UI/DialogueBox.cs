using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [Header("Entered Text")]
    [TextArea(3, 10)]
    [SerializeField] private string[] in_customDialog;

    [SerializeField] private float[] in_textDelay;

    [Header("Speaker")]
    [SerializeField] private string in_speakerName;

    [SerializeField] private bool in_randomPerson;

    [Header("Display Box")]
    [SerializeField] private UwUverse.DisplayDialog in_displayText;

    [SerializeField] private bool oneTime;

    private Sprite[] m_people;
    private int m_personIndex;

    private void Start()
    {
        m_people = Resources.LoadAll<Sprite>("GFX/DialogueSprites");
        m_personIndex = Random.Range(0, m_people.Length);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (in_randomPerson)
            {
                in_displayText.StartDialog(in_customDialog, in_textDelay, m_people[m_personIndex].name);
            }
            else if (name != "")
            {
                in_displayText.StartDialog(in_customDialog, in_textDelay, in_speakerName);
            }
            else
            {
                in_displayText.StartDialog(in_customDialog, in_textDelay);
            }
        }

        if (oneTime)
        {
            Destroy(gameObject);
        }
    }
}