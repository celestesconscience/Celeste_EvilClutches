using UnityEngine; //Using Unity's Programming tools
using UnityEngine.UI; //Using Unity's UI tools
using TMPro;

// PlayerMovement Class and Functions
public class Galaga_PlayerMovement : MonoBehaviour
{
    // Audio
    public AudioClip lifeLostSound; 

    // Variables
    public float speed = 5; // <-- Speed of the player's movement
    public int scoreVal = 0; // <-- Player's score value

    // Score Change Variables (so text doesn't stay up)
    private float scoreChangeTimer = 0f;
    private float scoreChangeDuration = 1.0f;

    // Rapid Fire Variables
    private bool rapidFire = false; // Stores whether Rapid Fire is currently active.
                                    // FALSE = normal shooting | TRUE = hold Space for rapid fire 
    public float rapidFireDuration = 10.0f; // How long Rapid Fire lasts after the player collects the Power Up
    public float rapidFireTimer = 0f; // Countdown for the Rapid Fire ability. Starts at duration (10) and counts down to 0
    public float rapidFireCooldown = 0.2f; // Time between each bullet while Rapid Fire is active
    private float rapidFireCooldownTimer = 0f; // Countdown that controls when the next Rapid Fire bullet can be fired

    // Rapid Fire Activated Text
    private float powerUpTextDuration = 3.0f; // How long the text stays visible
    private float powerUpTextTimer = 0f; // Countdown for the text

    // Rapid Fire Deactivated Text
    private float rapidFireDeactivatedTextDuration = 2.0f; // How long the text stays visible
    private float rapidFireDeactivatedTextTimer = 0f; // Countdown for the text

    // Player Bullet Projectile Reference
    public GameObject playerBullet;

    // UI Elements
    public TextMeshProUGUI scoreText; // <-- Reference to the TextMeshProUGUI component that displays the player's score
    public TextMeshProUGUI rapidFireText;
    public TextMeshProUGUI rapidFireDeactivatedText;
    public TextMeshProUGUI rapidFireInstructions;
    public TextMeshProUGUI scoreChange;

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
        scoreChange.text = "";

        spawner = GameObject.Find("FlyingItemSpawner").GetComponent<Galaga_FlyingItemSpawner>();

        // Starts with Power Up Text hidden
        rapidFireText.gameObject.SetActive(false);
        rapidFireInstructions.gameObject.SetActive(false);
        rapidFireDeactivatedText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Shooting
        if(rapidFire) // If rapid fire is active or true
        {
            // Hold Space for Rapid Fire
            if(Input.GetKey(KeyCode.Space))
            {
                rapidFireCooldownTimer -= Time.deltaTime; // Timer = Timer - Time passed in game

                if(rapidFireCooldownTimer <= 0) // If timer has reached 0, allowed to shoot
                {
                    Instantiate(playerBullet, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    rapidFireCooldownTimer = rapidFireCooldown; // After shooting, reset the cooldown
                }
            }
        }
        else // If rapid fire is not active or false
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
            // Rapid Fire Timer
            rapidFireTimer -= Time.deltaTime; // Start subtracting time passed

            // Rapid Fire Text Timer
            powerUpTextTimer -= Time.deltaTime;

            if(powerUpTextTimer <= 0) // If power up text timer reaches 0
            {
                rapidFireText.gameObject.SetActive(false); // Hide text
            }

            // End Rapid Fire when the ability duration expires
            if(rapidFireTimer <= 0) // When timer reaches zero
            {
                rapidFire = false; // Turn rapid fire off
                rapidFireTimer = 0; // Timer set at 0 so can't go to negatives

                spawner.ResetPowerUp(); // Starts timer/respawn logic 

                // Hide Power Up Text
                rapidFireText.gameObject.SetActive(false);
                rapidFireInstructions.gameObject.SetActive(false);

                // Show Rapid Fire Deactivated Text
                rapidFireDeactivatedText.gameObject.SetActive(true);
                rapidFireDeactivatedTextTimer = rapidFireDeactivatedTextDuration;

            }
        }

        // Rapid Fire Deactivated Text Timer
        if(rapidFireDeactivatedTextTimer > 0)
        {
            rapidFireDeactivatedTextTimer -= Time.deltaTime;
        }

        if(rapidFireDeactivatedTextTimer <= 0)
        {
            rapidFireDeactivatedText.gameObject.SetActive(false);
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

        // Score Change Timer
        if(scoreChangeTimer > 0)
        {
            scoreChangeTimer -= Time.deltaTime;
        }

        if(scoreChangeTimer <= 0)
        {
            scoreChange.text = "";
        }
    }

    // Show Score Change Method
    public void ShowScoreChange(int points) // Expects int number when called
    {
        if(points >= 0) // Only checks/shows non-negative numbers
        {
            scoreChange.text = "+" + points;
        }

        else // Handles negative numbers
        {
            scoreChange.text = "" + points;
        }

        scoreChangeTimer = scoreChangeDuration;
    }


    // Activates Rapid Fire
    public void ActivateRapidFire() // actually turns power-up on
    {
        rapidFire = true; // activate power up
        rapidFireTimer = rapidFireDuration; // the timer is set at the amount of time you get the power up for to start countdown
        rapidFireCooldownTimer = 0; // doesn't make player wait before their first rapid fire bullet
        powerUpTextTimer = powerUpTextDuration;

        // Show Power Up Text
        rapidFireText.gameObject.SetActive(true);
        rapidFireInstructions.gameObject.SetActive(true);

        print("Rapid Fire Activated!"); // For debugging
    }
    
    // Lives, Lives Display, Game Over
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            livesremaining -= 1;

            AudioSource.PlayClipAtPoint(lifeLostSound, transform.position, 2f);

            if(livesremaining <= 0)
            {
                livesremaining = 0;
                gameManager.GameOverMethod();
            }

            lives[livesremaining].enabled = false;
        }
    }
}