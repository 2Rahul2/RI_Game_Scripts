using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class decPlayerHealth : MonoBehaviour
{
    public Slider sliderHealth;
    public TextMeshProUGUI healthText;
    public int health , totalhealth;
    [SerializeField]private bool firstLevel; 
    [SerializeField] private float resettimeHeal = 2 , healTime ,waitforPause=2;
    [SerializeField] private GameObject RetryPannel;
    private void Start() {
        healTime = resettimeHeal;
    }
    private void Update() {

        if(health<=0){
            RetryPannel.SetActive(true);
            if(waitforPause<=0){
                Time.timeScale =0 ;
            }else{
                waitforPause-=Time.deltaTime;
            }
            RetryPannel.GetComponent<Animator>().Play("demofade");
        }
        if(health<totalhealth){
            if(healTime<=0){
                updatehealth(-3);
                if(health>totalhealth){
                    health=totalhealth;
                    updatehealth(0);
                }
                healTime = resettimeHeal;
            }else{
                healTime -= Time.deltaTime;
            }
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "bullet"){
            updatehealth(2);
        }        
    }
    public void updatehealth(int dmg){
            health -= dmg;
            sliderHealth.value = health;
            healthText.text = health.ToString() + "/"+totalhealth.ToString(); 
    }
}
