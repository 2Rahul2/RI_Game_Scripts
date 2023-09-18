using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalspawner : MonoBehaviour
{
    // Start is called before the first frame update
    private float spawntime;
    public float resetspawntime;
    public Transform spawnposition;
    public GameObject[] enemy;
    public GameObject image;
    public bool unprotect=false;
    public int health;
    public int forceShieldhealth;
    private int index=0;
    public killcount kc;
    public buildhealth BH;
    void Start()
    {
        spawntime = resetspawntime;
    }

    void Update()
    {
        if(health<=0){
            Destroy(image);
            // kc.spawnerkilled +=1;
            BH.Count +=1;
            Destroy(gameObject);
        }
        if(spawntime<=0){
            Instantiate(enemy[Random.Range(0,enemy.Length)] , spawnposition.position , spawnposition.rotation);
            spawntime = resetspawntime;
        }else{
            spawntime -= Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(unprotect){
            if(other.gameObject.tag=="bullet"){
                health -= 1;
            }
        }   
    }
}
