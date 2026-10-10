using UnityEngine;

// BulletShip Class and Functions
public class Galaga_BulletShip : MonoBehaviour
{
    // GameObject References
    public GameObject explosion; // <-- Reference to the explosion GameObject that will be instantiated when this ship is destroyed
    private GameObject player; // <-- Reference to the player GameObject for the points

    // Variables
    public float speed = 3; // <-- Speed of the bullet ship's movement
    public int points = 100; // <-- Points awarded to the player for destroying this ship

    // Bullet Stuff
    public GameObject enemyBullet; // <-- Reference to the enemy bullet GameObject that this ship will shoot
    public float enemybulletTimer = 0, enemybulletWait = 2; // <-- Timer for shooting bullets and the wait time between shots

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player"); // <-- Find the player GameObject in the scene
    }

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
       
       // Destroy the ship if it moves off the bottom of the screen
        if(transform.position.y < -6)
        {
            Destroy(gameObject); // <-- Destroy the ship GameObject to free up memory and resources
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("GalagaPlayer"))
        {
            if(collision.gameObject.CompareTag("PlayerBullet")) // <-- Check if the collision is with a player bullet
            {
                player.GetComponent<Galaga_PlayerMovement>().scoreVal += points; // <-- Add points to the player's score when this ship is destroyed
                player.GetComponent<Galaga_PlayerMovement>().scoreText.text = "Score: " + player.GetComponent<Galaga_PlayerMovement>().scoreVal; // <-- Update the score text to display the new score
                
                Destroy(collision.gameObject); // <-- Destory bullet
            }

            GameObject temp = Instantiate(explosion, transform.position, Quaternion.identity); // <-- Instantiate the explosion effect when this ship is destroyed
            Destroy(temp, 1.0f);
            Destroy(gameObject);
        }
    }
}