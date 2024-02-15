using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SkinnedMeshRenderer>().material.renderQueue = 3002;
    }


}
