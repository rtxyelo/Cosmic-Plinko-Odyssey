using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles scene-related behaviors such as restarting scenes, loading specific scenes, and quitting the application.
/// </summary>
public class SceneBehaviour : MonoBehaviour
{
    /// <summary>
    /// Restarts the current scene.
    /// </summary>
    public void RestartScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    /// <summary>
    /// Loads the specified scene by name.
    /// </summary>
    /// <param name="sceneName">Name of the scene to load.</param>
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Loads the specified scene by index.
    /// </summary>
    /// <param name="sceneInd">Index of the scene to load.</param>
    public void LoadSceneByIndex(int sceneInd)
    {
        SceneManager.LoadScene(sceneInd);
    }

    /// <summary>
    /// Quit the application when the escape key is pressed.
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResetGameScene()
    {
        GameBehaviour.isGameStart = false;
        GameBehaviour.isGamePaused = false;
        ScoreBehaviour.playerScore = 0;
        ScoreBehaviour.pointsBonusesCollectCount = 0;
        RestartScene();
	}

    public void LoadGameScene(string sceneName)
    {
		GameBehaviour.isGameStart = false;
		GameBehaviour.isGamePaused = false;
        ScoreBehaviour.playerScore = 0;
        ScoreBehaviour.pointsBonusesCollectCount = 0;
        LoadSceneByName(sceneName);
	}

    private void Update()
    {
        // Quit the application when the escape key is pressed.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }
    }
}