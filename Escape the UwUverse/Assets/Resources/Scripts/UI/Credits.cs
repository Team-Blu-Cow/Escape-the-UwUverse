using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UwUVerse
{
    public class Credits : MonoBehaviour
    {
        private TextMeshProUGUI m_tmp;

        private Queue<string> names = new Queue<string>();

        private void Awake()
        {
            m_tmp = GetComponent<TextMeshProUGUI>();
            string credits = GameController.ReadFile("Resources/Credits.txt");

            var lines = credits.Split("\n"[0]);
            foreach (string line in lines)
            {
                names.Enqueue(line);
            }

            m_tmp.text = credits;
        }

        // Start is called before the first frame update
        private void Start()
        {
            transform.localPosition = new Vector3(0, -names.Count * m_tmp.fontSize, 0);
            LeanTween.moveLocalY(gameObject, 630 + m_tmp.fontSize, 10 + names.Count).setOnComplete( () => { GameController.Instance.SwitchScene("MainMenu"); });
        }
    }
}