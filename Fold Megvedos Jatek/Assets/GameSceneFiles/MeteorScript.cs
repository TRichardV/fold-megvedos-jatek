using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour {

    public Transform MeteorP;


    int level;
    public int damage = 1;

    int maxHealth = 1;
    int health;

    float speed = 5f;


    public float desX;
    public float desY;

    float startX, startY;
    float kX, kY;

    void Start() {

        speed *= Time.fixedDeltaTime;

        level = 0;

        health = maxHealth;

        this.MeteorP = this.GetComponent<Transform>();

        startX = MeteorP.position.x;
        startY = MeteorP.position.y;

        float dX = desX - startX;
        float dY = desY - startY;

        float unit = Convert.ToSingle(Math.Sqrt(Math.Abs(dX * dX) + Math.Abs(dY * dY)) / speed);

        kX = dX / unit;
        kY = dY / unit;

    }


    void FixedUpdate() {

        MeteorP.position = new Vector2(MeteorP.position.x + kX, MeteorP.position.y + kY);

    }

    public void shot(int damage) {

        health -= damage;

        if (health <= 0) {

            Destroy(this.gameObject);

        }

    }

}
