using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthScript : MonoBehaviour {

    float maxHealth = 100f;
    float health = 0f;

    public int haveTankItem = 0;

    void Start() {

        health = maxHealth;

    }

    private void FixedUpdate() {

        Debug.Log(health + "/" + maxHealth);

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag.Equals("meteor")) {

            if (haveTankItem == 1) {

                if (this.health <= (this.maxHealth*0.334f)) {

                    this.health -= (collision.gameObject.GetComponent<MeteorScript>().damage/3);

                }
                else {

                    this.health -= collision.gameObject.GetComponent<MeteorScript>().damage;

                }

            }
            else if (haveTankItem == 2) {

                if (this.health <= (this.maxHealth * 0.5)) {

                    this.health -= (collision.gameObject.GetComponent<MeteorScript>().damage / 4);

                }
                else {

                    this.health -= collision.gameObject.GetComponent<MeteorScript>().damage/1.5f;

                }

            }
            else {

                this.health -= collision.gameObject.GetComponent<MeteorScript>().damage;

            }

            if (this.health <= 0) {

                Debug.Log("HALÁL");

            }

            collision.gameObject.GetComponent<MeteorScript>().shot(99999);

        }

    }

}
