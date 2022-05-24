using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {
    public void SavePlayer() {
        PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", transform.position.z);
    }

    //public void LoadPlayer() {
    //    Vector3 position;
    //    position.x = PlayerPrefs.GetFloat("PlayerPosX");
    //    position.y = PlayerPrefs.GetFloat("PlayerPosY");
    //    position.z = PlayerPrefs.GetFloat("PlayerPosZ");
    //    transform.position = position;
    //}
}
