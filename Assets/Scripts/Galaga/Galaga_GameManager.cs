using UnityEngine;
using UnityEngine.SceneManagement;

// GameManager Class and Functions
public class Galaga_GameManager : MonoBehaviour
{
    // GameObject References
    public GameObject pauseOverlay; 
    public GameObject controlsOverlay;
    public GameObject gameOverOverlay;

    // Audio
    public AudioSource bgMusic;
    public AudioSource gameOverSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player presses Escape key
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // If the Controls Overlay is open, go back to the Pause Overlay
            if(controlsOverlay.activeSelf)
            {
                CloseControlsMethod();
            }

            // If the Pause Overlay is open, resume the game
            else if(pauseOverlay.activeSelf)
            {
                ResumeMethod();
            }

            // If neither overlay is open, open the Pause Overlay
            else
            {
            PauseMenuMethod();
            }
        }
    }

    // Toggles the Pause Overlay
    public void PauseMenuMethod()
    {
        // Set the Pause Overlay to the opposite of its current active state
        pauseOverlay.SetActive(!pauseOverlay.activeSelf);

        // If the Pause Overlay is active, pause the game
        if(pauseOverlay.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    // Resumes the game
    public void ResumeMethod()
    {
        // Hide the Pause Overlay
        pauseOverlay.SetActive(false);

        // Resume the game
        Time.timeScale = 1;
    }

    // Opens the Controls Overlay
    public void OpenControlsMethod()
    {
        if(pauseOverlay.activeSelf) // Checks if the pause overlay is currently active
        {
            pauseOverlay.SetActive(false); // Hide the pause overlay before showing controls
            controlsOverlay.SetActive(true); // Shows the control overlay
            Time.timeScale = 0; // Makes sure game is still paused
            print("Controls menu opened. Game frozen"); // For debugging purposes
        }
    }
        
    // Closes the Controls Overlay
    public void CloseControlsMethod()
    {
        controlsOverlay.SetActive(false); // Hide the controls overlay before showing the pause overlay
        pauseOverlay.SetActive(true); // Show the pause overlay after closing the controls overlay
        Time.timeScale = 0; // Still keeps game paused
        print("Controls menu closed. Returned to pause menu."); // For debugging purposes
    }

    // Restarts the game
    public void RestartGameMethod()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Galaga");
        print("Game restarted");
    }

    // Shows Game Over Overlay
    public void GameOverMethod()
    {
        // Show the Game Over Overlay
        gameOverOverlay.SetActive(true);

        bgMusic.Stop();
        gameOverSound.Play();

        // Pause the game
        Time.timeScale = 0;
    }

    // Quits the game
    public void QuitGameMethod()
    {
        Application.Quit(); // Quit the application
    }
}
