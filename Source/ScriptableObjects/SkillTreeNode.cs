using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameStateSkillTree
{
    public enum ESkillAvailability
    {
        Lock,
        Available,
        Learned
    }
    
    [Serializable]
    public struct SkillTreeNode
    {
        public Skill skill;
        public List<Skill> requirements;
        public int cost;
        public ESkillAvailability skillAvailability;
        public Vector2 uiPosition;

        public SkillTreeNode(Skill skill, List<Skill> requirements, int cost,
            ESkillAvailability skillAvailability, Vector2 position)
        {
            this.skill = skill;
            this.requirements = requirements;
            this.cost = cost;
            this.skillAvailability = skillAvailability;
            this.uiPosition = position;
        }
    }
}