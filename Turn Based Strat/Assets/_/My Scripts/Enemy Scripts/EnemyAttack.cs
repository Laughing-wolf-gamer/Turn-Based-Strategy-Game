using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class EnemyAttack : MonoBehaviour {
        
        

        private PlayerBrain m_player;

        private void Awake(){
            m_player = Object.FindObjectOfType<PlayerBrain>().GetComponent<PlayerBrain>();
        }
        public void Attack(){
            m_player.Die();
        }

        


    }

}