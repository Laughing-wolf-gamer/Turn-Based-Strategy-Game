using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{

    [RequireComponent(typeof(PlayerMover),typeof(Inputs),typeof(Collider))]
    public class PlayerBrain : TurnManager {
        
        #region Variables.
        [SerializeField] private UnityEvent deathEvent;
        private Transform GFXTransform;
        private Inputs input;
        private Mover playerMover;
        private KeyHolder keyHolder;
        private bool enableInputs = false;
        public bool hasPickedUpItem {
            get{
                return pickedItems.Count > 0;
            }
        }
        private List<Interactable> pickedItems;
        
        #endregion


        #region Methods.
        protected override void Awake(){
            base.Awake();
            pickedItems = new List<Interactable>();
            input = GetComponent<Inputs>();
            playerMover = GetComponent<Mover>();
            keyHolder = GetComponent<KeyHolder>();
            //GFXTransform = transform.Find("Ninja GFX").transform;

        }
        protected override void Start(){
            base.Start();
            m_gameHandler.SetPlayer(this);
            
        }
        public void SetInputState(bool _state){
            enableInputs = _state;
        }
        

        
        private void Update(){
            if(playerMover.GetIsMoveing() || m_gameHandler.GetCurrentTurn != Turn.Player) {
                return;
            }
            if(enableInputs){
                if(input.GetInputs() != Vector2.zero) {
                    if(input.GetInputs().x > 0) {
                        playerMover.MoveLeft();
                    }else if(input.GetInputs().x < 0) {
                        playerMover.MoveRight();
                    }
                    if(input.GetInputs().y > 0) {
                        playerMover.MoveBack();
                    }else if(input.GetInputs().y < 0) {
                        playerMover.MoveFoward();
                    }
                }
                // if Wants to Rotate Camera Rotate Camera..........
                CamerMovement.Instance.RotateCamera(input.GetCameraRotationAmount());
            }
        }
        public override void FinsiedTurn(){
            // Captureing The Enemies if they didnt See Us..
            CaputreEnemies();
            CollectItem();
            // Finsided the Turn...
            base.FinsiedTurn();
            
            
        }
        public void Die(){
            deathEvent?.Invoke();
        }
        private void CaputreEnemies(){
            List<EnemyBrain> enemies = Board.Instance.FindEnemiesAt(Board.Instance.GetPlayerNode);
            if(enemies.Count > 0){
                foreach(EnemyBrain enemy in enemies){
                    enemy.KillEnemy();
                }
            }
        }
        public void CollectItem(){
            if(GameHandler.Instance.isOnItem() != null){
                Interactable item = GameHandler.Instance.isOnItem();
                if(item.GetITemType() == ItemType.KEY){
                    Key itemKey = item.GetComponent<Key>();
                    if(itemKey != null){
                        keyHolder.AddKey(itemKey.GetKeyType());
                        GameHandler.Instance.OpenDoors(itemKey.GetKeyType());
                    }
                }
                pickedItems.Add(item);
                item.Interact();
            }
        
        }
        
        
        

        #endregion


    }
}
