using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class buildhealth : MonoBehaviour
{
    private AudioSource explo;
    // new Variables;
    public int Count=0 , enemyCount=0;
    [SerializeField] private float waitForReset=2f;
    [SerializeField]private int totalCount;
    public GameObject Spawner ,pannel ,lastLevelWall ,lastlevelLookat;
    // [SerializeField] private orbcr Orb;
    public GameObject[] SphereRectifierWalls;
    public bool levelC , thirdLevel , lastLevel;
    public int totalenemyKilled=0;
    public GameObject camfollow;

    [SerializeField] private loadscene LoadScene;
    private float lastlevelTimewait=2f;
    public switchplayer SP;
    public GameObject BossHealth;
    private void Start() {
        explo = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(Count>=totalCount){
            Destroy(Spawner);
        }
        if(Count >= totalCount && enemyCount<=0){
            if(lastLevel){
                if(lastlevelTimewait <=0){
                    BossHealth.SetActive(true);
                    Destroy(lastLevelWall);
                    // lastLevelWall.SetActive(false);
                    lastLevel=false;
                    camfollow.GetComponent<CinemachineVirtualCamera>().Follow = SP.CurrentPlayerPos.transform;
                    camfollow.GetComponent<CinemachineVirtualCamera>().LookAt = SP.CurrentPlayerPos.transform;

                }else{
                    camfollow.GetComponent<CinemachineVirtualCamera>().Follow = lastlevelLookat.transform;
                    camfollow.GetComponent<CinemachineVirtualCamera>().LookAt = lastlevelLookat.transform;
                    lastlevelTimewait -= Time.deltaTime;
                }

            }
            if(thirdLevel){
                if(waitForReset<=0){
                    // print("NExtLEvel");
                    LoadScene.startLoad("Earth 1");
                    // load Scene
                }else{
                    pannel.SetActive(true);
                    pannel.GetComponent<Animator>().Play("demofade");
                    waitForReset -= Time.deltaTime;
                }    
            }else{
                foreach(var gObject in SphereRectifierWalls){
                    gObject.SetActive(false);
                }
            }
        }
        if(levelC && enemyCount<=0){
            if(waitForReset<=0){
                pannel.GetComponent<Animator>().Play("demofade");
                print("NExtLEvel");
                // load Scene
            }else{
                waitForReset -= Time.deltaTime;
            }
        }
    }
}
