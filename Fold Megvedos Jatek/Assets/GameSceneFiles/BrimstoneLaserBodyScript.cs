using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrimstoneLaserBodyScript : MonoBehaviour
{

    // OBJECTS
    public GameObject laser;
    public GameObject Electric;
    public GameObject sRocket;


    // DAMAGED OBJECT
    public List<GameObject> inLaser = new List<GameObject>();


    // DEFAULT POSITIONS, SCALES
    float defScaleX = 4f;
    float defScaleY = 1f;
    float defPosZ = 4f;

    float headDefPosZ = 12f;


    // STATS
    public float damage;
    public float damagePerSec;

    public float brimDamageRate;
    public float twentyBrimstoneBoostRate;


    // KNUTS HAMMER STATS
    public int chanceOfCrit;
    public float aoeDamageRate;
    public float damageCritRate;

    public float electricStillAlive;

    int doAnythingM = 4; // DO TWENTY IN EVERY 4th ATTACK


    // 20/20 STATS
    public float twentyDamageRate;


    // TRISAGION
    public float trisagionRate;


    // DAMAGE COUNTERS
    public float damageTickM;
    float damageTick = 0;


    // ITEMS
    public int haveKnuts = 0;
    public int haveTrisagion = 0;
    public int haveTwenty = 0;

    bool canSplit = true; // TWENTY

    int doAnything = 0; // KNUT'S HAMMER

    bool doCritDamage() {

        return Random.Range(0, 100) < chanceOfCrit;

    }

    private void FixedUpdate() {

        if (haveTrisagion > 0) {

            damageTick++;

            if (damageTick >= damageTickM) {

                for (int i = 0; i < inLaser.Count; i++) {

                    doingDamage(inLaser[i]);

                }

                damageTick = 0;

            }

        }

    }

    void tryShoot(float damagee, GameObject obj) {

        if (doAnything == 0 && haveTwenty > 0 && canSplit) {

            createSecondRocket(damagee, obj);
            createSecondRocket(damagee, obj);

        }

        doAnything++;

        if (doAnything >= doAnythingM) {

            doAnything = 0;

        }

    }


    int lastI = -1;
    void createSecondRocket(float damagee, GameObject obja) {

        GameObject obj = Instantiate(sRocket);

        obj.transform.parent = GameObject.Find("RocketLauncher").gameObject.transform;
        obj.transform.position = obja.transform.position;

        float x1 = obja.transform.position.x;
        float y1 = obja.transform.position.y;

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

        obj.GetComponent<RocketScript>().damage = damagee / brimDamageRate;
        obj.GetComponent<RocketScript>().speed = GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().rocketSpeed;


        obj.GetComponent<RocketScript>().canSplit = false;
        obj.GetComponent<RocketScript>().haveResist = true;

        obj.GetComponent<RocketScript>().chanceOfCrit = chanceOfCrit;
        obj.GetComponent<RocketScript>().aoeDamageRate = aoeDamageRate;
        obj.GetComponent<RocketScript>().damageCritRate = damageCritRate;

        obj.GetComponent<RocketScript>().twentyDamageRate = twentyDamageRate / twentyBrimstoneBoostRate;

        obj.GetComponent<RocketScript>().electricStillAlive = electricStillAlive;

        obj.GetComponent<RocketScript>().trisagionRate = trisagionRate;

        obj.GetComponent<RocketScript>().haveKnuts = haveKnuts;
        obj.GetComponent<RocketScript>().haveTrisagion = haveTrisagion;
        obj.GetComponent<RocketScript>().haveTwenty = haveTwenty;

        obj.GetComponent<RocketScript>().canSplit = false;
        obj.GetComponent<RocketScript>().haveResist = true;

        obj.transform.rotation = Quaternion.Euler(0, 0, -90 + rotateRocket(obja.transform.position, x1, y1));

    }

    float rotateRocket(Vector3 pos01, float x1, float y1) {

        float a = Vector3.Distance(pos01, new Vector3(x1, y1));
        float b = 1;
        float c = Vector3.Distance(new Vector3(b, pos01.y), new Vector3(x1, y1));

        float angle = (Mathf.Acos((Mathf.Pow(a, 2) + Mathf.Pow(b, 2) - Mathf.Pow(c, 2)) / (2 * a * b))) * Mathf.Rad2Deg;

        return angle;

    }

    int a;
    void doingDamage(GameObject obj) {
        Debug.Log(a);
        a++;
        if (obj.gameObject.tag.Equals("meteor")) {

            if (haveKnuts > 0) {

                if (obj.gameObject.GetComponent<MeteorScript>().isBullet != true) {

                    // DO CRIT DAMAGE
                    if (doCritDamage()) {

                        GameObject el = Instantiate(Electric);
                        el.GetComponent<ElectricScript>().aliveTickM = (int)Mathf.Round(1 / Time.deltaTime * electricStillAlive);
                        el.GetComponent<ElectricScript>().damagedMeteors.Add(obj.gameObject);

                        if (haveKnuts > 0) {

                            el.GetComponent<ElectricScript>().damage = damage * aoeDamageRate;
                            el.transform.position = obj.transform.position;
                            obj.gameObject.GetComponent<MeteorScript>().shot(damageCritRate * damage);
                            tryShoot(damageCritRate * damage * twentyDamageRate, obj); // 20/20

                        }

                    }

                    // DIDNT DO CRIT
                    else {

                        obj.gameObject.GetComponent<MeteorScript>().shot(damage);
                        tryShoot(damage * twentyDamageRate, obj); // 20/20

                    }



                }

                // IF IT IS A BULLET
                else if (obj.gameObject.GetComponent<MeteorScript>().isBullet == true) {

                    obj.gameObject.GetComponent<MeteorScript>().shot(damage);

                }

            }
            else if (haveKnuts == 0 && haveTwenty > 0) {

                obj.gameObject.GetComponent<MeteorScript>().shot(damage);
                tryShoot(damage * twentyDamageRate, obj); // 20/20

            }

            else if (haveKnuts == 0) {

                obj.gameObject.GetComponent<MeteorScript>().shot(damage);

            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (!inLaser.Contains(collision.gameObject)) {

            inLaser.Add(collision.gameObject);

        }


    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (inLaser.Contains(collision.gameObject)) {

            inLaser.Remove(collision.gameObject);

        }

    }

}
