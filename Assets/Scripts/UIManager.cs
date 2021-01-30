using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update  
    public Text timer;
    public float time = 300;

    // Update is called once per frame
    void Update()
    {
        
        time -= Time.deltaTime;
        timer.text = ((int)time/60).ToString() + ":" + ((int)time%60).ToString() ;
    }
    public void WinGame()
    {

    }
    public void LoseGame()
    {

    }
}
