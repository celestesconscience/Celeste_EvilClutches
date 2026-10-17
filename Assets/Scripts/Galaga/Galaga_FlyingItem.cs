using UnityEngine;

// FlyingItem Class and Functions
public class Galaga_FlyingItem : MonoBehaviour
{
    // gameObject References
    private GameObject player;
    public GameObject explosion;
    public GameObject healing;

    // Audio
    public AudioClip penaltySound;

    // Spawner Reference
    private Galaga_FlyingItemSpawner spawner;

    // Movement Variables
    public float speed = 6f;

    // Stores which direction the item should move
    private bool movingRight;

    // Determines whether the flying item is a PowerUp or Penalty Item
    public bool isPenaltyItem = false;

    // Determines whether this flying item is a Heart
    public bool isHeart = false;

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
        // Flying Item Movement
        if(isHeart)
        {
            // Move Heart Downward
            transform.Translate(-transform.up * speed * Time.deltaTime);
        }

        else if(movingRight) // If the power up is spawned on the left
        {
            // Move Power/Penalty to the right
            transform.Translate(transform.right * speed * Time.deltaTime);
        }

        else
        {
            // Move Power/Penalty to the left
            transform.Translate(-transform.right * speed * Time.deltaTime);
        }

        // Destroy when the item goes off screen
        if(transform.position.x > 12 || transform.position.x < -12 || transform.position.y < -6)
        {
            // Reset the side-flying item spawner only for Power Ups/Penalty Items
            if(!isHeart)
            {
                spawner.ResetPowerUp();
            }
            
            Destroy(gameObject);  
        }
    }

    // Collision logic for  Flying Item
    public void OnTriggerEnter2D(Collider2D collision) // When another 2D colliders enter objects trigger
    {
        if(collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("GalagaPlayer")) // Check if it was the player or their bullet
        {
            if(collision.gameObject.CompareTag("PlayerBullet"))
            {
                // Destroys player bullet
                Destroy(collision.gameObject);

                // If item is a Heart, ignore the bullet
                if(isHeart)
                {
                    return; // Stops here so the Heart is not destroyed or collected
                }
            }

            // Check which type of flying item was collected
            if(isHeart)
            {
                // Heart gives the player one extra life
                player.GetComponent<Galaga_PlayerMovement>().AddLife();
            }

            else if(isPenaltyItem)
            {
                // Penalty Item subtracts 300 points
                player.GetComponent<Galaga_PlayerMovement>().scoreVal -= 300;

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
            Destroy(gameObject);
        }
    }
}
