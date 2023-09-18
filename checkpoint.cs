using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public Transform chekpoint;
    public Transform player;
    public Animator anim;
    public enefov Gover;
    private bool once;

    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        once = false;
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!once)
        {
            //button.SetActive(false);
            if(Gover.GetComponent<enefov>().hascaught == true){
                anim.SetBool("black" , true);
                anim.SetBool("white" , false);
                once = true;
            }
        }else
        {
            //appear button
            button.SetActive(true);
        }
    //     if(Input.GetKeyDown(KeyCode.R)){
    //         player.position = chekpoint.position;
    //     }
    }

    public void respwan(){
        once = false;
        anim.SetBool("black" , false);
        anim.SetBool("white" , true);
        //anim.SetBool("black" , false);
        player.position = chekpoint.position;
        Gover.GetComponent<enefov>().numseenplayer = 0;
        Gover.GetComponent<enefov>().hascaught = false;
        button.SetActive(false);
        Gover.GetComponent<enefov>().ispatrol = true;
        Gover.GetComponent<enefov>().alertmark.SetActive(false);
        Gover.GetComponent<enefov>().redalertmark.SetActive(false);
        Gover.GetComponent<enefov>().wasinsight = false;
        Gover.GetComponent<enefov>().waittosee = 3f;


        // ispatrol = true;
        // alertmark.SetActive(false);
        // redalertmark.SetActive(false);
        // wasinsight = false;
        // waittosee = 3f;
    }
}
