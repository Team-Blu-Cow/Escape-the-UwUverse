using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UwUverse
{
    public class DisplayDialog : MonoBehaviour
    {
        private MasterInput m_controls;

        private TextMeshProUGUI m_tmp;
        private Queue<string> m_sentances;
        private Queue<float> m_delays;

        private bool m_typing = false;
        private string m_currentSentance;

        [SerializeField] private TextMeshProUGUI in_activeName;
        [SerializeField] private Image in_characterSprite;
        [SerializeField] private Image in_finishedMark;

        private void Start()
        {
            m_sentances = new Queue<string>();
            m_delays = new Queue<float>();
            m_tmp = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Awake()
        {
            m_controls = new MasterInput();
            m_controls.Interact.Confirm.performed += ctx => DialougeSkip();
            m_controls.Interact.Cancel.performed += ctx => EndDialog();
        }

        private void OnEnable()
        {
            m_controls.Enable();
        }

        private void OnDisable()
        {
            m_controls.Disable();
        }

        public void StartDialog(string[] customDialog, float[] textDelay)
        {
            in_activeName.transform.parent.gameObject.SetActive(false);
            in_characterSprite.enabled = false;
            in_finishedMark.enabled = false;
            m_tmp.text = "";

            m_sentances.Clear();
            m_delays.Clear();

            foreach (string sentance in customDialog)
            {
                m_sentances.Enqueue(sentance);
            }

            foreach (float delay in textDelay)
            {
                m_delays.Enqueue(delay);
            }

            while (m_delays.Count < m_sentances.Count)
            {
                m_delays.Enqueue(0);
            }

            LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 1, 1).setOnComplete(DisplayNextSentance);
        }

        public void StartDialog(string[] customDialog, float[] textDelay, string name)
        {
            in_activeName.transform.parent.gameObject.SetActive(true);
            in_characterSprite.enabled = true;
            in_finishedMark.enabled = false;
            m_tmp.text = "";

            in_activeName.text = name;
            string path = "GFX/DialogueSprites/" + name;
            in_characterSprite.sprite = Resources.Load<Sprite>(path);

            if (in_characterSprite.sprite == null)
            {
                Debug.LogWarning("Sprite at " + path + " not loaded");
                in_characterSprite.enabled = false;
            }

            m_sentances.Clear();
            m_delays.Clear();

            foreach (string sentance in customDialog)
            {
                m_sentances.Enqueue(sentance);
            }

            foreach (float delay in textDelay)
            {
                m_delays.Enqueue(delay);
            }

            while (m_delays.Count < m_sentances.Count)
            {
                m_delays.Enqueue(textDelay[0]);
            }

            LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 1, 1).setOnComplete(DisplayNextSentance);
        }

        private void DisplayNextSentance()
        {
            if (m_sentances.Count == 0)
            {
                EndDialog();
            }
            else
            {
                m_currentSentance = m_sentances.Dequeue();
                StopAllCoroutines();
                StartCoroutine(TypeSentance(m_currentSentance, m_delays.Dequeue()));
            }
        }

        private void DialougeSkip()
        {
            if (m_typing)
            {
                StopAllCoroutines();
                m_tmp.text = m_currentSentance;
                m_typing = false;
                in_finishedMark.enabled = true;
            }
            else
            {
                DisplayNextSentance();
            }
        }

        private void EndDialog()
        {
            LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 0, 1);
        }

        private IEnumerator TypeSentance(string sentance, float delay)
        {
            m_typing = true;
            in_finishedMark.enabled = false;
            m_tmp.text = "";
            foreach (char letter in sentance.ToCharArray())
            {
                m_tmp.text += letter;
                yield return new WaitForSeconds(delay);
            }
            in_finishedMark.enabled = true;
            m_typing = false;
        }
    }
}