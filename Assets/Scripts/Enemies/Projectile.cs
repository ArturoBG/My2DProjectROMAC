using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1f;
    public float damage = 2f;
    public bool right = false;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }

    public void SetDamage(float value)
    {
        damage = value;
    }

    public void SetDirection(bool isRight)
    {
        right = isRight;
        spriteRenderer.flipX = isRight;
    }

    private void Update()
    {
        if (right)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Destroyer")
        {
            Destroy(this.gameObject);
        }
    }
}