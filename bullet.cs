using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    public GameObject collideeffect , copterEffect;
    public GameObject energy;

    public boss bossObj;
    private particleHit ph;
    [SerializeField] private bool copterBullet;
    private void Start() {
        ph = GetComponent<particleHit>();
        // bossObj = GameObject.FindGameObjectWithTag("boss").GetComponent<boss>();
    }
    private void FixedUpdate() {
        transform.Translate(Vector3.forward * speed);
    }
    
    private void Update() {
        // transform.LookAt(bossObj.CurrentPlayer.transform.position);
        Destroy(gameObject , 2f);
    }
    void energyPaticles(){
            int rant = Random.Range(1,4);
            if(rant == 2){
            Instantiate(energy , transform.position , transform.rotation);
            }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player")
        {
            Instantiate(energy , transform.position , transform.rotation);
            Destroy(gameObject);
        }else if(other.gameObject.tag == "oppo" || other.gameObject.tag == "build" || other.gameObject.tag == "eskill")
        {   
            energyPaticles();
            if(copterBullet){
                Instantiate(copterEffect , transform.position , Quaternion.identity);
            }else{
                Instantiate(collideeffect , transform.position , Quaternion.identity);
            }
            Destroy(gameObject);
        }else if(other.gameObject.tag == "boss"){
            energyPaticles();
            bossObj.shieldhealth -= 1;
            Destroy(gameObject);
        }else{

            Instantiate(collideeffect , transform.position , Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
