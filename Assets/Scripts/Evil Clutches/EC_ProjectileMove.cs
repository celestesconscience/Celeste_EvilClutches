using UnityEngine;

public class EC_ProjectileMove : MonoBehaviour // <-- MonoBehaviour allows Unity to attach script to a GameObject
{

    public float speed = 6; // <-- Speed of the projectile's movement
    public int points = 100; // <-- Points awarded to the player when the projectile is collected

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-transform.right * speed * Time.deltaTime); // <-- Move the projectile to the left at a constant speed, multiplied by Time.deltaTime to make it frame rate independent
        if(transform.position.x < -10) // <-- Check if the projectile has moved off the left side of the screen
        {
            Destroy(gameObject); // <-- Destroy the projectile GameObject to free up memory and resources
        }
    }
}
