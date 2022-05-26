using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(returnToMenu), 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void returnToMenu()
    {
        SceneManager.LoadScene(1);
    }
}
