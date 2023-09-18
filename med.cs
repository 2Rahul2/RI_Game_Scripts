using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class med : MonoBehaviour
{
    float rotationspeed;
    private void Start() {
        rotationspeed = 2f;
    }
    private void Update() {
        transform.Rotate(0 , rotationspeed * Time.deltaTime , 0);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "playerhp"){
            Destroy(gameObject);
        }
    }
}
