using UnityEngine; //Using Unity's Programming tools
using UnityEngine.UI; //Using Unity's UI tools
using TMPro; //Using TextMeshPro tools

// PlayerMovement Class and Functions
public class Galaga_PlayerMovement : MonoBehaviour
{

    // Variables
    public float speed = 5; // <-- Speed of the player's movement
    public int scoreVal = 0; // <-- Player's score value

    // UI Elements
    public TextMeshProUGUI scoreText; // <-- Reference to the TextMeshProUGUI component that displays the player's score

    // Player Bullet Projectile Reference
    public GameObject playerBullet;

    // Player Lives UI Elements
    public Image[] lives;
    public int livesremaining = 3;

    // Game Manager
    public Galaga_GameManager gameManager;

    void Start()
    {
        scoreText.text = "Score: " + scoreVal; // <-- Initialize the score text to display the player's score at the start of the game
    }

    // Update is called once per frame
    void Update()
    {
        // Shoot a bullet when the Space key is pressed
        if(Input.GetKeyUp(KeyCode.Space))
        {
            Instantiate(playerBullet, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            // Vector3(0, 0.5f, 0) offsets the bullet slightly above the player's position
            // Quaternion.identity ensures the bullet is not rotated relative to the player
        }

        // Traveling up
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(transform.up * speed * Time.deltaTime);
        }

        // Traveling down
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(-transform.up * speed * Time.deltaTime);
        }

        // Traveling left
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-transform.right * speed * Time.deltaTime);
        }

        // Traveling right
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }

        // Constrict Movement
        transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -7f, 7f), // <-- Stop left and right
                Mathf.Clamp(transform.position.y, -3.5f, 3.5f), // <-- Stop up and down
                transform.position.z // <-- Keep the z position the same
                );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            livesremaining -= 1;

            if(livesremaining <= 0)
            {
                livesremaining = 0;
                gameManager.GameOverMethod();
            }

            lives[livesremaining].enabled = false;
        }
    }
}