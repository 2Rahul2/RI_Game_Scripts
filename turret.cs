using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform playerPosition , firepointPos;
    [SerializeField] private GameObject bullet ,explodeEffect;
    [SerializeField] private float distance , shootTime , resetTime ,health;
    private Animator anim;
    [SerializeField] private int rotatespeed;
    [SerializeField]private Material burned;
    private MeshRenderer meshRenderer;
    
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        anim = GetComponent<Animator>();
        shootTime = resetTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position , playerPosition.position)<distance){
            Vector3 RotatePos =  playerPosition.position - transform.position;
            transform.rotation = Quaternion.RotateTowards(transform.rotation , Quaternion.LookRotation(RotatePos) , Time.deltaTime*rotatespeed*2);
            if(shootTime <= 0){
                Instantiate(bullet , firepointPos.position , transform.rotation);
                shootTime = resetTime;
            }else{
                shootTime -= Time.deltaTime;
            }
        }

        if(health<=0){
            meshRenderer.sharedMaterial =burned;
            transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = burned;
            // play animation
            anim.Play("turretDown");
            Instantiate(explodeEffect , transform.position , transform.rotation);
            gameObject.tag ="Untagged";
            this.enabled=false;
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag =="bullet"){
            health -= 1;
        }
    }
}
