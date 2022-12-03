using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EarthScript : MonoBehaviour {

    float maxHealth = 100f;
    float health = 0f;

    public int haveTankItem = 0;
    public int haveWarmog = 0;

    float heal;

    float waitTick = 0;
    float waitTickM;

    float healTick = -1;
    float healTickM;

    public GameObject hpBar;

    void Start() {

        health = 1;

    }

    public void getWarmog() {

        if (haveWarmog == 1) {

            waitTickM = (int)Mathf.Round(1 / Time.deltaTime * 15);

            healTickM = (int)Mathf.Round(1 / Time.deltaTime * 1.5f);

            heal = 1;

        }
        if (haveWarmog == 2) {

            this.maxHealth *= 1.5f;

            waitTickM = (int)Mathf.Round(1 / Time.deltaTime * 20);

            healTickM = (int)Mathf.Round(1 / Time.deltaTime * 1f);

            heal = 2f;

        }

    }

    private void FixedUpdate() {

        hpBar.GetComponent<TextMeshProUGUI>().text = ((int)Mathf.Round(this.health) + "/" + this.maxHealth);
        
        if (waitTick == -1) {

            healTick++;

            if (healTick >= healTickM) {

                // HEAL

                this.health += this.maxHealth * (this.heal / 100);

                if (this.health > this.maxHealth) {

                    this.health = this.maxHealth;

                }

                healTick = 0;

            }

        }
        if (waitTick > -1) {

            waitTick++;

            if (waitTick >= waitTickM) {

                waitTick = -1;

            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag.Equals("meteor")) {

            waitTick = 0;

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
