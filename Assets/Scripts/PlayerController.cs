using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]Rigidbody2D rb;
    [SerializeField]float rocketThrust;
    public Vector2 rocketDirectionPoint;
    [SerializeField] private float rotationSpeed = 500f;

    public bool firingRocket;

    public bool playerControlEnabled = true;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 startScale;

    private void Start()
    {
        startPosition = gameObject.transform.position;
        startRotation = gameObject.transform.rotation;
        startScale = gameObject.transform.localScale;
        GameManager.Instance.resetLevelEvent.AddListener(ResetPosition);
    }

    public void ResetPosition()
    {
        gameObject.transform.position = startPosition;
        gameObject.transform.rotation = startRotation;
        gameObject.transform.localScale = startScale;
        rb.velocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (playerControlEnabled)
        {
            Vector3 objectPos = transform.position;
            Vector2 targ = new Vector2(rocketDirectionPoint.x - objectPos.x, rocketDirectionPoint.y - objectPos.y);

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            float step = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, step);

            if (firingRocket)
                FireRocket();
        }
    }

    private void FireRocket()
    {
        if (GameManager.Instance.currentFuel <= 0)
            return;

        var force = rocketThrust * transform.right;
        GameManager.Instance.StartTimer();
        rb.AddForce(force);
        GameManager.Instance.UseFuel();
    }
}
