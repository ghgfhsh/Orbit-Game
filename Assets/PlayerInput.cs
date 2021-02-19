using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour
{
    PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerController.firingRocket = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space)){
            playerController.firingRocket = false;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            playerController.rocketDirectionPoint = mousePos;
        }
    }

    private void OnMouseDown()
    {

    }
}
