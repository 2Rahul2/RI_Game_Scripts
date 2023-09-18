using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackdroneattackpos : MonoBehaviour
{
    // public GameObject marker;
    public GameObject mymarker;
    private RaycastHit hit;
    public Vector3 shootposition;
    public float followtime;
    public bool readytoshoot=false;
    // private Transform[] droneattackpos;
    public enemyDrone drone;
    public bool runscript=false;
    // private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        mymarker = GameObject.FindGameObjectWithTag("Respawn");
        // mymarker = Instantiate(marker , transform.position , Quaternion.identity); 
        // foreach(Transform child in mymarker.transform){
        //     drone.GetComponent<enemyDrone>().lookpos[index].position = child.position;
        //     index+=1;      
        // }
        // if(Physics.Raycast(transform.position , transform.forward , out hit)){
        //         print(hit.point);
        //         shootposition = new Vector3(hit.point.x , hit.point.y , hit.point.z);
        //         mymarker.transform.position = new Vector3(shootposition.x , shootposition.y , shootposition.z);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if(runscript){
            // mymarker.transform.eulerAngles = Vector3.Lerp(transform.eulerAngles , new Vector3(35,2,35) , 0.8f * Time.deltaTime);
            if(followtime<=0){
                mymarker.transform.position = mymarker.transform.position;
                readytoshoot = true;
            }else{
                
            mymarker.transform.localScale = Vector3.Lerp(mymarker.transform.localScale , new Vector3(35,2,35) , 8f * Time.deltaTime);
                if(Physics.Raycast(transform.position , transform.forward , out hit)){
                        // print(hit.point);
                        shootposition = new Vector3(hit.point.x , hit.point.y , hit.point.z);
                        // GameObject mymarker = Instantiate(marker , new Vector3(shootposition.x , shootposition.y , shootposition.z) , Quaternion.identity); 
                        mymarker.transform.position = new Vector3(shootposition.x , shootposition.y - 0.2f , shootposition.z);
                }
                followtime -= Time.deltaTime;
            }
        }
    }
}
