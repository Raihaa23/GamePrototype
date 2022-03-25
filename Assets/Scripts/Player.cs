using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class Player : MonoBehaviour
{
    public new Rigidbody2D rigidbody;
    public Shooting bulletPrefab1;
    public Shooting bulletPrefab2;
    public Shooting bulletPrefab3;

    private bool _isBullet1Selected, _isBullet2Selected, _isBullet3Selected;

    public float thrustSpeed = 1.0f;
    public bool thrusting;
    public Vector2 screenSize;
    public float playerWidth;
    public float playerHeight;
    public float turnDirection;
    public float rotationSpeed = 0.1f;

    private void Start()
    {
        var localScale = transform.localScale;
        playerWidth = localScale.x / -2;
        playerHeight = localScale.y / -2;
        if (Camera.main != null)
        {
            var main = Camera.main;
            float orthographicSize;
            screenSize = new Vector2(main.aspect * (orthographicSize = main.orthographicSize) + playerWidth,
                orthographicSize + playerHeight);
        }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Border();

        thrusting = Input.GetKey(KeyCode.W);

        if (Input.GetKey(KeyCode.A))
        {
            turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            turnDirection = -1.0f;
        }
        else
        {
            turnDirection = 0.0f;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (thrusting)
        {
            rigidbody.AddForce(transform.up * thrustSpeed);
        }

        if (turnDirection != 0.0f)
        {
            rigidbody.AddTorque(rotationSpeed * turnDirection);
        }
    }

    private void Shoot()
    {
        if (_isBullet1Selected)
        {
            var transform1 = transform;
            Shooting bullet = Instantiate(bulletPrefab1, transform1.position, transform1.rotation);
            bullet.Project(transform.up);
        }
        else if (_isBullet2Selected)
        {
            var transform1 = transform;
            Shooting bullet = Instantiate(bulletPrefab2, transform1.position, transform1.rotation);
            bullet.Project(transform.up);
        }
        else if (_isBullet3Selected)
        {
            var transform1 = transform;
            Shooting bullet = Instantiate(bulletPrefab3, transform1.position, transform1.rotation);
            bullet.Project(transform.up);
        }
    }

    public void SelectBullet1()
    {
        _isBullet1Selected = true;
    }

    public void SelectBullet2()
    {
        _isBullet2Selected = true;
    }

    public void SelectBullet3()
    {
        _isBullet3Selected = true;
    }

/*
    private void TurnOnCollisions()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = 0.0f;
            Destroy(gameObject);
        }
    }

    private void Border() //Keeping the Player inside the Border
    {
        if (transform.position.x < -screenSize.x)
        {
            var transform1 = transform;
            transform1.position = new Vector2(-screenSize.x, transform1.position.y);
        }
        else if (transform.position.x > screenSize.x)
        {
            var transform1 = transform;
            transform1.position = new Vector2(screenSize.x, transform1.position.y);
        }

        if (transform.position.y < -screenSize.y)
        {
            var transform1 = transform;
            transform1.position = new Vector2(transform1.position.x, -screenSize.y);
        }
        else if (transform.position.y > screenSize.y)
        {
            var transform1 = transform;
            transform1.position = new Vector2(transform1.position.x, screenSize.y);
        }
    }

}
