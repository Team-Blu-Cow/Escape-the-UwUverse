using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class flicker : MonoBehaviour
{
    [SerializeField] private Light2D light;
    [SerializeField] private float offTime = 1f;
    private float flickerSpeed = 0.02f;
    private float maxLuminosity;
    private bool off;

    private void Start()
    {
        maxLuminosity = Random.Range(0.1f, 1f);
        light.pointLightOuterRadius = maxLuminosity / 2f;
    }

    // Update is called once per frame
    private void Update()
    {
        Flicker();
    }

    private void Flicker()
    {
        if (!off)
        {
            if (flickerSpeed != 0)
            {
                bool validFlicker = false;
                float offset = 0f;
                float offsetRange = maxLuminosity / 5f;

                while (!validFlicker)
                {
                    offset = Random.Range(-offsetRange, offsetRange);
                    if ((offset + light.pointLightOuterRadius) < maxLuminosity &&
                        (offset + light.pointLightOuterRadius) > 0)
                    {
                        validFlicker = true;
                    }
                }
                light.pointLightOuterRadius = Mathf.Lerp(light.pointLightOuterRadius, light.pointLightOuterRadius + offset, flickerSpeed); // makes it smoother
            }
            else
            {
                light.pointLightOuterRadius = maxLuminosity;
            }
        }
    }

    public void TurnOff()
    {
        off = true;
        StartCoroutine(cTurnOff());
    }

    private IEnumerator cTurnOff()
    {
        float currentTime = 0f;
        float startPoint = light.pointLightOuterRadius;

        while (currentTime < offTime)
        {
            currentTime += Time.deltaTime;
            light.pointLightOuterRadius = Mathf.Lerp(startPoint, 0f, currentTime / offTime);
            yield return null;
        }

        yield break;
    }
}