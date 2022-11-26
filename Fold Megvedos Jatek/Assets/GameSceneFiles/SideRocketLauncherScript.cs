using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideRocketLauncherScript : MonoBehaviour {

    public GameObject rocket;
    
    float damagePercent = 50;
    float apsPercent = 100;

    float damage;
    float aps;

    int apsRate = 10;

    int canShotCounterMax;
    int canShotCounter = 0;

    private void Start() {

        refreshDatas();

    }

    private void FixedUpdate() {

        canShotCounter++;

        if (canShotCounter >= canShotCounterMax) {

            canShotCounter = 0;
            shootRocket();

            refreshDatas();

        }

    }

    GameObject getNearestObject() {

        GameObject n = null;

        GameObject[] meteors = GameObject.FindGameObjectsWithTag("meteor");
        float dis = Mathf.Infinity;

        for (int i = 0; i < meteors.Length; i++) {

            float dif = Mathf.Abs((meteors[i].transform.position - this.gameObject.transform.position).sqrMagnitude);

            if ( dif < dis ) {

                dis = dif;
                n = meteors[i];

            }

        }

        return n;

    }

    void randomCanShot() {

        int rate = Random.Range(0, apsRate);
        float apsPlusChance = aps * (rate / 100f);
        canShotCounterMax = (int)Mathf.Round(((1 / Time.deltaTime) / ((float)aps + apsPlusChance)));

    }

    void shootRocket() {

        GameObject des = getNearestObject();

        if (des != null) {

            GameObject obj = Instantiate(rocket);

            obj.transform.parent = GameObject.Find("RocketLauncher").transform;

            obj.transform.position = this.gameObject.transform.position;

            obj.GetComponent<RocketScript>().desX = des.transform.position.x;
            obj.GetComponent<RocketScript>().desY = des.transform.position.y;

            obj.GetComponent<RocketScript>().damage = damage;



            float a = Vector3.Distance(transform.position, new Vector3(des.transform.position.x, des.transform.position.y));
            float b = 1;
            float c = Vector3.Distance(new Vector3(b, transform.position.y), new Vector3(des.transform.position.x, des.transform.position.y));

            float angle = (Mathf.Acos((Mathf.Pow(a, 2) + Mathf.Pow(b, 2) - Mathf.Pow(c, 2)) / (2 * a * b))) * Mathf.Rad2Deg;
            obj.transform.rotation = Quaternion.Euler(0, 0, -90 + angle);

        }
        else {

            canShotCounter -= (int)Mathf.Round(canShotCounter / 10);

        }

    }

    Vector2 getIronDome(GameObject target) {

        float kX = target.GetComponent<MeteorScript>().kX;
        float kY = target.GetComponent<MeteorScript>().kY;

        float g1X = this.transform.position.x;
        float g1Y = this.transform.position.y;

        Vector2 vec = new Vector2();
        
        int time = -1;
        int i = 0;

        while (time == -1) {

            float g2X = kX * i;
            float g2Y = kY * i;



        }







        return vec;

    }

    void refreshDatas() {

        damage = GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().damage * (damagePercent/100);
        aps = GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().aps * (apsPercent / 100);

        randomCanShot();

    }

}
