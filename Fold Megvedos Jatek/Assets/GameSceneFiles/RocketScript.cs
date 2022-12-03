using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RocketScript : MonoBehaviour {

    public GameObject sRocket;

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

    float aoeDamage;
    float damageCrit;

    int haveTrisagion = 0;

    bool first = true;

    public bool canSplit = false;
    int haveTwenty = 0;

    public bool haveResist = false;
    int haveResistC = 0;
    int haveResistCM;


    void Start() {

        haveResistCM = (int)Mathf.Round(1 / Time.deltaTime * 0.25f);

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

        if (haveKnuts == 1) {

            chanceOfCrit = 100;
            aoeDamage = 10f;
            damageCrit = 30f;

        }
        else if (haveKnuts == 2) {

            chanceOfCrit = 100;
            aoeDamage = 10f;
            damageCrit = 30f;

        }

        haveTrisagion = GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().haveTrisagion;

        haveTwenty = GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().haveTwenty;

    }

    bool doCritDamage() {

        return Random.Range(0, 100) < chanceOfCrit;

    }

    void FixedUpdate() {

        Debug.Log("rocket's damage: " + damage);

        if (haveResist) {

            haveResistC++;

            if (haveResistC > haveResistCM) {

                haveResist = false;

            }

        }

        if (!isExploded)
        {
            RocketP.position = new Vector2(RocketP.position.x + kX, RocketP.position.y + kY);
        }

        if (RocketP.position.x > rangeOutX || RocketP.position.x < -rangeOutX ||
            RocketP.position.y > rangeOutY || RocketP.position.y < -rangeOutY) {

            Destroy(this.gameObject);

        }

    }

    void tryShoot(float damagee) {

        if (haveTwenty > 0 && canSplit) {

            createSecondRocket(damagee);
            createSecondRocket(damagee);

        }

    }
    int lastI = -1;

    void createSecondRocket(float damagee) {

        GameObject obj = Instantiate(sRocket);

        obj.transform.parent = GameObject.Find("RocketLauncher").gameObject.transform;

        obj.transform.position = this.gameObject.transform.position;

        float x1 = this.transform.position.x;
        float y1 = this.transform.position.y;

        int rIndex = Random.Range(0, 9);

        while (rIndex == lastI) {

            rIndex = Random.Range(0, 9);

        }

        lastI = rIndex;

        switch (rIndex) {

            case 0: x1 -= 2f; break;
            case 1: x1 -= 2f; y1 += 1f; break;
            case 2: x1 -= 2f; y1 += 2f; break;
            case 3: x1 -= 1f; y1 += 2f; break;
            case 4: y1 += 2f; break;
            case 5: x1 += 1f; y1 += 2f; break;
            case 6: x1 += 2f; y1 += 2f; break;
            case 7: x1 += 2f; y1 += 1f; break;
            case 8: x1 += 2f; break;

        }

        obj.GetComponent<RocketScript>().desX = x1;
        obj.GetComponent<RocketScript>().desY = y1;

        if (haveTwenty == 1) {

            obj.GetComponent<RocketScript>().damage = damagee / 3;

        }
        else if (haveTwenty == 2) {

            obj.GetComponent<RocketScript>().damage = damagee / 2;

        }

        obj.GetComponent<RocketScript>().canSplit = false;

        obj.GetComponent<RocketScript>().haveResist = true;

        obj.GetComponent<RocketScript>().speed = speed;


        float a = Vector3.Distance(transform.position, new Vector3(x1, y1));
        float b = 1;
        float c = Vector3.Distance(new Vector3(b, transform.position.y), new Vector3(x1, y1));

        float angle = (Mathf.Acos((Mathf.Pow(a, 2) + Mathf.Pow(b, 2) - Mathf.Pow(c, 2)) / (2 * a * b))) * Mathf.Rad2Deg;
        obj.transform.rotation = Quaternion.Euler(0, 0, -90 + angle);

    }

    

    private void OnTriggerEnter2D(Collider2D collision) {

        if(collision.gameObject.tag.Equals("meteor") && !haveResist) {

            if (haveKnuts > 0 && haveTrisagion == 0) {

                if (collision.gameObject.GetComponent<MeteorScript>().isBullet != true) {

                    //collision.gameObject.GetComponent<MeteorScript>().damage;

                    // DO CRIT DAMAGE
                    if (doCritDamage()) {

                        GameObject el = Instantiate(Electric);
                        el.GetComponent<ElectricScript>().damagedMeteors.Add(this.gameObject);

                        if (haveKnuts == 1) {

                            el.GetComponent<ElectricScript>().damage = aoeDamage;
                            el.transform.position = this.transform.position;
                            collision.gameObject.GetComponent<MeteorScript>().shot(damageCrit);
                            tryShoot(damageCrit); // 20/20

                        }
                        else if (haveKnuts == 2) {

                            el.GetComponent<ElectricScript>().damage = aoeDamage;
                            el.transform.position = this.transform.position;
                            collision.gameObject.GetComponent<MeteorScript>().shot(damageCrit);
                            tryShoot(damageCrit); // 20/20

                        }

                    }

                    // DIDNT DO CRIT
                    else {

                        collision.gameObject.GetComponent<MeteorScript>().shot(damage);
                        tryShoot(damage); // 20/20

                    }



                }

                // IF IT IS A BULLET
                else if (collision.gameObject.GetComponent<MeteorScript>().isBullet == true) {

                    collision.gameObject.GetComponent<MeteorScript>().shot(damage);
                    this.damage -= collision.gameObject.GetComponent<MeteorScript>().health;

                    if (this.damage <= 0) {

                        destroyT();

                    }

                }

            }
            else if (haveTrisagion > 0 && haveKnuts == 0) {

                // IF IT ISN'T A BULLET
                if (!collision.gameObject.GetComponent<MeteorScript>().isBullet == true) {

                    if (first) {

                        first = false;
                        collision.gameObject.GetComponent<MeteorScript>().shot(damage);
                        tryShoot(damage); // 20/20
                        
                        if (haveTrisagion == 1) {

                            this.damage *= 0.5f;

                        }
                        else if (haveTrisagion == 2) {

                            this.damage *= 0.5f;

                        }

                    }

                    else if (!first) {

                        collision.gameObject.GetComponent<MeteorScript>().shot(damage);
                        tryShoot(damage); // 20/20

                    }



                }
                // IF IT IS A BULLET
                else {

                    collision.gameObject.GetComponent<MeteorScript>().shot(damage);

                }


            }
            else if (haveKnuts == 0 && haveTrisagion == 0) {

                if (collision.gameObject.GetComponent<MeteorScript>().isBullet == true) {

                    collision.gameObject.GetComponent<MeteorScript>().shot(damage);
                    this.damage -= collision.gameObject.GetComponent<MeteorScript>().health;

                    if (this.damage <= 0) {

                        destroyT();

                    }

                }
                else if (collision.gameObject.GetComponent<MeteorScript>().isBullet == false) {

                    tryShoot(damage); // 20/20

                    collision.gameObject.GetComponent<MeteorScript>().shot(damage);

                    destroyT();

                }

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
