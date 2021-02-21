using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour
{
    PlayerController playerController;
    public bool playerControlEnabled = true;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        GameManager.Instance.resetLevelEvent.AddListener(EnablePlayerControl);
        GameManager.Instance.levelOverEvent.AddListener(DisablePlayerControl);
    }

    private void Update()
    {
        playerController.playerControlEnabled = playerControlEnabled;
        if (playerControlEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerController.firingRocket = true;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                playerController.firingRocket = false;
            }

            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                playerController.rocketDirectionPoint = mousePos;
            }
        }
    }

    public void DisablePlayerControl()
    {
        playerControlEnabled = false;
    }

    public void EnablePlayerControl()
    {
        playerControlEnabled = true;
    }
}
