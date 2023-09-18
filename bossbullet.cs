using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossbullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public boss bossObj;
    public LayerMask isplayer;
    public GameObject hiteffect;
    void Start(){
        bossObj = GameObject.FindGameObjectWithTag("boss").GetComponent<boss>();
    }
    private void FixedUpdate() {
        transform.Translate(Vector3.forward * speed);
    }
    

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject , 2f);
        if(Physics.CheckSphere(transform.position , 2f, isplayer)){
            Instantiate(hiteffect , bossObj.CurrentPlayer.transform.position , Quaternion.identity);
            bossObj.playerdmage(2);   
            Destroy(gameObject);
        }
        
    }
}
