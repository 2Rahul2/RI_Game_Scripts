using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parabola : MonoBehaviour
{
    public Rigidbody Player;
    public Transform target;

    public float gravity = -18;
    public float h = 20;

    private void Start() {
        Player.useGravity = false;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.X)){
            launch();
        }
    }

    void launch(){
        Physics.gravity = Vector3.up * gravity;
        Player.useGravity = true;
        Player.velocity = Calculatelaunch();
        print(Calculatelaunch());

    }

    Vector3 Calculatelaunch()
    {
        float displacementY = target.position.y - Player.position.y;
        
        Vector3 displacementXZ = new Vector3(target.position.x - Player.position.x , 0 , target.position.z - Player.position.z);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity* h );
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY-h)/gravity));

        return velocityXZ + velocityY;
    }
}
