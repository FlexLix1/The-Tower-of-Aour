using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu:MonoBehaviour {
    private PauseMenu pause;
    public Button loadButton;

    private void Start() {
        Cursor.lockState = CursorLockMode.Confined;
        pause = GetComponent<PauseMenu>();
        float checkLoad = PlayerPrefs.GetFloat("PlayerPosX");
        if(checkLoad == 0) {
            PlayerPrefs.SetFloat("PlayerPosX", -19.77f);
            PlayerPrefs.SetFloat("PlayerPosY", 2.83f);
            PlayerPrefs.SetFloat("PlayerPosZ", -0.94f);
            PlayerPrefs.SetInt("CurrentFloor", 0);
            loadButton.GetComponent<Image>().color = new Color(100, 100, 100);
        }

        if(checkLoad != -21.2f) {
            loadButton.GetComponent<Image>().color = Color.white;
        }
    }

    public void PlayGame() {
        PlayerPrefs.SetFloat("PlayerPosX", -19.77f);
        PlayerPrefs.SetFloat("PlayerPosY", 2.83f);
        PlayerPrefs.SetFloat("PlayerPosZ", -0.94f);
        PlayerPrefs.SetInt("CurrentFloor", 0);
        SceneManager.LoadScene(2);
    }

    public void LoadGame() {
        Time.timeScale = 1f;
        float checkLoad = PlayerPrefs.GetFloat("PlayerPosX");
        if(checkLoad == -19.77f)
            return;

        SceneManager.LoadScene(2);
    }

    public void LoadGameInside() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //ActiveAndDeactivateFloors currentFloor = player.GetComponent<ActiveAndDeactivateFloors>();

        //Vector3 loadPosition;
        //loadPosition.x = PlayerPrefs.GetFloat("PlayerPosX");
        //loadPosition.y = PlayerPrefs.GetFloat("PlayerPosY");
        //loadPosition.z = PlayerPrefs.GetFloat("PlayerPosZ");
        //player.transform.position = loadPosition;
        //currentFloor.currentfloor = PlayerPrefs.GetInt("CurrentFloor", currentFloor.currentfloor);
    }

    public void backToMainMenu() {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void BackInGame() {
        pause.PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        pause.GameIsPaused = false;
    }
    public void Quit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

}
