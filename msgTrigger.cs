using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class msgTrigger : MonoBehaviour
{
    public GameObject[] text;
    public GameObject canvasObject;
    public LayerMask isobj;
    private int count = 0;


    // goTo door;
    public Transform triggerPosition1;
    public Transform triggerPosition2;
    public GameObject GoToLocationParticle;
    public GameObject GotoScientistText , elevator;
    public door Door;
    private bool doorBool=false;
    //go to scientist
    public Transform particlePosition , particlePosition2 , LookAtDoor , playerpos , DoorPos;

    public endDevice checkIfCompleted;
    public CinemachineVirtualCamera cam;
    private float waitforcam = 2;
    void Start()
    {
        // GameObject myLocationParticle = Instantiate(GoToLocationParticle , particlePosition.position , Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.CheckSphere(transform.position ,4 , isobj)){
            if(Input.GetKeyUp(KeyCode.F)){
                canvasObject.SetActive(true);
            }
        }


        if(Physics.CheckSphere(triggerPosition1.position , 3 ,isobj)){
            if(Input.GetKeyUp(KeyCode.F)){
                triggerPosition2.gameObject.SetActive(true);
                canvasObject.SetActive(true);
                GotoScientistText.SetActive(true);
                GotoScientistText.GetComponent<nextMsg>().enabled=true;
                GoToLocationParticle.transform.position = particlePosition.position;
            }
        }
        if(Physics.CheckSphere(triggerPosition2.position , 3 , isobj)){
            if(checkIfCompleted.isdone==true){
                if(waitforcam <=0){
                    GoToLocationParticle.transform.position = particlePosition2.position;
                    cam.m_LookAt = playerpos;
                }else{
                    cam.m_LookAt = LookAtDoor;
                    waitforcam -= Time.deltaTime;
                }
            }
        }
        if(Physics.CheckSphere(particlePosition2.position , 3 , isobj)){
            if(Input.GetKeyUp(KeyCode.F)){
                waitforcam = 2f;
                cam.m_LookAt = DoorPos;
                cam.m_Follow = DoorPos;
                Door.anim.Play("door_2_open");
                Door.Mc.enabled=false;
                // door.GetComponent<door>().enabled=true;
                GoToLocationParticle.SetActive(false);
                doorBool=true;
            }
            if(doorBool){
                if(waitforcam<=0){
                    cam.m_LookAt = playerpos;
                    cam.m_Follow = playerpos;
                    elevator.GetComponent<elevator>().enabled=true;
                }else{
                    waitforcam -= Time.deltaTime;
                }
            }
        }
    }
}
