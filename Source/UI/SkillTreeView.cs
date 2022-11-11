using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameStateSkillTree
{
    public class SkillTreeView : MonoBehaviour
    {
        [Header("Node")]
        [SerializeField] private SkillNodeView skillNodeViewPrefab;
        [SerializeField] private RectTransform parentNode;

        [Header("Connections")]
        [SerializeField] private Image connectionPrefab;
        [SerializeField] private RectTransform parentConnection;

        private List<SkillNodeView> m_Views = new List<SkillNodeView>();
        private List<GameObject> m_Connections = new List<GameObject>();
        
        private Action<Skill> m_SelectSkill;
        private SkillNodeView m_LastSelect;
        
        public void Init(SkillTree tree, Action<Skill> onSelectSkill)
        {
            m_SelectSkill = onSelectSkill;
            CreateTree(tree);
        }

        private void CreateTree(SkillTree tree)
        {
            foreach (var node in tree.Tree)
            {
                var view = Instantiate(skillNodeViewPrefab, parentNode);
                var viewRect = view.gameObject.GetComponent<RectTransform>();
                var rect = viewRect.rect;

                view.gameObject.transform.localPosition = node.uiPosition;
                
                view.name = "Skill: " + node.skill.Name;
                
                view.Setup(node, tree.GetSkillAvailability(node.skill), () =>
                {
                    m_LastSelect?.SelectSkill(false);
                    m_SelectSkill(node.skill);
                    m_LastSelect = view;
                    m_LastSelect?.SelectSkill(true);
                });
                
                CreateConnections(tree, node);
                
                m_Views.Add(view);
            }
        }

        private void CreateConnections(SkillTree tree, SkillTreeNode node)
        {
            foreach (var req in node.requirements)
            {
                var reqNode = tree.Tree.Find(x => x.skill == req);
                
                var connection = Instantiate(connectionPrefab, parentConnection);
                connection.transform.localPosition = node.uiPosition;
                connection.name = node.skill.Name + " >> " + reqNode.skill.Name;
                connection.transform.rotation = Helpers.Rotate2D(node.uiPosition, reqNode.uiPosition);
                connection.rectTransform.sizeDelta = new Vector2(
                    Vector2.Distance(node.uiPosition, reqNode.uiPosition), connection.rectTransform.sizeDelta.y);
                    
                m_Connections.Add(connection.gameObject);
            }
        }

        public void Repaint(SkillTree tree)
        {
            for (var i = 0; i < m_Views.Count; i++)
            {
                var view = m_Views[i];
                var node = tree.Tree[i];
                view.RepaintSkillAvailability(tree.GetSkillAvailability(node.skill));
            }
        }
    }
}
