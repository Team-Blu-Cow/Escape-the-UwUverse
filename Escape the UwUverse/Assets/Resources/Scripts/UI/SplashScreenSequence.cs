using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class SplashScreenSequence : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    private int stars = 100;
    private GameObject Background;
    private GameObject Logo;
    private List<GameObject> StarList = new List<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        Background = GameObject.Find("Background");
        Logo = GameObject.Find("Logo");

        Vector3 v3BottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));    // scaling like this
        Vector3 v3TopRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));      // only works with
                                                                                       // sprites that are
        float xScale = v3TopRight.x - v3BottomLeft.x;                                  // 1 unit.
        float yScale = v3TopRight.y - v3BottomLeft.y;                                  //
                                                                                       //
        Background.transform.localScale = new Vector2(xScale, yScale);                 //
                                                                                       //
        if (xScale < yScale)                                                           //
            Logo.transform.localScale = new Vector2(xScale * 0.25f, xScale * 0.25f);   //
        else                                                                           //
            Logo.transform.localScale = new Vector2(yScale * 0.25f, yScale * 0.25f);   //

        for (int i = 0; i < stars; i++)
        {
            Vector3 Pos = Camera.main.ViewportToWorldPoint(new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)));
            Pos.Set(Pos.x, Pos.y, 0f);
            StarList.Add(GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Star"), Pos, Quaternion.Euler(Random.Range(0f, 360f), 0f, 0f)));

            float starScale = 0.005f;

            if (xScale < yScale)
                StarList[i].transform.localScale = new Vector2(xScale * starScale, xScale * starScale);
            else
                StarList[i].transform.localScale = new Vector2(yScale * starScale, yScale * starScale);
        }

        Logo.SetActive(false);
        StartCoroutine(Intro());
    }

    private IEnumerator Intro()
    {
        yield return new WaitForSeconds(3.5f); // intro wait, 1 second for levelloader

        foreach (GameObject star in StarList)
        {
            star.GetComponent<flicker>().TurnOff(); // wait before destroying each star (1 second to turn off by default)
        }

        yield return new WaitForSeconds(1f);

        foreach (GameObject star in StarList)
        {
            Destroy(star);
        }

        yield return new WaitForSeconds(1.5f); // dark to logo time

        float currentTime = 0;
        float timeTaken = 1f;
        Logo.SetActive(true);

        while (currentTime < timeTaken)
        {
            currentTime += Time.deltaTime;
            globalLight.intensity = Mathf.Lerp(0, 1, currentTime / timeTaken);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f); // logo to Main Menu time

        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().SwitchScene("MainMenu");

        yield break;
    }
}