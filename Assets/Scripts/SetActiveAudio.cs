using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveAudio : MonoBehaviour
{
    bool temp = true;
    // Start is called before the first frame update
    void Start()
    {
        AudioListener[] listner = GetComponents<AudioListener>();
        if (GameObject.FindGameObjectWithTag("MainMenuController"))
        {
            if (GameObject.FindGameObjectWithTag("MainMenuController").GetComponent<MyMenuController>().disableAudio)
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioListener>().enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
