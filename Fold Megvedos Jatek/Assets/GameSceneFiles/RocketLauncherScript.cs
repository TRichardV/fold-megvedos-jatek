using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RocketLauncherScript : MonoBehaviour {

    public GameObject rocket;

    public GameObject brimstoneLaser;
    public bool bulletBrim = false;

    int brimTimeOnM;
    int brimTimeOn = -1;

    public int haveIceborn = 0;

    bool canShot = false;
    int canShotCounter = 0;
    int canShotMaxCounter;

    public Transform LauncherP;

    public float aps;
    public float damage;
    public float score = 0;
    public float money = 0;

    public float rocketSpeed = 3f;

    // UPGRADE PANEL

    public GameObject panel;
    public GameObject button;
    bool panelVisible = false;

    public GameObject reloadBar;

    int dmgUP = 0;
    int apsUP = 0;

    float[,] dmgUPs = { { 5f, 25f }, { 7f, 60f }, { 10f, 130f}, { 15f, 300f }, { 22f, 1200f }, { 30f, 2500f }, { 40f, 7000f }, { 60f, 25000f }, { 100, 100000f} };
    float[,] apsUPs = { { 0.1f, 10f }, { 0.12f, 25f }, { 0.13f, 40f }, { 0.15f, 60f }, { 0.17f, 125f}, { 0.22f, 200f }, { 0.26F, 350F }, { 0.3f, 600f }, { 0.35f, 1300f } };

    void Start() {

        aps = 0.5f;
        damage = 1f;


        this.LauncherP = this.GetComponent<Transform>();

        canShotMaxCounter = (int)(1 / Time.fixedDeltaTime / aps);

        brimTimeOnM = (int)(1 / Time.fixedDeltaTime) * 3;

        setPanel();

    }

    void createRocket(float desX, float desY) {

        if (bulletBrim == false) {

            GameObject obj = Instantiate(rocket);

            obj.transform.parent = this.gameObject.transform;

            obj.transform.position = this.gameObject.transform.position;

            obj.GetComponent<RocketScript>().desX = desX;
            obj.GetComponent<RocketScript>().desY = desY;

            obj.GetComponent<RocketScript>().damage = damage;
            obj.GetComponent<RocketScript>().speed = rocketSpeed * Time.deltaTime;

            float a = Vector3.Distance(transform.position, new Vector3(desX, desY));
            float b = 1;
            float c = Vector3.Distance(new Vector3(b, transform.position.y), new Vector3(desX, desY));

            float angle = (Mathf.Acos((Mathf.Pow(a, 2) + Mathf.Pow(b, 2) - Mathf.Pow(c, 2)) / (2 * a * b))) * Mathf.Rad2Deg;
            obj.transform.rotation = Quaternion.Euler(0, 0, -90 + angle);

        }
        else {

            brimstoneLaser.active = true;
            brimstoneLaser.GetComponent<BrimstoneLaserScript>().setup();
            brimstoneLaser.GetComponent<BrimstoneLaserScript>().damage = damage;

            //float a = Mathf.Abs(this.gameObject.transform.position.x - desX);
            //float b = Mathf.Abs(this.gameObject.transform.position.y - desY);

            //float angle = Mathf.Atan(b / a) * Mathf.Rad2Deg;//tangens alfa = b/a;

            //brimstoneLaser.transform.rotation = Quaternion.Euler(0f, 0f, -90 + angle);

            //brimstoneLaser.transform.position = new Vector3(desX, brimstoneLaser.transform.position.y);

            brimTimeOn++;

        }

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

            }

        }

        if (Input.touchCount > 0) {

            Vector2 tPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            rotateOurself(tPos.x, tPos.y);

            if (tPos.y > LauncherP.position.y && canShot == true && !panelVisible) {

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
        Debug.Log(dmgUP + " " + dmgUPs.GetLength(0));
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
