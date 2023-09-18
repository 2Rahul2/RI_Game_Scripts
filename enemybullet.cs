using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemybullet : MonoBehaviour
{
    public float speed;
    public GameObject collideeffect;
    // Start is called before the first frame update
    private void FixedUpdate() {
        transform.Translate(Vector3.forward * speed);
    }
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject , 4f);
    }
        
    
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            Instantiate(collideeffect , transform.position , transform.rotation);
            Destroy(gameObject);
        }else{
            Destroy(gameObject);
        }
    }
}
