using UnityEngine; //Using Unity's Programming tools

// Galaga_PlayerMovement Class and Functions
public class Galaga_PlayerMovement : MonoBehaviour
{

    // Variables
    public float speed = 5; // <-- Speed of the player's movement

    // Player Bullet Projectile Reference
    public GameObject playerBullet;

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
}
