using UnityEngine;
using System.Collections;

public class EarthSpinScript : MonoBehaviour {
    public float speed = 10f;

    void FixedUpdate() {
        transform.Rotate(-speed/4 * Time.deltaTime, -speed * Time.deltaTime, 0);
    }
}