using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class buttonanim : MonoBehaviour , IPointerEnterHandler ,IPointerExitHandler
{
    // Start is called before the first frame update
    private Animator anim;
    [SerializeField] private int xaxis;
    public bool notHover;
    public float lerptime=0;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(notHover){
            if(lerptime<1){
                transform.position = Vector3.Lerp(transform.position ,new Vector3(xaxis ,transform.position.y ,transform.position.z) , lerptime);
                lerptime += (Time.deltaTime);
            }else{
                lerptime=0;
                notHover=false;
            }
        }
    }
    public void OnPointerEnter(PointerEventData evenData){
        // anim.enabled=true;
        // anim.Play("buttonidle");
        anim.Play("buttonanim");
        // print("hovering");
        // print(transform.GetChild(0).name);
        // transform.GetChild(0).localScale = Vector3.Lerp(transform.GetChild(0).localScale , new Vector3(1.2f , 1.2f , 1.2f) , 20*Time.deltaTime);
        // transform.GetChild(0).localScale = new Vector3(1.2f , 1.2f , 1.2f);
    }
    public void OnPointerExit(PointerEventData eventData){
        // anim.StopPlayback()
        // anim.enabled=false;
        // notHover = true;
        // print("exit");
        // transform.Translate(xaxis * Time.deltaTime, 0, 0);
        // transform.position = Vector3.Lerp(transform.position ,new Vector3(xaxis ,transform.position.y ,transform.position.z) , 25*Time.deltaTime);
        // transform.Translate(new Vector3(xaxis , transform.position.y ,transform.position.z) , 15* Time.deltaTime );
        anim.Play("buttonanim1");
        // transform.GetChild(0).localScale = new Vector3(1f,1f,1f);
    }
}
