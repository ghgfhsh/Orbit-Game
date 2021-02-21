using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LevelBorder : MonoBehaviour
{
    LineRenderer lineRenderer;
    BoxCollider2D boxCollider;

    private void OnDrawGizmos()
    {
        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider2D>();

        if(lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 4;
            lineRenderer.loop = true;

            Vector2 size = boxCollider.size;
            Vector3 worldPos = transform.TransformPoint(boxCollider.offset);

            float top = worldPos.y + (size.y / 2f);
            float btm = worldPos.y - (size.y / 2f);
            float left = worldPos.x - (size.x / 2f);
            float right = worldPos.x + (size.x / 2f);

            Vector3 topLeft = new Vector3(left, top, worldPos.z);
            Vector3 topRight = new Vector3(right, top, worldPos.z);
            Vector3 btmLeft = new Vector3(left, btm, worldPos.z);
            Vector3 btmRight = new Vector3(right, btm, worldPos.z);

            Vector3[] points = new Vector3[4] { topLeft, btmLeft, btmRight,  topRight};

            lineRenderer.SetPositions(points);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.Instance.KillPlayer("You Were Lost in Space");
        }
    }
}
