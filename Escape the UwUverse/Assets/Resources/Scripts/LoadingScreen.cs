using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(WaitThenLoad());
        GameController.StepController().Reset();
        GameController.Instance.SwitchLevel(GameController.Instance.currentLevel);
    }

    IEnumerator WaitThenLoad()
    {
        yield return new WaitForSecondsRealtime(1f);
        
    }
}
