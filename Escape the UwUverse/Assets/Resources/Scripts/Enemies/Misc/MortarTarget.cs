using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarTarget : MonoBehaviour
{
    public int m_stepCount = 0;
    public int m_maxStepCount = 0;
    public float m_scale = 0.5f;
    public float m_initialScale = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        GameController.StepController().PreStepEvent += OnPreStep;
        GameController.StepController().StepEvent += OnStep;

        m_scale = m_initialScale;
        transform.localScale = Vector3.one * m_scale;
    }
    
    public void OnPreStep()
    {
        m_stepCount++;
    }

    public void IncreaseScale()
    {
        float increment = (1f / (m_maxStepCount-1f))*m_stepCount;

        m_scale = Mathf.Lerp(m_initialScale, 1.1f, increment);

        //transform.localScale 
        LeanTween.scale(gameObject, Vector3.one * m_scale, 0.1f);
    }

    public void OnStep()
    {
        IncreaseScale();

        if (m_stepCount >= m_maxStepCount)
            Die();
    }

    public void Die()
    {
        // TODO: @jay create explosion effect or something

        GameController.StepController().PreStepEvent -= OnPreStep;
        GameController.StepController().StepEvent -= OnStep;

        Destroy(gameObject);
    }
}
