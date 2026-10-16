using UnityEngine;
using UnityEngine.InputSystem.Android;

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

    public void OnTriggerEnter2D(Collider2D collision) // When another 2d colliders enter objects trigger
    {
        if(collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("GalagaPlayer")) // Check if it was the player or their bullet
        {
            if(collision.gameObject.CompareTag("PlayerBullet"))
            {
                Destroy(gameObject);
            }

            player.GetComponent<Galaga_PlayerMovement>().ActivateRapidFire(); // Gets player's script and activates RF method when hit by player

            GameObject temp = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(temp, 1.0f);

            Destroy(gameObject);
        }
    }
}
