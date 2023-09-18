using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getpoints : MonoBehaviour
{
    public float x;
    public float y;
    public Transform player1;
    public Transform player2;
    private Vector3 currentpos;
    public GameObject cube;
    public GameObject plane;
    public Camera cam;
    public GameObject forceFill;
    public Vector3 groundMousepos;
    public switchplayer getplayer;
    private void Start() {
        // getplayer = GetComponent<switchplayer>();
        getplayer = GameObject.Find("trackingObject").GetComponent<switchplayer>();
    }
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Tab)){
        // }
        if(Input.GetKey(KeyCode.Tab)){
            if(getplayer.playernum == 1){
                cube.transform.position = player1.position;
                currentpos = player1.position;
            }else{
                cube.transform.position = player2.position;
                currentpos = player2.position;
            }
            forceFill.SetActive(true);
            lookcam();
        }
        if(Input.GetKeyUp(KeyCode.Tab)){
            // forceFill.transform.localScale = Vector3.Lerp(forceFill.transform.localScale , new Vector3(0,0,0) , 12*Time.deltaTime);
            forceFill.transform.localScale = new Vector3(0,0,0);
            forceFill.SetActive(false);
            while(Vector3.Distance(cube.transform.position , currentpos)<100){
                x = groundMousepos.x+Random.Range(-200 ,200);
                y = groundMousepos.z+Random.Range(-200 ,200);
                cube.transform.position = new Vector3(x , cube.transform.position.y ,y);
            }
            Instantiate(plane , new Vector3(cube.transform.position.x , plane.transform.position.y+10 , cube.transform.position.z) , Quaternion.identity);
            // cube.transform.position = player.position;
        }   
    }

    public void lookcam(){
            forceFill.transform.localScale = Vector3.Lerp(forceFill.transform.localScale , new Vector3(40,20,40) , 12*Time.deltaTime);

       Ray camray = cam.ScreenPointToRay(Input.mousePosition);
       Plane groundplane = new Plane(Vector3.up , Vector3.zero);

       float raylen;
       if(groundplane.Raycast(camray , out raylen)){
           groundMousepos = camray.GetPoint(raylen);
           forceFill.transform.position= new Vector3(groundMousepos.x ,forceFill.transform.position.y , groundMousepos.z);
       }
    }
}
