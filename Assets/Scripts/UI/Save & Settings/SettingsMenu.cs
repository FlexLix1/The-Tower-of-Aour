using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
    public AudioMixer audioMixer;
    public List<ResItem> resolution = new List<ResItem>();
    private int selectedResolution;
    public bool fullScreen;
    public TMP_Text resolutionLabel;

    private void Start() {
        bool foundRes = false;
        for (int i = 0; i < resolution.Count; i++) {
            if (Screen.width == resolution[i].horizontal && Screen.height == resolution[i].vertical) {
                foundRes = true;
                selectedResolution = i;
                UpdateResLabel();
            }
        }
        if (!foundRes) {
            ResItem newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;
            resolution.Add(newRes);
            selectedResolution = resolution.Count - 1;
            UpdateResLabel();
        }
        List<string> options = new List<string>();
    }


    public void Update() {
        if(fullScreen == true) {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }

    public void ApplyGraphics() {
        Screen.SetResolution(resolution[selectedResolution].horizontal, resolution[selectedResolution].vertical, fullScreen = true);
    }

    public void ResLeft() {
        selectedResolution--;
        if (selectedResolution < 0) {
            selectedResolution = 0;
        }
        UpdateResLabel();
    }

    public void ResRight() {
        selectedResolution++;
        if (selectedResolution > resolution.Count - 1) {
            selectedResolution = resolution.Count - 1;
        }
        UpdateResLabel();
    }

    public void UpdateResLabel() {
        resolutionLabel.text = resolution[selectedResolution].horizontal.ToString() + "x" + resolution[selectedResolution].vertical.ToString();
    }

    public void SetVolume(float volume) {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuailty(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullScreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

    [System.Serializable]
    public class ResItem {
        public int horizontal, vertical;
    }
}
