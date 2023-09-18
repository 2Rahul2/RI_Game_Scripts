using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildings : MonoBehaviour
{
    

    public int _buildhealth;
    public int damage; 
    public MeshRenderer material;
    public Material mat1;
    public Transform particlespoint;

    public GameObject flame;
    [SerializeField] private GameObject[] throwObject;
    public GameObject explosion ,carExplode;
    public bool change , car;
    public buildhealth MainCount;
    private AudioSource Eaudio;
    // Update is called once per frame
    private void Start() {
        Eaudio = GetComponent<AudioSource>();
        material = GetComponent<MeshRenderer>();
        // change = false;
    }
    void Update()
    {
        if(_buildhealth <= 0)
        {
            MainCount.Count += 1;
            if(car){
                foreach(var obj in throwObject){
                    Instantiate(carExplode, obj.transform.position , Quaternion.identity);
                    Rigidbody rb =obj.GetComponent<Rigidbody>();
                    propsFallForce fallforceScript = obj.GetComponent<propsFallForce>();
                    rb.constraints = RigidbodyConstraints.None;
                    rb.useGravity=true;
                    int len = fallforceScript.angle.Length-1;
                    float xDir = Mathf.Cos(fallforceScript.angle[Random.Range(0,len)] *Mathf.PI / 180)*10;
                    float yDir = Mathf.Sin(fallforceScript.angle[Random.Range(0,len)] * Mathf.PI/180) * 30;
                    if(Random.Range(0 ,2) == 1){
                        rb.AddForce(-xDir , yDir , 0 ,ForceMode.Impulse);
                    }else{rb.AddForce(xDir , yDir , 0 ,ForceMode.Impulse);}
                    obj.GetComponent<propsFallForce>().isfall = true;
                    // obj.GetComponent<Rigidbody>().useGravity=true;
                    // obj.GetComponent<Rigidbody>().AddForce(transform.up * 30 , ForceMode.Impulse);
                }
            }
            if(change)
            {
                explo();
                flamings();
                material.sharedMaterial = mat1; 
                Eaudio.Play();
                change = false;
                this.enabled=false;
            }
             
            // Transform randompoint = spawnpoint[Random.Range(0 , spawnpoint.Length - 1)];
            // Instantiate(oppo , randompoint.position , Quaternion.identity);
            //Destroy(gameObject);
        }
    }
    void explo()
    {
        Instantiate(explosion , particlespoint.position , Quaternion.identity);
        //Destroy(explosion , 1f);
    }
    void flamings()
    {
        Instantiate(flame , particlespoint.position , Quaternion.identity);
        //Destroy(flame , 10f);
    }
    

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "bullet")
        {
            _buildhealth -= damage;
        }
    }
}
