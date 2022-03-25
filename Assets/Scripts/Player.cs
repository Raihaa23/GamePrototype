using UnityEngine;
using TMPro;


public class Player : MonoBehaviour
{
    public new Rigidbody2D rigidbody;
    public Shooting bulletPrefab1;
    public Shooting bulletPrefab2;
    public Shooting bulletPrefab3;
    
    public float thrustSpeed = 1.0f;
    public bool thrusting;
    public Vector2 screenSize;
    public float playerWidth;
    public float playerHeight;
    public float turnDirection;
    public float rotationSpeed = 0.1f;
    
    public TextMeshProUGUI firstAchievement;
    private Shooting _selectedBullet;
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
        
        if (Input.GetKey(KeyCode.W))
        {
            thrusting = true;
            AchievementsManager.Instance.IncreaseButtonPress();
        }
        

        if (Input.GetKey(KeyCode.A))
        {
            turnDirection = 1.0f;
            AchievementsManager.Instance.IncreaseButtonPress();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            turnDirection = -1.0f;
            AchievementsManager.Instance.IncreaseButtonPress();
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
        var transform1 = transform;
        Shooting bullet = Instantiate(_selectedBullet, transform1.position, transform1.rotation);
        bullet.Project(transform.up);
    }

    public void SelectBullet1()
    {
        _selectedBullet = bulletPrefab1;
    }

    public void SelectBullet2()
    {
        _selectedBullet = bulletPrefab2;
    }

    public void SelectBullet3()
    {
        _selectedBullet = bulletPrefab3;
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
