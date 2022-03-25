using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Asteroid : MonoBehaviour
{
    public new Rigidbody2D rigidbody;
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    public float size = 1.0f;
    public float minSize = 0.35f;
    public float maxSize = 1.65f;
    public float movementSpeed = 50.0f;
    public float maxLifetime = 30.0f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(2, sprites.Length)];
        var transform1 = transform;
        transform1.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);

        transform1.localScale = Vector3.one * size;
        rigidbody.mass = size;

        Destroy(gameObject, maxLifetime);
    }

    public void SetTrajectory(Vector2 direction)
    {
        rigidbody.AddForce(direction * movementSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Bullet")) return;
        if ((size * 0.5f) >= minSize)
        {
            CreateSplit();
            CreateSplit();
        }

        FindObjectOfType<GameManager>().AsteroidDestroyed(this);
        Destroy(gameObject);
    }

    private void CreateSplit()
    {
        var transform1 = transform;
        Vector2 position = transform1.position;
        position += Random.insideUnitCircle * 0.5f;

        var half = Instantiate(this, position, transform1.rotation);
        half.size = size * 0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized);
    }

}
