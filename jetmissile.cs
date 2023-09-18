using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jetmissile : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    public GameObject explosioneffect;
    public AudioSource explosionSFX;
    public GameObject des;
    private BoxCollider BC;
    void Start()
    {
        BC = GetComponent<BoxCollider>();
        rb=GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 5 , ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.rotation.x<90){
            transform.Rotate(Time.deltaTime*30*1.4f , 0 ,0);
        }
    }
    private void OnCollisionEnter(Collision other) {
        Instantiate(explosioneffect , transform.position , Quaternion.identity);
        Collider[] enemyinrange = Physics.OverlapSphere(transform.position , 10f);
                // enemyinrange = GameObject.FindGameObjectsWithTag("eskill");
                foreach(var enemy in enemyinrange){
                    if(enemy.tag == "eskill"){
                        enemy.GetComponent<enehealth>().health -= 100;
                        print("yes");
                    }
                }
                explosionSFX.Play();
                Destroy(des);
                BC.enabled=false;

                
        Destroy(gameObject ,2f);
    }
}
