using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricScript : MonoBehaviour {

    List<GameObject> damagedMeteors = new List<GameObject>();

    public float damage;

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (!damagedMeteors.Contains(collision.gameObject)) {

            damagedMeteors.Add(collision.gameObject);

            collision.gameObject.GetComponent<MeteorScript>().shot(damage);

        }

    }

}
