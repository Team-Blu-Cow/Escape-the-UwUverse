using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarShotLand : MonoBehaviour
{
    public float disapearDistance = 3;
    private float currentYScale;

    private float speedModifier = 0;
    private float alphaModifier = 0;

    private float initY;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Color colour;

    private GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        initY = transform.position.y;
        transform.position += Vector3.up * disapearDistance;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        colour = sr.color;
        sr.sortingOrder = 10;
        rb.velocity = Vector2.down * 0.25f;
        currentYScale = transform.localScale.y;
        explosionPrefab = Resources.Load<GameObject>("Prefabs/Enemies/Mortar/MortarExplosion");
    }

    // Update is called once per frame
    void Update()
    {
        speedModifier += Time.deltaTime;
        rb.velocity += Vector2.down * (speedModifier*2);

        currentYScale += speedModifier*0.01f;

        transform.localScale = new Vector3(transform.localScale.x, currentYScale, transform.localScale.z);

        if (transform.position.y < initY)
        {
            GameObject.Instantiate(explosionPrefab, transform.position + (Vector3.up*0.5f), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
