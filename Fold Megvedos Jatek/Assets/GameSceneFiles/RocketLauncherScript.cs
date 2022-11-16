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
    float damage = 5;
    public int score = 0;
    public float money = 0;

    // UPGRADE PANEL

    public GameObject panel;
    public GameObject button;
    bool panelVisible = false;

    public GameObject reloadBar;

    int dmgUP = 0;
    int apsUP = 0;

    float[,] dmgUPs = { { 1f, 3f }, { 1.5f, 5f }, { 2f, 10f}, { 3f, 20f }, { 5f, 50f }, { 7f, 120f }, { 10f, 250f }, { 15f, 500f }, { 20, 1000f} };
    float[,] apsUPs = { { 0.1f, 3f }, { 0.15f, 6f }, { 0.25f, 20f }, { 0.35f, 50f }, { 0.45f, 100f}, { 0.60f, 200f }, { 0.80f, 500f }, { 1f, 1000f }, { 1.5f, 3000f } };

    void Start() {

        money = 100000f;

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

    }

    void FixedUpdate() {
        Debug.Log("damage: " + damage + " aps: " + aps);
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
        Debug.Log(apsUP + " " + apsUPs.GetLength(0));
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
