using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelWinScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text timeRemainingText;
    [SerializeField] private TMP_Text fuelRemainingText;


    public void SetStats(float timeRemaining, float fuelRemaining)
    {
        timeRemainingText.text = timeRemaining.ToString("#.0") + " seconds";
        fuelRemainingText.text = Mathf.RoundToInt(fuelRemaining).ToString();
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
