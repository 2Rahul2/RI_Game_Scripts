using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class retryPannel : MonoBehaviour
{
    public loadscene LoadScene;
    public void retryButton(){
        Time.timeScale =1;
        Scene scene = SceneManager.GetActiveScene();
        LoadScene.startLoad(scene.name);
        // SceneManager.LoadScene(scene.name);
    }
    public void quitButton(){
        LoadScene.startLoad("menu");
        // SceneManager.LoadScene("menu");
    }
}
