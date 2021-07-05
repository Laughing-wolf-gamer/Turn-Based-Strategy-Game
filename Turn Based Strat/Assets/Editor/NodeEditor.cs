using UnityEditor;
using UnityEngine;

namespace GamerWolf.TurnBasedStratgeyGame{

    [InitializeOnLoad()]
    public class NodeEditor{
        
        [DrawGizmo(GizmoType.NotInSelectionHierarchy | GizmoType.Pickable| GizmoType.Pickable)]
        public static void OnDrawSceneGizmos(Node node,GizmoType gizmoType,Board _board){
            if((gizmoType & GizmoType.Selected) != 0){
                Gizmos.color = Color.yellow;
            }else{
                Gizmos.color = Color.yellow * 0.4f;
            }
            Gizmos.DrawSphere(node.transform.position,0.5f);
            Gizmos.color = Color.blue;
            foreach(Vector2 dir in Board.direction){
                Node neighboursNodes = node.FindNeighbourAt(dir);
                Gizmos.DrawLine(node.transform.position,neighboursNodes.transform.position);
            }
        }

        


    }

}