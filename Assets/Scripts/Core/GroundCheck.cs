using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class GroundCheck : MonoBehaviour
    {

        [SerializeField] LayerMask platformLayerMask;

        public bool isGrounded;

        private void OnTriggerStay(Collider collider)
        {
            isGrounded = collider != null && (((1 << collider.gameObject.layer) * platformLayerMask) != 0);
        }

        private void OnTriggerExit(Collider collision)
        {
            isGrounded = false;
        }
    }
}
