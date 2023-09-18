using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propsFallForce : MonoBehaviour
{
    public bool isfall;
    private Rigidbody rb;
    private BoxCollider Bx;
    public float disappear;
    public int[] angle = new int[]{10,15,20 ,30,40 ,60 ,55 };
    private void Start() {
        rb = GetComponent<Rigidbody>();
        Bx = GetComponent<BoxCollider>();
    }
    void Update()
    {
        if(isfall){
            rb.AddForce(-transform.parent.transform.up *30 ,ForceMode.Force);
            if(disappear<=0){
                Bx.enabled=false;
                Destroy(gameObject , 3f);
            }else{
                disappear -= Time.deltaTime;
            }
        }
    }
}
