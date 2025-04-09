using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string gameSceneName = "GameScene"; // Set this in Inspector

    // Called by Start Button
    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
        Time.timeScale = 1f;
    }

    // Called by Exit Button
    public void ExitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
