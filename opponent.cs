using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class opponent : MonoBehaviour
{
    public Transform playerpos;
    private Transform player1;
    private Transform player2;
    
    public float followdistance;

    private Rigidbody rb;
    public NavMeshAgent agent;

    public float attackdistance;
    public GameObject bul;
    public Transform firepoint;
    public float waitforatt;
    private float startattack;
    public float hitforce;

    public LayerMask isplayer;
    private bool touchplayer;
    public float sradius;
    private float damage =2f;
    public float health;
    public GameObject smokeeffect;
    public Transform someposition;
    private AudioSource buleffect;

    public enehealth hp;
    public bool attack =false;
    private Vector3 stopposition;
    public switchplayer getcurrentplayerpos;
    private float justwait=0;

    void Start()
    {
        stopposition = transform.position;
        getcurrentplayerpos = GameObject.Find("trackingObject").GetComponent<switchplayer>();
        // player1 = GameObject.Find("newplayer").transform;
        // player2 = GameObject.Find("player2").transform;
        player1 = getcurrentplayerpos.player1.transform.GetChild(0);
        player2 = getcurrentplayerpos.player2.transform;
        attackdistance = Random.Range(14,18);
        buleffect = GetComponent<AudioSource>();
        hp = GetComponent<enehealth>();
        startattack = waitforatt;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(getcurrentplayerpos.playernum == 1){
            playerpos = player1;
        }else{
            playerpos = player2;
        }
        // print(agent.velocity);
        // playerpos = getcurrentplayerpos.CurrentPlayerPos;
        if(hp.health <= 0)
        {
            Instantiate(smokeeffect , someposition.position , Quaternion.identity);
        }
        touchplayer = Physics.CheckSphere(firepoint.position , sradius , isplayer);
        if(Vector3.Distance(transform.position , playerpos.position) > followdistance){
            if(justwait <=0){
                attack=false;
                stopposition = new Vector3(transform.position.x , transform.position.y , transform.position.z);
                followplayer();
            }else{
                justwait -= Time.deltaTime;
            }
        }
        else
        {
            justwait = 2f;
            attack = true;
            agent.destination = stopposition;
            attackplayer();
        }
        transform.LookAt(new Vector3(playerpos.position.x , transform.position.y , playerpos.position.z));
    }
    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "bullet")
        {
            health -= damage;
        }
    }

    void followplayer()
    {     
        agent.destination = playerpos.position;     
    }
    public void msg(){
        print("exe");
    }

    void attackplayer()
    {
        if(startattack <= 0)
        {
            Instantiate(bul , firepoint.position , firepoint.rotation);
            buleffect.Play();
            startattack = waitforatt;
        }
        else
        {
            startattack -= Time.deltaTime;
        }
    }

    void recoil()
    {  
        rb.AddForce(-transform.forward * hitforce  , ForceMode.Impulse);
    }
}
