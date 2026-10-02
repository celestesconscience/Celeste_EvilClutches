using UnityEngine;

public class Galaga_EnemyBullet : MonoBehaviour
{
    // Variables
    public float speed = 5; // <-- Speed of the enemy bullet's movement

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-transform.up* speed * Time.deltaTime); // <-- Move the projectile to the bottom at a constant speed, multiplied by Time.deltaTime to make it frame rate independent
        if(transform.position.y < -6) // <-- Check if the projectile has moved off the bottom of the screen
        {
            Destroy(gameObject); // <-- Destroy the projectile GameObject to free up memory and resources
        }
    }
}
