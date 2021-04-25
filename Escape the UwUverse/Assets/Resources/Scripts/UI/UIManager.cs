using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Player m_player;

    // Start is called before the first frame update
    private void Start()
    {
        m_player = FindObjectOfType<Player>();
        if (m_player == null)
        {
            Debug.LogError("NO PLAYER IN SCENE");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateHealth();
        UpdateShotCounter();
    }

    private void UpdateHealth()
    {
        if (m_player.maxHealth <= 3)
        {
            for (int i = 0; i < m_player.maxHealth; i++)
            {
                transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
            }
        }

        if (m_player.health <= 3)
        {
            for (int i = 0; i < m_player.health; i++)
            {
                transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    private void UpdateShotCounter()
    {
        transform.GetChild(1).GetComponentInChildren<Slider>().value = m_player.shotCooldown;
    }
}