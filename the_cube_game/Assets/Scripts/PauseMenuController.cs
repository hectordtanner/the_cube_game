using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject container;

    public bool isMenuOpen = false;

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isMenuOpen)
            {
                container.SetActive(false);
                Time.timeScale = 1;
                isMenuOpen = false;
            }
            else
            {
                container.SetActive(true);
                Time.timeScale = 0;
                isMenuOpen = true;
            }
        }
    }

    public void Resume()
    {
        container.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}