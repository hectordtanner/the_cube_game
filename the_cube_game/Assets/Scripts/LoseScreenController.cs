using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreenController : MonoBehaviour
{
    [SerializeField]
    private GameObject container;

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainGame");
        container.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}