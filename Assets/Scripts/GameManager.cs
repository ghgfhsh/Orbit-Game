using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    //level over information
    [SerializeField] private float levelTime = 13f;
    private float timeRemaining;
    public bool isLevelOver = false; //makes sure only one level over event can fire at a time
    private bool timerStarted = false;
    public LevelWinTrigger winPortal;

    //fuel information
    [SerializeField]private float totalFuel;
    public float currentFuel { get; private set; }
    private float fuelUsagePerSecond = 10;

    //UI Gameobjects
    public LevelWinScreen LevelWinScreen;
    public LevelLoseScreen levelLoseScreen;


    public UnityEvent resetLevelEvent = new UnityEvent();
    public UnityEvent levelOverEvent = new UnityEvent();

    private void Start()
    {
        currentFuel = totalFuel;
        timeRemaining = levelTime;
    }

    public void ResetLevel()
    {
        currentFuel = totalFuel;
        timeRemaining = levelTime;
        resetLevelEvent.Invoke();
        timerStarted = false;
        isLevelOver = false;
    }

    public void UseFuel()
    {
        currentFuel -= fuelUsagePerSecond * Time.deltaTime;
    }

    public void StartTimer() //start timer is called when the player first fires the rocket
    {
        if (!timerStarted)
        {
            StartCoroutine(LevelTimer());
            timerStarted = true;
        }
    }

    public void KillPlayer(string deathMessage)
    {
        if (isLevelOver)
            return;

        levelOverEvent.Invoke();
        levelLoseScreen.gameObject.SetActive(true);
        levelLoseScreen.SetMessage(deathMessage);
        isLevelOver = true;
    }

    public void WinLevel()
    {
        if (isLevelOver)
            return;

        isLevelOver = true;
        levelOverEvent.Invoke();
        LevelWinScreen.gameObject.SetActive(true);
        LevelWinScreen.SetStats(timeRemaining, currentFuel);
    }

    IEnumerator LevelTimer()
    {
        float currentTime = 0f;
        while(timeRemaining > 0 && !isLevelOver)
        {
            timeRemaining = Mathf.Clamp(levelTime - currentTime, 0f, float.MaxValue);
            yield return new WaitForSeconds(0.1f);
            currentTime += .1f;
        }

        if(timeRemaining <= 0)
        {
            KillPlayer("Portal Closed!");
            winPortal.ClosePortal();
        }

        yield return null;
    }
}
