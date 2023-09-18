using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player_extras : MonoBehaviour
{
    public GameObject[] cam;
    public GameObject MainCam;
    private float waitForCutScene =32f;
    public Animator playerAnimation;
    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "cutscene"){
            // MainCam.GetComponent<CinemachineBrain>().enabled=false;
            if(waitForCutScene <=0){
                MainCam.GetComponent<Animator>().enabled = false;
                MainCam.GetComponent<CinemachineBrain>().enabled=true;
                transform.parent.gameObject.GetComponent<stealthPlayer>().enabled=true;
            }else{
                playerAnimation.SetBool("run" , false);
                playerAnimation.SetBool("idle", true);
                MainCam.GetComponent<Animator>().enabled = true;
                MainCam.GetComponent<Animator>().Play("camAnim");
                transform.parent.gameObject.GetComponent<stealthPlayer>().enabled=false;
                MainCam.GetComponent<CinemachineBrain>().enabled=false;
                waitForCutScene -= Time.deltaTime;

            }
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "North")
        {
            for(int i = 0 ; i < 4 ; i++){
                if(cam[i].name != "north"){
                    cam[i].GetComponent<CinemachineVirtualCamera>().Priority = 0;
                    print("not north");
                }else
                {
                    print("is north");
                    cam[i].GetComponent<CinemachineVirtualCamera>().Priority = 1;
                }
            }
        }
        if(other.gameObject.tag == "West")
        {
            for(int i = 0 ; i < 4 ; i++){
                if(cam[i].name != "west"){
                    cam[i].GetComponent<CinemachineVirtualCamera>().Priority = 0;
                    print("not north");
                }else
                {
                    print("is north");
                    cam[i].GetComponent<CinemachineVirtualCamera>().Priority = 1;
                }
            }
        }
        if(other.gameObject.tag == "East")
        {
            for(int i = 0 ; i < 4 ; i++){
                if(cam[i].name != "east"){
                    cam[i].GetComponent<CinemachineVirtualCamera>().Priority = 0;
                    print("not north");
                }else
                {
                    print("is north");
                    cam[i].GetComponent<CinemachineVirtualCamera>().Priority = 1;
                }
            }
        }
        if(other.gameObject.tag == "South")
        {
            for(int i = 0 ; i < 4 ; i++){
                if(cam[i].name != "south"){
                    cam[i].GetComponent<CinemachineVirtualCamera>().Priority = 0;
                    print("not north");
                }else
                {
                    print("is north");
                    cam[i].GetComponent<CinemachineVirtualCamera>().Priority = 1;
                }
            }
        }
    }
}
