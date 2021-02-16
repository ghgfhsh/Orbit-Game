using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Attractor : MonoBehaviour
{
    const float G = 6.67408e-11f;
    [SerializeField]float attractorMass = 100000f;
    HashSet<Rigidbody2D> rigidbodies;

    private void Start()
    {
        rigidbodies = new HashSet<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var ridgidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        if (ridgidbody == null)
            return;

        rigidbodies.Add(ridgidbody);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var ridgidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        if (ridgidbody == null)
            return;

        rigidbodies.Remove(ridgidbody);
    }

    private void OnDisable()
    {
        rigidbodies.Clear();
    }

    void FixedUpdate()
    {
        // Cache the position
        var currentPosition = (Vector2)transform.position;

        // Apply force to all rigidbodies
        foreach (var body in rigidbodies)
        {
            // Calculate the magnitude of the force by the rigidbody mass and the attractor force
            float distance = Vector2.Distance(transform.parent.position, body.transform.position);
            float forceMagnitude = (G * attractorMass) / Mathf.Pow(distance, 2);

            // Calculate the force by getting the delta position with the previously calculated magnitude
            var force = -GetDirection(body, currentPosition) * forceMagnitude;

            // Apply
            body.AddForce(force);
        }
    }

    //Get the relative position of body opposing to the point
    static Vector2 GetDirection(Rigidbody2D body, Vector2 point)
    {
        // Calculate the delta position
        var delta = body.position - point;
        return delta.normalized;
    }
}
