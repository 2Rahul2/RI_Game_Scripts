using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ringpart : MonoBehaviour
{
    public ParticleSystem ring;
    public float radiusspeed;

    // Update is called once per frame
    void Update()
    {
        ParticleSystem.ShapeModule radii = ring.shape;
        radii.radius += 0.8f;
        Destroy(gameObject , 2f);
    }
}
