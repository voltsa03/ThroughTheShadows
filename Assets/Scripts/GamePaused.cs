// by Mihails Fedosejevs

// // Connecting main Unity Engine, that allows to use  Unity main classes and functions.
using UnityEngine;

using UnityEngine.SceneManagement;

// Creating a public class 'GamePaused', which is derived from 'MonoBehaviour' class in Unity.
public class GamePaused : MonoBehaviour
{
    // Creating boolean 'isPaused'. False by default
    private bool isPaused = false;

    // Creating new variable for saving scene before pause.
    private int sceneBeforePause;

    // Creating method 'Pause Game'.
    public void PauseGame()
    {
        // Checking that boolean 'isPaused' value is True.
        if (!isPaused)
        {
            // 
            sceneBeforePause = SceneManager.GetActiveScene().buildIndex;
            
            // Load the pause scene in additive mode
            SceneManager.LoadScene("PauseScreen", LoadSceneMode.Additive);

            //When game is paused, timescale is 0. Means game objects not moving. 
            Time.timeScale = 0;
            
            // Boolean 'isPaused' is switched to True.
            isPaused = true;
        }
        
    }

    // Craeting 'ResumeGame' method.
    public void ResumeGame()
    {
        if (isPaused)
        {
            // Unload the pause scene
            SceneManager.UnloadSceneAsync("PauseScreen");
            
            // Load the previous scene in additive mode
            SceneManager.LoadScene(sceneBeforePause, LoadSceneMode.Additive);

            // Timescale returns to normal, when game is resumed.
            Time.timeScale = 1;
            isPaused = false;
        }
    }
}