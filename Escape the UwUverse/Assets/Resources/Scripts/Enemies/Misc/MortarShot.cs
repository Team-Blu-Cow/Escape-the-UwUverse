using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarShot : MonoBehaviour
{
    public float disapearDistance = 3;
    private float currentAlpha = 1;

    private float speedModifier = 0;
    private float alphaModifier = 0;

    private float initY;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Color colour;

    // Start is called before the first frame update
    void Start()
    {
        initY = transform.position.y;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        colour = sr.color;
        sr.sortingOrder = 10;
        rb.velocity = Vector2.up*20;
    }

    // Update is called once per frame
    void Update()
    {
        speedModifier += Time.deltaTime;
        rb.velocity -= Vector2.up * (speedModifier);

        sr.color = new Color(colour.r, colour.g, colour.b, currentAlpha);

        if(transform.position.y > initY + disapearDistance)
        {
            alphaModifier += Time.deltaTime;
            currentAlpha -= (alphaModifier*0.1f);
        }

        if (currentAlpha < 0)
            Destroy(gameObject);
    }
}
