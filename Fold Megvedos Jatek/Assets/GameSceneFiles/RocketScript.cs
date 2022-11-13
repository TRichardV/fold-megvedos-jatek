using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour {

    readonly int type_normal = 0;
    int type;

    int damage = 1;

    float speed = 5f;

    public float desX;
    public float desY;

    float startX, startY;
    float kX, kY;

    readonly float rangeOutX = 5f;
    readonly float rangeOutY = 10f;


    public Transform RocketP;

    void Start() {

        type = type_normal;

        speed = speed * Time.fixedDeltaTime;

        this.RocketP = this.GetComponent<Transform>();

        startX = RocketP.position.x;
        startY = RocketP.position.y;

        float dX = desX - startX;
        float dY = desY - startY;

        float unit = Convert.ToSingle(Math.Sqrt(Math.Abs(dX * dX) + Math.Abs(dY * dY)) / speed);

        kX = dX / unit;
        kY = dY / unit;

    }


    void FixedUpdate() {

        RocketP.position = new Vector2(RocketP.position.x + kX, RocketP.position.y + kY);

        if (RocketP.position.x > rangeOutX || RocketP.position.x < -rangeOutX ||
            RocketP.position.y > rangeOutY || RocketP.position.y < -rangeOutY) {

            Destroy(this.gameObject);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if(collision.gameObject.tag.Equals("meteor")) {

            collision.gameObject.GetComponent<MeteorScript>().shot(damage);

            Destroy(this.gameObject);

        }

    }

}
