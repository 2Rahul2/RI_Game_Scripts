using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
public class removeBossData : MonoBehaviour
{
    public bool startDeleting;
    [SerializeField]private bool isplayer,triggerHack ,level1;
    [SerializeField] private buildhealth BH;
    private bool completed;
    [SerializeField] private LayerMask isplayerLayer;
    [SerializeField] private float hacktime,resethacktime,data ,escapeTime ,lookTime=2f;
    [SerializeField] private int totalTimetoDelete,deleteProgress,delay=1;
    [SerializeField] private TextMeshProUGUI completePercentText ,escapeText;
    [SerializeField] private GameObject[] spawnEnemy ,LightColorChange;
    [SerializeField] private Transform SpawnPos;
    [SerializeField] private Image blueBar;
    [SerializeField] private GameObject player , TriggerExit , RetryPannel ,puzzelPannel ,Level1HoldText;
    [SerializeField] private CinemachineVirtualCamera playerCam , DoorCam ,puzzelCam;
    [SerializeField] private Color red;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale=1;
        foreach(var obj in LightColorChange){
            obj.transform.GetChild(0).GetComponent<lightcolor>().enabled=false;
        }
        // totalTimetoDelete =InitalTotalTimeDelte;
        hacktime = resethacktime;
    }

    // Update is called once per frame
    void Update()
    {
        isplayer = Physics.CheckSphere(transform.position,4,isplayerLayer);
        if(level1){
            if(isplayer){
                Level1HoldText.SetActive(true);
            }else{
                Level1HoldText.SetActive(false);
            }
        }
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
        if(hacktime<=0){
            triggerHack=true;
            if(startDeleting){
                playerCam.Priority=5;
                puzzelCam.Priority=0;
                startdeleting();
            }else{
                puzzelPannel.transform.parent.gameObject.SetActive(true);
                puzzelPannel.SetActive(true);
                playerCam.Priority=0;
                puzzelCam.Priority=5;
            }
        }
        if(completed){
            completePercentText.text = "HACK FAILED";
            completePercentText.color = red;
            if(lookTime<=0){
                playerCam.Priority =5;
                DoorCam.Priority =0;
                // playerCam.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
                // playerCam.GetComponent<CinemachineVirtualCamera>().LookAt = player.transform;
            }else{
                playerCam.Priority =0;
                DoorCam.Priority =5;
                // playerCam.GetComponent<CinemachineVirtualCamera>().Follow = exitDoor.transform;
                // playerCam.GetComponent<CinemachineVirtualCamera>().LookAt = exitDoor.transform;
                lookTime -= Time.deltaTime;
            }
            if(escapeTime<=0){
                escapeText.text = "";
                escapeTime=0;
                Time.timeScale = 0;  
                RetryPannel.SetActive(true);
                RetryPannel.GetComponent<Animator>().Play("demofade");
                // RETRY LEVEL
            }else{
                escapeTime-= Time.deltaTime;
                float ET = Mathf.Round(escapeTime * 100f)/100;
                escapeText.text = "Escape Within "+ ET.ToString();
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
                foreach(var obj in LightColorChange){
                    obj.transform.GetChild(0).GetComponent<lightcolor>().enabled=true;
                }
                TriggerExit.SetActive(true);
                completed = true;
            }
            deleteProgress = (int)((data/totalTimetoDelete)*100);
            // blueBar.fillAmount += Time.deltaTime/(totalTimetoDelete*delay);
            completePercentText.text = "Hacking " + deleteProgress.ToString() + "%";
        }
    }
}
