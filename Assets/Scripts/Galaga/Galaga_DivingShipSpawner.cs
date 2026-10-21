using UnityEngine;

public class Galaga_DivingShipSpawner : MonoBehaviour
{
    // Dive Ship Spawning Variables
    public GameObject divingShip; // <-- Reference to the Diving Ship prefab that will spawn
    public float spawnTimer = 0f, spawnWait = 8f; // <-- Timer that counts up and how many seconds to wait between spawns

    // Update is called once per frame
    void Update()
    {
        // Diving Ship spawning logic
        spawnTimer += Time.deltaTime; // <-- Increase the spawn timer by the time passed since the previous frame

        if(spawnTimer > spawnWait) // <-- Check if enough time has passed to spawn another Diving Ship
        {
            spawnTimer = 0; // <-- Reset the timer to begin counting toward the next spawn

            float randomX = Random.Range(-7f, 7f); // <-- Choose a random horizontal position between -7 and 7

            Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);
            // Create the ship's spawn position using the random X variable and the spawner's Y and Z coordinates

            Instantiate(divingShip, spawnPosition, Quaternion.identity); // <-- Spawn the Diving Ship prefab at the randomly generated position with no rotation
        }
    }
}