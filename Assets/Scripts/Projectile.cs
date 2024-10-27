using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5.0f;
    public int damage = 10;
    private Vector2 targetPosition;

    public void SetTarget(Vector2 target)
    {
        targetPosition = target;
    }

    void Update()
    {
        // Move the projectile towards the target
        if ((Vector2)transform.position != targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject); // Destroy the projectile when it reaches the target
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Implement collision logic, e.g., damage the target
        Debug.Log(collision.gameObject.name+"got hit");
        Organism target = collision.GetComponent<Organism>();
        if (target != null)
        {
            target.TakeDamage(damage);
            Destroy(gameObject); // Destroy the projectile after hitting the target
        }
    }
}
