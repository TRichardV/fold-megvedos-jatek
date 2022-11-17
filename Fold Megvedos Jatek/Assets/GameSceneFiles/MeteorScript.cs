using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeteorScript : MonoBehaviour {

    public Transform MeteorP;

    public GameObject meteor;

    public int type;
    public int level;

    public int damage = 1;
    int score = 1;
    float money = 0.1f;

    float maxHealth = 10;
    float health;

    float speed = 2f;


    public float desX;
    public float desY;

    float startX, startY;
    float kX, kY;

    int hIndex = 0;
    int hIndexMax;

    int hIndex2 = 0;

    readonly float maxX = 2.2f;
    readonly int colNumber = 8;

    float movingTime;
    float movingX;
    float movingUnit;

    float minAPS = 0.5f;
    float maxAPS = 0.5f;

    int canShotCounter = 0;
    int canShotCounterM;

    public Boolean isBullet = false;

    Vector3 scale;

    void Start() {

    }

    public void set() {

        switch (type) {

            case 1:

                speed = 3f;

                movingX = (maxX * 2) / (colNumber - 1) / 4;
                movingTime = 0.2f;

                hIndexMax = (int)Math.Round(1 / Time.fixedDeltaTime * movingTime);

                movingUnit = movingX / hIndexMax;

                break;

            case 2:

                scale = new Vector3(1.3f, 1.3f, 2f);

                this.gameObject.transform.localScale = scale;
                
                speed = 1f;

                break;

            case 3:

                speed = 0.7f;

                movingX = (maxX * 2) / (colNumber - 1);
                movingTime = 0.2f;

                hIndexMax = (int)Math.Round(1 / Time.fixedDeltaTime * movingTime);

                movingUnit = movingX / hIndexMax;

                hIndex2 = Random.Range(0, 2);

                break;

            case 10:

                speed = 1.5f;
                break;

            case 11:

                speed = 1.5f;

                canShotCounterM = (int)Math.Round(Random.Range(1 / Time.fixedDeltaTime / minAPS, 1 / Time.fixedDeltaTime / maxAPS));

                minAPS = 0.5f;
                maxAPS = 0.5f;

                break;

            case 12:

                speed = 1.5f;

                movingX = (maxX * 2) / (colNumber - 1);
                movingTime = 0.4f;

                hIndexMax = (int)Math.Round(1 / Time.fixedDeltaTime * movingTime);

                movingUnit = movingX / hIndexMax;

                break;

            case 90:

                isBullet = true;
                speed = 4f;

                scale = new Vector3(0.3f, 1f, 0f);

                this.gameObject.transform.localScale = scale;

                break;

        }

        speed *= Time.fixedDeltaTime;

        health = maxHealth;

        this.MeteorP = this.GetComponent<Transform>();

        startX = MeteorP.position.x;
        startY = MeteorP.position.y;

        float dX = desX - startX;
        float dY = desY - startY;

        float unit = Convert.ToSingle(Math.Sqrt(Math.Abs(dX * dX) + Math.Abs(dY * dY)) / speed);

        kX = dX / unit;
        kY = dY / unit;

    }

    void FixedUpdate() {

        switch(type) {

            case 0: // NORMAL METEOR

                type0();
                break;

            case 1: // A MOVING METEOR (TO RIGHT AND LEFT)

                type1();
                break;

            case 2: // BIG METEOR

                type2();
                break;

            case 3: // METEOR WAVE TO THE RIGHT (OR LEFT)

                type3();
                break;

            case 10: // REGULAR SPACESHIP

                type10();
                break;

            case 11: // SHOOTING SPACESHIP

                type11();
                break;

            case 12: // MOVING AND SHOOTING SPACESHIP

                type12();
                break;

            case 90: // SPACESHIP BULLET 1

                type90();
                break;
        }

    }

    public void shot(float damage) {

        health -= damage;

        if (health <= 0) {

            if (!isBullet) {

                GameObject.Find("SecretMeteorLauncher").GetComponent<SecretMeteorLauncherScript>().minusMeteor();

            }

            if (damage != 99999) {

                GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().score += this.score;
                GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().money += this.money;

            }

            Destroy(this.gameObject);

        }

    }

    void createEnemyRocket(float desX, float desY, float posX, float posY, int type, int level) {

        GameObject obj = Instantiate(meteor);

        obj.transform.parent = this.gameObject.transform;

        obj.transform.position = new Vector2(posX, posY);

        obj.GetComponent<MeteorScript>().desX = desX;
        obj.GetComponent<MeteorScript>().desY = desY;

        obj.GetComponent<MeteorScript>().type = type;
        obj.GetComponent<MeteorScript>().level = level;

        obj.GetComponent<MeteorScript>().set();

    }

    public void type0() {

        MeteorP.position = new Vector2(MeteorP.position.x + kX, MeteorP.position.y + kY);

    }

    public void type1() {

        MeteorP.position = new Vector2(MeteorP.position.x + kX, MeteorP.position.y + kY);

        if (hIndex2 == 0) {

            MeteorP.position = new Vector2(MeteorP.position.x + movingUnit, MeteorP.position.y);

        }
        else if (hIndex2 == 1) {

            MeteorP.position = new Vector2(MeteorP.position.x - movingUnit, MeteorP.position.y);

        }

        hIndex++;

        if (hIndex >= hIndexMax) {

            hIndex = 0;

            if (hIndex2 == 0) {

                hIndex2 = 1;

            }
            else if (hIndex2 == 1) {

                hIndex2 = 0;

            }

        }

    }

    public void type2() {

        MeteorP.position = new Vector2(MeteorP.position.x + kX, MeteorP.position.y + kY);

    }

    public void type3() {

        MeteorP.position = new Vector2(MeteorP.position.x + kX, MeteorP.position.y + kY);

        if (hIndex2 == 0) {

            MeteorP.position = new Vector2(MeteorP.position.x + movingUnit, MeteorP.position.y);

            if (MeteorP.position.x >= maxX + 0.5f) {

                MeteorP.position = new Vector2(-maxX - 0.2f, MeteorP.position.y);

            }

        }

        else if (hIndex2 == 1) {

            MeteorP.position = new Vector2(MeteorP.position.x - movingUnit, MeteorP.position.y);

            if (MeteorP.position.x <= -maxX - 0.5f) {

                MeteorP.position = new Vector2(maxX + 0.2f, MeteorP.position.y);

            }

        }

    }

    public void type10() {

        MeteorP.position = new Vector2(MeteorP.position.x + kX, MeteorP.position.y + kY);

    }

    public void type11() {

        MeteorP.position = new Vector2(MeteorP.position.x + kX, MeteorP.position.y + kY);

        canShotCounter++;

        if (canShotCounter >= canShotCounterM) {

            canShotCounter = 0;
            canShotCounterM = (int)Math.Round(Random.Range(1 / Time.fixedDeltaTime / minAPS, 1 / Time.fixedDeltaTime / maxAPS));

            createEnemyRocket(MeteorP.position.x, -20, MeteorP.position.x, MeteorP.position.y, 90, 0);

        }

    }

    public void type12() {

        MeteorP.position = new Vector2(MeteorP.position.x + kX, MeteorP.position.y + kY);

        hIndex++;

        if (hIndex >= hIndexMax) {

            hIndex = 0;
            hIndex2++;

            if (hIndex2 >= 4) {

                hIndex2 = 0;

            }

        }

        if (hIndex2 == 1) {

            MeteorP.position = new Vector2(MeteorP.position.x - movingUnit, MeteorP.position.y);

        }

        if (hIndex2 == 3) {

            MeteorP.position = new Vector2(MeteorP.position.x + movingUnit, MeteorP.position.y);

        }

        canShotCounter++;

        if (canShotCounter >= canShotCounterM && (hIndex2 == 3 || hIndex2 == 1)) {

            canShotCounter = 0;
            canShotCounterM = (int)Math.Round(Random.Range(1 / Time.fixedDeltaTime / minAPS, 1 / Time.fixedDeltaTime / maxAPS));

            createEnemyRocket(MeteorP.position.x, -20, MeteorP.position.x, MeteorP.position.y, 90, 0);

        }

    }

    public void type90() {

        MeteorP.position = new Vector2(MeteorP.position.x + kX, MeteorP.position.y + kY);

    }

}
