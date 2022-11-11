using System.Collections.Generic;
using UnityEngine;

namespace GameStateSkillTree
{
    [CreateAssetMenu(fileName = "New SkillTree", menuName = "GameStateSkillTree/Create SkillTree")]
    public class SkillTree : ScriptableObject
    {
        [SerializeField] private Skill root;
        public Skill Root => root;
        
        [SerializeField] private List<SkillTreeNode> tree = new List<SkillTreeNode>();
        public List<SkillTreeNode> Tree => tree;

        public ESkillAvailability GetSkillAvailability(Skill skill)
        {
            var index = FindSkillIndex(skill);
            
            return index == -1 ? ESkillAvailability.Lock : tree[index].skillAvailability;
        }

        public int FindSkillIndex(Skill skill) => tree.FindIndex(x => x.skill == skill);

        public List<SkillTreeNode> GetAllBlockedSkills(Skill skill)
        {
            var skills = new List<SkillTreeNode>();
            
            foreach (var node in tree)
            {
                if (node.requirements.Contains(skill))
                    skills.Add(node);
            }

            return skills;
        }

        public void RefreshAvailability(int playerPoints)
        {
            for (var i = 0; i < tree.Count; i++)
            {
                var node = tree[i];
                
                if (node.skillAvailability == ESkillAvailability.Learned) continue;

                node.skillAvailability = ESkillAvailability.Lock;

                if (playerPoints >= node.cost)
                {
                    if (node.requirements.Count == 0)
                    {
                        node.skillAvailability = ESkillAvailability.Available;
                    }
                    else
                    {
                        foreach (var req in node.requirements)
                        {
                            var reqNode = tree[FindSkillIndex(req)];

                            if (reqNode.skillAvailability == ESkillAvailability.Learned)
                            {
                                node.skillAvailability = ESkillAvailability.Available;
                            }
                        }
                    }
                }

                tree[i] = node;
            }
        }

        public void Learn(Skill skill, ref int playerPoints)
        {
            var index = FindSkillIndex(skill);
            var node = tree[index];
            node.skillAvailability = ESkillAvailability.Learned;
            tree[index] = node;
            playerPoints -= node.cost;
            
            RefreshAvailability(playerPoints);
        }
        
        public bool UnLearn(Skill skill, ref int playerPoints)
        {
            var blockedSkills = GetAllBlockedSkills(skill).FindAll(x => x.skillAvailability == ESkillAvailability.Learned);
            foreach (var item in blockedSkills)
            {
                if (item.requirements.Count == 1) return false;

                bool isLearned = false;
                
                foreach (var req in item.requirements)
                {
                    if(req == skill) continue;
                    if (tree[FindSkillIndex(req)].skillAvailability == ESkillAvailability.Learned)
                    {
                        isLearned = true;
                        break;
                    }
                }

                if (!isLearned) return false;
            }

            var idx = FindSkillIndex(skill);
            var node = tree[idx];
            node.skillAvailability = ESkillAvailability.Available;
            playerPoints += node.cost;
            tree[idx] = node;
            
            RefreshAvailability(playerPoints);
            return true;
        }

        public void OBLIVION(ref int playerPoints)
        {
            for (var i = 0; i < tree.Count; i++)
            {
                var node = tree[i];
                if (node.skill == root || node.skillAvailability != ESkillAvailability.Learned) continue;

                node.skillAvailability = ESkillAvailability.Lock;
                tree[i] = node;
                
                playerPoints += tree[i].cost;
            }

            RefreshAvailability(playerPoints);
        }
    }
}
