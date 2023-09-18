using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class meterExplode : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private float distance,resettime;
    private float explodeTime;
    [SerializeField] private Image warningImg;
    [SerializeField] private Transform player , explodePosition;
    [SerializeField] private GameObject[] ExplodeEffect;
    [SerializeField] private GameObject CircleObject;
    [SerializeField] private LayerMask isenemy;
    [SerializeField] private bool electrifed;
    [SerializeField] private AudioSource sfx;
    private bool once;
    private void Start() {
        explodeTime=resettime;
    }
    void Update()
    {
        if(Vector3.Distance(transform.position , player.position) < distance){
            CircleObject.GetComponent<Animator>().Play("enlargeCircle");
            if(explodeTime>0){
                if(Input.GetKey(KeyCode.F)){
                    warningImg.fillAmount += Time.deltaTime/resettime;
                    explodeTime -= Time.deltaTime;
                }else{
                    warningImg.fillAmount = 0;
                    explodeTime = resettime;
                }
            }else{
                // Debug.Log("Exploded");
                if(!once){
                    Collider[] enemyisnotRange = Physics.OverlapSphere(transform.position , 14 ,isenemy);
                    Collider[] enemyinRange = Physics.OverlapSphere(transform.position , 7 ,isenemy);
                    foreach(var notenemy in enemyisnotRange){
                        print("not in range");
                        notenemy.GetComponent<enefov>().suspicious=true;
                        notenemy.GetComponent<enefov>().suspiciousTime=5f;
                        notenemy.GetComponent<enefov>().electricPos = explodePosition;
                    }
                    foreach(var enemy in enemyinRange){
                        enemy.GetComponent<enefov>().enabled=false;
                        enemy.GetComponent<BoxCollider>().enabled=false;
                        if(electrifed){
                            enemy.GetComponent<Animator>().Play("electric");
                        }else{
                            enemy.GetComponent<Animator>().Play("death");
                        }
                        print("Enemy ded");
                    }
                    foreach(var effect in ExplodeEffect){
                        Instantiate(effect , explodePosition.position , Quaternion.identity);
                    }
                    sfx.Play();
                    once=true;
                }
                // transform.GetChild(0).gameObject.GetComponent<Image>().sprite =null;
                // transform.GetChild(1).gameObject.GetComponent<Image>().sprite =null;
                // transform.GetChild(2).gameObject.GetComponent<Image>().sprite =null;

                // warningImg.GetComponent<Image>().sprite = null;
                // this.enabled=false;
                Destroy(gameObject ,1f);
            }
        }else{
            CircleObject.GetComponent<Animator>().Play("idleCircle");
            warningImg.fillAmount =0;
            explodeTime = resettime;
        }
    }
}
