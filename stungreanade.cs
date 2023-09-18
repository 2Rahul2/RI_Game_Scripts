using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stungreanade : MonoBehaviour
{
    public float gravity;
    public float h;
    public Transform jumppositon;  
    //public Transform startposition;

    public Rigidbody rigidbody;
    private bool jump;

    public Camera cam;

    void Start()
    {
        jump = true;
        rigidbody = GetComponent<Rigidbody>();     
    }

    void Update()
    {

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray , out RaycastHit rayhit))
        {
            jumppositon.position = rayhit.point;
            if(jump){
                launch();
                jump = false;
            }
        }

    }

    void launch(){
        Physics.gravity = Vector3.up * gravity;
        rigidbody.useGravity = true;

        rigidbody.velocity = calculateLaunch();
    }

    Vector3 calculateLaunch()
    {
        float displacementy = jumppositon.position.y - transform.position.y;

        Vector3 displacementxz = new Vector3(jumppositon.position.x - transform.position.x , 0 , jumppositon.position.z - transform.position.z);
        
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity *h);
        Vector3 velocityXZ = displacementxz / (Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementy - h)/gravity));

        return velocityXZ + velocityY;
    }
}
