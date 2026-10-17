using UnityEngine;

// Heart Item Class and Functions
public class Galaga_HeartItem : MonoBehaviour
{
    // Player Reference
    private GameObject player;

    // Heart References
    public GameObject healing;

    // Audio
    public AudioClip heartCollectedSound;

    // Movement
    public float speed = 6f;

    // How close the player needs to be to collect the Heart
    public float pickupDistance = 1.5f;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        // Move Heart straight downward
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Check if Heart is close enough to Player
        if(player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if(distance <= pickupDistance)
            {
                Galaga_PlayerMovement playerMovement = player.GetComponent<Galaga_PlayerMovement>();

                // Only collect if the player has room for another life
                if(playerMovement.livesremaining < playerMovement.lives.Length)
                {
                    playerMovement.AddLife();

                    AudioSource.PlayClipAtPoint(heartCollectedSound, transform.position, 2f);

                    GameObject temp = Instantiate(healing, transform.position, Quaternion.identity);
                    Destroy(temp, 1.0f);

                    Destroy(gameObject);
                }
            }
        }

        // Destroy Heart when it leaves the screen
        if(transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Only collect when the PLAYER touches the Heart
        if(collision.gameObject.CompareTag("GalagaPlayer"))
        {
            Galaga_PlayerMovement playerMovement =
                player.GetComponent<Galaga_PlayerMovement>();

            // Only collect if player has room for another life
            if(playerMovement.livesremaining < playerMovement.lives.Length)
            {
                // Add one life
                playerMovement.AddLife();

                // Play Heart collected sound
                AudioSource.PlayClipAtPoint(
                    heartCollectedSound,
                    transform.position,
                    2f
                );

                // Play healing animation
                GameObject temp = Instantiate(
                    healing,
                    transform.position,
                    Quaternion.identity
                );

                Destroy(temp, 1.0f);

                // Destroy Heart
                Destroy(gameObject);
            }
        }
    }
}