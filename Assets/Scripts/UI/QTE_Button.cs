using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTE_Button : MonoBehaviour
{
    [SerializeField] GameObject timerVis;
    [SerializeField] GameObject quicktime_Button;
    [SerializeField] string QTEinput;
    [SerializeField] string QTEanimation;
    [SerializeField] string QTEFail;
    float timeTilTick = 0.01f;
    float fillPerTick = 0.012f;
    GameObject player;
    Animator playerAnim;


    float currentFillAmount;
    float timer;

    void Start()
    {
        
    }
    private void OnEnable()
    {
        currentFillAmount = 1f;
        timer = 0f;
        Time.timeScale = 0.05f;
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.unscaledDeltaTime;

        if (timer >= timeTilTick)
        {
            timer = 0;
            currentFillAmount -= fillPerTick;
        }

        RegisterInput();
        
        
        timerVis.GetComponent<Image>().fillAmount = currentFillAmount;
    }

    void RegisterInput()
    {
        if (Input.GetButtonDown(QTEinput) && currentFillAmount > 0)
        {
            Time.timeScale = 1f;
            
            playerAnim.Play(QTEanimation);
            gameObject.SetActive(false);
        }
        if (currentFillAmount <= 0)
        {
            Time.timeScale = 1f;
            playerAnim.Play(QTEFail);
            gameObject.SetActive(false);
        }
    }
}
