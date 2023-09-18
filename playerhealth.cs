using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerhealth : MonoBehaviour
{
    public GameObject Player;
    public float health;
    public float buldmg;

    public healthText ht;

    // public LayerMask healthLayer;
    // private bool ismedkit;
    // public Transform healposition;
    //string strinhhp;

    private void Start() {
        //string strinhhp = hp.ToString();
    }
    void Update()
    {
        //hp.text = health.ToString();
        if(health > 100){
            health = 100;
        }
        end();
        ht.sethealth(health);

        if(health < 0)
        {
            health = 0;
        }
        
        // ismedkit = Physics.CheckSphere(healposition.position , 0.3f , healthLayer);
        // if(ismedkit){
        //     health += 10;
        //     print("healed");
        // }

    }
    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "bul")
        {
            health -= buldmg;
        }

        if(other.gameObject.tag == "hp")
        {
            health += 10;
        }

        if(other.gameObject.tag == "bomber"){
            health -= 20;
        }
    }

    void end()
    {
        if(health <= 0){
            Player.GetComponent<player>().enabled = false;
            Destroy(gameObject , 1f);
            SceneManager.LoadScene("jam");
        }
    }
}
