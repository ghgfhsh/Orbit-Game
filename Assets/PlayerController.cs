using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 rocketDirectionPoint;
    Rocket rocket;

    public bool firingRocket;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rocket = GetComponent<Rocket>();
        GameManager.Instance.ResetLevel();
    }

    private void FixedUpdate()
    {
        Vector3 objectPos = transform.position;
        Vector2 targ = new Vector2(rocketDirectionPoint.x - objectPos.x, rocketDirectionPoint.y - objectPos.y);

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (firingRocket)
            FireRocket();
    }

    private void FireRocket()
    {
        if (GameManager.Instance.currentFuel <= 0)
            return;

        var force = rocket.thrust * transform.right;
        rb.AddForce(force);
        GameManager.Instance.useFuel();
    }
}
