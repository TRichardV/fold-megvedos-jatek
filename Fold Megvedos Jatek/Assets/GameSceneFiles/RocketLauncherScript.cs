using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RocketLauncherScript : MonoBehaviour {

    public GameObject rocket;

    bool canShot = false;
    int canShotCounter = 0;
    int canShotMaxCounter;

    public Transform LauncherP;

    float aps = 1f;
    int damage = 5;
    public int score = 0;
    public float money = 0f;

    void Start() {

        this.LauncherP = this.GetComponent<Transform>();

        canShotMaxCounter = (int)(1 / Time.fixedDeltaTime / aps);

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

        GameObject.Find("MoneyCounter").GetComponent<TextMeshProUGUI>().text = "Money: " + money;
        GameObject.Find("ScoreCounter").GetComponent<TextMeshProUGUI>().text = "Score: " + score;

        if (canShotCounter >= canShotMaxCounter) {

            canShot = true;
            canShotCounter = 0;

        }
        if (canShot == false) {

            canShotCounter++;

        }

        if (Input.touchCount > 0) {

            Vector2 tPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            if (tPos.y > LauncherP.position.y && canShot == true) {

                createRocket(tPos.x, tPos.y);

                canShot = false;

            }

        } 

    }

}
