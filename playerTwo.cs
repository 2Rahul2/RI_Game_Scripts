using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;




public class playerTwo : MonoBehaviour
{
    public Animator anim;
    private string[] nameArr = {"attack1" , "attack2" , "attack3" , "run"};
    [SerializeField]
    private float[] attacktime = {};
    public int count = 0;
    private int mcount;

    [SerializeField]    
    private int movespeed , movementmultiplier;
    float horizontalmovement;
    float verticalmovement;
    public bool isanim=true;

    public bool attack=true;
    public bool Eattack=false;


    public float waitime;
    public float timewait;
    Vector3 movedirection;
    public Rigidbody rb;

    public Camera cam;

    [SerializeField]
    private GameObject Eskill;
    [SerializeField]
    private Transform Eskillpos;
    [SerializeField]
    private float eskilanimTime;
    
    public float Ecount=0 ,Ucount=0;

    private GameObject[] enemyObjects;
    private enehealth enemyHealth;

    public int energy;
    [SerializeField]
    private int requiredEnergy;
    private float startrun=0.3f;
    [SerializeField]
    private Transform[] ultpos;
    [SerializeField]
    public GameObject ultallpos;
    [SerializeField]
    private int ultspeed;
    private int ultposcount = 0;
    public bool ultcomplete= true;
    [SerializeField]
    private GameObject ulteffect;
    [SerializeField]
    private GameObject camfollow;
    public Collider[] enemyinrange;
    Collider coll;
    [SerializeField]
    private Transform attackpos;
    [SerializeField]
    private LayerMask isenemy;
    [SerializeField]
    private LayerMask energyLayer;
    public bool isattacked=false;
    public GameObject energyobj;
    public Transform energycollect;
    [SerializeField]
    private int dashspeed;
    public Transform dashtransform;

    [SerializeField]
    private float doubletaptime;
    public int tapcount=0;


    public int wtap=0;
    public int stap=0;
    public int atap=0;
    public int dtap=0;
    [SerializeField]
    private GameObject dasheffect;
    public GameObject slasheffectholder;
    [SerializeField] GameObject Defaultslasheffect1;
    [SerializeField] GameObject Iceslasheffect2;
    [SerializeField] GameObject Fireslasheffect3;
    [SerializeField]
    private Transform slasheffectpos1;
    [SerializeField]
    private Transform slasheffectpos2;
    public int swordcount;
    [SerializeField]
    private GameObject kickeffect;
    public Vector3 tempos;
    public int playerhealth=150;

    public bool iceattack;
    public bool fireattack;

    public float iceduration;
    public float fireduration;

    public int slashangle;
    public int slashdmg=3;
    public Transform sowrdposmele;
    public Transform lastswordpos;
    public skillstrack SkillTrack;


    public GameObject swordhit;


    public float angle1;
    public float angle2;
    public float angle3;

    public int health;
    public Slider sliderhealth;
    public TextMeshProUGUI healthtxt;
 
    // Start is called before the first frame update
    void Start()
    {
        slasheffectholder = Defaultslasheffect1;
        ultcomplete= true;
        movespeed =0;
        ulteffect.SetActive(false);
        waitime = timewait;
        mcount = count;
        coll = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.Play("idle");
    }
    void ult(){
        print("ult time");
        coll.enabled = false;
        rb.useGravity = false;
        transform.LookAt(new Vector3(ultpos[ultposcount].position.x , transform.position.y , ultpos[ultposcount].position.z));
        // rb.AddForce(transform.forward *ultspeed *Time.deltaTime, ForceMode.Impulse);
        transform.Translate(Vector3.forward *ultspeed*Time.deltaTime);
        if(Vector3.Distance(transform.position , ultpos[ultposcount].position)<3f){
            if(ultposcount == (ultpos.Length)-1){
                // foreach(var ene in enemyinrange){
                //     print(ene.name);
                //     try{
                //     ene.GetComponent<enehealth>().ultrigger =true;
                //     ene.GetComponent<enehealth>().ultcount =5;
                //     }catch{
                //         continue;
                //     }                    
                // }
                
                SkillTrack.fireattack = true;
                // fireattack= true;
                SkillTrack.fireduration = 8f;
                // fireduration = 8f;
                
                slashdmg = 28;
                slasheffectholder = Fireslasheffect3;
                ultposcount = 0;
                ultcomplete= true;
                ulteffect.SetActive(false);
                coll.enabled = true;
                rb.useGravity = true;

            camfollow.GetComponent<CinemachineVirtualCamera>().Follow = this.transform;
                camfollow.GetComponent<CinemachineVirtualCamera>().LookAt = this.transform;
                // 1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111
                
                ultallpos.transform.parent = this.transform;
              

            }else{
                print(ultposcount);
                ultposcount += 1;

                // foreach(var enemy in enemyinrange){
                //     print("hittt");
                //     print(enemy.name);
                //     // if(Vector3.Distance(new Vector3(tempos.x , tempos.y , tempos.z) , enemy.transform.position)<15){
                //         if(Random.Range(1,6) == 3){
                //             Instantiate(energyobj , enemy.transform.position , Quaternion.identity);
                //         }
                //         try{
                //             enemy.GetComponent<enehealth>().health -= slashdmg;
                //             enemy.GetComponent<enehealth>().updatehealth();
                //         }catch{
                //             continue;
                //         }
                //     // }
                // }
            }
        }
    }
    void dashtowards(){
            // transform.Translate(Vector3.forward *dashspeed*Time.deltaTime);
            rb.AddForce(dashtransform.forward * dashspeed , ForceMode.Impulse);
        
    }
  
    // Update is called once per frame
    private IEnumerator dashtime(float wait){
        yield return new WaitForSeconds(wait);
        if(wtap == 1 || stap == 1 ||atap==1||dtap==1){
            wtap = 0;
            stap = 0;
            atap = 0;
            dtap = 0;

        }
    }
    private IEnumerator dasheffectfalse(float wait){
        yield return new WaitForSeconds(wait);
        dasheffect.SetActive(false);
    }
    private IEnumerator s1(float swait){
        yield return new WaitForSeconds(swait);
        anim.applyRootMotion=false;
        if(swordcount == 1){
            GameObject slash = Instantiate(slasheffectholder ,slasheffectpos1.position , Quaternion.identity);
            slash.transform.Rotate(new Vector3(90 ,60-transform.rotation.y ,0));

        }else if(swordcount == 2){
            GameObject slash = Instantiate(slasheffectholder ,slasheffectpos2.position , Quaternion.identity);
            slash.transform.Rotate(new Vector3(-90 ,125 ,-180));
        }else if(swordcount == 3){
            Instantiate(kickeffect ,new Vector3(slasheffectpos1.position.x , slasheffectpos1.position.y , slasheffectpos1.position.z) , transform.rotation);
        }else if(swordcount == 4){
            GameObject slash = Instantiate(slasheffectholder ,sowrdposmele.position , sowrdposmele.rotation);
            slash.transform.Rotate(new Vector3(0,0 ,-280));
        }else if(swordcount == 5){
            GameObject slash = Instantiate(slasheffectholder ,lastswordpos.position , lastswordpos.rotation);
            slash.transform.Rotate(new Vector3(83 , -203 , 23.3f));
        }
        Collider[] attackrange = Physics.OverlapSphere(attackpos.position ,5f);
                        // if(Physics.SphereCastAll(attackpos.position,3f , transform.forward , out RaycastHit[] hit ,3 , isenemy)){
                        
                        foreach(var hi in attackrange){
                            if(hi.tag == "eskill"){
                                Instantiate(energyobj , hi.transform.position , Quaternion.identity);
                                try{
                                    hi.GetComponent<bosshealth>().updatehealth(slashdmg+10);
                                }catch{

                                }
                                try{
                                hi.GetComponent<enehealth>().health -= slashdmg;
                                hi.GetComponent<enehealth>().updatehealth();
                                Instantiate(swordhit , hi.transform.position , transform.rotation);


                                }catch{
                                    continue;
                                }
                            }
                            if(hi.tag =="build"){
                                hi.GetComponent<finalspawner>().health=0;
                            }
                        }
                        StopCoroutine("s1");
    }
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.W)){
        //     tapcount = 0;
        // }
        // if(wtap == 1){
        // }
        // if(iceattack && !fireattack){
        //     if(iceduration<=0){
        //         slashdmg=3;
        //         slasheffectholder = Defaultslasheffect1;
        //         iceattack=false;
        //     }else{
        //         iceduration -= Time.deltaTime;
        //     }
        // }
        // if(fireattack){
        //     if(fireduration<=0){
        //         slashdmg=3;
        //         slasheffectholder = Defaultslasheffect1;
        //         fireattack= false;
        //     }else{
        //         fireduration -= Time.deltaTime;
        //     }
        // }
        if(Input.GetKeyUp(KeyCode.C)){
            GameObject slash = Instantiate(slasheffectholder ,lastswordpos.position , lastswordpos.rotation);
            slash.transform.Rotate(new Vector3(angle1 ,angle2 ,angle3));
            // slash.transform.Rotate(new Vector3(lastswordpos.rotation.x , lastswordpos.rotation.y , lastswordpos.rotation.z));
            // swordcount = 1;
            // Instantiate(slasheffectholder ,slasheffectpos1.position , slasheffectpos1.rotation);
        }
        if(Input.GetKeyUp(KeyCode.V)){
            swordcount = 2;
            Instantiate(slasheffectholder ,slasheffectpos2.position , slasheffectpos2.rotation);
        }
        if(Input.GetKeyUp(KeyCode.W)){
            StartCoroutine("dashtime" , doubletaptime);
            wtap += 1;
            if(wtap == 2){
                dasheffect.SetActive(true);
                StartCoroutine("dasheffectfalse" , 0.4f);
                rb.AddForce(transform.forward * dashspeed , ForceMode.Impulse);
                StopCoroutine(dashtime(doubletaptime));
                wtap = 0;
            }
        }
        if(Input.GetKeyUp(KeyCode.S)){
            StartCoroutine("dashtime" , doubletaptime);
            stap += 1;
            if(stap == 2){
                dasheffect.SetActive(true);
                StartCoroutine("dasheffectfalse" , 0.4f);
                StopCoroutine(dashtime(doubletaptime));

                rb.AddForce(-transform.forward * dashspeed , ForceMode.Impulse);
                stap = 0;
            }
        }
        if(Input.GetKeyUp(KeyCode.A)){
            StartCoroutine("dashtime" , doubletaptime);
            atap += 1;
            if(atap == 2){
                dasheffect.SetActive(true);
                StartCoroutine("dasheffectfalse" , 0.4f);
                StopCoroutine(dashtime(doubletaptime));

                rb.AddForce(-transform.right * dashspeed , ForceMode.Impulse);
                atap = 0;
            }
        }if(Input.GetKeyUp(KeyCode.D)){
            StartCoroutine("dashtime" , doubletaptime);
            dtap += 1;
            if(dtap == 2){
                dasheffect.SetActive(true);
                StartCoroutine("dasheffectfalse" , 0.4f);
                StopCoroutine(dashtime(doubletaptime));

                rb.AddForce(transform.right * dashspeed , ForceMode.Impulse);
                dtap = 0;
            }
        }
        
        // if(tapcount == 1 && doubletaptime<=0){
        //     tapcount = 0;
        //     doubletaptime = 0.4f;
        // }else{
        //     doubletaptime -= Time.deltaTime;
        // }
        // if(Input.GetKeyUp(KeyCode.F)){
        //     attack = true;
        //     count = 1;
        // }
        // if(Physics.CheckSphere(attackpos.position , 1f , energyLayer)){
        //     print("energyy");
        //     energy += 3;
        // }
        Collider[] energyrange = Physics.OverlapSphere(energycollect.position ,0.3f);
                        // if(Physics.SphereCastAll(attackpos.position,3f , transform.forward , out RaycastHit[] hit ,3 , isenemy)){
                        
        foreach(var enehit in energyrange){
            if(enehit.tag == "energy"){
                energy += 3;
                Destroy(enehit.gameObject);                                                                
            }
        }
        if(energy>= requiredEnergy && Ucount<=0){   
                if(Input.GetKeyUp(KeyCode.Q)){
                    tempos= transform.position;
                    Collider[]  enemyinrange =Physics.OverlapSphere(new Vector3(tempos.x , tempos.y , tempos.z) , 15f , isenemy);
                    print("detect");
                    foreach(var ene in enemyinrange){
                        print(ene.name);
                        try{
                        ene.GetComponent<enehealth>().ultrigger =true;
                        ene.GetComponent<enehealth>().ultcount =5;
                        ene.GetComponent<enehealth>().ulttime =2.5f;
                        }catch{
                            continue;
                        }                    
                    }
                    // enemyinrange = GameObject.FindGameObjectsWithTag("eskill");
                    // camfollow.GetComponent<CinemachineVirtualCamera>().Follow = null;
                    camfollow.GetComponent<CinemachineVirtualCamera>().LookAt = null;
                    // 1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111
                    ultpos[(ultpos.Length)-1].position = transform.position;
                    ultallpos.transform.position = transform.position;
                    ultallpos.transform.parent = null;
                    ultcomplete=false;
                    energy = 0;
                    SkillTrack.U2.fillAmount = 0;
                    SkillTrack.U2.transform.GetChild(0).SetSiblingIndex(1);
                    SkillTrack.u2once=true;
                    Ucount = 12;
                }
            
        }
        if(ultcomplete==false){
            print("collider");
            ulteffect.SetActive(true);
            ult();
        }else{
            lookcam();
            myinput();
            if(Ecount<=0){
                if(Input.GetKeyUp(KeyCode.E)){
                    anim.applyRootMotion=false;
                    if(!SkillTrack.fireattack){
                        slashdmg =5;
                        slasheffectholder = Iceslasheffect2;
                        SkillTrack.iceduration =15f;
                        SkillTrack.iceattack = true;
                    }
                    StartCoroutine("Eattacktime" , 1);
                    Eattack = true;
                    movespeed =0;
                    count=0;
                    waitime = 0;

                    anim.SetBool("run" ,false);
                    anim.SetBool("attack1" ,false);
                    anim.SetBool("atttack2" ,false);
                    anim.SetBool("attack3" ,false);
                    anim.SetBool("eskill" ,true);
                    StartCoroutine("WaitAndPrint" , eskilanimTime);
                    Instantiate(Eskill , Eskillpos.position , Eskillpos.rotation);
                    SkillTrack.E2.fillAmount = 0;
                    SkillTrack.E2.transform.GetChild(0).SetSiblingIndex(1);
                    SkillTrack.e2once=true;
                    Ecount =5;
                    // Destroy(Eskill , 1.5f);
                }
            }
        }
       
        if((Input.GetKey(KeyCode.W) ||Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D)) && attack==false&&Eattack==false){
            // tapcount = 0;
            movespeed =10;
            waitime = 0;
            count=0;
            anim.SetBool("attack1" , false);
            anim.SetBool("atttack2" , false);
            anim.SetBool("attack3" , false);
            anim.SetBool("attack4" , false);
            anim.SetBool("attack5" , false);
            anim.SetBool("eskill" ,false);
            anim.SetBool("run" ,true);

        }else if(attack==false && Eattack==false){
            anim.SetBool("attack1" , false);
            anim.SetBool("atttack2" , false);
            anim.SetBool("attack3" , false);
            anim.SetBool("attack5" , false);
            anim.SetBool("attack4" , false);
            anim.SetBool("eskill" ,false);
            anim.SetBool("run" ,false);
            anim.Play("idle");
        }
                if(waitime <= 0){
                    if(startrun<=0){
                        count = 0;
                        attack=false;
                        startrun = 0.3f;
                    }else{
                        startrun -= Time.deltaTime;
                        // attack=true;
                    }
                    if(Input.GetKeyUp(KeyCode.F)){
                        startrun = 0.3f;
                        attack=true;
                        movespeed = 0;
                        anim.SetBool("run" , false);
                        anim.SetBool("attack4" , false);
                        anim.SetBool("attack5" , false);
                        anim.SetBool("atttack2" , false);
                        anim.SetBool("attack3" , false);
                        anim.SetBool("attack1" , false);
                        // if(Physics.CheckSphere(attackpos.position , 3f , isenemy)){
                        // RaycastHit[] hit = Physics.SphereCast(attackpos.position , 3f , transform.forward)
                        
                        //     print("attacked");
                        //     isattacked = true;
                        // }
                        count += 1;
                        if(count>=6){
                            count = 1;
                        }
                        if(count==1){
                            attack1();
                            swordcount = 1;
                            StartCoroutine("s1" ,0.2f);
                            // Instantiate(slasheffect1 ,slasheffectpos1.position , slasheffectpos1.rotation);
                        }else if(count == 2){
                            swordcount = 2;
                            StartCoroutine("s1" ,0.2f);
                            // Instantiate(slasheffect1 ,slasheffectpos2.position , slasheffectpos2.rotation);
                            attack2();
                        }else if(count == 3){
                            attack3();
                            swordcount = 3;
                            StartCoroutine("s1" , 0.2f);
                        }else if(count == 4){
                            attack4();
                            swordcount = 4;
                            StartCoroutine("s1" , 0.18f);
                        }else if(count == 5){
                            attack5();
                            anim.applyRootMotion = true;
                            swordcount = 5;
                            StartCoroutine("s1" , 0.5f);
                        }
                        waitime = attacktime[count];
                    }
                }else{
                    waitime -= Time.deltaTime;
                }
            
        // if(attack){
        // }
        
                // anim.Play(nameArr[count]);
                // if(Input.GetKey(KeyCode.W)&&attack==false){
                //     print("heeheheh");
                //     count = 4;
     
                // }else if(attack)                
                //     if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 ){        
                //     movespeed = 0;
                //         waitime = timewait;
                //         StopCoroutine("WaitAndPrint");
                //     }else{
                  
                //         print("hello");
                //         if(Input.GetKeyUp(KeyCode.F)){
                //             attack = true;
                //             isanim = true;
                //             count +=1;
                //             print(count);
                //             if(count>3){
                //                 count = 1;
                //             }
                //         }
                //         StartCoroutine("WaitAndPrint" , waitime);                                                                      
                //     }
                // }else{                                        
                //     count = 0;
                // }            
        // lookcam();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "bullet"){
            health -= 2;
            updateHP();
        }
    }
    public void updateHP(){
            sliderhealth.value =health;
            healthtxt.text = health.ToString()+"/500";
    }
    void attack5(){
        anim.SetBool("attack4" , false);anim.SetBool("attack5" ,true);
    }
    void attack4(){
        anim.SetBool("attack3" , false);anim.SetBool("attack4" , true);
    }
     void attack1(){
        anim.SetBool("run" ,false);anim.SetBool("attack3" ,false);anim.SetBool("atttack2" ,false);anim.SetBool("attack1" ,true);
    }
    void attack2(){
        anim.SetBool("run" ,false);anim.SetBool("attack1" ,false);anim.SetBool("attack3" ,false);anim.SetBool("atttack2" ,true);
    }
    void attack3(){
        anim.SetBool("run" ,false);anim.SetBool("attack1" ,false);anim.SetBool("atttack2" ,false);anim.SetBool("attack3" ,true);
    }

    void eanim(){
        anim.SetBool("run" ,false);anim.SetBool("attack1" ,false);anim.SetBool("atttack2" ,false);anim.SetBool("attack3" ,false);anim.SetBool("eskill" ,true);
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
        
        // anim.Play("run");
        horizontalmovement = Input.GetAxisRaw("Horizontal");
        verticalmovement = Input.GetAxisRaw("Vertical");

        movedirection = transform.forward * verticalmovement + transform.right * horizontalmovement;            
    }
    private void FixedUpdate() 
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            print("dashed");
            dashtowards();
        }
        moveplayer();
    }
    
    void moveplayer()
    {
        rb.AddForce(movedirection.normalized * movespeed * movementmultiplier, ForceMode.Acceleration);
    }
    private IEnumerator Eattacktime(float waitforit){

        yield return new WaitForSeconds(waitforit);
        enemyObjects = GameObject.FindGameObjectsWithTag("eskill");
        foreach(GameObject trackenemy in enemyObjects){
            if(Vector3.Distance(transform.position , trackenemy.transform.position)<11.5){
                Instantiate(energyobj ,trackenemy.transform.position , Quaternion.identity);
                try{
                    enemyHealth= trackenemy.GetComponent<enehealth>();
                    enemyHealth.health -= 8;
                    enemyHealth.updatehealth();
                }catch{
                    continue;
                }
            }
        }
    }
    private IEnumerator WaitAndPrint(float waitTime)
    {         
        yield return new WaitForSeconds(waitTime);
        
        anim.SetBool("eskill" , false);
        // movespeed =5;
        Eattack=false;      
    }
}
