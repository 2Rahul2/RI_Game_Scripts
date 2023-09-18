using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserTrap : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isdone;
    public GameObject lasers;
    public float wait , resetwait , waithalf;
    void Start()
    {
        wait=resetwait;
        waithalf = wait/2;
    }

    // Update is called once per frame
    void Update()
    {
        if(isdone){
            if(wait<=0){
                wait=resetwait;
                lasers.SetActive(false);
            }else if(wait<=waithalf){
                wait -= Time.deltaTime;
                lasers.SetActive(true);
            }else{
                wait -= Time.deltaTime;
            }
        }
    }
}
