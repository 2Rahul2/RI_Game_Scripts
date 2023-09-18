using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausemenu : MonoBehaviour
{
    private bool ispause;

    public GameObject pause;
    public GameObject message;
    public int numop;

    public float wait;
    public GameObject missioncom;
    public loadscene LoadScene;
    public GameObject missionComanime;

    void Start()
    {
        Time.timeScale =1;

        wait = 3f;
        ispause = true;
        // message.SetActive(true);
    }

    void Update()
    {    
        Destroy(message , 3f);
        
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(ispause)
            {
                Time.timeScale = 0f;
                pause.SetActive(true);
                ispause = false;
        
            }else
            {   
                pause.SetActive(false);
                Time.timeScale = 1f;
                ispause = true;
            }
        } 

        if(numop >= 198)
        {
            missioncom.SetActive(true);
            if(wait <= 0)
            {
                SceneManager.LoadScene("menu");
            }else
            {
                missionComanime.SetActive(true);
                wait -= Time.deltaTime;
            }
            
        }      
    }

    public void nobutton()
    {
        pause.SetActive(false);
        Time.timeScale = 1f;
        ispause = true;
    }

    public void yesbutton()
    {
        Time.timeScale = 1f;
        LoadScene.startLoad("menu");
        // SceneManager.LoadScene("menu");
    }
}
