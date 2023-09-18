using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jet : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    // private GameObject[] enemyinrange;
    public int speed;
    public getpoints GetCurrentPlayer;
    private float bombingtime;
    public float bombtime;
    public GameObject bomb;
    public Transform player1;
    public Transform player2;
    private Transform currentplayer;
    public Transform[] points;
    private bool once=true;
    public AudioSource jetSFX;
    // private GameObject playerObject;
    void Start()
    {
        jetSFX.Play();
        bombingtime = bombtime;
        GetCurrentPlayer = GameObject.Find("Main Camera").GetComponent<getpoints>();
        // if(GetCurrentPlayer.playernum == 1){
        //     currentplayer = GameObject.Find("newplayer").transform;
        // }else{
        //     currentplayer = GameObject.Find("player2").transform;
        // }
        // // player = GameObject.Find("newplayer");
        // try{
        //     player = GetCurrentPlayer.CurrentPlayerObject;
        // }catch{
        // }
        transform.LookAt(new Vector3(GetCurrentPlayer.groundMousepos.x , transform.position.y , GetCurrentPlayer.groundMousepos.z));
    }

    // Update is called once per frame
    void Update()
    {
        if(once){
            if(Vector3.Distance(GetCurrentPlayer.groundMousepos , transform.position)<25){
                // if(bombingtime<=0){
                //     Instantiate(bomb , new Vector3(GetCurrentPlayer.groundMousepos.x + Random.Range(-5,5) , transform.position.y , GetCurrentPlayer.groundMousepos.z+Random.Range(-5 ,5)) , transform.rotation);
                //     bombingtime =bombtime;
                // }else{
                //     bombingtime -= Time.deltaTime;
                // }
                foreach(var point in points){
                    Instantiate(bomb , point.position , transform.rotation);
                }
                // Collider[] enemyinrange = Physics.OverlapSphere(GetCurrentPlayer.CurrentPlayerPos.position , 15f);
                // // enemyinrange = GameObject.FindGameObjectsWithTag("eskill");
                // foreach(var enemy in enemyinrange){
                //     if(enemy.tag == "eskill"){
                //         print("yes");
                //     }
                // }
                once=false;
            }
        }
        transform.Translate(Vector3.forward * speed *Time.deltaTime);
        Destroy(gameObject , 6f);
    }
}
