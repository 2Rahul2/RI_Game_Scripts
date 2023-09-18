using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bosshealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int health , halfhealth;
    [SerializeField] private Material Mat;
    [SerializeField] private Color red ,blue;
    [SerializeField] private Slider slid;
    private bool once;
    [SerializeField] private loadscene LoadScene;
    [SerializeField] private float waitTime ,pannelwait;
    [SerializeField] private GameObject pannel;
    void Start()
    {
        Mat.SetColor("_EmissionColor" ,blue*1.9f);
        slid.maxValue=health;
        slid.value=health;
        halfhealth = health/2;
    }

    // Update is called once per frame
    void Update()
    {
        if(!once){
            if(health<=halfhealth){
                Mat.SetColor("_EmissionColor" ,red*2.5f);
                // Mat.SetColor()
                once=true;
            }
        }
        if(health<=0){
            if(pannelwait<=0){
                pannel.GetComponent<Animator>().Play("demofade");
            }else{
                pannelwait -= Time.deltaTime;
            }
            transform.parent.gameObject.GetComponent<Animator>().Play("dieBoss");
            transform.parent.gameObject.GetComponent<boss>().enabled=false;
            if(waitTime<=0){
                LoadScene.startLoad("menu");
            }else{
                waitTime -= Time.deltaTime;
            }
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag =="bullet"){
            updatehealth(1);
        }
        if(other.gameObject.tag =="copter"){
            updatehealth(2);
        }
    }
    public void updatehealth(int dmg){
            health -= dmg;
            slid.value =health;
    }
}
