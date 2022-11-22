using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RocketLauncherScript : MonoBehaviour {

    public GameObject rocket;

    bool canShot = false;
    int canShotCounter = 0;
    int canShotMaxCounter;

    public Transform LauncherP;

    float aps = 0.5f;
    float damage = 10;
    public float score = 0;
    public float money = 0;

    // UPGRADE PANEL

    public GameObject panel;
    public GameObject button;
    bool panelVisible = false;

    public GameObject reloadBar;

    int dmgUP = 0;
    int apsUP = 0;

    float[,] dmgUPs = { { 5f, 30f }, { 7f, 75f }, { 10f, 150f}, { 3f, 2000000f }, { 5f, 50f }, { 7f, 120f }, { 10f, 250f }, { 15f, 500f }, { 20, 1000f} };
    float[,] apsUPs = { { 0.1f, 10f }, { 0.12f, 20f }, { 0.15f, 35f }, { 0.2f, 60f }, { 0.25f, 125f}, { 0.33f, 200f }, { 0.4F, 350F }, { 1f, 1000000f }, { 1.5f, 3000f } };

    void Start() {

        this.LauncherP = this.GetComponent<Transform>();

        canShotMaxCounter = (int)(1 / Time.fixedDeltaTime / aps);

        setPanel();

    }

    void createRocket(float desX, float desY) {

        GameObject obj = Instantiate(rocket);

        obj.transform.parent = this.gameObject.transform;

        obj.transform.position = this.gameObject.transform.position;

        obj.GetComponent<RocketScript>().desX = desX;
        obj.GetComponent<RocketScript>().desY = desY;

        obj.GetComponent<RocketScript>().damage = damage;

        float a = Vector3.Distance(transform.position, new Vector3(desX, desY));
        float b = 1;
        float c = Vector3.Distance(new Vector3(b, transform.position.y), new Vector3(desX, desY));

        float angle = (Mathf.Acos((Mathf.Pow(a, 2) + Mathf.Pow(b, 2) - Mathf.Pow(c, 2)) / (2 * a * b))) * Mathf.Rad2Deg;
        GameObject.Find("SpaceShip").transform.rotation = Quaternion.Euler(-angle, 90f, -90f);
        obj.transform.rotation = Quaternion.Euler(0, 0, -90 + angle);

    }

    void FixedUpdate() {
        //Debug.Log("damage: " + damage + " aps: " + aps);
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
        if (canShot == false) {

            canShotCounter++;

            double sliderScaleUnit = 1d / canShotMaxCounter;
            double value = sliderScaleUnit * canShotCounter;
            reloadBar.GetComponent<Slider>().value = (float)value;

        }

        if (Input.touchCount > 0 && !panelVisible) {

            Vector2 tPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            if (tPos.y > LauncherP.position.y && canShot == true) {

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
        //Debug.Log(apsUP + " " + apsUPs.GetLength(0));
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
