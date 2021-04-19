using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenSequence : MonoBehaviour
{
    private GameObject Background;
    private GameObject Logo;

    private SpriteRenderer BackgroundSpriteRenderer;

    // Start is called before the first frame update
    private void Start()
    {
        Background = GameObject.Find("Background");
        Logo = GameObject.Find("Logo");

        Vector3 v3ViewPort = new Vector3();
        Vector3 v3BottomLeft;
        Vector3 v3TopRight;

        v3ViewPort.Set(0, 0, 10);
        v3BottomLeft = Camera.main.ViewportToWorldPoint(v3ViewPort);
        v3ViewPort.Set(1, 1, 10);
        v3TopRight = Camera.main.ViewportToWorldPoint(v3ViewPort);

        float xScale = v3TopRight.x - v3BottomLeft.x;
        float yScale = v3TopRight.y - v3BottomLeft.y;

        Background.transform.localScale = new Vector2(xScale, yScale);

        Logo.transform.localScale = new Vector2(yScale * 0.25f, yScale * 0.25f);
    }

    private IEnumerator Intro()
    {
        yield break;
    }
}