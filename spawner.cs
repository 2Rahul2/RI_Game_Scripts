using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{

    public GameObject oppo;

    public Transform[] spawnpoint;

    public float timeinterval;

    private float wavenumber;
    public float totalwave;

    public int numofoppo = 10;

    private int deadoppo;

    private void Start() {
        totalwave = 3f;
        waves();
    }
    void Update()
    {
        
    }
    void waves()
    {
        for(int i = 0 ; i <= 10 ; i +=1)
        {
            Transform randompoint = spawnpoint[Random.Range(0 , spawnpoint.Length - 1)];
            Instantiate(oppo , randompoint.position , Quaternion.identity);
        }

    }
    // void waves()
    // {
    //     hea = oppo.GetComponent<enehealth>().health;
    //     if(wavenumber == 1)
    //     {
    //         numofoppo = 10;
    //         foreach (int item in numofoppo)
    //         {
    //             Transform randompoint = spawnpoint[Random.Range(0 , spawnpoint.Length - 1)];
    //             Instantiate(oppo , randompoint.position , Quaternion.identity);
    //         }

    //         if(hea <= 0)
    //         {
    //             wavenumber += 1;
    //             print(wavenumber);
    //         }
    //     }
    // }

}
