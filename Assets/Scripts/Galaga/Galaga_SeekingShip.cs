using UnityEngine;

// SeekingShip Class and Functions
public class Galaga_SeekingShip : MonoBehaviour
{
    // GameObject References
    private GameObject player;
    public GameObject explosion; // <-- Reference to the explosion GameObject that will be instantiated when this ship is destroyed

    // Variables
    public float speed = 2f;
    public float maxDegrees = 10f;
    public int points = 150; // <-- Points awarded to the player for destroying this ship

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player"); // <-- Find the player GameObject in the scene
    }

    // Update is called once per frame
    void Update()
    {
        // Seek the player ship
        transform.up = -(player.transform.position - transform.position);
        transform.rotation = Quaternion.Euler(Mathf.Rad2Deg * transform.rotation.x,
                                            Mathf.Rad2Deg * transform.rotation.y,
                                            Mathf.Clamp(Mathf.Rad2Deg * transform.rotation.z, -maxDegrees, maxDegrees));

        // Move the ship downwards
        transform.Translate(-transform.up * speed * Time.deltaTime);

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
                player.GetComponent<Galaga_PlayerMovement>().scoreText.text = "SCORE: " + player.GetComponent<Galaga_PlayerMovement>().scoreVal; // <-- Update the score text to display the new score
                player.GetComponent<Galaga_PlayerMovement>().ShowScoreChange(points); // Shows the amount of points just given to player
                
                Destroy(collision.gameObject); // <-- Destory bullet
            }

            GameObject temp = Instantiate(explosion, transform.position, Quaternion.identity); // <-- Instantiate the explosion effect when this ship is destroyed
            Destroy(temp, 1.0f);
            Destroy(gameObject);
        }
    }
}
