using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{

    public event Action onPause;
    public event Action onResume;

    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject mainPauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            if (pauseMenuUI.activeSelf == false)
            {
                onPause();
                foreach (Transform child in pauseMenuUI.transform)
                {
                    child.gameObject.SetActive(false);
                }
                mainPauseScreen.SetActive(true);
            }
            if (pauseMenuUI.activeSelf == true)
            {
                onResume();
            }
            pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);
            
        }
    }
}
