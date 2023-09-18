using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class cubeattack : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform Bossa;
    public Transform bospos;
    private int randombospos;
    //public Transform launchpos;
    private Transform player;
    public float distance;
    public float[] speed;
    public float dashingspeed;
    private int ran;

    public bool isrotate;
    public float waittoattck;
    private Rigidbody rb;
    private bool touchedplayer;
    public LayerMask isplayer;
    public bool attack;
    public float waittospin;
    private boss Bigboy;
    private MeshRenderer meshR;
    
    void Start()
    {
        meshR = GetComponent<MeshRenderer>();
        Bossa = GameObject.Find("Role_T").transform;
        Bigboy = GameObject.Find("Role_T").GetComponent<boss>();
        player = Bigboy.CurrentPlayer.transform;
        rb = GetComponent<Rigidbody>();
        rb.detectCollisions = false;
        //isrotate = true;
        ran = Random.Range(1,4);
        waittoattck = 1f;
        waittospin = 1f;
        // randombospos = Random.Range(0 , bospos.Length);
        //Bigboy = GetComponent<boss>();
    }

    // Update is called once per frame
    void Update()
    {
        player = Bigboy.CurrentPlayer.transform;

        if(waittospin <= 0)
        {
            if(isrotate){
                rotatearo();
            }else
            {
                if(attack)
                {
                    if(waittoattck <= 0)
                    {
                        // Bigboy.animator.Play("cubeattack");
                        if(!touchedplayer)
                        {
                            transform.position = Vector3.MoveTowards(transform.position , player.position , dashingspeed * Time.deltaTime);
                            // if(dashingspeed< 100)
                            // {
                            //     dashingspeed += 8;
                            // }
                        }
                        destroyonplayer();              
                    }else
                    {
                        rotatearo();
                        //transform.position = Vector3.MoveTowards(transform.position , launchpos.position , dashingspeed * Time.deltaTime);
                        transform.position = new Vector3(transform.position.x , transform.position.y + 0.8f , transform.position.z);
                        waittoattck -= Time.deltaTime;
                    }
                }
            }

            if(Bigboy.GetComponent<boss>().isrota == 0)
            {
                isrotate = false;
                attack = true;
                //transform.position = new Vector3(transform.position.x , transform.position.y + 3 , transform.position.z);
            }
            if(Bigboy.GetComponent<boss>().isrota == 1)
            {
                isrotate = true;
                attack = false;
                waittoattck = 1f;
                //rb.detectCollisions = false;
                //waittospin = 1f;        
            }

            if(Bigboy.GetComponent<boss>().isrota == 4){
                waittospin = 1f;
                //Bigboy.GetComponent<boss>().isrota = 5;
            }
        }else
        {
            waittospin -= Time.deltaTime;
            movetoboss();
        }

    }

    void rotatearo()
    {
        //ran = Random.Range(-0.2f, 0.2f);
        transform.localEulerAngles = new Vector3(0,0,0);
        transform.position = Vector3.Lerp(transform.position , Bossa.position + (transform.position - Bossa.position).normalized * distance , 2f* Time.deltaTime );
        transform.RotateAround(Bossa.position ,  Vector3.up , 30*4f * Time.deltaTime);
        //transform.position = new Vector3(transform.position.x , transform.position.y + ran , transform.position.z);
    }

    void destroyonplayer()
    {
        //transform.position = Vector3.MoveTowards(transform.position , launchpos.position , dashingspeed * Time.deltaTime);
        touchedplayer = Physics.CheckSphere(transform.position , 2 , isplayer);
        if(touchedplayer)
        {
            Bigboy.playerdmage(6);
            rb.useGravity = true;
            rb.detectCollisions = true;
            attack = false;
            Bigboy.GetComponent<boss>().isrota = 3;
            meshR.material.DisableKeyword("_EMISSION");
            meshR.material.DOFade(0 , 3f);
            //waittospin =1f;
            
        }
        
        Destroy(gameObject , 4f);
    }

    void movetoboss()
    {
        transform.localEulerAngles = new Vector3(0,0,0);

        rb.useGravity = false;
        rb.detectCollisions = false;
        transform.position = Vector3.MoveTowards(transform.position , bospos.position , dashingspeed * Time.deltaTime);
    }
}
