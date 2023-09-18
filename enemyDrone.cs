using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDrone : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 finalposition;
    private boss Boss;
    public GameObject bullet;
    private trackdroneattackpos trackmarker; 
    private float waitforshoot =0.4f;
    private float extradistance;
    public Transform[] aimposition;
    public List<Transform> lookpos;
    private GameObject posobject;
    public int indexpo;
    private int index=0;
    private float totaltime=10f;
    void Start()
    {
        posobject = GameObject.FindGameObjectWithTag("Respawn");
        // posobject = GameObject.Find("droneforceField(Clone)");
        foreach(Transform child in posobject.transform){
            print(child.transform);
            lookpos.Add(child);
            print(index);
            // drone.GetComponent<enemyDrone>().lookpos[index].position = child.position;
            index+=1;      
        }
        extradistance = Random.Range(-10,10);
        trackmarker = GameObject.Find("dronepos").GetComponent<trackdroneattackpos>();
        Boss = GameObject.Find("Role_T").GetComponent<boss>();
        // mymarker = Instantiate(marker , transform.position , Quaternion.identity); 
            // if(Physics.Raycast(transform.position , transform.forward , out hit)){
            //     print(hit.point);
            //     shootposition = new Vector3(hit.point.x , hit.point.y , hit.point.z);
            //     mymarker.transform.position = new Vector3(shootposition.x , shootposition.y , shootposition.z);
            // }

        // finalposition. = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(totaltime<=0){
            trackmarker.runscript = false;
            trackmarker.readytoshoot = false;
            trackmarker.followtime = 5f;
            Destroy(posobject);
            Destroy(gameObject);
        }else{
            transform.position =  Vector3.Lerp(transform.position ,new Vector3 (finalposition.x , finalposition.y , finalposition.z) , 1.6f*Time.deltaTime);
            transform.LookAt(lookpos[indexpo].position);
            // transform.LookAt(new Vector3(trackmarker.shootposition.x + extradistance , trackmarker.shootposition.y , trackmarker.shootposition.z +extradistance));
            // transform.LookAt

            // if(Physics.Raycast(transform.position , transform.forward , out hit)){
            //     // print(hit.point);
            //     shootposition = new Vector3(hit.point.x , hit.point.y , hit.point.z);
            //     // GameObject mymarker = Instantiate(marker , new Vector3(shootposition.x , shootposition.y , shootposition.z) , Quaternion.identity); 
            //     mymarker.transform.position = new Vector3(shootposition.x , shootposition.y , shootposition.z);
            // }

            if(trackmarker.readytoshoot){
                if(waitforshoot<=0){
                    foreach(var aim in aimposition){
                        Instantiate(bullet , aim.position , transform.rotation);
                    }
                    waitforshoot = 0.9f;
                }else{
                    waitforshoot -= Time.deltaTime;
                }
            }
            totaltime -= Time.deltaTime;
        }
      
        
    }
}
