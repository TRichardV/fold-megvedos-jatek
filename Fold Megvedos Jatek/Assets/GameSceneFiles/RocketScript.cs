using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{

    public Transform RocketP;

    // Start is called before the first frame update
    void Start()
    {

        this.RocketP = this.GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        RocketP.position = new Vector2(RocketP.position.x, RocketP.position.y+0.1f);

    }
}
