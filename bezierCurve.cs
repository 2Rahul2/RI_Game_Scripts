using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bezierCurve : MonoBehaviour
{
 
    // public GameObject[] route;
    // [System.Serializable]
    // public Transform[,] points= new Transform[1,1];


    private int numofPoints=50;
    private Vector3 moveposition;
    private int first=0,second=0;
    public float wait=0.2f;
    public float resetwait;
    private Vector3 initialpos;
    public int numofpositions;


    public GameObject[] Routes;
    private Vector3[,] positions;
    private Transform[,] arrayy;
    // public GameObject travelObject;
    private Transform finalDestination;
    private float distancebetweentwopoints;
    // [HideInInspector]
    // private Transform startposition;
    public bool calculated;
    public LayerMask isplayer;
    public GameObject hiteffect;
    public switchplayer getcurrentplayerpos;
    private Transform player1;
    private Transform player2;
    public AudioSource bulletsfx;

    // Start is called before the first frame update
    void Start()
    {
        getcurrentplayerpos = GameObject.Find("trackingObject").GetComponent<switchplayer>();
        player1 = getcurrentplayerpos.player1.transform.GetChild(0);
        player2 = getcurrentplayerpos.player2.transform;
        if(getcurrentplayerpos.playernum == 1){
            finalDestination = player1;
        }else{
            finalDestination = player2;
        }
        // startposition.position = transform.position;
        // finalDestination= GameObject.FindGameObjectsWithTag("Player")[0].transform;
        initialpos = transform.position;
        arrayy = new Transform[numofpositions, 3];
        positions = new Vector3[numofpositions ,50];
        for(int i=0;i<numofpositions;i++){
            for(int j=0;j<3;j++){
                arrayy[i , j] =  Routes[i].transform.GetChild(j);
            }
        }
        // initialise first pos of next route as last pos of previous route
        for(int i=0;i<numofpositions;i++){
            for(int j=0;j<3;j++){
                if(j==0&&i>0){
                    arrayy[i , j].position = arrayy[i-1 ,2].position;
                }else{
                    if(j>1){
                        distancebetweentwopoints= Vector3.Distance(arrayy[i,j].position,arrayy[i,j-1].position);
                        while(Vector3.Distance(arrayy[i ,j].position , transform.position)>10&& distancebetweentwopoints>7 ){
                                arrayy[i ,j].position = new Vector3(Random.Range(initialpos.x-8 , initialpos.x+8),
                                Random.Range(initialpos.y-8 ,initialpos.y+8),
                                Random.Range(initialpos.z-8 ,initialpos.z+8)
                            );
                        }    
                    }else{
                        while(Vector3.Distance(arrayy[i ,j].position , transform.position)>10){
                            arrayy[i ,j].position = new Vector3(Random.Range(initialpos.x-5 , initialpos.x+5),
                            Random.Range(initialpos.y-5 ,initialpos.y+5),
                            Random.Range(initialpos.z-5 ,initialpos.z+5)
                            );
                        }
                    }
                }
            }
        }
        arrayy[0,0].position = transform.position;

        arrayy[numofpositions-1 ,2].position = new Vector3(finalDestination.position.x ,finalDestination.position.y+1 , finalDestination.position.z);
        // for(int i=0;i<Routes.Length;i++){
        //     for(int j=0;j<3;j++){
        //         print(arrayy[i , j].name);
        //     }
        // }
        // initialpos = transform.position;
     
        drawCurve();


    }
    void Update()
    {
            print(first);
            if(Physics.CheckSphere(transform.position , 0.5f ,isplayer) || Vector3.Distance(transform.position , finalDestination.position)<3f){
                Instantiate(hiteffect , transform.position ,transform.rotation);
                // bulletsfx.Stop();
                Destroy(gameObject);
            }
            // print("distance: " +Vector3.Distance(transform.position , finalDestination.position));
            Destroy(gameObject , 10f);

        if(calculated){
            if(first<numofpositions){
                if(wait<=0){
                    transform.position = positions[first,second];
                    // transform.position = positions[first,second];
                    // object.transform.position = positions[first , second];                    
                    second++;
                    if(second>=50){
                        second=0;
                        first++;
                    }
                    wait = resetwait;
                }else{
                    wait -= Time.deltaTime;
                }
            }      
        }
    }
    private void OnDrawGizmos() {
        if(calculated){
        for(int i =0;i<numofpositions;i++){
            for(int j=0;j<numofPoints;j++){
                // print(positions[i ,j]);
                Gizmos.DrawSphere(positions[i ,j] ,0.2f);
            }
        }          

        }
    }
    private void drawCurve(){
        // print("array length" + arrayy.Length);
        for(int k =0 ;k<numofpositions;k++){
            print("k"+k);
            for(int i=1;i<numofPoints+1 ;i++){
                float t = i / (float)numofPoints;
                positions[k ,i - 1] = calQuadCureve(t , arrayy[k ,0].position , arrayy[k , 1].position ,arrayy[k ,2].position);
                // positions[k ,i - 1] = calQuadCureve(t , point1.position , point2.position ,point3.position);
            }
        }
        calculated=true;
        bulletsfx.Play();
    }
    private Vector3 calQuadCureve(float ti ,Vector3 po , Vector3 p1 , Vector3 p2){
        float u = 1-ti;
        float tt = ti * ti;
        float uu = u * u;
        Vector3 p = uu * po;
        p += 2* u *ti *p1;
        p+= tt * p2;
        return p;
    }
}
