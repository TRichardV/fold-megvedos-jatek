using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RocketLauncherScript : MonoBehaviour {

    // MISSILES OBJECTS
    public GameObject rocket;


    // OTHER OBJECTS
    public GameObject panel;
    public GameObject button;
    public GameObject reloadBar;

    public GameObject dmgUPObject;
    public GameObject apsUPObject;


    // UPGRADE PANEL
    bool panelVisible = false;


    // STATS
    public float aps = 0.5f;
    public float damage = 10f;
    public float score = 0f;
    public float money = 0f;

    public float rocketSpeed = 3f; // WILL BEEN MULTIPLYED WITH DELTA.TIME


    // BRIMSTONE STATS
    float brimTimeOnSec1 = 6f;
    float brimTimeOnSec2 = 10f;

    float brimDamageRate = 0.4f;
    float brimDamagePerSec = 0.55f;

    float brimAPSRate1 = 0.3f;
    float brimAPSRate2 = 0.15f;


    // KNUTS HAMMER STATS
    int chanceOfCrit1 = 15;
    float aoeDamageRate1 = 0.5f;
    float damageCritRate1 = 1.5f;

    int chanceOfCrit2 = 30;
    float aoeDamageRate2 = 0.75f;
    float damageCritRate2 = 1.75f;

    float electricStillAlive = 1f; // In Sec


    // 20/20 ITEM STATS
    float twentyDamageRate1 = 0.3f;
    float twentyDamageRate2 = 0.5f;
    float twentyBrimstoneBoostRate = 1f;


    // IRON DOME STATS
    float domeDamagePercent1 = 40;
    float domeDamagePercent2 = 60;

    float domeAPSPercent1 = 50;
    float domeAPSPercent2 = 70;

    int domeAPSRange1 = 40;
    int domeAPSRange2 = 15;


    // TRISAGION STATS
    float trisagionRate1 = 0.3f;
    float trisagionRate2 = 0.45f;


    // 20/20 ITEM
    float twentyDamageRate;


    // KNUTS ITEM
    int chanceOfCrit;
    float aoeDamageRate;
    float damageCritRate;


    // BRIMSTONE ITEM
    public GameObject brimstoneLaser;
    public GameObject brimstoneLaserHead;
    public bool bulletBrim = false;

    public int haveBrimstone = 0;

    int brimTimeOnM;
    int brimTimeOn = -1;


    // IRON DOME
    public GameObject dome1;
    public GameObject dome2;

    public float domeDamagePercent;
    public float domeAPSPercent;
    public int domeAPSRange;


    // TRISAGION
    public float trisagionRate;


    // UPGRADES
    int dmgUP = 0;
    int apsUP = 0;
    //{ { 5f, 25f }, { 7.5f, 75f }, { 10f, 225f }
    float[,] dmgUPs = { { 4f, 30f }, { 6f, 75f }, { 7f, 150f }, { 9f, 220f }, { 13f, 300f }, { 14f, 600f }, { 15f, 1200f }, { 16f, 2500f }, { 18f, 6000 } };
    float[,] apsUPs = { { 0.1f, 10f }, { 0.125f, 20f }, { 0.13f, 60f }, { 0.15f, 110f }, { 0.175f, 180f }, { 0.20f, 270f }, { 0.24F, 600f }, { 0.28f, 1100f }, { 0.32f, 2200f } };


    // ITEMS FOR HANDLE
    public int haveIceborn = 0;
    public int haveKnuts = 0;
    public int haveTrisagion = 0;
    public int haveTwenty = 0;
    public int haveIronDome = 0;


    // SHOOTING
    bool canShot = false;
    int canShotCounter = 0;
    int canShotMaxCounter;


    // TESTING
    public float testMoney = 0f;


    // GET AN IRON DOME
    public void getDome() {

        if (haveIronDome == 1) {

            domeDamagePercent = domeDamagePercent1;
            domeAPSPercent = domeAPSPercent1;
            domeAPSRange = domeAPSRange1;

        }
        else if (haveIronDome == 2) {

            domeDamagePercent = domeDamagePercent2;
            domeAPSPercent = domeAPSPercent2;
            domeAPSRange = domeAPSRange2;

        }

        dome1.GetComponent<SideRocketLauncherScript>().refreshDatas();
        dome2.GetComponent<SideRocketLauncherScript>().refreshDatas();


    }

    // REFRESH IRON DOME STATS
    public void ironDomeRefresh() {

        if (haveIronDome > 0) {
            //Debug.Log(domeDamagePercent + " " + domeAPSPercent + " " + domeAPSRange);

            dome1.GetComponent<SideRocketLauncherScript>().damage = damage * (domeDamagePercent / 100);
            dome2.GetComponent<SideRocketLauncherScript>().damage = damage * (domeDamagePercent / 100);

            dome1.GetComponent<SideRocketLauncherScript>().aps = aps * (domeAPSPercent / 100);
            dome2.GetComponent<SideRocketLauncherScript>().aps = aps * (domeAPSPercent / 100);

            dome1.GetComponent<SideRocketLauncherScript>().apsRange = domeAPSRange;
            dome2.GetComponent<SideRocketLauncherScript>().apsRange = domeAPSRange;

        }

    }

    // SET THE APS AND THE BRIMSTONE SETTINGS
    public void setStats() {

        if (haveKnuts == 1) {

            chanceOfCrit = chanceOfCrit1;
            aoeDamageRate = aoeDamageRate1;
            damageCritRate = damageCritRate1;

        }
        else if (haveKnuts == 2) {

            chanceOfCrit = chanceOfCrit2;
            aoeDamageRate = aoeDamageRate2;
            damageCritRate = damageCritRate2;

        }


        if (haveBrimstone == 0) {

            canShotMaxCounter = (int)(1 / Time.fixedDeltaTime / aps);

        }
        else if (haveBrimstone == 1) {

            canShotMaxCounter = (int)(1 / Time.fixedDeltaTime / (aps * brimAPSRate1));
            brimTimeOnM = (int)Mathf.Round((1 / Time.fixedDeltaTime) * brimTimeOnSec1);

        }
        else if (haveBrimstone == 2) {

            canShotMaxCounter = (int)(1 / Time.fixedDeltaTime / (aps * brimAPSRate2));
            brimTimeOnM = (int)Mathf.Round((1 / Time.fixedDeltaTime) * brimTimeOnSec2);

        }


        if (haveTwenty == 1) {

            twentyDamageRate = twentyDamageRate1;

        }
        else if (haveTwenty == 2) {

            twentyDamageRate = twentyDamageRate2;

        }

        if (haveTrisagion == 1) {

            trisagionRate = trisagionRate1;

        }
        else if (haveTrisagion == 2) {

            trisagionRate = trisagionRate2;

        }

    }

    void Start() {

        setStats();

        setPanel();

        rocketSpeed *= Time.deltaTime;

        money += 2197f;
        testMoney += 2197f;

    }


    public void createRocketForTheDome(float desX, float desY, Vector2 pos, float ldamage) {

        GameObject obj = Instantiate(rocket);

        obj.transform.parent = transform;
        obj.transform.position = pos;

        obj.GetComponent<RocketScript>().desX = desX;
        obj.GetComponent<RocketScript>().desY = desY;

        obj.GetComponent<RocketScript>().damage = ldamage;
        obj.GetComponent<RocketScript>().speed = rocketSpeed;

        obj.GetComponent<RocketScript>().chanceOfCrit = chanceOfCrit;
        obj.GetComponent<RocketScript>().aoeDamageRate = aoeDamageRate;
        obj.GetComponent<RocketScript>().damageCritRate = damageCritRate;

        obj.GetComponent<RocketScript>().twentyDamageRate = twentyDamageRate;

        obj.GetComponent<RocketScript>().electricStillAlive = electricStillAlive;

        obj.GetComponent<RocketScript>().haveKnuts = haveKnuts;
        obj.GetComponent<RocketScript>().haveTrisagion = haveTrisagion;
        obj.GetComponent<RocketScript>().haveTwenty = haveTwenty;

        obj.GetComponent<RocketScript>().haveResist = false;

        if (this.haveTwenty > 0) { obj.GetComponent<RocketScript>().canSplit = true; }

        obj.transform.rotation = Quaternion.Euler(0, 0, -90 + rotateRocket(desX, desY));

    }

    void createRocket(float desX, float desY) {

        if (bulletBrim == false) {

            GameObject obj = Instantiate(rocket);

            obj.transform.parent = transform;
            obj.transform.position = transform.position;

            obj.GetComponent<RocketScript>().desX = desX;
            obj.GetComponent<RocketScript>().desY = desY;

            obj.GetComponent<RocketScript>().damage = damage;
            obj.GetComponent<RocketScript>().speed = rocketSpeed;

            obj.GetComponent<RocketScript>().chanceOfCrit = chanceOfCrit;
            obj.GetComponent<RocketScript>().aoeDamageRate = aoeDamageRate;
            obj.GetComponent<RocketScript>().damageCritRate = damageCritRate;

            obj.GetComponent<RocketScript>().twentyDamageRate = twentyDamageRate;

            obj.GetComponent<RocketScript>().electricStillAlive = electricStillAlive;

            obj.GetComponent<RocketScript>().trisagionRate = trisagionRate;

            obj.GetComponent<RocketScript>().haveKnuts = haveKnuts;
            obj.GetComponent<RocketScript>().haveTrisagion = haveTrisagion;
            obj.GetComponent<RocketScript>().haveTwenty = haveTwenty;

            obj.GetComponent<RocketScript>().haveResist = false;

            if (this.haveTwenty > 0) { obj.GetComponent<RocketScript>().canSplit = true; }

            obj.transform.rotation = Quaternion.Euler(0, 0, -90 + rotateRocket(desX, desY));

        }
        else {

            brimstoneLaser.active = true;
            brimstoneLaserHead.active = true;

            //Debug.Log(damage + " " + brimDamageRate + " " + damage * brimDamageRate);

            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().damage = damage * brimDamageRate;
            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().damagePerSec = brimDamagePerSec;
            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().setup();

            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().brimDamageRate = brimDamageRate;

            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().chanceOfCrit = chanceOfCrit;
            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().aoeDamageRate = aoeDamageRate;
            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().damageCritRate = damageCritRate;

            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().twentyDamageRate = twentyDamageRate * twentyBrimstoneBoostRate;

            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().twentyBrimstoneBoostRate = twentyBrimstoneBoostRate;

            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().trisagionRate = trisagionRate;

            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().electricStillAlive = electricStillAlive;
            Debug.Log(haveKnuts + " " + haveTrisagion + " " + haveTwenty);
            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().haveKnuts = haveKnuts;
            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().haveTrisagion = haveTrisagion;
            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().haveTwenty = haveTwenty;

            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().onStart();

            brimTimeOn++;

        }

    }

    float rotateRocket(float desX, float desY) {

        float a = Vector3.Distance(transform.position, new Vector3(desX, desY));
        float b = 1;
        float c = Vector3.Distance(new Vector3(b, transform.position.y), new Vector3(desX, desY));

        float angle = (Mathf.Acos((Mathf.Pow(a, 2) + Mathf.Pow(b, 2) - Mathf.Pow(c, 2)) / (2 * a * b))) * Mathf.Rad2Deg;
        return angle;

    }

    void rotateOurself(float desX, float desY) {

        float a = Vector3.Distance(transform.position, new Vector3(desX, desY));
        float b = 1;
        float c = Vector3.Distance(new Vector3(b, transform.position.y), new Vector3(desX, desY));

        float angle = (Mathf.Acos((Mathf.Pow(a, 2) + Mathf.Pow(b, 2) - Mathf.Pow(c, 2)) / (2 * a * b))) * Mathf.Rad2Deg;
        GameObject.Find("SpaceShip").transform.rotation = Quaternion.Euler(-angle, 90f, -90f);

    }

    void FixedUpdate() {

        //Debug.Log("All of that money that you've get thus far: " + testMoney);

        GameObject.Find("MoneyCounter").GetComponent<TextMeshProUGUI>().text = "<sprite=1>" + money;
        GameObject.Find("ScoreCounter").GetComponent<TextMeshProUGUI>().text = "Score:\n" + score;

        if (canShotCounter == 1) {

            reloadBar.SetActive(true);

        }

        if (canShotCounter >= canShotMaxCounter) {

            canShot = true;
            canShotCounter = 0;

            reloadBar.SetActive(false);

        }
        if (canShot == false && brimTimeOn == -1) {

            canShotCounter++;

            double sliderScaleUnit = 1d / canShotMaxCounter;
            double value = sliderScaleUnit * canShotCounter;
            reloadBar.GetComponent<Slider>().value = (float)value;

        }

        if (brimTimeOn > -1) {

            brimTimeOn++;

            if (brimTimeOn >= brimTimeOnM) {

                brimTimeOn = -1;
                brimstoneLaser.active = false;
                brimstoneLaserHead.active = false;

            }

        }

        if (Input.touchCount > 0) {

            Vector2 tPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            rotateOurself(tPos.x, tPos.y);

            if (tPos.y > transform.position.y-1f && canShot == true && !panelVisible) {

                createRocket(tPos.x, tPos.y);

                canShot = false;

            }

        } 

    }

    void setPanel() {

        panel.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = $"[LVL. {dmgUP}.]\n${dmgUPs[dmgUP, 1]}";
        panel.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = $"[LVL. {apsUP}.]\n${apsUPs[apsUP, 1]}";

    }

    public void upgPanelPressed() {

        if (!panelVisible) {

            panel.SetActive(true);
            panel.transform.parent.GetComponent<Animator>().Play("UpgradePanelOpen",0,0);
            panelVisible = true;

        }

        else {

            panel.transform.parent.GetComponent<Animator>().Play("UpgradePanelClose", 0, 0); ;
            panelVisible = false;

        }

    }

    public void damageUpgrade() {

        if (dmgUP < dmgUPs.GetLength(0) && money >= dmgUPs[dmgUP, 1]) {

            damage += dmgUPs[dmgUP, 0];
            money -= dmgUPs[dmgUP, 1];

            dmgUP++;

            if (dmgUP <= dmgUPs.GetLength(0)-1) {

                GameObject.Find("dmgUP").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"[LVL. {dmgUP}].\n${dmgUPs[dmgUP, 1]}";

            }
            else {

                GameObject.Find("dmgUP").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"[LVL. MAX]";

            }

        }

    }

    public void attackPerSecUpgrade() {

        if (apsUP < apsUPs.GetLength(0) && money >= apsUPs[apsUP, 1]) {

            aps += apsUPs[apsUP, 0];
            money -= apsUPs[apsUP, 1];

            apsUP++;

            if (apsUP <= apsUPs.GetLength(0) - 1) {

                GameObject.Find("apsUP").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"[LVL. {apsUP}].\n${apsUPs[apsUP, 1]}";

            }
            else {

                GameObject.Find("apsUP").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"[LVL. MAX]";

            }

            canShotMaxCounter = (int)(1 / Time.fixedDeltaTime / aps);

        }

    }

    public void getDamageUp() {

        if (dmgUP < dmgUPs.GetLength(0)) {

            dmgUP++;

            if (dmgUP <= dmgUPs.GetLength(0) - 1) {

                dmgUPObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"[LVL. {dmgUP}].\n${dmgUPs[dmgUP, 1]}";

            }
            else {

                dmgUPObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"[LVL. MAX]";

            }

        }

        ironDomeRefresh();

    }

    public void getAPSUp() {

        if (apsUP < apsUPs.GetLength(0)) {

            apsUP++;

            if (apsUP <= apsUPs.GetLength(0) - 1) {

                apsUPObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"[LVL. {apsUP}].\n${apsUPs[apsUP, 1]}";

            }
            else {

                apsUPObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"[LVL. MAX]";

            }

            canShotMaxCounter = (int)(1 / Time.fixedDeltaTime / aps);

        }

        ironDomeRefresh();

    }

}
