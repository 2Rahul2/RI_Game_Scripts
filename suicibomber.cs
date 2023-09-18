using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class suicibomber : MonoBehaviour
{
    public NavMeshAgent agent;
    public Rigidbody rigidbody;

    public Transform player;
    
    private Animator animator;


    //paraBOLAA!!
    public float gravity;
    public float h;
    public Transform jumppositon;

    private bool isjump;
    public GameObject exploeffect;
    public float waitfordead;



    public float health;
    public float damage;
    private float ran;
    public GameObject medki;
    public Transform medkispawner;

    public boundryremover boundry;
    public basedest wavedetect;
    public pausemenu pm;
    public robokll rokll;

    public GameObject hiteffect;
    public Transform hitposition;

    // Start is called before the first frame update
    void Start()
    {
        //waitfordead = 1.3f;
        isjump = true;
        agent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
         followplayer();
        if(Vector3.Distance(transform.position , player.position) < 8){
            if(isjump){
                animator.SetBool("jump" , true);
                agent.enabled = false;
                launch();
                isjump = false;
            }

        }

        if(isjump == false){
            if(Vector3.Distance(transform.position , player.position) > 0.1f){
                if(waitfordead <= 0 && isjump == false){
                    deadeffect();
                }else
                {
                    waitfordead -= Time.deltaTime;
                }
                
            }
        }
        dead();
    }


    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            deadeffect();
        }

        if(other.gameObject.tag == "bullet"){
            health -= damage;
        }
    }

    void dead(){

        ran = Random.Range(0 , 15);
        if(health <= 0){
            Instantiate(hiteffect , hitposition.position , Quaternion.identity);
            boundry.GetComponent<boundryremover>().num_oppo += 1;
            wavedetect.GetComponent<basedest>().numofop += 1;
            pm.GetComponent<pausemenu>().numop += 1;
            rokll.GetComponent<robokll>().numofbomber -= 1;
            if(ran == 6){
                Instantiate(medki , medkispawner.position , Quaternion.identity);         
            }
            Destroy(gameObject);
        }
        
    }

    void deadeffect(){
            Instantiate(exploeffect , transform.position , Quaternion.identity);
            rokll.GetComponent<robokll>().numofbomber -= 1;

            Destroy(gameObject);

    }
    void followplayer(){
        agent.destination = player.position;
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
