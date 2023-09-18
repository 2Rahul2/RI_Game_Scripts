using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class endDevice : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prevNode;
    public door unlock;
    public bool isdone;
    public Sprite unlockimg , lockimg;
    public int CurrentAlignedCount;
    public int TotalAlignedCount;
    public rotatedevice ProtectedDevice;
    public bool noprotect , FirstEvent ,lastEvent;
    public stealthEvents SE;
    public removeBossData RBD;

    void Update()
    {
        // if(Input.GetKeyUp(KeyCode.L)){
        //     gameObject.GetComponent<Image>().sprite = unlockimg;
        //  }
        // isdone = prevNode.GetComponent<rotatedevice>().truealigned;
        if(noprotect){
            if(CurrentAlignedCount>=TotalAlignedCount){
                gameObject.GetComponent<Image>().sprite = unlockimg;
            }else{
                gameObject.GetComponent<Image>().sprite = lockimg;
            }
        }else{
            if(!ProtectedDevice.protectedhack){
                print("work");
                if(CurrentAlignedCount>=TotalAlignedCount){
                    gameObject.GetComponent<Image>().sprite = unlockimg;
                }else{
                    gameObject.GetComponent<Image>().sprite = lockimg;
                }
            }
        }
    }
    public void completeHack(){
        if(CurrentAlignedCount>=TotalAlignedCount){
            transform.parent.gameObject.SetActive(false);
            transform.parent.parent.gameObject.SetActive(false);
            if(FirstEvent){
                SE.doneEvent1=true;
            }else if(lastEvent){
                RBD.startDeleting = true;
            }else{
                unlock.GetComponent<door>().anim.Play("door_2_open");
                unlock.GetComponent<door>().Mc.enabled=false;
                print("completed");
            }
        }
    }
}
