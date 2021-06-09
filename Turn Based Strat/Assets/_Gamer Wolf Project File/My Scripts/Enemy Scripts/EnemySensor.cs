using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class EnemySensor : MonoBehaviour {
        
        #region Variables.
        [SerializeField] private Vector3 directionToSearch = new Vector3(0f,0f,Board.spacing);

        private Node m_nodeToSearch;
        
        public bool m_foundPlayer;
        private Board m_board;
        
        private bool m_isPlayerHid;

        #endregion


        #region Methods.

        private void Start() {
            m_board = Board.Instance;
            
        }
        public void UpdateSensore(Node _currentNode){
            if(!m_isPlayerHid){
                Vector3 worldSpacePositonToSearch = transform.TransformVector(directionToSearch) + transform.position;
                m_nodeToSearch = m_board.FindNodeAt(worldSpacePositonToSearch);
                if(_currentNode.GetLinkedNodes.Contains(m_nodeToSearch)){
                    if(m_nodeToSearch == m_board.GetPlayerNode){
                        m_foundPlayer = true;
                    }
                }
            }
            
        }
        public void SetPlayerHiden(bool _State){
            m_isPlayerHid = _State;
        }
        

        
        
        public bool GetFoundPlayer{
            get{
                return m_foundPlayer;
            }
        }
        

        #endregion



    }
}
