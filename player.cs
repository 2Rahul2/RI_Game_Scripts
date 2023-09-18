using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;




public class player : MonoBehaviour
{

    public float dragvalue;
    public float movespeed;
    public int downforce;
    public float movementmultiplier;
    float horizontalmovement;
    float verticalmovement;

    Vector3 movedirection;
    Vector3 fordirection;

    public Camera cam;

    public Rigidbody rb;
    public Animator anim;

    public GameObject weap;
    public GameObject ringweapon;
    public Transform ringspawner;
    public float dashspeed;

    public int ringammo;
    public Transform playerbody;
    public float rotationspeed;

    float horimove;
    float vermove;
    float currentYpos;
    Vector3 currentmovingpos;

    private bool iscrouch;
    private BoxCollider colider;
    private Vector3 currentcollidepos;

    public float hipo;

    public Transform landobject;
    private bool crouchforonce = false;

    public GameObject drone;
    public Transform dronepos;
    public GameObject dronefinalpos;
    public float cooldown;
    public int cooldowntime;
    public float cooldownact;
    public Transform droneinitialpos;
    [SerializeField]
    private Transform Qinitialpos;
    [SerializeField]
    private Transform Qfinalpos;
    [SerializeField]
    private GameObject ult;

    public Material txt;
    public int energy;
    
    public float ultcooldown;
    [SerializeField]
    private float resetult;


    [SerializeField]
    private Slider ultslider;
    [SerializeField]
    private Text ultcooltext;


    [SerializeField]
    private GameObject ulteffect;
    [SerializeField]
    private LayerMask isenergy;
    private RaycastHit slopehit;
    private Vector3 slopedirection;
    public float angle;
    public skillstrack st;
    public int health;
    public Slider sliderhealth;
    public TextMeshProUGUI healthtxt;
    void Start()
    {

        // ultslider.value = energy;
        // ulteffect.SetActive(false);
        // ultcooldown = resetult;
        cooldown = 0;
        hipo = 100f;
        currentYpos = transform.position.y;
        colider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        currentcollidepos = new Vector3(colider.size.x , colider.size.y , colider.size.z);
        controldrag();
    }

    // Update is called once per frame
    void Update()
    {
        // slopecal();
            //  angle = Vector3.Angle(Vector3.up , slopehit.normal);
// print(slopecal());
            // print(slopehit.collider.gameObject.name);
            // print("angle:" +angle);

        Collider[] attackrange = Physics.OverlapSphere(transform.position ,6f);
                        // if(Physics.SphereCastAll(attackpos.position,3f , transform.forward , out RaycastHit[] hit ,3 , isenemy)){
        foreach(var hi in attackrange){
            if(hi.tag == "energy"){
                energy += Random.Range(2,6);
                Destroy(hi.gameObject);                
            }
        }        
        // if(Physics.CheckSphere(ringspawner.position , 0.3f , isenergy)){
        //     energy += 3;Random.Range
        // }
        rb.AddForce(-transform.up*downforce);
        // if(transform.position.y > currentYpos+0.2){
        //     transform.position = new Vector3(transform.position.x , currentYpos , transform.position.z); 
        // }
        if(ultcooldown <= 0){
            // ultcooltext.text = "";
            if(energy>=50){
                Image img = st.U1.GetComponent<Image>();
                var tempCol = img.color;
                tempCol.a = 1f;
                img.color = tempCol;
                // ulteffect.SetActive(true);
                if(Input.GetKeyUp(KeyCode.Q)){
                    tempCol.a = 0.2f;
                    img.color = tempCol;
                    Instantiate(ult , Qinitialpos.position , Quaternion.identity);
                    energy = 0;
                    // ulteffect.SetActive(false);
                    // ultslider.value = 0;
                    st.U1.fillAmount =0;
                    st.U1.transform.GetChild(1).SetAsFirstSibling();
                    st.u1once=true;
                    ultcooldown = resetult;
                }
                // ult.transform.position = Qinitialpos.position;
                // ult.transform.DOJump(Qfinalpos.position , 3f , 1 , 0.5f);
            }
        }
        // else{
            // ultcooldown -= Time.deltaTime;
            // ultcooltext.text = (Mathf.Round(ultcooldown * 10.0f) * 0.1f).ToString();
        // }

        if(cooldown<=0){
            if(Input.GetKeyUp(KeyCode.E)){
                drone.transform.position = droneinitialpos.position;
                drone.SetActive(true);
                // drone.transform.parent = dronefinalpos.transform;
                drone.transform.DOJump(dronefinalpos.transform.position , -2f , 1 , 0.5f);
                // Instantiate(drone , dronepos.position , dronepos.rotation);     
                st.E1.fillAmount =0;
                st.E1.transform.GetChild(0).SetSiblingIndex(1);
                st.e1once =true;           
                cooldown = cooldowntime;
                cooldownact = 10;
            }
        }
        // else{
        //     cooldown -= Time.deltaTime;
        //     if(cooldownact<=0){
        //         drone.SetActive(false);
        //         // drone.transform.position = dronepos.position;
        //         // Destroy(drone);
        //         cooldownact = 10;
        //     }else{
        //         // drone.transform.position = dronefinalpos.position;
        //         cooldownact -= Time.deltaTime;
        //     }
        // }
        
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
            anim.SetBool("run" , true);
            anim.SetBool("idle", false);
            anim.SetBool("runshoot" , false);
            currentmovingpos = new Vector3(transform.position.x , transform.position.y , transform.position.z);
            if(Input.GetMouseButton(0))
            {
                weap.SetActive(true);
                anim.SetBool("idle" , false);
                anim.SetBool("runshoot" , true);
                anim.SetBool("run" , false);
            }else
            {
                weap.SetActive(false);
            }
        }

        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            transform.position = currentmovingpos;
            //anim.SetTrigger("idle");
            anim.SetBool("idle" , true);
            anim.SetBool("runshoot" , false);
            anim.SetBool("run" , false);
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            Color color = txt.color;
            color.a = 1;
            txt.color = color;
        }
        if(Input.GetKey(KeyCode.LeftShift)){
            Color color = txt.color;
            color.a = 0.5f;
            txt.color = color;
            //crouch
            movespeed = 3;
            docrouch();
        }else
        {
            
            if(crouchforonce)
            {
                landobject.position = new Vector3(landobject.position.x , landobject.position.y + 3 , landobject.position.z);
            }
            crouchforonce = false;
            colider.size = currentcollidepos;
            colider.center = new Vector3(colider.center.x , 0.92f , colider.center.z);
            movespeed = 6;
            //rb.useGravity = true;
        }
        lookcam();
        //dash();
        spacering();
        myinput();
        slopedirection = Vector3.ProjectOnPlane(movedirection , slopehit.normal);
        // if(Physics.CheckSphere(transform.position))

    }
    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "bullet"){
            health -= 2;
            sliderhealth.value =health;
            healthtxt.text = health.ToString()+"/200";

        }
    }
    void spacering()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(ringammo > 0)
            {
                Instantiate(ringweapon , ringspawner.position , ringspawner.rotation);
                ringammo -= 1;
            }      
        }
    }
    void dash()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.forward * dashspeed , ForceMode.Impulse);
        }
    }
    void controldrag()
    {
        rb.drag = dragvalue;
    }
    public void lookcam()
    {
        Ray cameraray = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundplane = new Plane(Vector3.up , Vector3.zero);

        float raylen;

        if(groundplane.Raycast(cameraray , out raylen))
        {
            Vector3 pointtolook = cameraray.GetPoint(raylen);
            transform.LookAt(new Vector3(pointtolook.x , transform.position.y , pointtolook.z));
            //playerbody.LookAt(new Vector3(pointtolook.x , playerbody.position.y , pointtolook.z));
        }
    }

    void myinput()
    {
        horizontalmovement = Input.GetAxisRaw("Horizontal");
        verticalmovement = Input.GetAxisRaw("Vertical");

        movedirection = transform.forward * verticalmovement + transform.right * horizontalmovement;            
    }
    bool slopecal(){
        if(Physics.Raycast(transform.position  , Vector3.down , out slopehit , 3f)){
            if(slopehit.normal != Vector3.up){
                // slopedirection = Vector3.ProjectOnPlane(movedirection , slopehit.normal);
                return true;
            }else{
                return false;
            }
        }
        return false;
    }

    private void FixedUpdate() 
    {
        moveplayer();
    }
    
    void moveplayer()
    {
        if(slopecal()){
            // rb.useGravity = false;
            rb.AddForce(slopedirection * movespeed *movementmultiplier,ForceMode.Force);
        }else{
            // rb.useGravity=true;
            rb.AddForce(movedirection.normalized * movespeed * movementmultiplier, ForceMode.Acceleration);

        }
    }

    void docrouch()
    {
        if(!crouchforonce)
        {
            landobject.position = new Vector3(landobject.position.x , landobject.position.y - 3 , landobject.position.z);
            crouchforonce = true;
        }
        rb.useGravity = false;
        iscrouch = true;
        colider.size = new Vector3(colider.size.x, 0.8f , colider.size.z);
        colider.center = new Vector3(colider.center.x , 0.5f , colider.center.z);
    }
    // void newmyinput()
    // {
    //     horimove = Input.GetAxisRaw("Horizontal");
    //     vermove = Input.GetAxisRaw("Vertical");

    //     fordirection =  transform.forward * vermove ;
    //     rb.AddForce(fordirection * movespeed * movementmultiplier *Time.deltaTime, ForceMode.Acceleration);
    //     //rotate body by A & D keys
    //     transform.Rotate(0 , horimove * rotationspeed , 0);
    // }
}
