using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UwUVerse
{
    public class LevelEndUI : MonoBehaviour
    {
        [SerializeField] private int m_currentLevel;
        private System.Action<bool> handler;

        // Start is called before the first frame update
        private void Start()
        {
            GetComponent<Canvas>().enabled = false;

            // Add on clicks for the buttons
            GetComponentsInChildren<Button>()[0].onClick.AddListener(() => { GameController.Instance.SwitchScene("MainMenu"); });
            if (m_currentLevel == 3)
            {
                GetComponentsInChildren<Button>()[0].onClick.AddListener(() => { GameController.Instance.SwitchScene("GameEnd"); });
            }
            else
            {
                GetComponentsInChildren<Button>()[1].onClick.AddListener(() => { Resetlevel(m_currentLevel + 1); });
            }
            GetComponentsInChildren<Button>()[2].onClick.AddListener(() => { Resetlevel(m_currentLevel); });

            handler = (in_) => GameReset(in_);

            Player.player_died += handler;
        }

        private void OnDestroy()
        {
            Player.player_died -= handler;
        }

        public void GameReset(bool in_died)
        {
            GameController.Instance.isPaused = true;
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
                "Moves Taken:" + GameController.LevelStats().moves + "\n" +
                "Enemies Killed:" + GameController.LevelStats().enemiesKilled + "\n" +
                "Shots Fired:" + GameController.LevelStats().shots + "\n";
        }

        private void Resetlevel(int in_level)
        {
            //GameController.StepController().ResetEntities();
            //GameController.StepController().ClearMoves();
            GameController.Instance.currentLevel = in_level;
            GameController.Instance.SwitchLevel(in_level);
        }
    }
}