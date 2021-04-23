using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Player player;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateHealth();
        UpdateShotCounter();
    }

    private void UpdateHealth()
    {
        for (int i = 0; i < player.maxHealth; i++)
        {
            transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < player.health; i++)
        {
            transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
        }
    }

    private void UpdateShotCounter()
    {
        transform.GetChild(1).GetComponentInChildren<Slider>().value = player.shotCooldown;
    }
}