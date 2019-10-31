using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveAudio : MonoBehaviour
{
    AudioListener listner;
    // Start is called before the first frame update
    void Start()
    {
        listner = GetComponent<AudioListener>();
        if (GameObject.FindGameObjectWithTag("MainMenuController"))
        {
            if (GameObject.FindGameObjectWithTag("MainMenuController").GetComponent<MyMenuController>().disableAudio)
            {
                listner.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
