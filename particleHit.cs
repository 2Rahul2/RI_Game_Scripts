using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleHit : MonoBehaviour
{
    // public ParticleSystem part;
    public float health;
    // public playerTwo player;
    public enehealth mainhealth;
    void Start()
    {
        // player = GetComponent<playerTwo>();
        health = mainhealth.health;
    }
    

    void OnParticleCollision(GameObject other)
    {
        mainhealth.health -= 1;
           print("bruh");
    }
    // private void OnCollisionEnter(Collision other) {
    //     if(other.gameObject.tag == "bullet"){
    //         health -=1;
    //     }
        
    // }
}
