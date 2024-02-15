using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitions : MonoBehaviour
{

    [SerializeField] GameObject blackFade;


    public IEnumerator StartBlackFade()
    {
        blackFade.SetActive(true);
        yield return new WaitForSeconds(3);
        blackFade.SetActive(false);
    }



}
