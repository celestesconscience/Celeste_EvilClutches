using UnityEngine; //Using Unity's Programming tools
using UnityEngine.UI; //Using Unity's UI tools
using TMPro;

// PlayerMovement Class and Functions
public class Galaga_PlayerMovement : MonoBehaviour
{

    // Variables
    public float speed = 5; // <-- Speed of the player's movement
    public int scoreVal = 0; // <-- Player's score value

    // Rapid Fire
    private bool rapidFire = false; // bool to store whether player is in rapid fire state
    public float rapidFireDuration = 5f; // how long rapid fire will last
    public float rapidFireTimer = 0f; // the timer to watch rapid fire/countdown
    public float rapidFireCooldown = 0.2f; // how frequently bullets can be fired during RF
    private float rapidFireCooldownTimer = 0f; // how long until able to fire the next bullet

    // Player Bullet Projectile Reference
    public GameObject playerBullet;

    // UI Elements
    public TextMeshProUGUI scoreText; // <-- Reference to the TextMeshProUGUI component that displays the player's score

    // Player Lives UI Elements
    public Image[] lives;
    public int livesremaining = 3;

    // Game Manager
    public Galaga_GameManager gameManager;

    // Flying Item Spawner
    private Galaga_FlyingItemSpawner spawner;

    void Start()
    {
        scoreText.text = "Score: " + scoreVal; // <-- Initialize the score text to display the player's score at the start of the game

        spawner = GameObject.Find("FlyingItemSpawner").GetComponent<Galaga_FlyingItemSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        // Shooting
        if(rapidFire) // if rapid fire is active or true
        {
            // Hold Space for rapid fire
            if(Input.GetKey(KeyCode.Space))
            {
                rapidFireCooldownTimer -= Time.deltaTime; // timer = timer - time passed in game

                if(rapidFireCooldownTimer <= 0) // if timer has reached 0, allowed to shoots
                {
                    Instantiate(playerBullet, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    rapidFireCooldownTimer = rapidFireCooldown; // after shooting, reset the cooldown
                }
            }
        }
        else // if rapid fire is not active or false
        {
            // Normal shooting / Tap Space 
            if(Input.GetKeyUp(KeyCode.Space))
            {
                Instantiate(playerBullet, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                // Vector3(0, 0.5f, 0) offsets the bullet slightly above the player's position
                // Quaternion.identity ensures the bullet is not rotated relative to the player
            }
        }

        // Rapid Fire Timer
        if(rapidFire) // if active (starts timer at 5)
        {
            rapidFireTimer -= Time.deltaTime; // start subtracting time passed

            if(rapidFireTimer <= 0) // when timer reaches zero
            {
                rapidFire = false; // turn rapid fire off
                rapidFireTimer = 0; // timer set at 0 so can't go to negatives

                spawner.ResetPowerUp();
            }
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

    // Activates Rapid Fire
    public void ActivateRapidFire() // actually turns power-up on
    {
        rapidFire = true; // activate power up
        rapidFireTimer = rapidFireDuration; // the timer is set at the amount of time you get the power up for to start countdown
        rapidFireCooldownTimer = 0; // doesn't make player wait before their first rapid fire bullet

        print("Rapid Fire Activated!"); // For debugging
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