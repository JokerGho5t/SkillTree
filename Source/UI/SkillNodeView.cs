using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameStateSkillTree
{
    public class SkillNodeView : MonoBehaviour
    {
        [SerializeField] private Selectable selectable;
        [SerializeField] private Image bkg;
        [SerializeField] private Image selectBorder;
        [SerializeField] private Color lockColor;
        [SerializeField] private Color availableColor;
        [SerializeField] private Color learnedColor;

        [SerializeField] private Text nameSkill;

        private Action m_OnSelect;
        private SelectHandler m_SelectHandler;

        private void Awake()
        {
            m_SelectHandler = selectable.gameObject.AddComponent<SelectHandler>();
            selectBorder.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            m_SelectHandler.OnSelection += OnSelectEvent;
        }

        private void OnDisable()
        {
            m_SelectHandler.OnSelection -= OnSelectEvent;
        }

        private void OnSelectEvent(bool isSelect)
        {
            if (isSelect)
                m_OnSelect?.Invoke();
        }

        public void SelectSkill(bool isSelect)
        {
            selectBorder.gameObject.SetActive(isSelect);
        }

        public void Setup(SkillTreeNode skillNode, ESkillAvailability skillAvailability, Action onSelect = null)
        {
            m_OnSelect = onSelect;
            nameSkill.text = skillNode.skill.Name;
            RepaintSkillAvailability(skillAvailability);
        }

        public void RepaintSkillAvailability(ESkillAvailability availability)
        {
            bkg.color = availability switch
            {
                ESkillAvailability.Lock => lockColor,
                ESkillAvailability.Available => availableColor,
                ESkillAvailability.Learned => learnedColor
            };
        }
    }
}
