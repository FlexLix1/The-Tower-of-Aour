using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Cutscene : MonoBehaviour {

    VideoPlayer Intro;

    void Awake() {
        Intro = GetComponent<VideoPlayer>();
        Intro.Play();
        Intro.loopPointReached += CheckOver;
    }


    void CheckOver(UnityEngine.Video.VideoPlayer vp) {
        SceneManager.LoadScene("Main Menu");
    }
}

