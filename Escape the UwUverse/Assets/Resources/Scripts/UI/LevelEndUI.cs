using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UwUVerse
{
    public class LevelEndUI : MonoBehaviour
    {
        [SerializeField] private int m_currentLevel;

        // Start is called before the first frame update
        private void Start()
        {
            GetComponent<Canvas>().enabled = false;

            // Add on clicks for the butons
            GetComponentsInChildren<Button>()[0].onClick.AddListener(() => { GameController.Instance.SwitchScene("MainMenu"); });
            GetComponentsInChildren<Button>()[1].onClick.AddListener(() => { GameController.Instance.SwitchLevel(m_currentLevel + 1); });
            GetComponentsInChildren<Button>()[2].onClick.AddListener(() => { GameController.Instance.SwitchLevel(m_currentLevel); });
        }

        public void GameReset(bool in_died)
        {
            GetComponent<Canvas>().enabled = true;
            GetComponentsInChildren<Button>()[1].interactable = false;
            GetComponentsInChildren<Button>()[1].GetComponentInChildren<TextMeshProUGUI>().enabled = false;

            if (!in_died)
            {
                GetComponentsInChildren<Button>()[1].interactable = true;
                GetComponentsInChildren<Button>()[1].GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            }

            SetWin(in_died);
            SetStats();
        }

        private void SetWin(bool in_died)
        {
            if (in_died)
            {
                GetComponentInChildren<TextMeshProUGUI>().text = "You Died, Try Again";
            }
            else
            {
                GetComponentInChildren<TextMeshProUGUI>().text = "You Win!!";
            }
        }

        private void SetStats()
        {
            GetComponentsInChildren<TextMeshProUGUI>()[1].text = "Stats: \n" +
                "Moves Taken: \n" +
                "Enemies Killed: \n" +
                "Shots Fired: \n";
        }
    }
}