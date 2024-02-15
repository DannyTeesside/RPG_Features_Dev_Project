using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class CameraFollow : MonoBehaviour
    {

        [SerializeField] Transform player;
        [SerializeField] Vector3 cameraOffset;

        void LateUpdate()
        {
            Vector3 desiredPosition = player.position + cameraOffset;
            transform.position = desiredPosition;

            transform.LookAt(player);
        }
    }
}
