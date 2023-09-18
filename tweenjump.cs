using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class tweenjump : MonoBehaviour
{
    public Transform gb;
    public LayerMask isobject;
    private bool target;
    public GameObject bul;
    public GameObject[] targetOg;
    public int radii;
    private float waitforattack;
    public float attackspeed;
    void Start()
    {
        // transform.DOJump(gb.position , -2f ,1 ,0.5f);
        waitforattack = attackspeed;
    }
    void Update()
    {
        if(Vector3.Distance(transform.position ,gb.position) > 3f){
            transform.position = Vector3.MoveTowards(transform.position , gb.position , 5*Time.deltaTime);
        }
        target = Physics.CheckSphere(transform.position , radii, isobject);
        if(target){
            targetOg =GameObject.FindGameObjectsWithTag("eskill");
            transform.LookAt(new Vector3(targetOg[0].transform.position.x , targetOg[0].transform.position.y , targetOg[0].transform.position.z));
            if(waitforattack <= 0){
                Instantiate(bul , transform.position  ,transform.rotation);
                waitforattack = attackspeed;
            }else{
                waitforattack -= Time.deltaTime;
            }
        }
    }
}
