using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu:MonoBehaviour {
    public AudioMixer audioMixer;
    public List<ResItem> resolution = new List<ResItem>();
    public RawImage resolutionLabel, offImg, qualityLabel, textSpeedImage;
    public Texture[] resolutionImg, qualityImg, textSpeedText, fullscreenImg;
    private int selectedResolution, selectQualitySetting = 5;
    public bool fullScreen;
    public int textSelection = 1;
    public float[] textSpeeds;

    private void Start() {
        TextSpeedChange();
        //bool foundRes = false;
        for(int i = 0; i < resolution.Count; i++) {
            if(Screen.width == resolution[i].horizontal && Screen.height == resolution[i].vertical) {
                //foundRes = true;
                selectedResolution = i;
                UpdateResLabel();
            }
        }

        //if(!foundRes) {
        //ResItem newRes = new ResItem();
        //newRes.horizontal = Screen.width;
        //newRes.vertical = Screen.height;
        //resolution.Add(newRes);
        //selectedResolution = 0;

        //UpdateResLabel();
        //}
    }

    public void ApplyGraphics() {
        Screen.SetResolution(resolution[selectedResolution].horizontal, resolution[selectedResolution].vertical, fullScreen = true);
    }

    public void ResLeft() {
        if(selectedResolution == 0)
            return;

        selectedResolution--;
        UpdateResLabel();
    }

    public void ResRight() {
        if(selectedResolution == resolution.Count - 1)
            return;

        selectedResolution++;
        UpdateResLabel();
    }

    void UpdateResLabel() {
        resolutionLabel.texture = resolutionImg[selectedResolution];
    }

    //Quality Settings
    public void QualityImgChangeUp() {
        if(selectQualitySetting == 5)
            return;

        selectQualitySetting++;
        UpdateQualityImg();
    }

    public void QualityImgChangeDown() {
        if(selectQualitySetting == 0)
            return;

        selectQualitySetting--;
        UpdateQualityImg();
    }

    void UpdateQualityImg() {
        qualityLabel.texture = qualityImg[selectQualitySetting];
        if(selectQualitySetting < 3) {
            QualitySettings.SetQualityLevel(selectQualitySetting, false);
        } else {
            QualitySettings.SetQualityLevel(selectQualitySetting, true);
        }
    }

    //Volume
    public void SetVolume(float volume) {
        audioMixer.SetFloat("Volume", volume);
    }

    //Fullscreen
    public void FullScreenOn() {
        offImg.texture = fullscreenImg[0];
        Screen.fullScreen = true;
    }

    public void FullScreenOff() {
        offImg.texture = fullscreenImg[1];
        Screen.fullScreen = false;
    }

    void SetFullScreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

    //Text Speed
    public void TextSpeedUp() {
        if(textSelection == 2)
            return;

        textSelection++;
        TextSpeedChange();
    }

    public void TextSpeedDown() {
        if(textSelection == 0)
            return;

        textSelection--;
        TextSpeedChange();
    }

    void TextSpeedChange() {
        textSpeedImage.texture = textSpeedText[textSelection];
        PlayerPrefs.SetFloat("TextSpeed", textSpeeds[textSelection]);
    }
}

[System.Serializable]
public class ResItem {
    public int horizontal, vertical;
}