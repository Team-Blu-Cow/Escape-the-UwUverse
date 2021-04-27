using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static event Action SceneSwitch;

    private Animator transition;
    [SerializeField] [Range(1, 100)] private float transitionTime = 1f;

    private void Start()
    {
        GameController.Instance.isPaused = false;
        GameController.Instance.LevelLoader = this;
        transition = GetComponentInChildren<Animator>();
    }

    public void SwitchScene(string in_Scene)
    {
        StartCoroutine(LoadLevel(in_Scene));
    }

    private IEnumerator LoadLevel(string in_Scene)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneSwitch?.Invoke();
        SceneManager.LoadScene(in_Scene);
    }

    public void QuitAppliction()
    {
        Application.Quit();
        Debug.Log("Quit Called");
    }
}