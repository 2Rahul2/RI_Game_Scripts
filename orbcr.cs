using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbcr : MonoBehaviour
{

    public Transform[] spawnpoint;
    public GameObject oppo;

    public int num_of_oppo;

    public GameObject orbeffect;
    public robokll rokil;

    // destroy the spawners
    [SerializeField] private GameObject Spawner;
    public bool DestryOrb;
    private void Update() {
        if(DestryOrb){
            
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player")
        {
            Destroy(Spawner);
            // Instantiate Effects
            Destroy(gameObject);
            // for(int i = 0 ; i<num_of_oppo ;i++)
            // {
            //     Transform randompoint = spawnpoint[Random.Range(0 , spawnpoint.Length - 1)];
            //     Instantiate(oppo , randompoint.position , Quaternion.identity);
            //     rokil.GetComponent<robokll>().numofbomber += 1;
            // }
            // Destroy(gameObject);
            // Destroy(orbeffect);
        }
    }
}
