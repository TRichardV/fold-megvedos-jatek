using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

        float dis = 100000f;

        for (int i = 0; i < meteors.Length; i++) {

            float difX = Math.Abs(meteors[i].transform.position.x - this.gameObject.transform.position.x);
            float difY = Math.Abs(meteors[i].transform.position.y - this.gameObject.transform.position.y);

            float dif = (float)Math.Sqrt(Math.Abs((difX * difX) + (difY * difY)));

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

        if (des != null && des.transform.position.y > this.transform.localPosition.y) {

            GameObject obj = Instantiate(rocket);

            obj.transform.parent = GameObject.Find("RocketLauncher").transform;

            obj.transform.position = this.gameObject.transform.position;

            Vector2 targetDis = getIronDome(des);

            obj.GetComponent<RocketScript>().desX = targetDis.x;
            obj.GetComponent<RocketScript>().desY = targetDis.y;

            obj.GetComponent<RocketScript>().damage = damage;
            obj.GetComponent<RocketScript>().speed = GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().rocketSpeed * Time.deltaTime;



            /*float a = Vector3.Distance(transform.position, new Vector3(des.transform.position.x, des.transform.position.y));
            float b = 1;
            float c = Vector3.Distance(new Vector3(b, transform.position.y), new Vector3(des.transform.position.x, des.transform.position.y));

            float angle = (Mathf.Acos((Mathf.Pow(a, 2) + Mathf.Pow(b, 2) - Mathf.Pow(c, 2)) / (2 * a * b))) * Mathf.Rad2Deg;
            obj.transform.rotation = Quaternion.Euler(0, 0, -90 + angle);*/

        }
        else {

            canShotCounter -= (int)Mathf.Round(canShotCounter / 10);

        }

    }

    Vector2 getIronDome(GameObject target) {

        // OUR ROCKET'S SPEED
        float rSpeed = GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().rocketSpeed * Time.deltaTime;


        // TARGET'S POSITONS
        float tX = target.transform.position.x;
        float tY = target.transform.position.y;

        // TARGET'S MOVE PER TICK
        float kX = target.GetComponent<MeteorScript>().kX;
        float kY = target.GetComponent<MeteorScript>().kY;

        // OUR POSITION
        float g1X = this.gameObject.transform.localPosition.x;
        float g1Y = this.gameObject.transform.localPosition.y;

        Vector2 vec = new Vector2();
        
        int time = -1;
        int i = 0;

        while (time == -1) {

            // TARGET'S POSITION
            float g2X = tX + kX * i;
            float g2Y = tY + kY * i;

            // ROCKET'S POSITION THERE
            float g3X = g1X + i * moveVector(g2X, g2Y, rSpeed).x;
            float g3Y = g1Y + i * moveVector(g2X, g2Y, rSpeed).y;

            if (tX > g1X) {

                if (g3X >= g2X && g3Y >= g2Y) {

                    time = i;

                }

            }

            else if (tX < g1X) {

                if (g3X <= g2X && g3Y >= g2Y) {

                    time = i;

                }

            }
            else {

                time = i;

            }

            i+=1;

            if (i > 200) {

                time = 0;
                Debug.Log("ajjajj");

            }

        }

        float g2X2 = tX + kX * time;
        float g2Y2 = tY + kY * time;



        vec = new Vector2(g2X2, g2Y2);

        return vec;

    }

    Vector2 moveVector(float desX, float desY, float speed) {

        Vector2 move = new Vector2();

        // OUR POSITION
        float startX = this.gameObject.transform.localPosition.x;
        float startY = this.gameObject.transform.localPosition.y;

        Debug.Log(startX + " " + startY);

        // DIFFERENCE BETWEEN POSITIONS
        float dX = desX - startX;
        float dY = desY - startY;

        // DIRECT DIRECTION IN UNIT
        float unit = (float)Math.Floor(Math.Sqrt(Math.Abs(dX * dX) + Math.Abs(dY * dY)) / speed);

        // SPEED PER UNIT -> IN RATIO
        move = new Vector2(dX / unit, dY / unit);

        return move;

    }

    void refreshDatas() {

        damage = GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().damage * (damagePercent/100);
        aps = GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().aps * (apsPercent / 100);

        randomCanShot();

    }

}
