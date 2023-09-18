using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    private bool touchdoor;
    public float door_radius;
    public bool islocked;
    public LayerMask layerMask; 
    public Animator anim;
    public BoxCollider Mc;
    public GameObject connection;
    void Update()
    {
        touchdoor = Physics.CheckSphere(transform.position , door_radius , layerMask);

        if(touchdoor){
            if(Input.GetKeyUp(KeyCode.F)){
                if(!islocked){
                    anim.Play("door_2_open");
                    Mc.enabled=false;
                }else{
                    connection.transform.parent.gameObject.SetActive(true);
                    connection.SetActive(true);
                }
                // transform.position = new Vector3(transform.position.x + 2  , 0 , 0);
            }
            
            //message pop up : press k to open  
        }
        // make another object appear;
    }
}
