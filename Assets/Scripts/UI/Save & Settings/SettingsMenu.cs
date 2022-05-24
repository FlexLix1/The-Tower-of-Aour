using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
    public AudioMixer audioMixer;
    public List<ResItem> resolution = new List<ResItem>();
    public List<QualityItem> qualityItems = new List<QualityItem>();
    public RawImage resolutionLabel, offImg, qualityLabel;
    public Texture[] resolutionImg, qualityImg, textSpeedText, fullscreenImg;
    private int selectedResolution, selectedQualityImg;
    public bool fullScreen;
    public int textSelection;
    public float[] textSpeeds;

    
    

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
        if (fullScreen == true) {
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
        resolutionLabel.texture = resolutionImg[selectedResolution];
    }
    
    public void QualityImgChangeDown() {
        selectedQualityImg--;
        if(selectedQualityImg < 0) {
            selectedQualityImg = 0;
        }
        UpdateQualityImg();
    }

    public void QualityImgChangeUp() {
        selectedQualityImg++;
        if(selectedQualityImg > qualityItems.Count - 1) {
           selectedQualityImg = qualityItems.Count - 1;
        }
    }

    public void UpdateQualityImg() {
        qualityLabel.texture = qualityImg[selectedQualityImg];
    }

    public void SetVolume(float volume) {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetFullScreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

    public void FullScreenOn() {
        offImg.texture = fullscreenImg[0];
        Screen.fullScreen = true;
    }

    public void FullScreenOff() {
        offImg.texture = fullscreenImg[1];
        Screen.fullScreen = false;
    }

    public void TextSpeedChange() {
        PlayerPrefs.SetFloat("TextSpeed",textSpeeds[textSelection]);
    }

    public void TextSpeedUp() {
        if (textSelection == 2)
            return;
        textSelection++;
        TextSpeedChange();
    }
    public void TextSpeedDown() {
        if (textSelection == 0)
            return;
        textSelection--;
        TextSpeedChange();
    }
}

[System.Serializable]
public class ResItem {
    public int horizontal, vertical;
}

[System.Serializable]
public class QualityItem {
    public Texture Verylow, Low, Medium, High, VeryHigh, Ultra;
}