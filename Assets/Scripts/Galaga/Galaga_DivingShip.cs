using UnityEngine;

public class Galaga_DivingShip : MonoBehaviour
{
    // Variables
    public float speed = 10.0f;
    public int points = 200; // <-- Points awarded to the player for destroying this ship

    // GameObject References
    public GameObject explosion; // <-- Reference to the explosion GameObject that will be instantiated when this ship is destroyed
    private GameObject player; // <-- Reference to the player GameObject for the points

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player"); // <-- Find the player GameObject in the scene
    }

    // Update is called once per frame
    void Update()
    {
        // Move the ship downwards
        transform.Translate(-transform.up * speed * Time.deltaTime); // <-- Move the ship to the bottom at a constant speed, multiplied by Time.deltaTime to make it frame rate independent
       
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
