using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartQTE : MonoBehaviour
{

    [SerializeField] GameObject QTE;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            QTE.SetActive(true);   
            Destroy(gameObject);

        }
    }
}
