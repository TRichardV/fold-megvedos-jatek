using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeteorScript : MonoBehaviour {

    public Transform MeteorP;

    public GameObject meteor;

    public GameObject parentB;

    public int type;
    public int level;

    public float damage;
    float score;
    float money;

    float maxHealth;
    public float health;

    float speed;


    public float desX;
    public float desY;

    float startX, startY;
    float kX, kY;

    int hIndex = 0;
    int hIndexMax;

    int hIndex2 = 0;
    
    public int hIndex3 = 0;

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

    bool canGetDamage = true;

    Vector3 scale;

    float[,,] stats;

    void statUpload() {

        // SPEED - HEALTH - DAMAGE - SCORE - MONEY

        stats = new float[15, 1, 5] {

            { { 1.7f, 10f, 10f, 5f, 1f } },
            { { 1.3f, 20f, 20f, 10f, 1.5f } },
            { { 1f, 30f, 30f, 50f, 2f } },
            { { 0f, 0f, 0f, 0f, 0f } },
            { { 0f, 0f, 0f, 0f, 0f } },
            { { 0f, 0f, 0f, 0f, 0f } },
            { { 1.8f, 22f, 30f, 40f, 2.25f } },
            { { 1.6f, 30f, 30f, 60f, 3.5f } },
            { { 1.6f, 30f, 30f, 80, 4f } },
            { { 3f, 1f, 15f, 1f, 0.05f } },
            { { 0.6f, 300f, 10f, 1000f, 100f } },
            { { 1.0f, 30f, 30f, 20f, 2f } },
            { { 2f, 1f, 0f, 0f, 3f } },
            { { 2f, 1f, 0f, 0f, 6f } },
            { { 2f, 1f, 0f, 0f, 10f } },

        };

    }

    void getStats(int index, int level) {

        speed = stats[index, level, 0];
        maxHealth = stats[index, level, 1];
        damage = stats[index, level, 2];
        score = stats[index, level, 3];
        money = stats[index, level, 4];

    } 

    void Start() {

    }

    public void set() {

        statUpload();

        switch (type) {

            case 0:

                getStats(0, 0);

                break;

            case 1:

                scale = new Vector3(0.16f, 0.16f, 0.16f);

                this.gameObject.transform.localScale = scale;

                getStats(1, 0);

                break;

            case 2:

                scale = new Vector3(0.22f, 0.22f, 0.22f);

                this.gameObject.transform.localScale = scale;

                getStats(2, 0);

                break;

            case 3:

                speed = 3f;

                movingX = (maxX * 2) / (colNumber - 1) / 4;
                movingTime = 0.2f;

                hIndexMax = (int)Math.Round(1 / Time.fixedDeltaTime * movingTime);

                movingUnit = movingX / hIndexMax;

                getStats(3, 0);

                break;

            case 4:

                scale = new Vector3(1.3f, 1.3f, 1.3f);

                this.gameObject.transform.localScale = scale;
                
                speed = 1f;

                getStats(4, 0);

                break;

            case 5:

                speed = 0.7f;

                movingX = (maxX * 2) / (colNumber - 1);
                movingTime = 0.2f;

                hIndexMax = (int)Math.Round(1 / Time.fixedDeltaTime * movingTime);

                movingUnit = movingX / hIndexMax;

                hIndex2 = Random.Range(0, 2);

                getStats(5, 0);

                break;

            case 10:

                speed = 1.5f;

                getStats(6, 0);

                break;

            case 11:

                speed = 1.5f;

                canShotCounterM = (int)Math.Round(Random.Range(1 / Time.fixedDeltaTime / minAPS, 1 / Time.fixedDeltaTime / maxAPS));

                minAPS = 0.5f;
                maxAPS = 0.5f;

                getStats(7, 0);

                break;

            case 12:

                speed = 1.5f;

                movingX = (maxX * 2) / (colNumber - 1);
                movingTime = 0.4f;

                hIndexMax = (int)Math.Round(1 / Time.fixedDeltaTime * movingTime);

                movingUnit = movingX / hIndexMax;

                getStats(8, 0);

                break;

            case 90:

                isBullet = true;
                scale = new Vector3(0.3f, 1f, 0.1f);

                this.gameObject.transform.localScale = scale;

                getStats(9, 0);

                break;

            case 100:

                speed = 3f;
                scale = new Vector3(0.5f, 0.5f, 0.5f);
                this.gameObject.transform.localScale = scale;

                hIndex = 0;

                hIndexMax = (int)MathF.Round(4 * (1/Time.deltaTime));
                
                hIndex2 = 0;

                getStats(10, 0);

                break;

            case 110:

                speed = 1.5f;

                canShotCounterM = (int)Math.Round(Random.Range(1 / Time.fixedDeltaTime / minAPS, 1 / Time.fixedDeltaTime / maxAPS));

                minAPS = 0.2f;
                maxAPS = 0.5f;

                getStats(11, 0);

                break;

            case 200:

                getStats(12, 0);

                break;

            case 201:

                getStats(13, 0);
                break;

            case 202:

                damage = 0f;

                getStats(14, 0);

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
            case 1: // NORMAL METEOR t2
            case 2: // NORMAL METEOR t3

                type0();
                break;

            case 3: // A MOVING METEOR (TO RIGHT AND LEFT)

                type3();
                break;

            case 4: // BIG METEOR

                type4();
                break;

            case 5: // METEOR WAVE TO THE RIGHT (OR LEFT)

                type5();
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

            case 100: // BOSS 1

                type100();
                break;

            case 110: // BOSS'S SPACESHIP

                type110();
                break;

            case 200: // COIN t1
            case 201: // COIN t2
            case 202: // COIN t3

                type200();
                break;

        }

    }

    public void shot(float damage) {

        if (canGetDamage == true) {

            health -= damage;

        }
        Debug.Log(health + " " + damage);

        if (health <= 0) {

            if (!isBullet) {

                if (type >= 110 && type < 200) {

                    parentB.GetComponent<MeteorScript>().hIndex3--;

                }
                else {

                    GameObject.Find("SecretMeteorLauncher").GetComponent<SecretMeteorLauncherScript>().minusMeteor();

                }

            }

            if (damage != 99999) {

                GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().score += this.score;
                GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().money += this.money;

            }

            Destroy(this.gameObject);

        }

    }

    void createEnemyRocket(float desX, float desY, float posX, float posY, int type, int level) {

        GameObject obj = Instantiate(meteor, new Vector2(posX, posY), Quaternion.identity);

        //obj.transform.parent = this.gameObject.transform;

        obj.transform.position = new Vector2(posX, posY);

        obj.GetComponent<MeteorScript>().desX = desX;
        obj.GetComponent<MeteorScript>().desY = desY;

        obj.GetComponent<MeteorScript>().type = type;
        obj.GetComponent<MeteorScript>().level = level;

        if (type >= 110) {

            obj.GetComponent<MeteorScript>().parentB = this.gameObject;

        }

        obj.GetComponent<MeteorScript>().set();

    }

    public void type0() {

        MeteorP.position = new Vector2(MeteorP.position.x + kX, MeteorP.position.y + kY);

    }

    public void type3() {

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

    public void type4() {

        MeteorP.position = new Vector2(MeteorP.position.x + kX, MeteorP.position.y + kY);

    }

    public void type5() {

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

    public void type100() {

        if (hIndex2 == 0) { // MOVE IN

            MeteorP.position = new Vector2(MeteorP.position.x, MeteorP.position.y - speed);

        }
        if (MeteorP.position.y <= 3f && hIndex2 == 0) {

            hIndex2 = 1;

        }
        if (hIndex2 == 1) { // PREP PHASE

            hIndex++;

            if (hIndex >= hIndexMax) {

                hIndex2 = 2;
                hIndex = 0;

            }

        }

        if (hIndex2 == 2) { //SPAWN SPACESHIPS

            canGetDamage = false;
            
            hIndex3 = 2;

            hIndex2 = 3;

            createEnemyRocket(MeteorP.position.x - 1f, MeteorP.position.y - 1f, MeteorP.position.x - 1f, MeteorP.position.y + 3f, 110, 0);
            createEnemyRocket(MeteorP.position.x + 1f, MeteorP.position.y - 1f, MeteorP.position.x + 1f, MeteorP.position.y + 3f, 110, 0);

        }

        if (hIndex3 <= 0 && hIndex2 == 3) {

            canGetDamage = true;

            hIndex2 = 1;

        }

    }

    public void type110() {

        if (MeteorP.position.y >= desY) {

            MeteorP.position = new Vector2(MeteorP.position.x + kX, MeteorP.position.y + kY);

        }

        canShotCounter++;

        if (canShotCounter >= canShotCounterM) {

            canShotCounter = 0;
            canShotCounterM = (int)Math.Round(Random.Range(1 / Time.fixedDeltaTime / minAPS, 1 / Time.fixedDeltaTime / maxAPS));

            createEnemyRocket(MeteorP.position.x, -20, MeteorP.position.x, MeteorP.position.y, 90, 0);

        }

    }

    public void type200() {

        MeteorP.position = new Vector2(MeteorP.position.x + kX, MeteorP.position.y + kY);

    }

}
