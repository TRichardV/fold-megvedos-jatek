using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RocketScript : MonoBehaviour {

    // MISSILES, OBJECTS
    public GameObject sRocket;
    public GameObject Electric;


    // STATS
    public float damage;
    public float speed;


    // KNUTS HAMMER STATS
    public int chanceOfCrit;
    public float aoeDamageRate;
    public float damageCritRate;

    public float electricStillAlive;


    // 20/20 STATS
    public float twentyDamageRate;

    float haveResistInSec = 0.25f;


    // 20/20 ITEM
    public bool haveResist = false;
    int haveResistC = 0;
    int haveResistCM;


    // TRISAGION ITEM
    public float trisagionRate;

    // DESTINATION FIND
    public float desX;
    public float desY;

    float startX;
    float startY;
    float kX;
    float kY;


    // EXCEPTION HANDLE
    readonly float rangeOutX = 5f;
    readonly float rangeOutY = 10f;


    // EFFECTS
    ParticleSystem explosionParticle;
    bool isExploded = false;


    // ITEMS
    public int haveKnuts;
    public int haveTrisagion;
    public int haveTwenty;

    bool first = true;
    public bool canSplit = false;


    void Start() {

        haveResistCM = (int)Mathf.Round(1 / Time.deltaTime * haveResistInSec); 

        startX = transform.position.x;
        startY = transform.position.y;

        float dX = desX - startX;
        float dY = desY - startY;

        float unit = Convert.ToSingle(Math.Sqrt(Math.Abs(dX * dX) + Math.Abs(dY * dY)) / speed);

        kX = dX / unit;
        kY = dY / unit;

    }

    bool doCritDamage() {

        return Random.Range(0, 100) < chanceOfCrit;

    }

    void FixedUpdate() {

        if (haveResist) {

            haveResistC++;

            if (haveResistC > haveResistCM) {

                haveResist = false;

            }

        }

        if (!isExploded) {

            transform.position = new Vector2(transform.position.x + kX, transform.position.y + kY);
        
        }

        if (transform.position.x > rangeOutX || transform.position.x < -rangeOutX ||
            transform.position.y > rangeOutY || transform.position.y < -rangeOutY) {

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
        obj.transform.position = transform.position;

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

        obj.GetComponent<RocketScript>().damage = damage * twentyDamageRate;
        obj.GetComponent<RocketScript>().speed = speed;

        obj.GetComponent<RocketScript>().chanceOfCrit = chanceOfCrit;
        obj.GetComponent<RocketScript>().aoeDamageRate = aoeDamageRate;
        obj.GetComponent<RocketScript>().damageCritRate = damageCritRate;

        obj.GetComponent<RocketScript>().twentyDamageRate = twentyDamageRate;

        obj.GetComponent<RocketScript>().electricStillAlive = electricStillAlive;

        obj.GetComponent<RocketScript>().trisagionRate = trisagionRate;

        obj.GetComponent<RocketScript>().haveKnuts = haveKnuts;
        obj.GetComponent<RocketScript>().haveTrisagion = haveTrisagion;
        obj.GetComponent<RocketScript>().haveTwenty = haveTwenty;

        obj.GetComponent<RocketScript>().canSplit = false;
        obj.GetComponent<RocketScript>().haveResist = true;


        float a = Vector3.Distance(new Vector3(transform.position.x, transform.position.y), new Vector3(x1, transform.position.y));
        float b = Vector3.Distance(new Vector3(x1, transform.position.y), new Vector3(x1, y1));


        float angle = Mathf.Atan(b / a) * Mathf.Rad2Deg;

        if (x1 > transform.position.x) {

            angle = 0 - (90 - angle);

            if (y1 < transform.position.y) {

                angle -= 90;

            }

        }
        else if (x1 < transform.position.x) {

            angle = 90 - angle;

        }
        else {

            angle = 0;

        }

        obj.transform.rotation = Quaternion.Euler(0, 0, angle);

    }

    

    private void OnTriggerEnter2D(Collider2D collision) {

        if(collision.gameObject.tag.Equals("meteor") && !haveResist) {

            if (haveKnuts > 0 && haveTrisagion == 0) {

                if (collision.gameObject.GetComponent<MeteorScript>().isBullet != true) {

                    // DO CRIT DAMAGE
                    if (doCritDamage()) {

                        GameObject el = Instantiate(Electric);
                        el.GetComponent<ElectricScript>().aliveTickM = (int)Mathf.Round(1 / Time.deltaTime * electricStillAlive);
                        el.GetComponent<ElectricScript>().damagedMeteors.Add(collision.gameObject);

                        if (haveKnuts > 0) {

                            el.GetComponent<ElectricScript>().damage = damage * aoeDamageRate;
                            el.transform.position = this.transform.position;
                            collision.gameObject.GetComponent<MeteorScript>().shot(damage * damageCritRate);
                            tryShoot(damage * damageCritRate * twentyDamageRate); // 20/20

                        }

                    }

                    // DIDNT DO CRIT
                    else {

                        collision.gameObject.GetComponent<MeteorScript>().shot(damage);
                        tryShoot(damage * twentyDamageRate); // 20/20

                    }

                    destroyT();

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
                        tryShoot(damage * twentyDamageRate); // 20/20
                        damage *= trisagionRate;

                    }

                    else if (!first) {

                        collision.gameObject.GetComponent<MeteorScript>().shot(damage);
                        tryShoot(damage * twentyDamageRate); // 20/20

                    }

                }
                // IF IT IS A BULLET
                else {

                    collision.gameObject.GetComponent<MeteorScript>().shot(damage);

                }


            }
            else if (haveKnuts > 0 && haveTrisagion > 0) {

                if (collision.gameObject.GetComponent<MeteorScript>().isBullet == false) {

                    // DO CRIT DAMAGE
                    if (doCritDamage()) {

                        GameObject el = Instantiate(Electric);
                        el.GetComponent<ElectricScript>().damagedMeteors.Add(collision.gameObject);
                        el.GetComponent<ElectricScript>().aliveTickM = (int)Mathf.Round(1 / Time.deltaTime * electricStillAlive);

                        if (haveKnuts > 0) {

                            el.GetComponent<ElectricScript>().damage = damage * aoeDamageRate;
                            el.transform.position = transform.position;
                            collision.gameObject.GetComponent<MeteorScript>().shot(damage * damageCritRate);
                            tryShoot(damage * damageCritRate * twentyDamageRate); // 20/20

                        }

                    }

                    // DIDNT DO CRIT
                    else {

                        collision.gameObject.GetComponent<MeteorScript>().shot(damage);
                        tryShoot(damage * twentyDamageRate); // 20/20

                    }

                    if (first) {

                        first = false;
                        damage *= trisagionRate;

                    }

                }
                else {

                    collision.gameObject.GetComponent<MeteorScript>().shot(damage);

                }

            }
            else if (haveKnuts == 0 && haveTrisagion == 0) {

                if (collision.gameObject.GetComponent<MeteorScript>().isBullet == true) {

                    float dam = this.damage;
                    this.damage -= collision.gameObject.GetComponent<MeteorScript>().health;
                    collision.gameObject.GetComponent<MeteorScript>().shot(dam);

                    if (this.damage <= 0) {

                        destroyT();

                    }

                }
                else if (collision.gameObject.GetComponent<MeteorScript>().isBullet == false) {

                    tryShoot(damage * twentyDamageRate); // 20/20

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
