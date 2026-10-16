using UnityEngine;

// WaveSpawner Class and Functions
public class Galaga_WaveSpawner : MonoBehaviour
{
    // Wave Spawning Variables
    public GameObject[] enemyWaves; // <-- Array of enemy wave GameObjects that will be spawned
    public float waveTimer = 0, waveWait = 3; // <-- Timer for spawning waves and the wait time between waves

    // Heart Spawning
    public GameObject heartItem;
    public float heartTimer = 0f; // Clock for the Heart, counts up
    public float heartWait = 30f; // How long the heart has to wait before spawning

    // Update is called once per frame
    void Update()
    {
        // Wave spawning logic
        waveTimer += Time.deltaTime; // <-- Increment the wave timer by the time elapsed since the last frame
        
        if(waveTimer > waveWait) // <-- Check if the wave timer has exceeded the wait time before spawning a new wave
        {
            waveTimer = 0; // <-- Reset the wave timer before spawning a new wave to ensure proper timing
            Instantiate(enemyWaves[Random.Range(0, enemyWaves.Length)], transform.position, Quaternion.identity); // <-- Spawn a random enemy wave at the current position and rotation of this object
            // Random.Range(0, enemyWaves.Length - 1) selects a random index from the enemyWaves array to determine which wave to spawn next. 
        }

        // Heart spawning timer
        heartTimer += Time.deltaTime; // Start counting timer up

        if(heartTimer > heartWait) // If timer reaches wait amount then...
        {
            heartTimer = 0; // Reset back to 0

            Instantiate(heartItem, transform.position, Quaternion.identity);
        }
    }
}
