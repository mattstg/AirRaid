using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MyMenuController : MonoBehaviour
{
    public bool disableAudio = false;
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        DontDestroyOnLoad(this.gameObject);
    }

    public void DisableAudio(bool isDisableAudio)
    {
        disableAudio = isDisableAudio;
    }
}
