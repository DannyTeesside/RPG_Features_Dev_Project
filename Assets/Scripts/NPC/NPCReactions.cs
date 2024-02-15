using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCReactions : MonoBehaviour
{

    AudioSource reaction;
    // Start is called before the first frame update
    void Start()
    {
        reaction = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")

        {
            reaction.Play();
        }
    }
}
