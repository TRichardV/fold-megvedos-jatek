using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SideRocketLauncherScript : MonoBehaviour {

    public GameObject rocket;
    
    public float damagePercent = 50;
    public float apsPercent = 50;

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

            if (InterceptionDirection(des.transform.position, this.transform.position, new Vector2(des.GetComponent<MeteorScript>().kX, des.GetComponent<MeteorScript>().kY), GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().rocketSpeed * Time.deltaTime, out var direction)) {

                GameObject obj = Instantiate(rocket);

                obj.transform.parent = GameObject.Find("RocketLauncher").transform;

                obj.transform.position = this.gameObject.transform.position;



                obj.GetComponent<RocketScript>().desX = direction.x;
                obj.GetComponent<RocketScript>().desY = direction.y;

                obj.GetComponent<RocketScript>().damage = damage;
                obj.GetComponent<RocketScript>().speed = GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().rocketSpeed * Time.deltaTime;
                

                Debug.Log(direction.x + " " + direction.y);

            }

        }
        else {

            canShotCounter -= (int)Mathf.Round(canShotCounter / 10);

        }

    }

    public bool InterceptionDirection(Vector2 a, Vector2 b, Vector2 vA, float sB, out Vector2 result) {

        var aToB = b - a;
        var dC = aToB.magnitude;
        var alpha = Vector2.Angle(aToB, vA) * Mathf.Deg2Rad;

        var sA = vA.magnitude;
        var r = sA / sB;

        if (SolveQuadratic( 1 - r * r, 2 * r * dC * Mathf.Cos(alpha), -(dC * dC), out var root1, out var root2) == 0) {

            result = Vector2.zero;
            return false;

        }
        var dA = Mathf.Max(root1, root2);
        var t = dA / sB;
        var c = a + vA * t;
        Debug.Log(c);
        //result = (c - b).normalized;
        result = c;
        
        return true;

    }

    public int SolveQuadratic(float a, float b, float c, out float root1, out float root2) {

        var discriminant = b * b - 4 * a * c;
        if (discriminant < 0) {

            root1 = Mathf.Infinity;
            root2 = -root1;
            return 0;

        }

        root1 = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
        root2 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);

        return discriminant > 0 ? 2 : 1;
    
    }

    /*Vector2 getIronDome(GameObject target) {

        // OUR ROCKET'S SPEED
        float rSpeed = 5f * Time.deltaTime;//GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().rocketSpeed * Time.deltaTime;


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

            i+=10;

            /*if (time == i) {

                int e = i;
                while ()

            }

            if (i > 500) {

                time = -2;
                //Debug.Log("ajjajj");

            }

        }

        if (time != -2) {

            float g2X2 = tX + kX * time;
            float g2Y2 = tY + kY * time - 5 * kY;

            vec = new Vector2(g2X2, g2Y2);

        }
        else {

            vec = new Vector2(0f, 0f);

        }




        return vec;

    }*/

    /*Vector2 moveVector(float desX, float desY, float speed) {

        Vector2 move = new Vector2();

        // OUR POSITION
        float startX = this.gameObject.transform.localPosition.x;
        float startY = this.gameObject.transform.localPosition.y;

        // DIFFERENCE BETWEEN POSITIONS
        float dX = desX - startX;
        float dY = desY - startY;

        // DIRECT DIRECTION IN UNIT
        float unit = (float)Math.Floor(Math.Sqrt(Math.Abs(dX * dX) + Math.Abs(dY * dY)) / speed);

        // SPEED PER UNIT -> IN RATIO
        move = new Vector2(dX / unit, dY / unit);

        return move;

    }*/

    public void refreshDatas() {

        damage = GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().damage * (damagePercent/100);
        aps = GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().aps * (apsPercent / 100);

        randomCanShot();

    }

}
