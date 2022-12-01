using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RocketScript : MonoBehaviour {

    readonly int type_normal = 0;
    int type;

    public float damage;

    public float speed;

    public float desX;
    public float desY;

    float startX, startY;
    float kX, kY;

    readonly float rangeOutX = 5f;
    readonly float rangeOutY = 10f;

    ParticleSystem explosionParticle;
    bool isExploded = false;

    public Transform RocketP;

    public GameObject Electric;

    int haveKnuts;
    int chanceOfCrit = 0;

    void Start() {

        type = type_normal;

        this.RocketP = this.GetComponent<Transform>();

        startX = RocketP.position.x;
        startY = RocketP.position.y;

        float dX = desX - startX;
        float dY = desY - startY;

        float unit = Convert.ToSingle(Math.Sqrt(Math.Abs(dX * dX) + Math.Abs(dY * dY)) / speed);

        kX = dX / unit;
        kY = dY / unit;

        haveKnuts = GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().haveKnuts;

        Electric.GetComponent<ElectricScript>().damage = 2000f;//this.damage

        if (haveKnuts == 1) {

            chanceOfCrit = 100;

        }
        else if (haveKnuts == 2) {

            chanceOfCrit = 100;

        }

    }

    bool doCritDamage() {

        return Random.Range(0, 100) < chanceOfCrit;

    }

    void FixedUpdate() {

        if (!isExploded)
        {
            RocketP.position = new Vector2(RocketP.position.x + kX, RocketP.position.y + kY);
        }

        if (RocketP.position.x > rangeOutX || RocketP.position.x < -rangeOutX ||
            RocketP.position.y > rangeOutY || RocketP.position.y < -rangeOutY) {

            Destroy(this.gameObject);

        }

    }

    

    private void OnTriggerEnter2D(Collider2D collision) {

        if(collision.gameObject.tag.Equals("meteor")) {

            if (haveKnuts > 0 && doCritDamage()) {

                Electric.gameObject.active = true;

            }

            collision.gameObject.GetComponent<MeteorScript>().shot(damage);

            if (collision.gameObject.GetComponent<MeteorScript>().isBullet == true) {

                this.damage -= collision.gameObject.GetComponent<MeteorScript>().health;

                if (this.damage <= 0) {

                    destroyT();

                }

            }
            else {

                destroyT();

            }

        }

    }

    void destroyT() {

        GameObject.Find("Main Camera").GetComponent<Shake>().start = true;

        transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        explosionParticle = transform.GetComponentInChildren<ParticleSystem>();
        explosionParticle.Play();

        StartCoroutine(Destroyer());

    }

    IEnumerator Destroyer()
    {
        isExploded = true;
        yield return new WaitForSeconds(explosionParticle.main.duration);
        Destroy(this.gameObject);
    }

}
