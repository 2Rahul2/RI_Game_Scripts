using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.AI;

public class mechanimation : MonoBehaviour
{
    public opponent checkbool;
    private enehealth health;
    private float currenthealth;
    // private NavMeshAgent agent;

    private Animator anim;
    public Transform sparkpos;
    public GameObject spark;
    // public GameObject part;
    // private float speed;
    // Start is called before the first frame update
    void Start()
    {
        // agent = GetComponent<NavMeshAgent>();
        health = GetComponent<enehealth>();
        currenthealth = health.health;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(health.health<=health.starthealth/2){
        //     Destroy(part);
        // }
        if(currenthealth != health.health){
            Instantiate(spark,sparkpos.position , Quaternion.identity);
            currenthealth=health.health;
        }
        // if(checkbool.agent.velocity.x<=0||checkbool.agent.velocity.y<=0||checkbool.agent.velocity.z<=0){
        //     if(checkbool.attack == true){
        //         anim.Play("attack");
        //     }else{
        //         anim.Play("walk");
        //     }
        // }else{
        //     anim.Play("walk");
        // }
        if(checkbool.attack==true){
            // anim.StopPlayback();
            // anim.Play("idle");

            anim.Play("attack");
        }else{
            // anim.Play("idle");
            anim.Play("walk");
        }
    }
}
