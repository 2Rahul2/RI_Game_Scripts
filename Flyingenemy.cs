using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Flyingenemy : MonoBehaviour
{
    public GameObject curveBullet;
    private float waitforshoot;
    public float resetshoot;
    private Rigidbody rb;

    public Transform Player; 
    // public Transform FlyObject;
    public int speed;
    private NavMeshAgent ai;
    [SerializeField] private float distance;

    private switchplayer getcurrentplayerpos;
    private Transform player1;
    private Transform player2;

    // Start is called before the first frame update
    void Start()
    {
        getcurrentplayerpos = GameObject.Find("trackingObject").GetComponent<switchplayer>();
        player1 = getcurrentplayerpos.player1.transform.GetChild(0);
        player2 = getcurrentplayerpos.player2.transform;
        ai = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
       waitforshoot=resetshoot;
    }

    // Update is called once per frame
    void Update()
    {
        if(getcurrentplayerpos.playernum == 1){
            Player = player1;
        }else{
            Player = player2;
        }
        // FlyObject.LookAt(new Vector3(FlyObject.position.x , FlyObject.position.y ,Player.position.z));
        transform.LookAt(new Vector3(Player.position.x , transform.position.y ,Player.position.z));
        ai.SetDestination(Player.position);
        if(Vector3.Distance(transform.position , Player.position) > distance){
            ai.SetDestination(Player.position);   
        }else{
            ai.SetDestination(transform.position);
        }
        if(Input.GetKeyUp(KeyCode.R)){
            // GameObject mybullet = Instantiate(curveBullet , transform.position ,transform.rotation);
            // mybullet.GetComponent<bezierCurve>().startposition.position = transform.position;
        }
        attack();
    }
    void attack(){
        if(waitforshoot<=0){
            Instantiate(curveBullet , transform.position ,transform.rotation);
            waitforshoot=resetshoot;
        }else{
            waitforshoot -= Time.deltaTime;
        }
    }
}
