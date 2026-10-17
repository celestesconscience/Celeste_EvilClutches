using UnityEngine;

// FlyingItem Class and Functions
public class Galaga_FlyingItem : MonoBehaviour
{
    // gameObject References
    private GameObject player;
    public GameObject explosion;

    // Spawner Reference
    private Galaga_FlyingItemSpawner spawner;

    // Movement Variables
    public float speed = 6f;

    // Stores which direction the item should move
    private bool movingRight;

    // Determines whether the flying item is a PowerUp or Penalty Item
    public bool isPenaltyItem = false;

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
            // Move Power/Penalty to the right
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else
        {
            // Move Power/Penalty to the left
            transform.Translate(-transform.right * speed * Time.deltaTime);
        }

        // Destroy when the item goes off screen
        if(transform.position.x > 12 || transform.position.x < -12)
        {
            spawner.ResetPowerUp();
            Destroy(gameObject);  
        }
    }

    // Collision logic for  Flying Item
    public void OnTriggerEnter2D(Collider2D collision) // When another 2D colliders enter objects trigger
    {
        if(collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("GalagaPlayer")) // Check if it was the player or their bullet
        {
            // Destroy the player bullet if the item was hit by a bullet
            if(collision.gameObject.CompareTag("PlayerBullet"))
            {
                // Destroys player bullet
                Destroy(collision.gameObject);
            }

            if(isPenaltyItem)
            {
                // Penalty Item subtracts 300 points
                player.GetComponent<Galaga_PlayerMovement>().scoreVal -= 300;

                // Update the score display
                player.GetComponent<Galaga_PlayerMovement>().scoreText.text = "SCORE: " + player.GetComponent<Galaga_PlayerMovement>().scoreVal;

                // Show -300 under the score
                player.GetComponent<Galaga_PlayerMovement>().ShowScoreChange(-300);
            }
            else
            {
                // If it is not a Penalty, it is the PowerUp
                player.GetComponent<Galaga_PlayerMovement>().ActivateRapidFire();
            }

            
            // Play explosion animation when a PowerUp or Penalty Item is collected
            GameObject temp = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(temp, 1.0f);

            // Reset the side-flying item spawner only for PowerUps/Penalty Items
            spawner.ResetPowerUp();

            // Destroy flying item
            Destroy(gameObject);
        }
    }
}
