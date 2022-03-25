using UnityEngine;

public class Shooting : MonoBehaviour
{
    public new Rigidbody2D rigidbody;
    public float speed = 500.0f;
    public float maxLifetime = 10.0f;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        this.rigidbody.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D()
    {
        Destroy(this.gameObject);
    }

}
