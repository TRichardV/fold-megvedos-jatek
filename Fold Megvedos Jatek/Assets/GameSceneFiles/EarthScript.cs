using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthScript : MonoBehaviour {

    float maxHealth = 100f;
    float health = 0f;

    void Start() {

        health = maxHealth;

    }

    private void FixedUpdate() {

        //Debug.Log(health + "/" + maxHealth);

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag.Equals("meteor")) {

            this.health -= collision.gameObject.GetComponent<MeteorScript>().damage;

            if (this.health <= 0) {

                //Debug.Log("AJJAJ");

            }

            collision.gameObject.GetComponent<MeteorScript>().shot(99999);

        }

    }

}
