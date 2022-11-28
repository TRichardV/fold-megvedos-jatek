using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrimstoneLaserScript : MonoBehaviour {

    List<GameObject> inLaser = new List<GameObject>();

    float defScaleX = 12f;
    float defScaleY = 1f;
    float defPosZ = 11f;

    public float damage;
    float damageTick = 0;
    float damageTickM;

    private void Start() {

        //defPosZ = this.gameObject.transform.localPosition.z;

        damageTickM = (int)Mathf.Round(1 / Time.fixedDeltaTime / 10);

        setup();

        //Debug.Log(defScaleY + " asdasd");

    }

    public void setup() {

        Vector3 pos = this.gameObject.transform.localPosition;
        Vector3 scale = this.gameObject.transform.localScale;

        this.gameObject.transform.localPosition = new Vector3(pos.x, pos.y, 11f);
        this.gameObject.transform.localScale = new Vector3(defScaleX, defScaleY, scale.z);

        //this.gameObject.transform.localScale = new Vector2(this.gameObject.transform.localScale.x, defScaleY);
        //this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.z, defPosZ);
        
    }

    private void FixedUpdate() {

        damageTick++;
        Debug.Log(inLaser.Count);

        while(inLaser.Count == 0 && this.gameObject.transform.localScale.y < 100f) {

            this.gameObject.transform.localScale = new Vector2(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y + 0.1f);
            this.gameObject.transform.localPosition += new Vector3(0f, 0f, 0.05f);

        }
        while (inLaser.Count > 1) {

            this.gameObject.transform.localScale = new Vector2(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y - 0.1f);
            this.gameObject.transform.localPosition -= new Vector3(0f, 0f, 0.05f);

        }

        if (damageTick >= damageTickM) {

            damageTick = 0;

            if (inLaser.Count != 0) {

                doingDamage(inLaser[0]);

            }

        }



    }

    void doingDamage(GameObject obj) {

        obj.GetComponent<MeteorScript>().shot(damage);
        Debug.Log("dam dam dam");

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (!inLaser.Contains(collision.gameObject)) {

            inLaser.Add(collision.gameObject);

        }


    }

    private void OnTriggerExit2D(Collider2D collision) {
        
        if (inLaser.Contains(collision.gameObject)) {

            inLaser.Remove(collision.gameObject);

        }

    }

}
