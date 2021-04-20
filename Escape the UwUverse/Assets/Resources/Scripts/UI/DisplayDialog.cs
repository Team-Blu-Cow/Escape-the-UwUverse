using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UwUverse
{
    public class DisplayDialog : MonoBehaviour
    {
        public MasterInput controls;

        private TextMeshProUGUI tmp;
        private Queue<string> sentances;
        private Queue<float> delays;

        private bool typing = false;
        private string currentSentance;

        [SerializeField] private int maxHeight;
        [SerializeField] private int minHeight;

        [SerializeField] private TextMeshProUGUI activeName;
        [SerializeField] private Image characterSprite;
        [SerializeField] private Image finishedMark;

        private void Start()
        {
            sentances = new Queue<string>();
            delays = new Queue<float>();
            tmp = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Awake()
        {
            string[] text = new string[1];
            float[] delay = new float[1];

            text[0] = "test";
            delay[0] = 0.1f;

            controls = new MasterInput();
            controls.Interact.Confirm.performed += ctx => DialougeSkip();
            controls.Interact.Cancel.performed += ctx => EndDialog();
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        public void StartDialog(string[] customDialog, float[] textDelay)
        {
            activeName.transform.parent.gameObject.SetActive(false);
            characterSprite.enabled = false;
            finishedMark.enabled = false;
            tmp.text = "";

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
                delays.Enqueue(0);
            }

            LeanTween.move(gameObject, new Vector3(transform.position.x, maxHeight, 0), 1).setOnComplete(DisplayNextSentance);
        }

        public void StartDialog(string[] customDialog, float[] textDelay, string name)
        {
            activeName.transform.parent.gameObject.SetActive(true);
            characterSprite.enabled = true;
            finishedMark.enabled = false;
            tmp.text = "";

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

            LeanTween.move(gameObject, new Vector3(transform.position.x, maxHeight, 0), 1).setOnComplete(DisplayNextSentance);
        }

        private void DisplayNextSentance()
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

        private void DialougeSkip()
        {
            if (typing)
            {
                StopAllCoroutines();
                tmp.text = currentSentance;
                typing = false;
                finishedMark.enabled = true;
            }
            else
            {
                DisplayNextSentance();
            }
        }

        private void EndDialog()
        {
            LeanTween.move(gameObject, new Vector3(transform.position.x, minHeight, 0), 1);
        }

        private IEnumerator TypeSentance(string sentance, float delay)
        {
            typing = true;
            finishedMark.enabled = false;
            tmp.text = "";
            foreach (char letter in sentance.ToCharArray())
            {
                tmp.text += letter;
                yield return new WaitForSeconds(delay);
            }
            finishedMark.enabled = true;
            typing = false;
        }
    }
}