using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class level1Removedata : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isplayer , triggerHack ,completed;
    [SerializeField] private LayerMask isplayerLayer;
    [SerializeField] private float totalTimetoDelete, hacktime ,resethacktime ,data ,delay ,deleteProgress;
    [SerializeField] private Image blueBar;
    [SerializeField] private TextMeshProUGUI completePercentText;
    [SerializeField] private loadscene LoadScene;
    private float waittimeagin=2f;
    public GameObject Pannel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isplayer = Physics.CheckSphere(transform.position,4,isplayerLayer);
        if(isplayer){
            if(hacktime>0){
                if(Input.GetKey(KeyCode.F)){
                    blueBar.fillAmount += Time.deltaTime/resethacktime;
                    // blueBar.fillAmount =
                    hacktime-=Time.deltaTime;
                }else{
                    blueBar.fillAmount = 0;
                    hacktime=resethacktime;
                }
            }
        }else if(!isplayer && !triggerHack){
            blueBar.fillAmount = 0;
            hacktime = resethacktime;
        }

        if(hacktime <= 0){
            triggerHack=true;
            startdeleting();
        }
        if(completed){
            if(waittimeagin<=0){
                LoadScene.startLoad("FinalStealth");
            }else{
                Pannel.GetComponent<Animator>().Play("demofade");
                waittimeagin -= Time.deltaTime;
            }
        }
    }

    void startdeleting(){
        if(!completed){
            data += Time.deltaTime/delay;
            if(deleteProgress >70 && deleteProgress<90){
                delay = 2;
            }else if(deleteProgress<100&&deleteProgress>90){
                delay = 8;
            }else if(deleteProgress>=100){
                deleteProgress = 100;
                completed = true;
            }
            deleteProgress = (int)((data/totalTimetoDelete)*100);
            // blueBar.fillAmount += Time.deltaTime/(totalTimetoDelete*delay);
            completePercentText.text = "Hacking " + deleteProgress.ToString() + "%";
        }
    }
}
