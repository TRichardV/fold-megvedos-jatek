using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherScript : MonoBehaviour {

    public GameObject rocket;

    bool canShot = false;
    int canShotCounter = 0;
    int canShotMaxCounter = 30;

    public Transform LauncherP;

    void Start() {

        this.LauncherP = this.GetComponent<Transform>();

    }

    void createRocket(float desX, float desY) {

        GameObject obj = Instantiate(rocket);

        obj.transform.parent = this.gameObject.transform;

        obj.transform.position = this.gameObject.transform.position;

        obj.GetComponent<RocketScript>().desX = desX;
        obj.GetComponent<RocketScript>().desY = desY;

    }

    void FixedUpdate() {

        canShotCounter++;

        if (canShotCounter >= canShotMaxCounter) {

            canShot = true;
            canShotCounter = 0;

        }

        if (Input.touchCount > 0) {

            if (canShot) {

                Vector2 tPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                if (tPos.y > LauncherP.position.y) {

                    createRocket(tPos.x, tPos.y);

                    canShot = false;

                }

            }

        } 

    }

}
