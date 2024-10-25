using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouDied_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button TryAgain;
    
    void Start()
    {
        TryAgain.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
