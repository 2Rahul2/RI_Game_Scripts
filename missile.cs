using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Transform playerpos;
    // public Transform startpos;
    private bool hit;
    public LayerMask isplayer;
    public GameObject impacteffect;
    // public GameObject cur;
    public float followtime = 2f;
    private boss Boss;
 
    // Update is called once per frame
    private void Start() {
        Boss = GameObject.FindGameObjectWithTag("boss").GetComponent<boss>();
    }
    void Update()
    {
        try{
            playerpos = GameObject.Find("newplayer").transform;
        }catch{
            playerpos = GameObject.Find("player2").transform;
        }
        if(followtime>=0){
            transform.LookAt(new Vector3(playerpos.position.x , transform.position.y ,  playerpos.position.z));
            transform.position = Vector3.MoveTowards(transform.position , playerpos.position , speed*Time.deltaTime);
            speed += Time.deltaTime;
            followtime -= Time.deltaTime;
        }else{
            
            transform.Translate(Vector3.forward *speed*Time.deltaTime);
        }
        hit = Physics.CheckSphere(transform.position , 0.4f , isplayer);
        if(hit){
            Boss.AS.Stop();
            Boss.AS.clip = Boss.missileImpact;
            Boss.AS.Play();
            Boss.playerdmage(40);
        Instantiate(impacteffect , transform.position , transform.rotation);

            speed = 15;
            Destroy(gameObject);
        }
        Destroy(gameObject , 7f);
    }
    private void OnCollisionEnter(Collision other) {
        Instantiate(impacteffect , transform.position , transform.rotation);
        Destroy(gameObject);
    }
}
