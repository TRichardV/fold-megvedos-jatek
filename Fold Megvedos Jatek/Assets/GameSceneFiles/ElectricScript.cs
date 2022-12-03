using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricScript : MonoBehaviour {

    public List<GameObject> damagedMeteors = new List<GameObject>();

    public float damage;

    int aliveTick = 0;
    int aliveTickM;

    private void Start() {

        aliveTickM = (int)(1 / Time.deltaTime) * 1;

    }

    private void FixedUpdate() {

        aliveTick++;

        if (aliveTick >= aliveTickM) {

            Destroy(this.gameObject);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (!damagedMeteors.Contains(collision.gameObject)) {

            damagedMeteors.Add(collision.gameObject);

            collision.gameObject.GetComponent<MeteorScript>().shot(damage);

        }

    }

}
