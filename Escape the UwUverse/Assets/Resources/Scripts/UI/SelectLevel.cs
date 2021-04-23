using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UwUverse
{
    public class SelectLevel : MonoBehaviour
    {
        private int m_Level;

        private void SwapLevel()
        {
            GameController.Instance.SwitchLevel(m_Level);
        }

        public void Setup(int in_level)
        {
            m_Level = in_level;
            GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("GFX/DialogueSprites/Level" + m_Level);
            GetComponentInChildren<TextMeshProUGUI>().text = "Level " + m_Level;
            GetComponentInChildren<Button>().onClick.AddListener(SwapLevel);
        }
    }
}