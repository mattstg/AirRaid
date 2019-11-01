using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioListener[] listner = GetComponents<AudioListener>();
        if (GameObject.FindGameObjectWithTag("MainMenuController"))
        {
            if (GameObject.FindGameObjectWithTag("MainMenuController").GetComponent<MyMenuController>().disableAudio)
            {
                Debug.Log("Here");
                foreach(AudioListener l in listner)
                l.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
