using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class LevelWinTrigger : MonoBehaviour
{
    public float spinSpeed = 4.8f;
    public float moveSpeed = 2f;


    private bool playWinAnimation = false;
    private bool playCloseAnaimation = false;
    private GameObject objectToAnimate;
    private float startDistance;
    private Vector2 objectToAnimateStartScale;
    private Vector3 portalStartScale;

    private void Start()
    {
        portalStartScale = gameObject.transform.localScale;
        GameManager.Instance.resetLevelEvent.AddListener(ResetTransform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !GameManager.Instance.isLevelOver)
        {
            GameManager.Instance.WinLevel();
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            objectToAnimate = collision.gameObject;
            startDistance = Mathf.Abs(Vector2.Distance(transform.position, objectToAnimate.transform.position));
            objectToAnimateStartScale = objectToAnimate.transform.localScale;
            playWinAnimation = true;
        }
    }

    private void Update()
    {
        if (playWinAnimation)
        {
            float step = moveSpeed * Time.deltaTime;
            objectToAnimate.transform.Rotate(0f, 0f, spinSpeed); //rotate object
            objectToAnimate.transform.position = Vector2.MoveTowards(objectToAnimate.transform.position, transform.position, step); //move towards center of portal

            //shrink the object as it get closer
            float currentDistance = Mathf.Abs(Vector2.Distance(transform.position, objectToAnimate.transform.position));
            float currentScale = currentDistance / startDistance;
            if (currentScale > .1f)
                objectToAnimate.transform.localScale = objectToAnimateStartScale * currentScale;
            else
            {
                playWinAnimation = false;
            }

        }

        //shrinks the portal over time until it is gone
        if (playCloseAnaimation)
        {
            if (transform.localScale.x > 0.1)
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 2f * Time.deltaTime);
            else
            {
                transform.localScale = new Vector3(0, 0, 0);
                playCloseAnaimation = false;
            }
        }
    }

    public void ClosePortal()
    {
        playCloseAnaimation = true;
    }

    public void ResetTransform()
    {
        transform.localScale = portalStartScale;
    }


    private void DisableScripts(GameObject obj)
    {
        MonoBehaviour[] comps = obj.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour c in comps)
        {
            c.enabled = false;
        }
        obj.GetComponent<SpriteRenderer>().enabled = true;
    }
}
