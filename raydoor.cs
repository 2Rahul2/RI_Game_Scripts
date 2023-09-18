using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raydoor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private LayerMask DoorMask;
    [SerializeField] private Transform RayPoistion;
    private RaycastHit hit;
    private bool itHit;
    private float waitforClose;
    private GameObject door;
    [SerializeField] private float resetWaitforClose;
    void Start()
    {
        waitforClose = resetWaitforClose;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(RayPoistion.position, RayPoistion.transform.TransformDirection(Vector3.forward) *4, Color.yellow);
        if(Physics.Raycast(RayPoistion.position , RayPoistion.transform.TransformDirection(Vector3.forward) ,out hit , 4 ,DoorMask)){
            print(hit.transform.name);
            itHit=true;
            waitforClose=resetWaitforClose;
            door =hit.transform.gameObject;
            hit.transform.gameObject.GetComponent<Animator>().Play("door_2_open");
            hit.transform.gameObject.GetComponent<BoxCollider>().enabled=false;
        }else{
            if(itHit){
                if(waitforClose<=0){
                    door.GetComponent<Animator>().Play("door_1_close");
                    door.GetComponent<BoxCollider>().enabled=true;
                    itHit=false;
                    
                }else{
                    waitforClose-= Time.deltaTime;
                }
            }
        }
    }
}
