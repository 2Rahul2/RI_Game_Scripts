using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;
using Cinemachine;
public class switchplayer : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public Transform CurrentPlayerPos;
    public GameObject CurrentPlayerObject;
    public GameObject[] playericons1;
    public GameObject[] playericons2;

    public GameObject camfollow;
    public int playernum=1;
    public playerTwo playerulttrack;
    public GameObject player1UI;
    public GameObject player2UI;
    public helicopter CopterHeli;
    public decPlayerHealth[] checkHealthStatus;
    public GameObject RetryPannel , SwitchWarning;
    private float waitforPause=1.5f;
    private bool player1Die=true ,player2Die=true;

    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale =1;
        CurrentPlayerPos = player1.transform.GetChild(0);
        CurrentPlayerObject = player1;
        // camfollow = camfollow.GetComponent<CinemachineVirtualCamera>();
        player1.SetActive(true);
        player2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        if(checkHealthStatus[0].health <=0 && checkHealthStatus[1].health >=0){
            if(player2Die){
                switchPlayer2();
                player2Die=false;
            }
        }
        if(checkHealthStatus[1].health<=0 && checkHealthStatus[0].health>=0){
            if(player1Die){
                switchplayer1();
                player1Die=false;
            }
        }

        if(checkHealthStatus[0].health <=0&& checkHealthStatus[1].health <=0){
            RetryPannel.SetActive(true);
            if(waitforPause<=0){
                Time.timeScale =0 ;
            }else{
                waitforPause-=Time.deltaTime;
            }
            RetryPannel.GetComponent<Animator>().Play("demofade");
        }




        if(playerulttrack.ultcomplete == true){
            if(Input.GetKeyUp(KeyCode.Alpha1)&&playernum==2){
                if(checkHealthStatus[0].health <= 0){
                    print("can't switch Player");
                    SwitchWarning.GetComponent<Animator>().Play("fadeIdle");
                    SwitchWarning.GetComponent<Animator>().Play("fadeText");
                }else{
                    switchplayer1();
                }

            }
        }
        if(Input.GetKeyUp(KeyCode.Alpha2)&&playernum==1){
            if(checkHealthStatus[1].health <=0){
                SwitchWarning.GetComponent<Animator>().Play("fadeIdle");
                SwitchWarning.GetComponent<Animator>().Play("fadeText");
                print("cant switch player");
            }else{
                switchPlayer2();
                // print("hehehe");
            }
        }
    }
    void switchplayer1(){

                    playernum = 1;
                    player2UI.SetActive(false);
                    player2.SetActive(false);
                    player1.SetActive(true);
                    player1UI.SetActive(true);
                    CurrentPlayerObject = player1;
                    CurrentPlayerPos = player1.transform.GetChild(0);
                    // CopterHeli.playerPos = player1.transform.GetChild(0);
                    // CopterHeli.followPlayer = player1.transform.GetChild(0);
                    player1.transform.GetChild(0).position = new Vector3(player2.transform.position.x , player2.transform.position.y , player2.transform.position.z);

                    camfollow.GetComponent<CinemachineVirtualCamera>().Follow = player1.transform.GetChild(0);
                    camfollow.GetComponent<CinemachineVirtualCamera>().LookAt = player1.transform.GetChild(0);
    }

    void switchPlayer2(){
                playernum =2;
                player2.SetActive(true);
                player2UI.SetActive(true);
                player1.SetActive(false);
                player1UI.SetActive(false);
                CurrentPlayerObject = player2;
                CurrentPlayerPos = player2.transform;
                // CopterHeli.playerPos = player2.transform;
                // CopterHeli.followPlayer =player2.transform;
                // for(int i = 0;i<playericons1.Length;i++){
                //     playericons1[i].SetActive(false);
                //     playericons2[i].SetActive(true);
                // }
                player2.transform.position = new Vector3(player1.transform.GetChild(0).position.x ,  player1.transform.GetChild(0).position.y , player1.transform.GetChild(0).position.z);
                camfollow.GetComponent<CinemachineVirtualCamera>().Follow = player2.transform;
                camfollow.GetComponent<CinemachineVirtualCamera>().LookAt = player2.transform;
    }
}
