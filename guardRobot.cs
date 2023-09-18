using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class guardRobot : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform[] patrolpoint;

    public bool idle;
    public bool patrol;

    private int indexvalue;

    public float waitforpoint;
    private Animator animator;

    public Transform Playerpos;
    private float timetowalk;


    //spot variables
    private float watchtplayertime;

    private void Start() {
        timetowalk = 1f;
        agent = GetComponent<NavMeshAgent>();
        indexvalue = Random.Range(0 , patrolpoint.Length);
        waitforpoint = 2f;
        animator = GetComponent<Animator>();
    }
    private void Update() {
        if(patrol){
            patrolmovement();
        }
    }

    private void patrolmovement(){

        agent.destination = patrolpoint[indexvalue].position;
        if(Vector3.Distance(transform.position , patrolpoint[indexvalue].position) < 0.3f){
            if(waitforpoint <= 0){
                indexvalue = Random.Range(0 , patrolpoint.Length);            
            }else
            {
                agent.destination = transform.position;
                waitforpoint -= Time.deltaTime;
            }
        }
    }
    //idle code
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            //move towards player for 1sec
            if(timetowalk > 0){
                agent.destination = Playerpos.position;
            }else
            {
                timetowalk -= Time.deltaTime;
            }
        }
    }
    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player"){

            if(watchtplayertime <= 0){
                //gameoVERR///lollllllll
            }else
            {
                //Animator.SetBool("sus");
                watchtplayertime -= Time.deltaTime;
            }
        }
    }
}
