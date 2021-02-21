using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Attractor : MonoBehaviour
{
    HashSet<Rigidbody2D> rigidbodies;
    LineRenderer lineRenderer;
    [SerializeField]private float attractionForce;

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
       foreach(var rb in rigidbodies)
        {
            var direction = (transform.position - rb.transform.position).normalized;
            var forceVector = direction * attractionForce;
            rb.AddForce(forceVector);
        }
    }

    
}
