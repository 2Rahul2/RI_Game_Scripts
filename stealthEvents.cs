using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
public class stealthEvents : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool FirstLevel ,SecondLevel ,ThirdLevel;
    [SerializeField] private LayerMask isplayer;
    [SerializeField] private Transform player ,cam;
    [SerializeField] private GameObject Puzzle , lookObject ,TextContinue , changeDoorLayer;
    [SerializeField] private CinemachineVirtualCamera Vcam ,VamLook ,playercam;
    private float opendoorTime=1.5f , waitTime=2.5f;
    public bool doneEvent1;
    [SerializeField] private Material DoorIndicator;
    [SerializeField] private Color BlueColor , RedColor;

    [SerializeField] private GameObject SecondEventInstantiate;
    // SECOND LEVEL VARIABLES:

    [SerializeField] private GameObject TextContinue2 , LookObject2 ,changeDoorLayer2;
    private float opendoorTime2=1.5f , waitTime2=2.5f;
    private bool doneEvent2;
    [SerializeField] private Material DoorIndicator2;
    // THIRD

    [SerializeField] private GameObject exitText;
    [SerializeField] private loadscene LoadScene;


    [SerializeField] private GameObject CurrentArrow , NextArrow;
    void Start()
    {
        if(FirstLevel){
            DoorIndicator2.SetColor("_EmissionColor" ,RedColor*2.5f);
            DoorIndicator.SetColor("_EmissionColor" ,RedColor*2.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(FirstLevel){
            if(Physics.CheckSphere(transform.position ,3 ,isplayer)){
                TextContinue.SetActive(true);
                if(Input.GetKeyUp(KeyCode.F)){
                    Puzzle.SetActive(true);
                    playercam.Priority =0;
                    Vcam.Priority =5;
                }
            }else{
                TextContinue.SetActive(false);
            }
            if(doneEvent1){
                Vcam.Priority=0;
                VamLook.Priority =10;
                // cam.GetComponent<CinemachineVirtualCamera>().Follow = lookObject.transform;
                // cam.GetComponent<CinemachineVirtualCamera>().LookAt = lookObject.transform;
                if(opendoorTime<=0){
                    DoorIndicator.SetColor("_EmissionColor" ,BlueColor*2.5f);
                    changeDoorLayer.layer = LayerMask.NameToLayer("Water");
                }else{
                    opendoorTime -= Time.deltaTime;
                }
                if(waitTime<=0){
                    VamLook.Priority=0;
                    Vcam.Priority=0;
                    playercam.Priority=5;
                    SecondEventInstantiate.SetActive(true);
                    CurrentArrow.SetActive(false);
                    NextArrow.SetActive(true);
                    Destroy(gameObject);

                    // Destroy this Object
                    // Instantiate Other Event
                }else{
                    waitTime -= Time.deltaTime;
                }
            }
        }

        if(SecondLevel){
            if(Physics.CheckSphere(transform.position , 3, isplayer)){                
                TextContinue2.SetActive(true);
                if(Input.GetKeyUp(KeyCode.F)){
                    doneEvent2=true;
                }
            }else{
                TextContinue2.SetActive(false);
            }
            if(doneEvent2){
                playercam.GetComponent<CinemachineVirtualCamera>().Follow = LookObject2.transform;
                playercam.GetComponent<CinemachineVirtualCamera>().LookAt = LookObject2.transform;
                if(opendoorTime2<=0){
                    DoorIndicator2.SetColor("_EmissionColor" , BlueColor*2.5f);
                    changeDoorLayer2.layer = LayerMask.NameToLayer("Water");
                }else{
                    opendoorTime2 -= Time.deltaTime;
                }
                if(waitTime2<=0){
                    playercam.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
                    playercam.GetComponent<CinemachineVirtualCamera>().LookAt = player.transform;
                    doneEvent2=false;
                    CurrentArrow.SetActive(false);
                    NextArrow.SetActive(true);
                    Destroy(gameObject);
                    // Instanitate Another Event;
                }else{
                    waitTime2-= Time.deltaTime;
                }
            }
        }

        if(ThirdLevel){
            if(Physics.CheckSphere(transform.position ,3 ,isplayer)){
                exitText.SetActive(true);
                if(Input.GetKeyUp(KeyCode.F)){
                    LoadScene.startLoad("Earth");
                }
            }else{
                exitText.SetActive(false);
            }
        }
    }
}
