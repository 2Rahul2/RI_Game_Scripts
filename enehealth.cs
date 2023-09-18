using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enehealth : MonoBehaviour
{
    public float health;
    public float starthealth;
    public int damage;
    public Animator anim;
    public particleHit mainhealth;
    public Transform healthbar;
    public float startscale;
    private float currentYscale;
    public GameObject bullethitEffect;

    //take ult damage
    public bool ultrigger;
    public float ultwait=0f;
    public float ulttime;
    public int ultcount;
    public GameObject ulthiteffect;
    public Transform middlepos;
    public buildhealth buildH;

    private void Start() {
        buildH = GameObject.Find("Main Camera").GetComponent<buildhealth>();
        buildH.enemyCount += 1;
        starthealth = health;
        startscale = healthbar.localScale.y;
        currentYscale = healthbar.localScale.y;
        mainhealth = GetComponent<particleHit>();
        anim = GetComponent<Animator>();    
    }

    private void Update() {
        // healthbar.localScale.y = (currentYscale / health)*100f;
        // print(currentYscale);
        dead();
        ult();
    }
    private IEnumerator waitforultdamage(float wait){
        yield return new WaitForSeconds(wait);
        Instantiate(ulthiteffect , transform.position , transform.rotation);
        health -= 4;
        updatehealth();
        StopCoroutine("waitforultdamage");
    }
    public void ult(){
        if(ultrigger && ultcount>=0){
            if(ulttime <=0){
                if(ultwait <=0){
                    Instantiate(ulthiteffect , middlepos.position , Quaternion.identity);
                    health -= 4;
                    updatehealth();
                    ultwait = 0.2f;
                    ultcount -= 1;
                }else{
                    ultwait -= Time.deltaTime;
                }
            }else{
                ulttime -= Time.deltaTime;
            }
            // for(int i =0;i<=5;i++){
            //     StartCoroutine("waitforultdamage" , 0.4f);
            //     if(i==5){
            //         ultrigger=false;
            //         break;
            //     }
            // }
        }
    }
    public void updatehealth(){
            currentYscale = health/starthealth * startscale;
            healthbar.localScale = new Vector3(healthbar.localScale.x , currentYscale , healthbar.localScale.z);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "bullet")
        {

            health -= damage;
            Instantiate(bullethitEffect , transform.position , transform.rotation);
            updatehealth();
            // currentYscale = healthbar.localScale.y/2;
            // print(currentYscale);
            // divvalue = health;
            // print((float)(currentYscale/divvalue));
            // healthbar.localScale.y = currentYscale;
        // currentYscale = (float)((healthbar.localScale.y /(float)(2*health))*100);

        }else if(other.gameObject.tag == "copter"){
            health -= (damage + 4);
            Instantiate(bullethitEffect , transform.position , transform.rotation);
            updatehealth();
        }

        if(other.gameObject.tag == "ring")
        {
            health -= damage + 1;
        }
    }
    private void OnParticleCollision(GameObject other) {
        health -= 4;
    }

    void dead()
    {
        if(health <=0){
            buildH.totalenemyKilled += 1;
            buildH.enemyCount -= 1;
            // kilcounts.enemykilled += 1;
            Destroy(gameObject);
        }
        // if(health <= 0 )
        // {
        //     ran = Random.Range(0 , 10);

        //     enemy.GetComponent<opponent>().enabled = false;

        //     boundryR.GetComponent<boundryremover>().num_oppo += 1;
        //     wavedetect.GetComponent<basedest>().numofop += 1;
        //     Pm.GetComponent<pausemenu>().numop += 1;
        //     rokil.GetComponent<robokll>().numofbomber -= 1;

        //     if(ran == 5){
        //         Instantiate(medkit , medkitspawner.position , medkitspawner.rotation);
        //     }

        //     Destroy(gameObject); 
        // }
    }
}
