using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    private PauseMenu pause;

    private void Start()
    {
        pause = GetComponent<PauseMenu>();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void BackInGame()
    {
        pause.PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        pause.GameIsPaused = false;
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

}
