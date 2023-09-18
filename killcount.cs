using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killcount : MonoBehaviour
{
    public int spawnerkilled=0;
    public AudioClip bossTrack;
    public int enemykilled=0;
    public GameObject platform , boundry;
    public GameObject BossTrigger;
    public GameObject[] walls;
    public switchplayer getplayer;
    public GameObject Boss;
    public AudioSource playTracks;
    private float wait=4f;
    private bool once;
    private void Update() {
        if(spawnerkilled == 4 && enemykilled>=50){
            platform.SetActive(true);
            if(!once){
                if(Vector3.Distance(getplayer.CurrentPlayerPos.transform.position , BossTrigger.transform.position)<50f){
                    if(wait<=0){
                        foreach(var wal in walls){
                            wal.SetActive(true);
                        }
                        boundry.SetActive(true);
                        Destroy(BossTrigger);
                        Boss.GetComponent<boss>().enabled = true;
                        playTracks.clip = bossTrack;
                        playTracks.PlayDelayed(1.8f);
                        playTracks.volume =1;
                        once = true;
                    }else{
                        playTracks.volume -= Time.deltaTime/2;
                        wait-= Time.deltaTime;
                    }
                }
            }
        }
    }
}

