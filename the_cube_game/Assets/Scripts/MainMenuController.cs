using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private string gameSceneName;

    public void PlayTimed()
    {
        GameSettings.isTimerOn = true;
        SceneManager.LoadScene(gameSceneName);
    }

    public void PlayZen()
    {
        GameSettings.isTimerOn = false;
        SceneManager.LoadScene(gameSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
