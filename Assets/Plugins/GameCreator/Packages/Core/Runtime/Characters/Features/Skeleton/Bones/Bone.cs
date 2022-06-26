using System;
using System.Runtime.CompilerServices;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Characters
{
    [Serializable]
    public class Bone : TPolymorphicItem<Bone>, IBone
    {
        private enum Type
        {
            None,
            Human,
            Path
        }
        
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField] private Type m_Type = Type.Human;
        
        [SerializeField] private HumanBodyBones m_Human = HumanBodyBones.Hips;
        [SerializeField] private string m_Path = string.Empty;

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public Bone()
        { }

        public Bone(HumanBodyBones humanBone)
        {
            this.m_Type = Type.Human;
            this.m_Human = humanBone;
        }

        public Bone(string bonePath)
        {
            this.m_Type = Type.Path;
            this.m_Path = bonePath;
        }
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public GameObject Get(Animator animator)
        {
            Transform transform = this.GetTransform(animator);
            return transform != null ? transform.gameObject : null;
        }
        
        public Transform GetTransform(Animator animator)
        {
            return this.m_Type switch
            {
                Type.None => null,
                Type.Human => animator.GetBoneTransform(this.m_Human),
                Type.Path => animator.transform.Find(this.m_Path),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        // STRING: --------------------------------------------------------------------------------

        public override string ToString()
        {
            return this.m_Type switch
            {
                Type.None => "(none)",
                Type.Human => TextUtils.Humanize(this.m_Human.ToString()),
                Type.Path => this.m_Path,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}