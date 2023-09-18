using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class energy : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform playerpos;
    public LayerMask isplayer;
    public player myplayer;
    public playerTwo myplayer2;

    [SerializeField]
    private Slider ultslider;
    [SerializeField]
    private Slider ultslider2;

    private GameObject slideobject;
    private Rigidbody rb;
    private float waitt=0.3f;
    private switchplayer getcurrentplayer;
    public float gravity;
    public Transform jumppositon;
    public float h;
    public int speed;


    void Start()
    {
        // playerpos = GameObject.Find("newplayer").transform;
        try{
            playerpos = GameObject.Find("newplayer").transform;
        }catch{
            playerpos = GameObject.Find("player2").transform;
        }
        getcurrentplayer = GameObject.Find("trackingObject").GetComponent<switchplayer>();
        rb = GetComponent<Rigidbody>();
        transform.LookAt(new Vector3(playerpos.position.x , playerpos.position.y , playerpos.position.z));
        // rb.AddForce(0 , 20 , -transform.forward * 30 );
        // rb.AddForce(-transform.forward*30 , ForceMode.Impulse);
        // rb.AddForce(transform.up*20 , ForceMode.Impulse);

        // slideobject = GameObject.FindGameObjectsWithTag("slide")[0];
        // ultslider = GetComponent<slideobject.Slider>();
        // ultslider = GameObject.Find("slide").GetComponent<Slider>();
        // ultslider2 = GameObject.Find("slide2").GetComponent<Slider>();
        // myplayer = GameObject.Find("newplayer").GetComponent<player>();
        // myplayer2 = GameObject.Find("player2").GetComponent<playerTwo>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(Physics.CheckSphere(transform.position , 0.2f , isplayer)){
        //     myplayer.energy += 4;
        //     // myplayer2.energy += 5;
            
        //     // ultslider2.value = myplayer2.energy;
        //     ultslider.value = myplayer.energy;
        //     Destroy(gameObject);
        // }else{
            // if(Physics.CheckSphere(transform.position , 0.2f , isplayer)){
                // Destroy(gameObject);
                // if(getcurrentplayer.playernum == 1){
                //     myplayer = GameObject.Find("newplayer").GetComponent<player>();
                //     myplayer.energy += 3;
                //     Destroy(gameObject);

                // }else{
                //     myplayer2 = GameObject.Find("player2").GetComponent<playerTwo>();
                //     myplayer2.energy += 2;
                //     Destroy(gameObject);
                // }
            // }
            if(waitt<=0){
                // playerpos = GameObject.Find("newplayer").transform;

                if(getcurrentplayer.playernum == 2){
                    // myplayer2 = GameObject.Find("player2").GetComponent<playerTwo>();
                    playerpos = GameObject.Find("energycollect").transform;
                    transform.LookAt(new Vector3(playerpos.position.x , playerpos.position.y , playerpos.position.z));
                    // jumppositon = GameObject.Find("player2").transform;
                    // launch();
                    transform.Translate(Vector3.forward * speed *Time.deltaTime);
                    // transform.DOJump(playerpos.position , 1f , 1 ,0.8f );
                }else{
                    // myplayer = GameObject.Find("newplayer").GetComponent<player>();

                    playerpos = GameObject.Find("ringeffecrspawn").transform;
                    transform.LookAt(new Vector3(playerpos.position.x , playerpos.position.y , playerpos.position.z));
                    // jumppositon = GameObject.Find("newplayer").transform;
                    // launch();
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                    // transform.DOJump(playerpos.position , 2f , 1 , 0.8f);
                }
                    
            }else{
                transform.Translate(Vector3.up * 15 *Time.deltaTime);
                waitt-=Time.deltaTime;
            }

        
    }

    void launch()
    {
        Physics.gravity = Vector3.up * gravity;
        rb.useGravity = true;

        rb.velocity = calculateLaunch();
    }

    Vector3 calculateLaunch()
    {
        float displacementy = jumppositon.position.y - transform.position.y;
        Vector3 displacementxz = new Vector3(jumppositon.position.x - transform.position.x , 0 , jumppositon.position.z - transform.position.z);
        Vector3 velocityY = Vector3.up* Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementxz / (Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementy - h)/gravity));

        return velocityXZ;         
    }
}
