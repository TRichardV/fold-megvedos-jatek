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


    // UPGRADE PANEL
    bool panelVisible = false;


    // STATS
    public float aps = 0.5f;
    public float damage = 1f;
    public float score = 0f;
    public float money = 0f;

    public float rocketSpeed = 3f; // WILL BEEN MULTIPLYED WITH DELTATIME


    // BRIMSTONE STATS
    float brimTimeOnSec1 = 3f;
    float brimTimeOnSec2 = 4f;

    float brimDamageRate = 0.1f;
    float brimDamagePerSec = 0.1f;

    float brimAPSRate1 = 0.5f;
    float brimAPSRate2 = 0.35f;


    // KNUTS HAMMER STATS
    int chanceOfCrit1 = 20;
    float aoeDamageRate1 = 0.5f;
    float damageCritRate1 = 1.5f;

    int chanceOfCrit2 = 35;
    float aoeDamageRate2 = 0.75f;
    float damageCritRate2 = 1.75f;

    float electricStillAlive = 1f; // In Sec


    // 20/20 ITEM STATS
    float twentyDamageRate1 = 0.3f;
    float twentyDamageRate2 = 0.5f;
    float twentyBrimstoneBoostRate = 1f;


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


    // UPGRADES
    int dmgUP = 0;
    int apsUP = 0;

    float[,] dmgUPs = { { 5f, 25f }, { 7f, 60f }, { 10f, 130f }, { 15f, 300f }, { 22f, 1200f }, { 30f, 2500f }, { 40f, 7000f }, { 60f, 25000f }, { 100, 100000f } };
    float[,] apsUPs = { { 0.1f, 10f }, { 0.12f, 25f }, { 0.13f, 40f }, { 0.15f, 60f }, { 0.17f, 125f }, { 0.22f, 200f }, { 0.26F, 350F }, { 0.3f, 600f }, { 0.35f, 1300f } };


    // ITEMS FOR HANDLE
    public int haveIceborn = 0;
    public int haveKnuts = 0;
    public int haveTrisagion = 0;
    public int haveTwenty = 0;


    // SHOOTING
    bool canShot = false;
    int canShotCounter = 0;
    int canShotMaxCounter;


    // SET THE APS AND THE BRIMSTONE SETTINGS
    void setStats() {

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

    }

    void Start() {

        setStats();

        setPanel();

        rocketSpeed *= Time.deltaTime;

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

            Debug.Log(damage + " " + brimDamageRate + " " + damage * brimDamageRate);

            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().damage = damage * brimDamageRate;
            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().damagePerSec = brimDamagePerSec;
            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().setup();

            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().brimDamageRate = brimDamageRate;

            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().chanceOfCrit = chanceOfCrit;
            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().aoeDamageRate = aoeDamageRate;
            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().damageCritRate = damageCritRate;

            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().twentyDamageRate = twentyDamageRate * twentyBrimstoneBoostRate;

            brimstoneLaserHead.GetComponent<BrimstoneLaserScript>().twentyBrimstoneBoostRate = twentyBrimstoneBoostRate;

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

        GameObject.Find("MoneyCounter").GetComponent<TextMeshProUGUI>().text = "Money: " + money;
        GameObject.Find("ScoreCounter").GetComponent<TextMeshProUGUI>().text = "Score: " + score;

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

            if (tPos.y > transform.position.y && canShot == true && !panelVisible) {

                createRocket(tPos.x, tPos.y);

                canShot = false;

            }

        } 

    }

    void setPanel() {

        panel.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"[LVL. {dmgUP}.]\n${dmgUPs[dmgUP, 1]}";
        panel.transform.GetChild(1).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"[LVL. {apsUP}.]\n${apsUPs[apsUP, 1]}";

    }

    public void upgPanelPressed() {

        if (!panelVisible) {

            panel.gameObject.active = true;
            button.gameObject.active = false;
            panelVisible = true;

        }

        else {

            panel.gameObject.active = false;
            button.gameObject.active = true;
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

}
