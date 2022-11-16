using UnityEngine;
using System.Collections;

public class PlanetRotate : MonoBehaviour
{
    public Transform target;
    public float speed;
    void Start()
    {
        if (target == null)
        {
            target = this.gameObject.transform;
            Debug.Log("RotateAround target not specified. Defaulting to parent GameObject");
        }
    }

    void FixedUpdate()
    {
        transform.RotateAround(target.transform.position, WhichVector(gameObject.name), speed * Time.deltaTime);
    }

    Vector3 WhichVector(string planet)
    {
        Vector3 vector3 = new Vector3();
        switch (planet)
        {
            case "Sun":
                vector3 = Vector3.up;
                break;
            case "Moon":
                vector3 = target.transform.up;
                break;
        }
        return vector3;
    }
}