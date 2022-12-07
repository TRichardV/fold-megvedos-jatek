using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EarthScript : MonoBehaviour {

    // OBJECTS
    public GameObject hpBar;


    // STATS
    float maxHealth = 100f;

    float health;


    // WARMOG STATS
    float waitForHealInSec1 = 15f;
    float healInEverySecond1 = 1.5f;
    float healPercent1 = 1f;

    float waitForHealInSec2 = 20f;
    float healInEverySecond2 = 1f;
    float healPercent2 = 2f;


    // ARTEMIS STATS
    float activationPercent1 = 33f;
    float damagePercent1 = 40f;

    float activationPercent2 = 50f;
    float damagePercent2 = 25f;


    // WARMOG
    float healPercent;
    float waitForHealInSec;
    float healInEverySecond;


    // WARMOG AUXILIARY VARIABLES
    float waitTick = 0;
    float waitTickM;

    float healTick = -1;
    float healTickM;


    // ARTEMIS
    float activationPercent;
    float damagePercent;


    // ITEMS
    public int haveTankItem = 0;
    public int haveWarmog = 0;

    // EXPLOSION EFFECT
    public GameObject Explosion = null;

    private void Start() {

        health = maxHealth;

    }


    public void getWarmog() {

        if (haveWarmog == 1) {

            waitForHealInSec = waitForHealInSec1;
            healInEverySecond = healInEverySecond1;
            healPercent = healPercent1;

            waitTickM = (int)Mathf.Round(1 / Time.deltaTime * waitForHealInSec);
            healTickM = (int)Mathf.Round(1 / Time.deltaTime * healInEverySecond);

        }
        if (haveWarmog == 2) {

            waitForHealInSec = waitForHealInSec2;
            healInEverySecond = healInEverySecond2;
            healPercent = healPercent2;

            waitTickM = (int)Mathf.Round(1 / Time.deltaTime * waitForHealInSec);
            healTickM = (int)Mathf.Round(1 / Time.deltaTime * healInEverySecond);

        }

    }

    public void getArtemis() {

        if (haveTankItem == 1) {

            activationPercent = activationPercent1;
            damagePercent = damagePercent1;

        }
        if (haveTankItem == 2) {

            activationPercent = activationPercent2;
            damagePercent = damagePercent2;

        }

    }

    private void FixedUpdate() {

        hpBar.GetComponent<TextMeshProUGUI>().text = ((int)Mathf.Round(this.health) + "/" + this.maxHealth);
        
        if (waitTick == -1) {

            healTick++;

            if (healTick >= healTickM) {

                this.health += this.maxHealth * (this.healPercent / 100);

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


    private IEnumerator OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag.Equals("meteor")) {

            waitTick = 0;

            if (haveTankItem > 0) {

                if (this.health <= (this.maxHealth * activationPercent)) {

                    this.health -= (collision.gameObject.GetComponent<MeteorScript>().damage * (damagePercent/100));

                }
                else {

                    this.health -= collision.gameObject.GetComponent<MeteorScript>().damage;

                }

            }
            else {

                this.health -= collision.gameObject.GetComponent<MeteorScript>().damage;

            }

            if (this.health <= 0) {

                Debug.Log("HALÁL");

            }

            collision.gameObject.GetComponent<MeteorScript>().shot(999999);

            GameObject obj = Instantiate(Explosion, collision.transform.position, Quaternion.identity);
            obj.GetComponent<ParticleSystem>().Play();
            yield return new WaitForSeconds(1);
            Destroy(obj);
        }

    }

}
