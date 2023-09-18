using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helicopter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject opponent;
    public bool findOpponent;
    public LayerMask isenemy;
    public switchplayer currentplayerPos;
    public Transform  mainBody , followPlayer;
    private Transform playerPos;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float timetoshoot , resetshoot;
    private Quaternion targetRotation;
    private float lerptime=1 , verticalLerpTime=1;
    public int distanceFollow , distant , rotateSpeed;
    void Start()
    {
        playerPos = currentplayerPos.CurrentPlayerPos;
        timetoshoot = resetshoot;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerPos = currentplayerPos.CurrentPlayerPos;
        followPlayer.LookAt(new Vector3(playerPos.position.x , followPlayer.position.y , playerPos.position.z));
        // Vector3 direction = followPlayer.TransformDirection(Vector3.forward);
        // print("direction : "+direction);
        float hori = Vector3.Dot(mainBody.forward , playerPos.position);
        // print((int)hori);
        // transform.position = Vector3.MoveTowards(transform.position , new Vector3())
        if(opponent!= null){
            // float rotationLook = opponent.transform.position.x - opponent.transform.position.z;
            Vector3 rotatePos = opponent.transform.position - mainBody.position;
            rotatePos.y = 0;
            // mainBody.LookAt(new Vector3(opponent.transform.position.x , mainBody.position.y , opponent.transform.position.z));
            // print(Vector3.Distance(mainBody.transform.position , playerPos.position));
            if(Vector3.Distance(mainBody.transform.position , playerPos.position) >distant){
                mainBody.position = Vector3.MoveTowards(mainBody.transform.position , new Vector3(playerPos.position.x ,transform.position.y , playerPos.position.z) ,distanceFollow * Time.deltaTime);
                Vector3 tiltRotation = playerPos.position - mainBody.position;
                if((int)hori>180){
                    // tiltRotation.y =0;tiltRotation.z =0;
                    if(verticalLerpTime<1){
                        mainBody.rotation = Quaternion.RotateTowards(mainBody.rotation ,Quaternion.LookRotation(tiltRotation) , rotateSpeed* Time.deltaTime);
                        // mainBody.transform.rotation = Quaternion.Lerp(mainBody.transform.rotation , Quaternion.Euler(20 , 0 , 0) , verticalLerpTime);
                        // mainBody.transform.rotation = Quaternion.Lerp(mainBody.transform.rotation , Quaternion.Euler(20 , mainBody.transform.position.y , mainBody.transform.position.z) , verticalLerpTime);
                        verticalLerpTime += Time.deltaTime;
                    }
                    // print("forwards");
                }else{
                    // print("backward");
                    if(verticalLerpTime<1){
                        Vector3 minusTiltRotation = tiltRotation;
                        minusTiltRotation.x = -minusTiltRotation.x;
                        mainBody.rotation = Quaternion.RotateTowards(mainBody.rotation ,Quaternion.LookRotation(minusTiltRotation) , rotateSpeed* Time.deltaTime);

                        // mainBody.transform.rotation = Quaternion.Lerp(mainBody.transform.rotation , Quaternion.Euler(20 , 0 , 0) , verticalLerpTime);
                        // mainBody.transform.rotation = Quaternion.Lerp(mainBody.transform.rotation , Quaternion.Euler(-20 , mainBody.transform.position.y , mainBody.transform.position.z) , verticalLerpTime);
                        verticalLerpTime += Time.deltaTime;
                    }
                }
                // mainBody.transform.position = Vector3.MoveTowards(mainBody.transform.position , new Vector3(opponent.transform.position.x+8 ,transform.position.y , opponent.transform.position.z+8) ,distanceFollow * Time.deltaTime);
            }else{
            mainBody.rotation = Quaternion.RotateTowards(mainBody.rotation ,Quaternion.LookRotation(rotatePos) , rotateSpeed* Time.deltaTime);
                verticalLerpTime = 0;
            }
            transform.LookAt(opponent.transform.position);
            if(timetoshoot<=0){
                Instantiate(bullet , transform.position , transform.rotation);
                timetoshoot = resetshoot;
            }else{
                timetoshoot -= Time.deltaTime;
            }
        }else{
            findOpponent=true;
        }
        if(lerptime<1){
            // mainBody.transform.rotation = Quaternion.Lerp(mainBody.transform.rotation ,Quaternion.Euler(mainBody.transform.position.x , targetRotation.y , mainBody.transform.position.z) ,lerptime);
            // mainBody.transform.rotation = Quaternion.Lerp(mainBody.transform.rotation ,Quaternion.Euler(mainBody.transform.position.x , opponent.transform.position.y , mainBody.transform.position.z) ,lerptime);
            lerptime += Time.deltaTime;
        }
        if(findOpponent){
            // mainBody.transform.Rotate(0,mainBody.transform.rotation.y,0);
            Collider[]  enemyinrange =Physics.OverlapSphere(new Vector3(playerPos.position.x  , playerPos.position.y  ,playerPos.position.z) , 35f , isenemy);
            if(enemyinrange != null){
                try{
                    opponent = enemyinrange[0].gameObject;
                    targetRotation = Quaternion.LookRotation(opponent.transform.position - mainBody.transform.position);
                    lerptime = 0;
                    findOpponent=false;
                }catch{
                    findOpponent=false;

                    opponent = null;
                }
            }
        }
    }
}
