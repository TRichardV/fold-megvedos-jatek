using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{

    final int type_normal = 0;
    int type = type_normal;

    float desX = 0f;
    float desY = 3f;

    public Transform RocketP;


    void Start() {

        this.RocketP = this.GetComponent<Transform>();
        
    }


    void FixedUpdate() {

        RocketP.position = new Vector2(RocketP.position.x, RocketP.position.y+0.1f);

    }
}
