using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelLoseScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text deathMessageText;


    public void SetMessage(string message)
    {
        deathMessageText.text = message;
    }

    public void OnMainMenuButtonPressed()
    {

    }

    public void OnNextLevelButtonPressed()
    {

    }

    public void OnRetryButtonPressed()
    {
        GameManager.Instance.ResetLevel();
        gameObject.SetActive(false);
    }
}
