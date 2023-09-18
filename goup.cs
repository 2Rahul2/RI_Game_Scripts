using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goup : MonoBehaviour
{
    private Vector3 movement;
    [SerializeField]private GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        movement = new Vector3(transform.position.x , transform.position.y + 8.77f ,transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
            // transform.Translate(movement * 0.8f *Time.deltaTime);
            // transform.position = Vector3.Lerp(transform.position , movement  ,  2 *Time.deltaTime);
        
    }
    private  void OnCollisionStay(Collision other){
        wall.SetActive(true);
        if(other.gameObject.tag =="Player"){
            transform.position = Vector3.Lerp(transform.position , movement  ,  0.8f*Time.deltaTime);
        }
    }
}
