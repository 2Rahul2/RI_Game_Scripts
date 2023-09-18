using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class boss : MonoBehaviour
{
    public Transform playerpos;
    private NavMeshAgent ai;
    public float attackdistance;
    //walking variables
    public int walkspeed;
    private bool isleft;
    private bool iswalk;
    public float walktime;
    public int walkdistance;

    //kicking variables
    private bool iskick;
    public float kickduration;
    public float runspeed;
    public float rundistance;
    public bool reachedplayer;
    public int rpRadius;
    public LayerMask isplayer;
    public bool kickonce;

    public Transform[] kickpos;
    private bool kick1hit;

    //jump variables
    public float gravity;
    public float h;
    public Transform jumppositon;

    //spinyy brr brrr attack
    //public cubeattack cube;
    public float spintime;
    public bool isrot;
    public int isrota;

    private bool startspinagain;
    private float waittorunnext;
    private float runspinmethod;
    //random variables
    private bool idle;
    private Rigidbody rb;
    private Animator animator;

    //machine gun variables
    public bool ishoot;
    public float timeforshoot;
    private float resetshoot;

    //missile variables
    public LayerMask solidobject;
    public GameObject missile;
    public Transform missilepos;

    public Transform missilefirepoint;
    public float missilespeed;
    private bool missileonplayer;
    private bool runmissileonce;
    private bool runforever;
    public float followplayertime;
    public float missileduration;
    public player Playerobj;
    public int shieldhealth;
    [SerializeField]
    private float standstilltime;
    public Transform[] dronePos;
    public GameObject[] drone;
    private bool dronebool;
    
    public trackdroneattackpos dronepos;
    private float dronetime = 2f;
    public GameObject droneforcefieldmarker;
    public Transform groundpos;
    
    

    // jump
    private bool isjump;
    [SerializeField] private bool isair;
    private RaycastHit hit;
    public GameObject[] jumphiteffect;
    public float waitforjump = 3f;
    private int numberofjump=1;

    public switchplayer choosePlayer;
    public GameObject CurrentPlayer;
    public Transform[] spincubePosition;
    public GameObject spincubeObject;
    private bool boolspinattackonce=true;
    public GameObject stuneffect;
    public GameObject hiteffect;

    public GameObject gun;
    // private void jump();
    private bool droneattackonce;
    private List<System.Action> move = new List<System.Action>();
    private int[] movenumberset = {0,1,3,5,7,9,11};
    private int moveindexpos=1;
    private int previousmoveindexpos;
    private bool shootmissileonce=true;private bool shootmissiletwice=true;
    // public List<Action> moves;
    // public Transform dronepospos;
    public GameObject kickstartVFX;
    private float gobackdurationinKick;
    public float resetgobacksuration;
    private bool VFXisChild;
    private bool lookatplayer=true;
    public AudioSource AS;
    public AudioClip kick1 , kick2 , jumpImpact , missileSFX , missileImpact;
    public GameObject play1 ,play2;
    // AUDIO CLIPSS

    
    void Start()
    {
        AS = GetComponent<AudioSource>();
        gobackdurationinKick = resetgobacksuration;
        if(choosePlayer.playernum == 1){
            CurrentPlayer = choosePlayer.player1.transform.GetChild(0).gameObject;
        }else{
            CurrentPlayer = choosePlayer.player2.transform.gameObject;
        }
        // vfx = GetComponent<VisualEffect>();
        kickonce = false;
        runmissileonce = false;
        resetshoot = timeforshoot;
        //ishoot = true;
        startspinagain = false;
        waittorunnext = 0.5f;
        rb = GetComponent<Rigidbody>();
        ai = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        idle = false;
        iswalk = true;
        runspinmethod = 0f;
        move.Add(jump);//0
        move.Add(resetshootfun);//1
        move.Add(()=> shoot(true , resetshoot));//2
        move.Add(resetmissile);//3
        move.Add(()=>initialMis(true));//4
        move.Add(resetspinmove);//5
        move.Add(initailaziespinmovement);//6
        move.Add(resetkick);//7
        move.Add(kickboy);//8
        move.Add(resetsidewalk);//9
        move.Add(sidewalk);//10
        move.Add(resetdrone);//11
        move.Add(droneattack);//12
        //cube = GetComponent<cubeattack>();
    }
    //idle
    //jump
    //run and kick twice
    //missile
    //machine gun attack
    //spin boy
    //side walk
    // Update is called once per frame
    void Update()
    {
        if(choosePlayer.playernum == 1){
            CurrentPlayer = choosePlayer.player1.transform.GetChild(0).gameObject;
        }else{
            CurrentPlayer = choosePlayer.player2.transform.gameObject;
        }
        // missile.SetActive(false);
        if(shieldhealth<=0){
            if(standstilltime<=0){
                shieldhealth = 100;
                standstilltime = 15f;
            }else{
                standstilltime -= Time.deltaTime;    
                // initialMis();

            }
            // do Nothing
        }else{
            // if(Physics.CheckSphere)
            
            // initailkick();
            // kickboy();
            // initailaziespinmovement();
            // shoot();
            // if(Input.GetKeyUp(KeyCode.B)){
            // // initialMis();
            //     // boolspinattackonce = true;
            //     }
                // jump();      
            move[moveindexpos]();
            
            // StartCoroutine("WaitForNextMove", 0.4f);      
            // droneattack();
            // sidewalk();
            if(lookatplayer){
                transform.LookAt(new Vector3(CurrentPlayer.transform.position.x , transform.position.y , CurrentPlayer.transform.position.z));
            }

        }
        // kickboy();
        // if(animator.GetCurrentAnimatorStateInfo(0).IsName("idelboy")){
        //     print("bruhh");
        // }
        // shoot();
        //initialize the brain machanic
        // initailaziespinmovement();       

        //idle can take 3secs
        //kick can occur after every 3 stages
        //spin attack after 4 stages
        //machine attack after 2 stage
        //side walk after 3 stages
        //side walk and kick are random
        //missile after 5 stages

        // if(idle)
        // {
        //     //ilde animation
        // }else
        // {
        //     // if(iskick)
        //     // {
        //     //     runkick();
        //     // }
        //     if(iswalk){
        //         sidewalk();
        //     }
        // }
        // initialMis();
    }

    public void playerdmage(int dmg){
        if(choosePlayer.playernum==1){
            play1.GetComponent<decPlayerHealth>().updatehealth(dmg);
            // CurrentPlayer.
        }else{
            play2.GetComponent<decPlayerHealth>().updatehealth(dmg);
            // CurrentPlayer
        }
    }
    private IEnumerator WaitForNextMove(float wait){
        yield return new WaitForSeconds(wait);
        // moveindexpos =7;
        moveindexpos = movenumberset[Random.Range(0,movenumberset.Length)];
        StopCoroutine("WaitForNextMove");
    }
    private IEnumerator waitjumptime(float wait){
        yield return new WaitForSeconds(wait);
                if(Physics.Raycast(groundpos.position , -transform.up , 0.5f)){
                    print("jumping");
                        ai.enabled = false;
                        transform.DOJump(CurrentPlayer.transform.position , 7f , 1,1f);
                        isjump= true;
                }
                if(!Physics.Raycast(groundpos.position , -transform.up , 0.5f)){
                    isair = true;isjump= false;
                    waitforjump = 3f; 
                }
    }
    private IEnumerator waitforjumpanimation(float wait){
        yield return new WaitForSeconds(wait);
        animator.Play("idelboy");

    }
    void jump(){
        if(numberofjump<=3){
            if(waitforjump<=0){
                animator.Play("jump");
                StartCoroutine("waitjumptime" , 0.4f);

            }else{
                if(Physics.Raycast(groundpos.position , -transform.up , 0.4f)){
                    isjump = true;
                }
                if(isjump && isair){
                    if(Physics.CheckSphere(groundpos.position , 18f ,isplayer )){
                        playerdmage(25);
                        if(choosePlayer.playernum==1){
                            CurrentPlayer.GetComponent<player>().rb.AddForce(transform.forward * 12*5 , ForceMode.Impulse);
                        }else{
                            CurrentPlayer.GetComponent<playerTwo>().rb.AddForce(transform.forward * 12*5 , ForceMode.Impulse);
                        }
                        // Playerobj.rb.AddForce(transform.forward * 12*5 , ForceMode.Impulse);
                    }
                        AS.clip = jumpImpact;
                        AS.Play();
                    Instantiate(jumphiteffect[0] , groundpos.position , Quaternion.identity);
                    Instantiate(jumphiteffect[1] , groundpos.position , Quaternion.identity);
                    Instantiate(jumphiteffect[2] , groundpos.position , Quaternion.identity);

                    numberofjump += 1;
                    isjump=false;isair = false;
                    ai.enabled=true;
                    
                    StartCoroutine("waitforjumpanimation" ,0.5f);
                }waitforjump -= Time.deltaTime;
            }
        }else{
            animator.Play("idelboy");
            StartCoroutine("WaitForNextMove", 0.4f);
        }
    }
    void resetdrone(){
        droneattackonce = true;dronebool=true;
        moveindexpos += 1;
    }
    void droneattack(){
        if(droneattackonce){
           
                // dronepos.SetActive(true);
                GameObject mydronemarker = Instantiate(droneforcefieldmarker , new Vector3(transform.position.x , transform.position.y -2f , transform.position.z) ,Quaternion.identity);
                // droneforcefieldmarker.SetActive(true);
                dronepos.mymarker = mydronemarker; 
                dronepos.runscript = true;
                dronebool = true;   
                // GameObject mydronepos = Instantiate(dronepos , dronepospos.position , transform.rotation);
                // mydronepos.transform.parent = this.transform;
            
            if(dronebool){
                for(int i = 0;i<dronePos.Length;i++){
                    GameObject mydrone = Instantiate(drone[i] , transform.position , transform.rotation);
                    mydrone.GetComponent<enemyDrone>().finalposition.x = dronePos[i].position.x;
                    mydrone.GetComponent<enemyDrone>().finalposition.y = dronePos[i].position.y;
                    mydrone.GetComponent<enemyDrone>().finalposition.z = dronePos[i].position.z;
                    mydrone.GetComponent<enemyDrone>().indexpo = i;
                    mydrone.transform.parent = this.transform;
                    // mydrone.transform.position =  Vector3.Lerp(transform.position , dronePos[i].position , 1*Time.deltaTime);;
                    if(i==dronePos.Length-1){
                        droneattackonce =false;
                        dronebool=false;
                    }
                    // drone[i].GetComponent<enemyDrone>().finalposition.position = dronePos[i].position;
                }
            }
            StartCoroutine("WaitForNextMove", 10f);

        }
            
        // foreach(var droneObj in drone){
        // }
    }
    void shieldDestroy(){
        if(shieldhealth<=0){
            
        }
    }
    private IEnumerator missilewait(float wait){
        yield return new WaitForSeconds(wait);
        if(shootmissiletwice){
            gun.SetActive(true);
            AS.clip = missileSFX;
            AS.Play();
            Instantiate(missile , missilefirepoint.position , missilefirepoint.rotation);
            shootmissiletwice=false;
        }
            // missile.SetActive(true);
            // missile.GetComponent<missile>().followtime = 2f;
            // missile.transform.position = new Vector3(missilefirepoint.position.x , missilefirepoint.position.y , missilefirepoint.position.z);
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("missile" , false);
        animator.SetBool("idle" , true);    
        gun.SetActive(false);
        // StartCoroutine("WaitForNextMove", 0.4f);
        StartCoroutine("WaitForNextMove", 5f);

        StopCoroutine("missilewait");

    }
    void resetmissile(){
        shootmissileonce = true;
        shootmissiletwice=true;
        moveindexpos += 1;
    }
    void initialMis(bool shootmissile){
        if(shootmissileonce){
            // if(Input.GetKeyUp(KeyCode.B)){
            // animator.Play("missile");
            animator.SetBool("missile" , true);
            animator.SetBool("idle" , false);

            StartCoroutine("missilewait" , 1.4f);
            shootmissileonce = false;
            
        }
            // missile.GetComponent<missile>().playerpos = CurrentPlayer.transform;

    }
    void initailmissile()
    {
        if(Input.GetKeyUp(KeyCode.B))
        {
            runmissileonce = true;
            runforever = true;
            missileduration = 5f;
            followplayertime = 2f;
        }
        if(runforever)
        {
            if(missileduration <= 0)
            {
                missileduration = 5f;
                followplayertime = 2f;
                runmissileonce = false;
                runforever = false;
                missile.SetActive(false);
            }else{
                missilelaunch();
                missilespeed = 10;

                missileduration -= Time.deltaTime;
            }
        }
    }
    void missilelaunch()
    {
        missilepos.LookAt(new Vector3(playerpos.position.x , missilepos.position.y , playerpos.position.z));
        if(runmissileonce)
        {
            missile.SetActive(true);
            missilepos.position = new Vector3(missilefirepoint.position.x , missilefirepoint.position.y , missilefirepoint.position.z);
            runmissileonce = false;
        }

        if(runforever)
        {
            missilepos.position = Vector3.MoveTowards(missilepos.position , playerpos.position , missilespeed * Time.deltaTime);
            missileonplayer = Physics.CheckSphere(missilepos.position , 0.1f , isplayer);
            missileonplayer = Physics.CheckSphere(missilepos.position , 0.1f , solidobject);

            if(missileonplayer)
            {
                missilespeed = 5;
                missile.SetActive(false);
                runforever = false;
            }
        }
    }
    void resetshootfun(){
        moveindexpos += 1;
        ishoot = true;
        timeforshoot= resetshoot;
    }
    public void shoot(bool shootbool , float shootime)
    {
        if(ishoot)
        {
            // print(timeforshoot);
            if(timeforshoot <= 0)
            {
                gun.SetActive(false);
                animator.SetBool("shoot" , false);
                gun.SetActive(false);
                StartCoroutine("WaitForNextMove", 2f);
                // timeforshoot = resetshoot;
                ishoot=false;
                shootbool= false;
            }else
            {
                gun.SetActive(true);
                // ishoot = true;
                animator.SetBool("shoot", true);
                timeforshoot -= Time.deltaTime;
            }
        }
    }
    void resetspinmove(){
        boolspinattackonce = true;
        startspinagain = true;isrot=false;isrota=4;
        moveindexpos += 1;
    }
    void initailaziespinmovement()
    {
        if(boolspinattackonce){

            animator.SetBool("spawncube" , true);
            animator.SetBool("idle" , false);
            animator.Play("spawncube");
            foreach(var cubeobj in spincubePosition){
                GameObject mycube = Instantiate(spincubeObject , new Vector3(cubeobj.position.x + Random.Range(-40,40), cubeobj.position.y + Random.Range(0,10), cubeobj.position.z + Random.Range(-40,40)) , cubeobj.rotation);
                mycube.GetComponent<cubeattack>().bospos = cubeobj;
            }
            boolspinattackonce =false;
        }
        if(runspinmethod < 7){
            spinattack();
            runspinmethod += Time.deltaTime;
        }
        
        if(Input.GetKeyUp(KeyCode.B)){
            boolspinattackonce = true;
            startspinagain= true;
        }
        if(isrota ==0 ){
            animator.SetBool("cubeattack" , true);
            animator.SetBool("spawncube" , false);
            // animator.SetBool("cubeattack" , true);
            // animator.Play("cubeattack");
        }
        if(isrota == 3){
            // animator.StopPlayback()
            // animator.Play("idelboy");
            animator.SetBool("cubeattack" , false);
            animator.SetBool("idle" , true);
            StartCoroutine("WaitForNextMove", 5f);
        }
        if(startspinagain){
            isrota = 4;
            if(waittorunnext <= 0){
                isrota = 6;
                spintime = 6f;
                runspinmethod = 0f;
                waittorunnext = 0.5f;
                // animator.Play("idelboy");
                startspinagain = false;
            }else{
                waittorunnext -= Time.deltaTime;
            }
        }

    }
    // void runkick()
    // {
    //     if(Vector3.Distance(transform.position , playerpos.position) > rundistance)
    //     {
    //         reachedplayer = Physics.CheckSphere(transform.position , rpRadius , isplayer);
    //         if(!reachedplayer)
    //         {
    //             ai.destination = playerpos.position;
    //         }else
    //         {
    //             if(kickduration<=0)
    //             {
    //                 iskick = false;
    //                 kickduration = 3f;
    //             }else
    //             {
    //                 ai.destination = transform.position;
    //                 kickduration -= Time.deltaTime;
    //             }
    //             //kick animation twice
    //         }         
    //     }else{
    //         iswalk = true;
    //     }
    // }

    void initailkick()
    {
        if(Vector3.Distance(transform.position , CurrentPlayer.transform.position) > rundistance)
        {
            iskick = true;
        }
    }
    private IEnumerator waitforkick(float wait){
        yield return new WaitForSeconds(wait);

                // animator.SetBool("run" , true);
        iskick=true;
        moveindexpos +=1;
        StopCoroutine("waitforkick");

    }
    void resetkick(){
        kickduration = 2f;
        // iskick= true;
        if(gobackdurationinKick <= 0){
            lookatplayer=true;
            if(!iskick){
                kickstartVFX.GetComponent<visual>().vfx.Stop();
                transform.position = new Vector3(CurrentPlayer.transform.position.x -3 ,transform.position.y , CurrentPlayer.transform.position.z);
                ai.enabled=true;
                animator.SetBool("run" , true);animator.SetBool("walkback" ,false);
                
                StartCoroutine("waitforkick" , 0.2f);
                iskick=true;
            }
        }else{
            lookatplayer=false;
            ai.enabled=false;
            if(!VFXisChild){
                // kickstartVFX.transform.parent = this.transform;
                kickstartVFX.transform.rotation = transform.rotation;

                // kickstartVFX.transform.LookAt(new Vector3(0 , CurrentPlayer.transform.position.y , 0));
                kickstartVFX.transform.position = new Vector3(dronePos[3].position.x , kickstartVFX.transform.position.y , dronePos[3].position.z);
                // kickstartVFX.transform.position.x = kickstartVFX.transform.position.x-4;
                kickstartVFX.GetComponent<visual>().vfx.Play();
                VFXisChild=true;
            }
            else{
                kickstartVFX.transform.position = kickstartVFX.transform.position;
                // kickstartVFX.transform.parent = null;
            }
            gobackdurationinKick -= Time.deltaTime;
            animator.SetBool("walkback" ,true);animator.SetBool("idle" , false);
            // transform.position -= Vector3.forward * 1.5f*Time.deltaTime;
            // transform.position = Vector3.Lerp(transform.position , new Vector3(transform.position.x-3  , transform.position.y , transform.position.z -3) , 1.2f *Time.deltaTime);
        }
        // moveindexpos += 1;

    }
    void enablePlayer(bool isenable){
        if(choosePlayer.playernum == 1){
            CurrentPlayer.GetComponent<player>().enabled = isenable;
        }else{
            CurrentPlayer.GetComponent<playerTwo>().enabled = isenable;
        }
        CurrentPlayer.GetComponent<Animator>().enabled = isenable;
    }
    void kickboy()
    {
        // if(Vector3.Distance(transform.position , CurrentPlayer.transform.position) > rundistance)
        // {
        //     iskick = true;
        // }
        if(iskick)
        {
            reachedplayer = Physics.CheckSphere(transform.position , rpRadius , isplayer);
            if(!reachedplayer)
            {
                // animator.SetBool("idle" , false);
                // animator.SetBool("run" , true);

                // animator.SetBool("kick1" , false);
                    // animator.SetBool("kick2" , false);
                    animator.SetBool("run" ,false);
                    animator.SetBool("idle", true);
                    print("reached");
                VFXisChild=false;gobackdurationinKick =resetgobacksuration;
                StartCoroutine("WaitForNextMove", 2f);
                iskick=false;

                        // transform.DOJump(CurrentPlayer.transform.position , 7f , 1,2f);
                // transform.position = CurrentPlayer.transform.position;
                // transform.position = new Vector3(CurrentPlayer.transform.position.x -8 ,transform.position.y , CurrentPlayer.transform.position.z);
                ai.destination = transform.position;

                // ai.destination = CurrentPlayer.transform.position;
            }else
            {
                transform.position = Vector3.Lerp(transform.position , new Vector3(CurrentPlayer.transform.position.x -3 ,transform.position.y , CurrentPlayer.transform.position.z) , 1.8f*Time.deltaTime);
                
                // if(choosePlayer.playernum==1){
                //                     // CurrentPlayer.GetComponent<player>()
                //                     CurrentPlayer.GetComponent<player>().enabled=false;
                //                 }else{
                //                     CurrentPlayer.GetComponent<playerTwo>().enabled=false;
                // }
                // CurrentPlayer.GetComponent<Animator>().enabled=false;
                enablePlayer(false);
                
                animator.SetBool("run" , false); 
                if(kickduration <= 0)
                {
                    print("goingtoexit");
                    animator.SetBool("kick1" , false);
                    animator.SetBool("kick2" , false);
                    
                    animator.SetBool("idle", true);
                    // print("kick completed");
                    // CurrentPlayer.GetComponent<Animator>().enabled=true;

                    // if(choosePlayer.playernum==1){
                    //     CurrentPlayer.GetComponent<player>().enabled=true;
                    // }else{
                    //     CurrentPlayer.GetComponent<playerTwo>().enabled=true;
                    // }
                    enablePlayer(true);
                    kickduration = 2f;
                    gobackdurationinKick = resetgobacksuration;VFXisChild=false;
                    StartCoroutine("WaitForNextMove", 2f);
                    kickonce = false;
                    iskick = false;

                }else{
                    kickduration -= Time.deltaTime;    
                    ai.destination = transform.position;
                    animator.SetBool("kick1" , true);
                    if(animator.GetCurrentAnimatorStateInfo(0).IsName("firstkick") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f){
                        reachedplayer = Physics.CheckSphere(transform.position , rpRadius , isplayer);
                        if(reachedplayer){
                            kick1hit = Physics.CheckSphere(kickpos[0].position , 4 , isplayer);
                            if(kick1hit){
                                if(!kickonce){
                                    AS.clip = kick1;
                                    AS.Play();
                                    playerdmage(20);
                                Instantiate(hiteffect ,new Vector3(CurrentPlayer.transform.position.x , CurrentPlayer.transform.position.y +3 , CurrentPlayer.transform.position.z) , Quaternion.identity);
                                GameObject stuneff = Instantiate(stuneffect , CurrentPlayer.transform.position , Quaternion.identity);
                                    // Playerobj.GetComponent<player>().hipo -= 10;
                                    // Destroy(stuneff);
                                    // print("huhu");
                                    animator.SetBool("kick1", false);
                                    kickonce = true;
                                }
                            }
                        }else{
                            print("istriggering");
                            enablePlayer(true);
                            StartCoroutine("WaitForNextMove", 1f);
                            animator.SetBool("Kickk1" , false);
                            animator.SetBool("idle", true);
                            iskick=false;
                        }
                    }else{                    
                        animator.SetBool("kick2" , true);
                        if(Physics.CheckSphere(kickpos[1].position , 3 , isplayer)){
                            if(kickonce){
                                playerdmage(25);
                                AS.Stop();                                
                                AS.Play();
                                Instantiate(hiteffect ,new Vector3(CurrentPlayer.transform.position.x , CurrentPlayer.transform.position.y +3 , CurrentPlayer.transform.position.z) , Quaternion.identity);
                                // if(choosePlayer.playernum==1){
                                //     // CurrentPlayer.GetComponent<player>()
                                //     CurrentPlayer.GetComponent<player>().rb.AddForce(transform.forward * 12*3 , ForceMode.Impulse);
                                // }else{
                                //     CurrentPlayer.GetComponent<playerTwo>().rb.AddForce(transform.forward * 12*3 , ForceMode.Impulse);
                                // }
                                // Playerobj.GetComponent<player>().hipo -= 10;
                                // print("secndhuh");
                                kickonce = false;
                            }
                        }
                        //animator.SetBool("kick1" , false);
                    }
                    


                    // if(animator.GetCurrentAnimatorStateInfo(0).IsName("seckick"))
                    // {
                    //     animator.SetBool("kick1" , false);
                    //     Debug.Log("lolol");
                    // }else{
                    //     animator.SetBool("kick1" , true);
                    //     animator.SetBool("kick2" , false);
                    // }
                    //animator.SetBool("kick1" , true);
                    // print("kick1");
                    // print("kick2");
                }
                
            }
        }
    }


    void spinattack()
    {
        // if(spintime <= 0)
        // {
        //     cube.isrotate = false;
        //     cube.attack = true;
        // }else{
        //     spintime -= Time.deltaTime;
        //     cube.attack = false;
        //     cube.isrotate = true;
        //     cube.waittoattck = 1f;
        //     cube.waittospin = 1f;
        // }
        if(spintime <= 0)
        {
            isrota = 0;
        }else{
            spintime -= Time.deltaTime;
            isrota = 1;
        }
    }
    void resetsidewalk(){
        ai.enabled = false;
        walktime = 10f;moveindexpos += 1;
        if(Random.Range(0,1) == 1){
            isleft = true;
        }else{
            isleft=false;
        }
    }
    void sidewalk()
    {
        if(walktime <= 0)
        {
            animator.SetBool("leftside" ,false);
            animator.SetBool("rightside" ,false);
            animator.SetBool("idle" ,true);
            // animator.Play("idelboy");
            ai.enabled = true;
            StartCoroutine("WaitForNextMove", 5f);

            // iswalk = false;
            // iskick = true;
        }else{
            walktime -= Time.deltaTime;
            // transform.position = playerpos.position + (transform.position - playerpos.position).normalized * walkdistance;
            // transform.LookAt(new Vector3(CurrentPlayer.transform.position.x , transform.position.y , CurrentPlayer.transform.position.z));            
            if(isleft)
            {
                // animator.Play("RightWalk");
                animator.SetBool("rightside" ,true);animator.SetBool("idle" , false);
                transform.RotateAround(CurrentPlayer.transform.position , Vector3.up , -walkspeed * Time.deltaTime);
            }else
            {
                animator.SetBool("leftside" ,true);animator.SetBool("idle" , false);
                // animator.Play("LeftWalk");
                //transform.position = playerpos.position + (transform.position - playerpos.position).normalized * walkdistance;
                transform.RotateAround(CurrentPlayer.transform.position , Vector3.up , walkspeed * Time.deltaTime);
            }
        }
        //leftwalk
    }

    void launch()
    {
        Physics.gravity = Vector3.up * gravity;
        rb.useGravity = true;

        rb.velocity = calculateLaunch();
    }

    Vector3 calculateLaunch()
    {
        float displacementy = jumppositon.position.y - transform.position.y;
        Vector3 displacementxz = new Vector3(jumppositon.position.x - transform.position.x , 0 , jumppositon.position.z - transform.position.z);
        Vector3 velocityY = Vector3.up* Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementxz / (Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementy - h)/gravity));

        return velocityXZ;         
    }
}
