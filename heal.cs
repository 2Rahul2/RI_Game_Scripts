using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heal : MonoBehaviour
{
    bool ismedkit;
    public LayerMask layerMask;
    public GameObject player;

    void Update()
    {
        // ismedkit = Physics.CheckSphere(transform.position , 0.2f , layerMask);        
        // if(ismedkit){
        // }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "hp"){
            player.GetComponent<playerhealth>().health += 10;
        }
    }
}
