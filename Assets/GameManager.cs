using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton Pattern
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public GameObject selectedRocket;

    private float levelTimer;

    private bool levelOver = false;

    public float totalFuel;

    public float currentFuel;

    public static float fuelUsagePerSecond = 10;

    public void ResetLevel()
    {
        currentFuel = totalFuel;
    }

    public void useFuel()
    {
        currentFuel -= fuelUsagePerSecond * Time.deltaTime;
    }

    public void StartTimer()
    {
        levelTimer = Time.time;
    }

    public void KillPlayer(string deathMessage)
    {
        if (levelOver)
            return;

        float timeTaken = Time.time - levelTimer;
        Debug.Log(deathMessage + " Time Taken: " + timeTaken);
        levelOver = true;
    }

    public void WinLevel()
    {
        if (levelOver)
            return;

        float timeTaken = Time.time - levelTimer;
        Debug.Log("Player Won Level Time Taken: " + timeTaken);
        levelOver = true;
    }
}
