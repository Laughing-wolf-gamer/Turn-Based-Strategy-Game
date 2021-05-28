using UnityEngine;
using GamerWolf.Utilitys;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class Board : MonoBehaviour {
        
        #region Variables.
        public static float spacing = 8f;
        public static Vector2[] direction = {
            new Vector2(spacing,0f),
            new Vector2(-spacing,0f),
            new Vector2(0f,spacing),
            new Vector2(0f,-spacing)
        };

        [SerializeField] private List<Node> allNodeList = new List<Node>();
        
        #endregion


        #region Methods.

        #region Singelton.
        public static Board Instance{get; private set;}

        #endregion
        private void Awake(){
            if(Instance == null){
                Instance = this;
            }else{
                Destroy(Instance);
            }
        }
        private void Start() {


            
        }

        
        private void Update(){
            
        }
        public List<Node> GetAllNodeList(){
            return allNodeList;
        }

        #endregion


    }

}