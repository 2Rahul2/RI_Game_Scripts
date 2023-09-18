using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class mainmenu : MonoBehaviour
{
    [SerializeField] private GameObject Main_Screen , Option_Screen , Credits_Screen , Map_Screen;
    [SerializeField] private TMP_Dropdown graphicsDropDown , qualityDropDown;
    [SerializeField] private int x,y , UserX ,UserY , levelCount=0 ,len_levels;
    [SerializeField] private GameObject[] LevelsList;
    [SerializeField] public GameObject LoadPannel;
    [SerializeField] private loadscene LoadSceneObject;
    private void Start() {
        foreach(var obj in LevelsList){
            obj.SetActive(false);
        }
        len_levels = LevelsList.Length;
        LevelsList[0].SetActive(true);
        // Screen.fullScreenMode = Screen.fullScreenMode;
        // Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        UserX=Display.main.systemWidth;
        UserY=Display.main.systemHeight;
        Screen.SetResolution(UserX ,UserY , Screen.fullScreenMode = FullScreenMode.FullScreenWindow);
        graphicsDropDown.options[0].text = UserX.ToString()+" X "+ UserY.ToString() + " Full Screen";
        graphicsDropDown.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = UserX.ToString()+"X"+UserY.ToString()+" Full Screen";
        // Default Graphics
        QualitySettings.SetQualityLevel(1);
        qualityDropDown.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Medium";
        qualityDropDown.value = 1;
    }
    public void leftArrow(){
        levelCount -= 1;
        if(levelCount<=0){
            levelCount=0;
        }
        for(int i = 0; i<len_levels;i++){
            LevelsList[i].SetActive(false);
        }
        LevelsList[levelCount].SetActive(true);
    }
    public void rightArrow(){
        levelCount += 1;
        if(levelCount>=len_levels){
            levelCount=len_levels-1;
        }
        for(int i = 0; i<len_levels;i++){
            LevelsList[i].SetActive(false);
        }
        LevelsList[levelCount].SetActive(true);
    }
    public void SetGraphics(int qualityValue){
        QualitySettings.SetQualityLevel(qualityValue);
    }
    public void startg()
    {
        load1();
        // SceneManager.LoadScene("level1");
    }
    private void Update() {
        if(Input.GetKeyUp(KeyCode.L)){
            Screen.fullScreen = !Screen.fullScreen;
        }
    }
    public void selectmap(){
        Map_Screen.SetActive(true);Main_Screen.SetActive(false);
    }
    public void option(){
        Main_Screen.SetActive(false);Option_Screen.SetActive(true);
    }
    public void optionBack(){
        // float x = Scre
        Main_Screen.SetActive(true);Option_Screen.SetActive(false);
    }
    public void SelectLevelBack(){
        Map_Screen.SetActive(false);Main_Screen.SetActive(true);
    }
    public void load1(){
        // SceneManager.LoadScene("level1");
        LoadPannel.SetActive(true);
        Main_Screen.transform.parent.gameObject.SetActive(false);
        LoadSceneObject.startLoad("level1");
        // print("level1");
    }
    public void load2(){

        // print("level2");
    }public void load3(){
        Main_Screen.transform.parent.gameObject.SetActive(false);
        LoadSceneObject.startLoad("FinalStealth");
        // print("level3");
    }public void load4(){
        Main_Screen.transform.parent.gameObject.SetActive(false);
        LoadSceneObject.startLoad("Earth");
        // print("level4");
    }public void load5(){
        Main_Screen.transform.parent.gameObject.SetActive(false);
        LoadSceneObject.startLoad("Earth 1");
        // print("level5");
    }
    public void SetReso(int val){
        if(val == 0){
            // print(x+" X "+y);
            // Screen.fullScreenMode = Screen.fullScreenMode;
            Screen.SetResolution(UserX ,UserY , Screen.fullScreenMode = FullScreenMode.FullScreenWindow);
            graphicsDropDown.options[0].text = UserX.ToString()+"X"+UserY.ToString()+" Full Screen";
        graphicsDropDown.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = UserX.ToString()+" X "+UserY.ToString()+" Full Screen";
        }
        if(val == 1){
            x=1920;
            y=1080;
            // Screen.fullScreenMode = FullScreenMode.Windowed;
            Screen.SetResolution(x ,y , Screen.fullScreenMode = FullScreenMode.Windowed);
        graphicsDropDown.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = x.ToString()+"X"+y.ToString()+" Windowed";

        }
        if(val == 2){
            x=1680;
            y=1050;
            Screen.SetResolution(x,y ,Screen.fullScreenMode = FullScreenMode.Windowed);
        graphicsDropDown.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = x.ToString()+"X"+y.ToString()+" Windowed";

        }
        if(val == 3){
            x=1600;
            y=900;
            Screen.SetResolution(x,y,Screen.fullScreenMode = FullScreenMode.Windowed);
            graphicsDropDown.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = x.ToString()+"X"+y.ToString()+" Windowed";

        }if(val==4){
            x=1366;
            y=768;
            Screen.SetResolution(x,y,Screen.fullScreenMode = FullScreenMode.Windowed);
            graphicsDropDown.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = x.ToString()+"X"+y.ToString()+" Windowed";
            // x = 1620;y=640;
            // Screen.SetResolution
        }
        if(val == 5){
            x=1280;
            y=768;
            Screen.SetResolution(x,y,Screen.fullScreenMode = FullScreenMode.Windowed);
            graphicsDropDown.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = x.ToString()+"X"+y.ToString()+" Windowed";
        }
    }
    public void exit()
    {
        Application.Quit();
    }
}
