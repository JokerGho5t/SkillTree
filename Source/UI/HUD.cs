using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameStateSkillTree
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private Text playerPointsText;
        [SerializeField] private Button btn_Earn;

        [Space] 
        [SerializeField] private Text skillInfoText;
        [SerializeField] private Button btn_Learn;
        [SerializeField] private Button btn_Unlearn;
        [SerializeField] private Button btn_OBLIVION;


        public void Init(int points, UnityAction onClickLearn, UnityAction onClickUnlearn, UnityAction onClickOBLIVION,
            UnityAction onClickEarn)
        {
            btn_Earn.onClick.AddListener(onClickEarn);
            btn_Learn.onClick.AddListener(onClickLearn);
            btn_Unlearn.onClick.AddListener(onClickUnlearn);
            btn_OBLIVION.onClick.AddListener(onClickOBLIVION);

            
            RepaintPlayerInfo(points);
            skillInfoText.text = $"Skill: \nCost: ";
            
            btn_Learn.interactable = false;
            btn_Unlearn.interactable = false;
        }

        public void Repaint(int points, SkillTreeNode skillTreeNode)
        {
            RepaintSkillInfo(skillTreeNode);
            RepaintPlayerInfo(points);
        }

        public void RepaintSkillInfo(SkillTreeNode skillTreeNode)
        {
            skillInfoText.text = $"Skill: {skillTreeNode.skill.Name}\nCost: {skillTreeNode.cost}";
            
            btn_Learn.interactable = skillTreeNode.skillAvailability == ESkillAvailability.Available;
            btn_Unlearn.interactable = skillTreeNode.skillAvailability == ESkillAvailability.Learned;
        }

        public void RepaintPlayerInfo(int points)
        {
            playerPointsText.text = $"Player: {points}";
        }
    }
}