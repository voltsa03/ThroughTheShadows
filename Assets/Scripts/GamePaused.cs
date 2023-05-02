// by Mihails Fedosejevs

// // Connecting main Unity Engine, that allows to use  Unity main classes and functions.
using UnityEngine;
using UnityEngine.UI;

// Creating a public class 'GamePaused', which is derived from 'MonoBehaviour' class in Unity.
public class GamePaused : MonoBehaviour
{
    // Creating boolean 'isPaused'. True by default
    private bool isPaused = true;
   

    // Creating method 'Pause Game'.
    public void PauseGame()
    {
        // Checking that boolean 'isPaused' value is True.
        if (isPaused)
        {
            //When game is paused, timescale is 0. Means game objects not moving. 
            Time.timeScale = 0;
            
            // Boolean 'isPaused' is switched to True.
            isPaused = true;
        }
        
    }

    // Craeting 'ResumeGame' method.
    public void ResumeGame()
    {
        if (!isPaused)
        {
            // Timescale returns to normal, when game is resumed.
            Time.timeScale = 1;
            isPaused = false;
        }
    }
}