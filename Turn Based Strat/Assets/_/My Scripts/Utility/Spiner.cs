using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class Spiner : MonoBehaviour {
        
        #region Variables.
        [SerializeField] private float rotationSpeed = 4f;

        #endregion


        #region Methods.

        private void Start() {
            iTween.RotateBy(gameObject,iTween.Hash(
                "y",360f,
                "looptype",iTween.LoopType.loop,
                "Speed",rotationSpeed,
                "easyType",iTween.EaseType.linear
            ));
        }

        
        
        #endregion


    }

}
