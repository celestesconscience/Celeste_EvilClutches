using UnityEngine;
using UnityEngine.InputSystem.Android;

// FlyingItem Class and Functions
public class Galaga_FlyingItem : MonoBehaviour
{
    // gameObject References
    private GameObject player;
    public GameObject explosion;

    // Audio
    public AudioClip penaltySound;

    // Spawner Reference
    private Galaga_FlyingItemSpawner spawner;

    // Movement Variables
    public float speed = 6f;

    // Stores which direction the item should move
    private bool movingRight;

    // Start is called once when the item is created
    void Start()
    {
        player = GameObject.Find("Player"); // <-- Find the player GameObject in the scene

        spawner = GameObject.Find("FlyingItemSpawner").GetComponent<Galaga_FlyingItemSpawner>();

        // Check which side the item spawned on
        if(transform.position.x < 0) // Spawned on left
        {
            movingRight = true;
        }
        else // Spawned on right
        {
            movingRight = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(movingRight) // If the power up is spawned on the left
        {
            // Move to the right
            transform.Translate(transform.right * speed * Time.deltaTime);
        }

        else // If the power up is spawned on the right
        {
            // Move to the left
            transform.Translate(-transform.right * speed * Time.deltaTime);
        }

        // Destroy when the item goes off screen
        if(transform.position.x > 12 || transform.position.x < -12)
        {
            spawner.ResetPowerUp();
            Destroy(gameObject);
        }
    }

    // Collision logic for Power Up
    public void OnTriggerEnter2D(Collider2D collision) // When another 2D colliders enter objects trigger
    {
        if(collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("GalagaPlayer")) // Check if it was the player or their bullet
        {
<<<<<<< HEAD
            if(collision.gameObject.CompareTag("PlayerBullet"))
            {
                // Destroys player bullet
                Destroy(collision.gameObject);

                // If item is a Heart, ignore the bullet
                if(isHeart)
                {
                    return; // Stops here so the Heart is not destroyed or collected
                }
=======
            // Destroys bullet
            if(collision.gameObject.CompareTag("PlayerBullet"))
            {
                Destroy(gameObject);
>>>>>>> parent of 374544d (PowerUp, Penalty Item, and Heart/Add Life Item + Polishes)
            }

            player.GetComponent<Galaga_PlayerMovement>().ActivateRapidFire(); // Gets player's script and activates RF method when hit by player

            // Explosion animation for power up
            GameObject temp = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(temp, 1.0f);

<<<<<<< HEAD
                // Update the score display
                player.GetComponent<Galaga_PlayerMovement>().scoreText.text = "Score: " + player.GetComponent<Galaga_PlayerMovement>().scoreVal;

                // Show -300 under the score
                player.GetComponent<Galaga_PlayerMovement>().ShowScoreChange(-300);

                // Play penalty sound
                AudioSource.PlayClipAtPoint(penaltySound, transform.position, 2f);
            }

            else
            {
                // If it is not a Heart or Penalty, it is the PowerUp
                player.GetComponent<Galaga_PlayerMovement>().ActivateRapidFire();
            }

            // Play the appropriate animation
            if(isHeart)
            {
                // Play healing animation when the player collects a Heart
                GameObject temp = Instantiate(healing, transform.position, Quaternion.identity);
                Destroy(temp, 1.0f);
            }

            else
            {
                // Play explosion animation when a PowerUp or Penalty Item is collected
                GameObject temp = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(temp, 1.0f);
            }

            // Reset the side-flying item spawner only for PowerUps/Penalty Items
            if(!isHeart)
            {
                spawner.ResetPowerUp();
            }

            // Destroy flying item
=======
>>>>>>> parent of 374544d (PowerUp, Penalty Item, and Heart/Add Life Item + Polishes)
            Destroy(gameObject);
        }
    }
}
