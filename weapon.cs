using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public GameObject bullet;
    public GameObject stungrenade;
    public Transform firepoint;
    public Transform stunpoint;

    public float waitforshoot = -2;
    private float startattack;

    private bool isshooting;

    private GameObject Sg;
    public bool stunlevel;
    private bool stunpresent;
    public AudioSource shootsound;

    //for throwing grenades
    // public GameObject stunG;
    // public float gravity;
    // public float h;
    // public Transform jumppositon;
    // public Transform launchpos;

    private float waittime = 5f;
    private void Start() {
        StartCoroutine(WaitAndPrint(waittime));
        isshooting = false;
    }
    void Update()
    {
        if(stunpresent){
            Destroy(Sg , 4f);
        }
        if(stunlevel)
        {
            if(Input.GetMouseButtonDown(0))
            {
                stunpresent = true;
                Sg = Instantiate(stungrenade  , stunpoint.position , Quaternion.identity);
                Sg.SetActive(true);
            }else
            {
                stunpresent = false;
            }
        }else
        {
            if(Input.GetMouseButton(0))
            {
                isshooting = true;
                if(startattack > 0)
                {
                    if(isshooting)
                    {
                        shoot();
                        startattack = waitforshoot;
                    }
                    }else
                    {
                        isshooting = false;
                        startattack += Time.deltaTime;
                    }            
            }
        }
    }
    private IEnumerator WaitAndPrint(float tim){
        print("waiting");
        yield return new WaitForSeconds(tim);
        print("done");
    }

    void shoot()
    {
        shootsound.Play();
        Instantiate(bullet , firepoint.position , firepoint.rotation);
    }

    // void launch()
    // {
    //     stunG.Physics.gravity = Vector3.up * gravity;
    //     stunG.GetComponent<Rigidbody>().useGravity = true;
    //     stunG.GetComponent<Rigidbody>().velocity = calclaunch();
    // }

    // Vector3 calclaunch()
    // {
    //     float displaceY = jumppositon.position.y - launchpos.position.y;

    //     Vector3 displaceXZ = new Vector3(jumppositon.position.x - launchpos.position.z , 0 , jumppositon.position.z - launchpos.position.z);

    //     Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity *h);
    //     Vector3 velocityXZ = displacementxz / (Mathf.Sqrt(-2 * h /gravity) + Mathf.Sqrt(2*(displacementy - h) / gravity));

    //     return velocityXZ + velocity;
    // }
}
