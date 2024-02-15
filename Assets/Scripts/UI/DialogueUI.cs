using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Dialogue;
using TMPro;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {

        GameObject avatar;
        GameObject newAvatar;
        PlayerConversant playerConversant;
        [SerializeField] TextMeshProUGUI AIText;
        [SerializeField] TextMeshProUGUI conversantName;
        AudioSource voiceOver;


        // Start is called before the first frame update
        void Start()
        {
            
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            playerConversant.onConversationUpdated += UpdateUI;
            voiceOver = GetComponent<AudioSource>();
            //avatar = playerConversant.GetSpeakerAvatar();

            UpdateUI();
            
        }

        void Next()
        {
            playerConversant.Next();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("X"))
            {
                Next();
            }
        }

        void UpdateUI()
        {
            gameObject.SetActive(playerConversant.IsActive());
            if(!playerConversant.IsActive())
            {
                return;
            }
            if (newAvatar != null)
            {
                Destroy(newAvatar.gameObject);
            }
            avatar = playerConversant.GetSpeakerAvatar();
            if (avatar != null)
            {
                newAvatar = Instantiate(avatar) as GameObject;
                newAvatar.transform.SetParent(transform, false);


            }
            conversantName.text = playerConversant.GetCurrentConversantName();
            //voiceOver.Stop();
            AIText.text = playerConversant.GetText();
            voiceOver.clip = playerConversant.GetVoiceClip();
            voiceOver.Play();
        }
    }
}
