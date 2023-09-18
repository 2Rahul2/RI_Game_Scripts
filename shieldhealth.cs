using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldhealth : MonoBehaviour
{
    public int health;
    public finalspawner mainspawner;
    public buildhealth totalkilled;
    public int totalcount;

    
    void Update()
    {
        if(totalkilled.totalenemyKilled>=totalcount){
            mainspawner.unprotect = true;
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "bullet"){
            health -= 1;
        }
    }
}
