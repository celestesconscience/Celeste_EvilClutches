using UnityEngine; //Using Unity's Programming tools

//ControlsMenu Class and Functions
public class ControlsMenu : MonoBehaviour // <-- MonoBehaviour allows Unity to attach script to a GameObject
{
    // Controls Panel
    public GameObject controlsMenu; // <-- Reference to the Controls Menu GameObject

    // Toggles the visibility of the Controls Panel
    public void ControlsMenuMethod()
    {
        // Set the Controls Panel to the opposiite of its current active state
        // activeSelf checks if the panel is currently active (true) or inactive (false)
        // ! means "Not", so it changes true to false or false to true
        // Basically, set controls panel((!opposite)controlspanel.whatever state)
        controlsMenu.SetActive(!controlsMenu.activeSelf);

        if(controlsMenu.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player presses C for Controls Menu
        if(Input.GetKeyDown(KeyCode.C))
        {
            // Run the Controls Menu function to open or close the panel
            ControlsMenuMethod();
        }
    }
}
