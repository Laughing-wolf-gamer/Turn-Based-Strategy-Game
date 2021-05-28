using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame {
    public class Node : MonoBehaviour {
        
        #region Variables.
        [SerializeField] private Transform nodeViewPrefabs;
        [Header("Stats")]
        [SerializeField] private float scaleTime = 0.1f;
        [SerializeField] private float dealyTime = 0.25f;
        [Header("ITween Variables.")]
        [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInExpo;

        [SerializeField] private bool autoRun;
        private Vector3 initalSize;
        #endregion


        #region Methods.

        private void Start() {
            if(nodeViewPrefabs != null){
                initalSize = nodeViewPrefabs.localScale;
                nodeViewPrefabs.localScale = Vector3.zero;
            }
            
        }
        

        public void ScaleNodeView(){
            if(nodeViewPrefabs != null){
                iTween.ScaleTo(nodeViewPrefabs.gameObject,iTween.Hash(
                    "time",scaleTime,
                    "scale",initalSize,
                    "easyType",easeType,
                    "dealyTime",dealyTime
                ));
            }
        }

        #endregion


    }
    
}
