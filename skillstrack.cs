using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class skillstrack : MonoBehaviour
{
    // public float Ecount1;
    // public float EcountReset1;
    public player player1;
    public playerTwo player2;



    //Player two sword skills: 
    public bool iceattack;
    public bool fireattack;
    public float iceduration;
    public float fireduration;
    // public int slashdmg;
    // private GameObject Slasheffectholder;
    public GameObject Defaultslasheffect;
    // public GameObject Fireslasheffect;
    // public GameObject Iceslasheffect;


    public Image U1;
    public Image E1;
    public Image U2;
    public Image E2;
    [SerializeField] private TextMeshProUGUI UT1, ET1 , UT2 ,ET2;
    public bool u1once , u2once ,e1once, e2once;
    // public bool e1once;
    // public bool e2once;
    // public bool u2once;

    // Start is called before the first frame update
    void Start()
    {
        // UT1.text = "lol";
    }

    // Update is called once per frame
    void Update()
    {

        if(iceattack && !fireattack){
            if(iceduration<=0){
                player2.slashdmg = 3;
                player2.slasheffectholder = Defaultslasheffect;
                iceattack=false;
            }else{
                iceduration -= Time.deltaTime;
            }
        }

        if(fireattack){
            if(fireduration <= 0){
                player2.slashdmg = 3;
                player2.slasheffectholder = Defaultslasheffect;
                fireattack = false;
            }else{
                fireduration -= Time.deltaTime;
            }
        }

        if(player1.ultcooldown>0){
            U1.fillAmount += Time.deltaTime/15; 
            UT1.text = player1.ultcooldown.ToString("f1");
            // U1.transform.GetChild(1).SetSiblingIndex(0);
            player1.ultcooldown -= Time.deltaTime;
        }else{
            if(u1once){
                UT1.text = "";
                U1.transform.GetChild(0).SetSiblingIndex(1);
                u1once = false;
            }
        }
        // player 1 eskill cooldown
        if(player1.cooldown>0){
            E1.fillAmount += Time.deltaTime/ 15;
            player1.cooldown -= Time.deltaTime;
            ET1.text = player1.cooldown.ToString("f1");
            if(player1.cooldownact<=0){
                player1.drone.SetActive(false);
                player1.cooldownact = 10;
            }else{
                player1.cooldownact -= Time.deltaTime;
            }
        }else{
            if(e1once){
                ET1.text = "";
                E1.transform.GetChild(1).SetSiblingIndex(0);
                e1once = false;
            }
        }


        if(player2.Ucount >0){
            player2.Ucount -= Time.deltaTime;
            U2.fillAmount += Time.deltaTime /12;
            UT2.text = player2.Ucount.ToString("f1");
        }else{
            if(u2once){
                UT2.text = "";
                U2.transform.GetChild(1).SetSiblingIndex(0);
                u2once =false;
            }
        }

        if(player2.Ecount>0){
            E2.fillAmount += Time.deltaTime / 5;
            ET2.text = player2.Ecount.ToString("f1");
            player2.Ecount -= Time.deltaTime;
        }else{
            if(e2once){
                ET2.text = "";
                E2.transform.GetChild(1).SetSiblingIndex(0);
                e2once = false;
            }
        }

        // player two sword skills

        // if(player2.iceattack && !player2.fireattack){
        //     if(player2.iceduration<=0){
        //         player2
        //     }
        // }



    }
}
