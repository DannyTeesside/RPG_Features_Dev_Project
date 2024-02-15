using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateMachine : MonoBehaviour
{

    public enum GameState { Roaming, Battling, Chase, CutScene, Paused };


    public GameState currentGameState = GameState.Roaming;

    [SerializeField] PauseMenu pauseMenu;


    BattleManager battleScript;

    AudioSource music;

    [SerializeField] AudioClip[] chaseMusic;
    [SerializeField] GameObject freeRoamUI;
    [SerializeField] GameObject transitions;

    float musicStartLength;


    public event Action onChaseStart;
    public event Action onRoaming;
    public event Action onBattleStart;

    public void EnterRoaming()
    {
        currentGameState = GameState.Roaming;
        music.Stop();
        freeRoamUI.SetActive(true);
        battleScript.enabled = false;
        onRoaming();
    }

    public void EnterBattle()
    {
        currentGameState = GameState.Battling;
        battleScript.enabled = true;
        onBattleStart();
    }

    // Start is called before the first frame update
    void Awake()
    {
        music = GetComponent<AudioSource>();
        battleScript = GetComponent<BattleManager>();
        battleScript.enabled = false;
        pauseMenu.onPause += PauseGame;
        pauseMenu.onResume += ResumeGame;
    }
    public void EnterCutscene()
    {
        currentGameState = GameState.CutScene;
        freeRoamUI.SetActive(false);
        battleScript.enabled = false;
    }


    public void EnterChase()
    {
        StartCoroutine(StartChase());
    }

    private IEnumerator StartChase()
    {
        StartBlackFade();
        battleScript.enabled = false;
        currentGameState = GameState.Chase;
        yield return new WaitForSeconds(1);
        freeRoamUI.SetActive(false);
        onChaseStart();
        PlayChaseMusic();

    }

    public void StartBlackFade()
    {
        StartCoroutine(transitions.GetComponent<Transitions>().StartBlackFade());
    }


    // Update is called once per frame
    void Update()
    {
        switch (currentGameState)
        {
            case (GameState.Roaming):
                //if (battleScript.battleUI.activeSelf == true)
                //{
                //    battleScript.battleUI.SetActive(false);
                //}
                break;
            case (GameState.Battling):
                
                break;
            case (GameState.Chase):

                break;
            case (GameState.CutScene):

                break;
        }
    }

    void PlayChaseMusic()
    {
        
        music.Play();
        
    }

    void PauseGame()
    {
        //currentGameState = GameState.Paused;

        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
