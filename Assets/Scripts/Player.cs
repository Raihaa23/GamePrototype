using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public new Rigidbody2D rigidbody;
    public Shooting bulletPrefab1;
    public Shooting bulletPrefab2;
    public Shooting bulletPrefab3;

    private bool isBullet1selected, isBullet2selected, isBullet3selected;

    public float thrustSpeed = 1.0f;
    public bool thrusting;
    public Vector2 screensize;
    public float PlayerWidth, PlayerHeight;
    public float turnDirection = 0.0f;
    public float rotationSpeed = 0.1f;

    private void Start()
    {
        PlayerWidth = transform.localScale.x / -2;
        PlayerHeight = transform.localScale.y / -2;
        screensize = new Vector2(Camera.main.aspect * Camera.main.orthographicSize + PlayerWidth, Camera.main.orthographicSize + PlayerHeight);
        
    }

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Border();

        this.thrusting = Input.GetKey(KeyCode.W);

        if (Input.GetKey(KeyCode.A))
        {
            this.turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.turnDirection = -1.0f;
        }
        else
        {
            this.turnDirection = 0.0f;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (this.thrusting)
        {
            this.rigidbody.AddForce(this.transform.up * this.thrustSpeed);
        }

        if (this.turnDirection != 0.0f)
        {
            this.rigidbody.AddTorque(this.rotationSpeed * this.turnDirection);
        }
    }

    private void Shoot()
    {
        if (isBullet1selected)
        {
            Shooting bullet = Instantiate(this.bulletPrefab1, this.transform.position, this.transform.rotation);
            bullet.Project(this.transform.up);
        }
        else if (isBullet2selected)
        {
            Shooting bullet = Instantiate(this.bulletPrefab2, this.transform.position, this.transform.rotation);
            bullet.Project(this.transform.up);
        }
        else if (isBullet3selected)
        {
            Shooting bullet = Instantiate(this.bulletPrefab3, this.transform.position, this.transform.rotation);
            bullet.Project(this.transform.up);
        }
    }

    public void SelectBullet1()
    {
        isBullet1selected = true;
        Debug.Log("Testing");
    }

    public void SelectBullet2()
    {
        isBullet2selected = true;
    }

    public void SelectBullet3()
    {
        isBullet3selected = true;
    }

    private void TurnOnCollisions()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            this.rigidbody.velocity = Vector3.zero;
            this.rigidbody.angularVelocity = 0.0f;
            Destroy(gameObject);
        }
    }

    private void Border() //Keeping the Player inside the Border
    {
        if (transform.position.x < -screensize.x)
        {
            transform.position = new Vector2(-screensize.x, transform.position.y);
        }
        else if (transform.position.x > screensize.x)
        {
            transform.position = new Vector2(screensize.x, transform.position.y);
        }

        if (transform.position.y < -screensize.y)
        {
            transform.position = new Vector2(transform.position.x, -screensize.y);
        }
        else if (transform.position.y > screensize.y)
        {
            transform.position = new Vector2(transform.position.x, screensize.y);
        }
    }

}
