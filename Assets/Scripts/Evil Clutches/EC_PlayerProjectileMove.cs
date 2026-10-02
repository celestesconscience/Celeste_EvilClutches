using UnityEngine;

public class EC_PlayerProjectileMove : MonoBehaviour // <-- MonoBehaviour allows Unity to attach script to a GameObject
{

    public float speed = 6; // <-- Speed of the projectile's movement
    public int damage = 10; // <-- Damage dealt by the projectile

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime); // <-- Move the spit to the right at a constant speed, multiplied by Time.deltaTime to make it frame rate independent
        if(transform.position.x > 10) // <-- Check if the projectile has moved off the right side of the screen
        {
            Destroy(gameObject); // <-- Destroy the projectile GameObject to free up memory and resources
        }
    }
}
