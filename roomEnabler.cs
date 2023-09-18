using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomEnabler : MonoBehaviour
{
    // Start is called before the first frame update
    public bool enable , disable;
    public GameObject[] enableObject , disableObject;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            if(enable){
                foreach(var appearObj in enableObject){
                    appearObj.SetActive(true);
                }
            }
            if(disable){
                foreach(var disappearObj in disableObject){
                    disappearObj.SetActive(false);
                }
            }
            Destroy(gameObject , 1f);
        }
    }
}
