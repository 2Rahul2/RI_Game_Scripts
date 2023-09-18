using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class soldie : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rb;    
    public Transform Player;
    public GameObject playe;
    public Transform firepoint;
    public GameObject bullet;

    public int follow;
    public float attackplayer;
    private float startattack;
    public float waitforattack;

    private Animator anim;
    public float gravity = -18;
    public float h = 20;

    public float ran;
    private bool isjump;
    public bool isground;
    public Transform grouncheck;
    float groddistance = 0.3f;
    public LayerMask layermask;
    public Transform jumpposition;

    public float jumpsec;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        startattack = waitforattack;   
    }
    private void Update() 
    {
        isground = Physics.CheckSphere(grouncheck.position , groddistance , layermask);
        transform.LookAt(new Vector3(Player.position.x , transform.position.y , Player.position.z));
        if(Vector3.Distance(transform.position , Player.position) > follow)
        {
            followplayer();
        }
        else if(Vector3.Distance(transform.position , Player.position) < follow)
        {
            attackplay();
        }

        jumpbool();

        // if(isjump)
        // {
        //     agent.enabled = false;
        //     launch();
        //     anim.SetBool("jump" , true);
        //     isjump = false;
        // }else
        // {
        //     print("true");
        //     //agent.enabled = true;
        // }
    }   

    void jumpbool()
    {
        ran = Random.Range(0 , 120);
        if(ran == 6 && isground && jumpsec < 1){
            agent.enabled = false;
            launch();
            anim.SetBool("jump" , true);
            isjump = false;
            jumpsec -= Time.deltaTime;
        }else
        {
            jumpsec = 1; 
        }
    }
    void attackplay()
    {
        agent.destination = transform.position;
        if(startattack <= 0){
            anim.SetBool("attack" , true);
            anim.SetBool("jump" , false);
            Instantiate(bullet , firepoint.position , firepoint.rotation);
            startattack = waitforattack;
        }
        else{
            anim.SetBool("attack" , true);
            anim.SetBool("jump" , false);

            startattack -= Time.deltaTime;
        }
    }
    void followplayer()
    {
        agent.destination = Player.position;
        anim.SetBool("run" , true);  
        anim.SetBool("attack" , false);  
        anim.SetBool("jump" , false);
    }

    void launch(){
        Physics.gravity = Vector3.up * gravity;
        rb.useGravity = true;
        rb.velocity = Calculatelaunch();
        print(Calculatelaunch());

    }
    Vector3 Calculatelaunch()
    {
        float displacementY = jumpposition.position.y - transform.position.y;
        
        Vector3 displacementXZ = new Vector3(jumpposition.position.x - transform.position.x , 0 , jumpposition.position.z - transform.position.z);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity* h );
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY-h)/gravity));

        return velocityXZ + velocityY;
    }
}
