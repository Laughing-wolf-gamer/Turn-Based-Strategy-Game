using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class TurnManager : MonoBehaviour {
        
        #region Variables.



        // Private Variables...................
        protected GameHandler m_gameHandler;
        private bool m_isTurnCompleted;
        #endregion


        #region Methods.

        protected virtual void Awake(){

        }
        protected virtual void Start() {
            m_gameHandler = GameHandler.Instance;
        }

        public virtual void FinsiedTurn(){
            m_isTurnCompleted = true;
            // Update Game Handler for Turn Changeing.........
            if(m_gameHandler != null){
                m_gameHandler.UpdateTurn();
            }
        }
        public bool GetIsTurnComplete{
            get{
                return m_isTurnCompleted;  
            }set{
                m_isTurnCompleted = value;
            }
        }

        #endregion
    }

}
