using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dronebullet : MonoBehaviour
{
    public float speed;
    public GameObject exp;
    private boss BOSS;
    public LayerMask isplayer;

    // Update is called once per frame
    private void Start() {
        BOSS = GameObject.FindGameObjectWithTag("boss").GetComponent<boss>();
    }
    private void FixedUpdate() {
        transform.Translate(Vector3.forward * speed);
        Destroy(gameObject , 2f);
        if(Physics.CheckSphere(transform.position , 2 , isplayer)){
            BOSS.playerdmage(3);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "ground"){
        Instantiate(exp ,transform.position , transform.rotation);
        Destroy(gameObject);

        }
    }
    // private void OnCollisionEnter(Collision other) {
    // }
}
    
