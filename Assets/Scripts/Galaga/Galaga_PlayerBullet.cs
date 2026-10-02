using UnityEngine; //Using Unity's Programming tools

// Galaga_PlayerBullet Class and Functions
public class Galaga_PlayerBullet : MonoBehaviour // <-- MonoBehaviour allows Unity to attach script to a GameObject
{

    public float speed = 5; // <-- Speed of the bullet's movement

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up* speed * Time.deltaTime); // <-- Move the bullet to the top at a constant speed, multiplied by Time.deltaTime to make it frame rate independent
        if(transform.position.y > 6) // <-- Check if the bullet has moved off the top of the screen
        {
            Destroy(gameObject); // <-- Destroy the bullet GameObject to free up memory and resources
        }
    }
}
