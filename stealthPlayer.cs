using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stealthPlayer : MonoBehaviour
{
    private Rigidbody rb;
    public float movespeed ,movementmultiplier ,resetspeed;
    public Camera cam;
    private Animator anim;
    public Outline outline;
    Vector3 movedirection;
    public Material txt;
    private float horizontalmovement ,verticalmovement;
    private RaycastHit slopehit;
    private Vector3 slopedirection;
    public Transform groundpos;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        resetspeed = movespeed;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {
        myinput();
        moveplayer();   
    }

    // Update is called once per frame
    void Update()
    {
        lookcam();
        if(Input.GetKey(KeyCode.LeftShift)){
            gameObject.layer = LayerMask.NameToLayer("Default");
            outline.enabled = true;
            Color color = txt.color;
            color.a = 0.1f;
            txt.color = color;
            movespeed = 3;
            // anim["Run_guard_AR"].speed = 0.3f;
            // Animation
        }else{
            gameObject.layer = LayerMask.NameToLayer("player");
            Color color = txt.color;
            color.a = 1;
            txt.color = color;
            outline.enabled = false;
            movespeed=resetspeed;
        }
    }
    bool slopecal(){
        if(Physics.Raycast(groundpos.position  , Vector3.down , out slopehit , 6f , groundLayer)){
            if(slopehit.normal != Vector3.up){
            print("slope");
                slopedirection = Vector3.ProjectOnPlane(movedirection , slopehit.normal);
                return true;
            }else{
                return false;
            }
        }
        return false;
    }
    void lookcam(){
        Ray cameraray = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundplane = new Plane(Vector3.up , Vector3.zero);

        float raylen;
        if(groundplane.Raycast(cameraray , out raylen)){
            Vector3 pointtoLook = cameraray.GetPoint(raylen);
            transform.LookAt(new Vector3(pointtoLook.x , transform.position.y , pointtoLook.z));
        }
    }
    void myinput(){
        horizontalmovement = Input.GetAxisRaw("Horizontal");
        verticalmovement = Input.GetAxisRaw("Vertical");
        movedirection = transform.forward * verticalmovement + transform.right * horizontalmovement;
        if(movedirection == new Vector3(0,0,0)){
            anim.SetBool("run" , false);
            anim.SetBool("idle", true);
        }
        else{
            anim.SetBool("run" , true);
            anim.SetBool("idle" , false);
        }
    }
    void moveplayer(){
        if(slopecal()){
            rb.AddForce(slopedirection.normalized * movespeed *movementmultiplier,ForceMode.Acceleration);
        }else{
            rb.AddForce(movedirection.normalized * movespeed *movementmultiplier , ForceMode.Acceleration);
        }
    }
}
