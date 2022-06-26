using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.VisualScripting
{
    [Title("Raycast 3D")]
    [Description("Returns true if there's a clear line of sight between two positions")]

    [Category("Physics/Raycast 3D")]
    
    [Parameter("Source", "The scene position where the raycast originates")]
    [Parameter("Target", "The targeted position where the raycast ends")]
    
    [Parameter("Layer Mask", "A bitmask that skips any objects that don't belong to the list")]
    
    [Example(
        "Note that this Instruction uses Unity's 3D physics engine. " +
        "It won't collide with any 2D objects"
    )]

    [Keywords("Check", "Collide", "Linecast", "See", "3D")]
    
    [Image(typeof(IconLineStartEnd), ColorTheme.Type.Green)]
    [Serializable]
    public class ConditionPhysicsRaycast3D : Condition
    {
        private static RaycastHit[] HITS = new RaycastHit[1];
        
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField] private PropertyGetPosition m_Source = GetPositionCamerasMain.Create;
        [SerializeField] private PropertyGetGameObject m_Target = GetGameObjectPlayer.Create();

        [SerializeField] private LayerMask m_LayerMask = -1;

        // PROPERTIES: ----------------------------------------------------------------------------
        
        protected override string Summary => $"check Raycast {this.m_Source} and {this.m_Target}";
        
        // RUN METHOD: ----------------------------------------------------------------------------

        protected override bool Run(Args args)
        {
            Vector3 source = this.m_Source.Get(args);
            GameObject target = this.m_Target.Get(args);

            if (target == null) return false;

            int hits = Physics.RaycastNonAlloc(
                source, target.transform.position - source, HITS,
                Vector3.Distance(source, target.transform.position), 
                this.m_LayerMask, QueryTriggerInteraction.Ignore
            );

            return hits == 0 || HITS[0].collider.gameObject == target;
        }
    }
}