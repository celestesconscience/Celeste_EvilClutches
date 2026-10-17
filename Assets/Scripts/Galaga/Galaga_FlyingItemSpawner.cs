using UnityEngine;

// FlyingItemSpawner Class and Functions
public class Galaga_FlyingItemSpawner : MonoBehaviour
{
    // Spawner References
    public GameObject powerUp;
    public GameObject powerUpWave;
    public Transform leftSpawner;
    public Transform rightSpawner;

    // Spawning Variables
    public float itemTimer = 0f; // What timer starts at
    public float itemWait = 15f; // What time has to get to before spawning in/How long it has to wait
    public bool powerUpActive = false; // Checks if power up is flying around so another doesn't spawn too fast


    // Update is called once per frame
    void Update()
    {
        // Spawning timer
        itemTimer += Time.deltaTime;

        if(itemTimer > itemWait && powerUpActive == false) // If the item timer has waited long enough/longer than the wait time, then...
        {
            itemTimer = 0; // Reset timer back to 0
            powerUpActive = true;

            // Randomly choose left or right
            if(Random.Range(0, 2) == 0)
            {
                Instantiate(powerUpWave, leftSpawner.position, Quaternion.identity);
            }
            else
            {
                Instantiate(powerUpWave, rightSpawner.position, Quaternion.identity);
            }
        }
    }

    // Resets the PowerUp spawner
    public void ResetPowerUp()
    {
        powerUpActive = false; // If power up is not active
        itemTimer = 0; // start countdown over
        // For when the power up flies off screen and player misses trying to get it
    }
}