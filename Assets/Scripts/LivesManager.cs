using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    public int LivesCount = 4;
    public RawImage[] Lives;
    // Start is called before the first frame update
    
    public void DecreaseLivesCount()
    {
        LivesCount--;
        Lives[LivesCount].enabled = false;
    }
}
