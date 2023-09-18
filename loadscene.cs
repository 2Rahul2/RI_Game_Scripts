using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class loadscene : MonoBehaviour
{
    [SerializeField] public GameObject LoadPannel;
    // [SerializeField] private Image loadingImgage;
    public IEnumerator LoadScene(string sceneName){
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);
        // AsyncOperatio
        LoadPannel.SetActive(true);
        loadOperation.allowSceneActivation =false;
        while(!loadOperation.isDone){
            // loadingImgage.GetComponent<Animator>().Play("loadImg");
            // loading Screen
            if(loadOperation.progress >= 0.9f){
                print("loaded");
                loadOperation.allowSceneActivation=true;
                // if(Input.GetKeyUp(KeyCode.F)){}
            }
            yield return null;
        }
    }
    public void startLoad(string name){
        IEnumerator myCoroutuine = LoadScene(name);
        StartCoroutine(myCoroutuine);
    }
}
