using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{

    public class PlayerCompass : MonoBehaviour {
        
        #region Variables.
        [SerializeField] private Transform arrowHeadPrefab;
        [SerializeField] private Vector3 arrowScale;
        [SerializeField] private float startDistance = 0.4f;
        [SerializeField] private float endDistance = 1.5f;

        [SerializeField] private float moveTime = 1f;
        [SerializeField] private float dealy = 0.4f;
        [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;


        /// Private Varibales.
        private List<Transform> arrowList = new List<Transform>();
        private Board m_board;

        #endregion


        #region Methods.

        private void Start() {
            m_board = Board.Instance;
            arrowList = new List<Transform>();
            
            SetUpCompassArrow();
            
        }
        
        

        
        private void SetUpCompassArrow(){
            if(!arrowHeadPrefab){
                return;
            }
            foreach (Vector2 direction in Board.direction){
                Vector3 dir = new Vector3(direction.x,0f,direction.y).normalized;
                Quaternion rotation = Quaternion.LookRotation(dir);
                Transform arrowInstance = Instantiate(arrowHeadPrefab,transform.position + dir * startDistance,rotation,transform);
                arrowInstance.localScale = arrowScale;
                arrowInstance.gameObject.SetActive(false);
                arrowList.Add(arrowInstance);
                
            }
        }
        private void ArrowMovement(Transform _arrow){
            
            iTween.MoveBy(_arrow.gameObject,iTween.Hash(
                "z",endDistance - startDistance,
                "loopType",iTween.LoopType.loop,
                "time",moveTime,
                "easeType",easeType
            ));
        }
        
        public void ArrowMovement(){
            foreach (Transform arrows in arrowList){
                ArrowMovement(arrows);
            }
        }
        public void ShowArrow(bool _showArrow){
            if(m_board == null){
                Debug.LogError(string.Concat(nameof(PlayerCompass)," Error No Board Found"));
                return;
            }
            if(m_board.GetPlayerNode != null){
                for(int i = 0; i < Board.direction.Length;i++){
                    Node NeighbourNodes = Board.Instance.GetPlayerNode.FindNeighbourAt(Board.direction[i]);
                    if(NeighbourNodes != null && _showArrow){
                        bool activeState = Board.Instance.GetPlayerNode.GetLinkedNodes.Contains(NeighbourNodes);
                        arrowList[i].gameObject.SetActive(activeState);
                    }else{
                        arrowList[i].gameObject.SetActive(false);
                    }
                }
            }
            Debug.Log("Arrow Moveing");
            ResetArrowMovement();
            ArrowMovement();
        }
        private void ResetArrowMovement(){
            for(int i = 0; i < arrowList.Count; i++){
                iTween.Stop(arrowList[i].gameObject);
                Vector3 direciton = new Vector3(Board.direction[i].x,0f,Board.direction[i].y).normalized;
                arrowList[i].position = transform.position + direciton * startDistance;
            }
        }

        #endregion


    }

}
