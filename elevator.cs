using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    public GameObject charcter;

    public Transform first;
    public bool playerinrange;
    public Transform centerpos;
    public LayerMask playermask;
    public int radii;

    private bool clicked;
    public float lerptime;
    public float lerpspeed;

    void Update()
    {
        if(lerptime >= 1){
            clicked = false;
            charcter.transform.parent = null;
            this.enabled=false;
        }
        if(clicked){
            transform.position = Vector3.Lerp(transform.position , new Vector3(transform.position.x ,first.position.y , transform.position.z) ,lerptime);
            lerptime += Time.deltaTime / lerpspeed;
            // transform.position = Vector3.MoveTowards(transform.position , first.position , elespeed * Time.deltaTime);
        }
        playerinrange = Physics.CheckSphere(centerpos.position , radii , playermask);
        if(playerinrange){
            if(Input.GetKey(KeyCode.F)){
                charcter.transform.parent = this.transform;
                clicked=true;
            }
        }
    }
}
