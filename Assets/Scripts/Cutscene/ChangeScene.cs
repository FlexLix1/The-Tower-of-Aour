using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour {
    VideoPlayer Intro;
    public int timer;
    public GameObject text;


    void Awake() {
        text.SetActive(false);
        Intro = GetComponent<VideoPlayer>();
        Intro.Play();
        Intro.loopPointReached += CheckOver;

        Invoke(nameof(TextPopUp), timer);
    }

    void TextPopUp() {
        text.SetActive(true);

    }
    void CheckOver(UnityEngine.Video.VideoPlayer vp) {
        SceneManager.LoadScene("Main Menu");
    }
}
