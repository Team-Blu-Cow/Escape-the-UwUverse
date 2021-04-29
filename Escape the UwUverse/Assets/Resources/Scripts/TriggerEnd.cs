using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UwUVerse
{
    public class TriggerEnd : MonoBehaviour
    {
        private LevelEndUI levelEnd;

        private void Start()
        {
            levelEnd = FindObjectOfType<LevelEndUI>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            levelEnd.GameReset(false);
        }
    }
}