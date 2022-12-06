using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricScript : MonoBehaviour {

    // DAMAGED METEORS
    public List<GameObject> damagedMeteors = new List<GameObject>();


    // STATS
    public float damage;


    // ALIVE DURATION
    int aliveTick = 0;
    public int aliveTickM;


    private void FixedUpdate() {

        aliveTick++;

        if (aliveTick >= aliveTickM) {

            Destroy(this.gameObject);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (!damagedMeteors.Contains(collision.gameObject) && collision.gameObject.tag == "meteor") {

            damagedMeteors.Add(collision.gameObject);

            collision.gameObject.GetComponent<MeteorScript>().shot(damage);

        }

    }

}
