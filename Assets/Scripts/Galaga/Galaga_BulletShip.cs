using UnityEngine;

public class Galaga_BulletShip : MonoBehaviour
{
    // Variables
    public float speed = 3; // <-- Speed of the bullet ship's movement

    // Bullet Stuff
    public GameObject enemyBullet; // <-- Reference to the enemy bullet GameObject that this ship will shoot
    public float enemybulletTimer = 0, enemybulletWait = 2; // <-- Timer for shooting bullets and the wait time between shots

    // Update is called once per frame
    void Update()
    {
        // Enemy bullet shooting logic
        enemybulletTimer += Time.deltaTime; // <-- Increment the enemy bullet timer by the time elapsed since the last frame
        if(enemybulletTimer > enemybulletWait) // <-- Check if the enemy bullet timer has exceeded the wait time before shooting
        {
            enemybulletTimer = 0; // <-- Reset the enemy bullet timer before shooting to ensure proper timing
            Instantiate(enemyBullet, transform.position, transform.rotation); // <-- Shoot a bullet when the timer exceeds the wait time
        }

        // Move the ship downwards
        transform.Translate(-transform.up* speed * Time.deltaTime); // <-- Move the ship to the bottom at a constant speed, multiplied by Time.deltaTime to make it frame rate independent
        if(transform.position.y < -6)
        {
            Destroy(gameObject); // <-- Destroy the ship GameObject to free up memory and resources
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}