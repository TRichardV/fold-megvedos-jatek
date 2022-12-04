using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeteorScript : MonoBehaviour {

    // OBJECTS
    public GameObject meteor;
    public GameObject parentB;


    // GENERAL STATS
    public int type;
    public int level;

    public Boolean isBullet = false;


    // DEF STATS
    public float damage;
    float score;
    float money;
    float maxHealth;
    public float speed;

    public float health;
    bool canGetDamage = true;


    // SHOOTING STATS
    float minAPS = 0.5f;
    float maxAPS = 0.5f;

    int canShotCounter = 0;
    int canShotCounterM;


    // DIRECTION SETTINGS
    public float desX;
    public float desY;
    float startX;
    float startY;
    public float kX;
    public float kY;

    float movingTime;
    float movingX;
    float movingUnit;


    // AUXILIARY VARIABLES
    int hIndex = 0;
    int hIndex2 = 0;
    public int hIndex3 = 0;

    int hIndexMax;

    Vector3 scale;


    // DEF SPAWNING CORDINATES
    readonly float maxX = 2.2f;
    readonly int colNumber = 8;


    // ICEBORN STATS
    public float defSpeed;
    public int slowTick = -1;
    public int slowTickM;
    public int stunTick = -1;
    public int stunTickM;

    public float slowInSec1 = 1.5f;
    public float slowInSec2 = 2f;
    public float stunInSec2 = 1f;

    // STATS
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
            { { 0.6f, 300f, 10f, 1000f, 100f } }, // BOSS
            { { 1.0f, 30f, 30f, 20f, 2f } },
            { { 2f, 1f, 0f, 0f, 3f } },
            { { 2f, 1f, 0f, 0f, 6f } },
            { { 2f, 1f, 0f, 0f, 10f } },

        };

    }

    void getStats(int index, int level) {

        speed = stats[index, level, 0];
        defSpeed = speed * Time.fixedDeltaTime;
        maxHealth = stats[index, level, 1];
        damage = stats[index, level, 2];
        score = stats[index, level, 3];
        money = stats[index, level, 4];

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

        startX = transform.position.x;
        startY = transform.position.y;

        float dX = desX - startX;
        float dY = desY - startY;

        float unit = Convert.ToSingle(Math.Sqrt(Math.Abs(dX * dX) + Math.Abs(dY * dY)) / speed);

        kX = dX / unit;
        kY = dY / unit;

    }

    void setTravel() {

        startX = transform.position.x;
        startY = transform.position.y;

        float dX = desX - startX;
        float dY = desY - startY;

        float unit = Convert.ToSingle(Math.Sqrt(Math.Abs(dX * dX) + Math.Abs(dY * dY)) / speed);

        kX = dX / unit;
        kY = dY / unit;

    }

    // STUN WITH ICEBORN
    void stun() {

        if (stunTick > -1) {

            stunTick++;

            if (stunTick == 1) {

                this.speed = 0f;


                setTravel();
            }

            if (stunTick >= stunTickM) {

                stunTick = -1;

                slowTick++;

            }

        }

    }

    // SLOW WITH ICEBORN
    void slow() {

        if (slowTick > -1) {

            slowTick++;

            if (slowTick == 1) {

                this.speed = defSpeed * 0.5f;

                setTravel();

            }


            if (slowTick >= slowTickM) {

                slowTick = -1;

                speed = defSpeed;

                setTravel();

            }

        }

    }

    void FixedUpdate() {

        stun(); // IF IT'S OVER IT STARTS THE SLOW
        slow();

        // TASK SWITCH
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
    
    // GET DAMAGE
    public void shot(float damage) {

        if (canGetDamage == true) {

            health -= damage;

            if (GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().haveIceborn == 1) {

                slowTickM = (int)Mathf.Round(1 / Time.deltaTime * slowInSec1);

                slowTick = 0;

            }
            else if (GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().haveIceborn == 2) {

                slowTickM = (int)Mathf.Round(1 / Time.deltaTime * slowInSec2);
                stunTickM = (int)Mathf.Round(1 / Time.deltaTime * stunInSec2);

                slowTick = -1;
                stunTick = 0;

            }

        }

        if (health <= 0) {

            if (!isBullet) {

                if (type >= 110 && type < 200) {

                    parentB.GetComponent<MeteorScript>().hIndex3--;

                }
                else {

                    GameObject.Find("SecretMeteorLauncher").GetComponent<SecretMeteorLauncherScript>().minusMeteor();

                }

            }

            if (damage != 999999) {

                GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().score += this.score;
                GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().money += this.money;

            }

            Destroy(this.gameObject);

        }

    }

    // CREATE AUXILIARY MISSILES
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

    // NORMAL METEOR 1-3
    public void type0() {

        transform.position = new Vector2(transform.position.x + kX, transform.position.y + kY);

    }
    
    // A MOVING METEOR (TO RIGHT AND LEFT)
    public void type3() {

        transform.position = new Vector2(transform.position.x + kX, transform.position.y + kY);

        if (hIndex2 == 0) {

            transform.position = new Vector2(transform.position.x + movingUnit, transform.position.y);

        }
        else if (hIndex2 == 1) {

            transform.position = new Vector2(transform.position.x - movingUnit, transform.position.y);

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

    // BIG METEOR
    public void type4() {

        transform.position = new Vector2(transform.position.x + kX, transform.position.y + kY);

    }

    // METEOR WAVE TO THE RIGHT (OR LEFT)
    public void type5() {

        transform.position = new Vector2(transform.position.x + kX, transform.position.y + kY);

        if (hIndex2 == 0) {

            transform.position = new Vector2(transform.position.x + movingUnit, transform.position.y);

            if (transform.position.x >= maxX + 0.5f) {

                transform.position = new Vector2(-maxX - 0.2f, transform.position.y);

            }

        }

        else if (hIndex2 == 1) {

            transform.position = new Vector2(transform.position.x - movingUnit, transform.position.y);

            if (transform.position.x <= -maxX - 0.5f) {

                transform.position = new Vector2(maxX + 0.2f, transform.position.y);

            }

        }

    }

    // REGULAR SPACESHIP
    public void type10() {

        transform.position = new Vector2(transform.position.x + kX, transform.position.y + kY);

    }

    // SHOOTING SPACESHIP
    public void type11() {

        transform.position = new Vector2(transform.position.x + kX, transform.position.y + kY);

        canShotCounter++;

        if (canShotCounter >= canShotCounterM) {

            canShotCounter = 0;
            canShotCounterM = (int)Math.Round(Random.Range(1 / Time.fixedDeltaTime / minAPS, 1 / Time.fixedDeltaTime / maxAPS));

            createEnemyRocket(transform.position.x, -20, transform.position.x, transform.position.y, 90, 0);

        }

    }

    // MOVING AND SHOOTING SPACESHIP
    public void type12() {

        transform.position = new Vector2(transform.position.x + kX, transform.position.y + kY);

        hIndex++;

        if (hIndex >= hIndexMax) {

            hIndex = 0;
            hIndex2++;

            if (hIndex2 >= 4) {

                hIndex2 = 0;

            }

        }

        if (hIndex2 == 1) {

            transform.position = new Vector2(transform.position.x - movingUnit, transform.position.y);

        }

        if (hIndex2 == 3) {

            transform.position = new Vector2(transform.position.x + movingUnit, transform.position.y);

        }

        canShotCounter++;

        if (canShotCounter >= canShotCounterM && (hIndex2 == 3 || hIndex2 == 1)) {

            canShotCounter = 0;
            canShotCounterM = (int)Math.Round(Random.Range(1 / Time.fixedDeltaTime / minAPS, 1 / Time.fixedDeltaTime / maxAPS));

            createEnemyRocket(transform.position.x, -20, transform.position.x, transform.position.y, 90, 0);

        }

    }

    // SPACESHIP BULLET 1
    public void type90() {

        transform.position = new Vector2(transform.position.x + kX, transform.position.y + kY);

    }

    // BOSS 1
    public void type100() {

        if (hIndex2 == 0) { // MOVE IN

            transform.position = new Vector2(transform.position.x, transform.position.y - speed);

        }
        if (transform.position.y <= 3f && hIndex2 == 0) {

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

            createEnemyRocket(transform.position.x - 1f, transform.position.y - 1f, transform.position.x - 1f, transform.position.y + 3f, 110, 0);
            createEnemyRocket(transform.position.x + 1f, transform.position.y - 1f, transform.position.x + 1f, transform.position.y + 3f, 110, 0);

        }

        if (hIndex3 <= 0 && hIndex2 == 3) {

            canGetDamage = true;

            hIndex2 = 1;

        }

    }

    // BOSS'S SPACESHIP
    public void type110() {

        if (transform.position.y >= desY) {

            transform.position = new Vector2(transform.position.x + kX, transform.position.y + kY);

        }

        canShotCounter++;

        if (canShotCounter >= canShotCounterM) {

            canShotCounter = 0;
            canShotCounterM = (int)Math.Round(Random.Range(1 / Time.fixedDeltaTime / minAPS, 1 / Time.fixedDeltaTime / maxAPS));

            createEnemyRocket(transform.position.x, -20, transform.position.x, transform.position.y, 90, 0);

        }

    }
    
    // COIN 1-3 
    public void type200() {

        transform.position = new Vector2(transform.position.x + kX, transform.position.y + kY);

    }

}
