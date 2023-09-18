using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class endDeviceOther : MonoBehaviour
{
public GameObject prevNode;
    public door unlock;
    public bool isdone;
    public Sprite unlockimg , lockimg;
    public int CurrentAlignedCount;
    public int TotalAlignedCount;
    public rotatedevice ProtectedDevice;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyUp(KeyCode.L)){
        //     gameObject.GetComponent<Image>().sprite = unlockimg;
        //  }
        // isdone = prevNode.GetComponent<rotatedevice>().truealigned;
            print("work");
            if(CurrentAlignedCount>=TotalAlignedCount){
                gameObject.GetComponent<Image>().sprite = unlockimg;
            }else{
                gameObject.GetComponent<Image>().sprite = lockimg;
            }  
    }
    public void completeHack(){
        if(CurrentAlignedCount>=TotalAlignedCount){
            transform.parent.gameObject.SetActive(false);
            transform.parent.parent.gameObject.SetActive(false);
            // unlock.GetComponent<door>().anim.Play("door_2_open");
            // unlock.GetComponent<door>().Mc.enabled=false;
            print("completed");
        }
    }
}
