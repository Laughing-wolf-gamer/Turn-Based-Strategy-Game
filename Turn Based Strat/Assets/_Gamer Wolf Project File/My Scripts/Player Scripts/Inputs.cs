using UnityEngine;
using GamerWolf.Utilitys;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class Inputs : MonoBehaviour {
        
        #region Variables.
        private float horizontalInput;
        private float verticalInputs;

        private Collider m_collider;
        #endregion


        #region Methods.
        private void Awake(){
            m_collider = GetComponent<Collider>();
        }
        private void Update(){
            
            verticalInputs = MobileInputs.GetSwipDirectionWithCollider().z;
            horizontalInput = MobileInputs.GetSwipDirectionWithCollider().x;
            
        }
        public Vector2 GetInputs(){
            return new Vector2(horizontalInput,verticalInputs).normalized;
        }
        public float GetCameraRotationAmount(){
            float swipMultiPlier = 5f;
            return MobileInputs.GetSwipAmount() * swipMultiPlier;
        }
        public Vector3 GetDraggingDirection(){
            float swipMultiPlier = 2f;
            return MobileInputs.GetDragDirection() * swipMultiPlier;
        }

        #endregion


    }

}