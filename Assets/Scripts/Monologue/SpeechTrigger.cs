using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechTrigger : MonoBehaviour
{

    AudioSource monologue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            monologue = GetComponent<AudioSource>();
            monologue.Play();
            
            Destroy(gameObject, monologue.clip.length);

        }
    }

}
