using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soldhealth : MonoBehaviour {
    public float health;
    public float damage;
    public Animator animator;
    public GameObject sold;

    public GameObject medkit;

    private void Start() {
        animator = GetComponent<Animator>();
    }
    private void Update() {
        dead();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "bullet" || other.gameObject.tag == "ring")
        {
            health -= damage;
        }
    }
    void dead()
    {
        if(health <= 0)
        {

            animator.SetBool("dead" , true);
            sold.GetComponent<soldie>().enabled = false;
            Instantiate(medkit , transform.position , transform.rotation);
            Destroy(gameObject , 1f);
        }
    }

    
}