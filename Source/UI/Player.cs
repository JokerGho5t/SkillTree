using GameStateSkillTree;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SkillTreeView skillTreeView;
    [SerializeField] private HUD hud;

    [Space]
    
    [SerializeField] private SkillTree skillTree;
    [SerializeField] private int playerPoints;

    private Skill targetSkill;
    
    public void Start()
    {
        skillTree.RefreshAvailability(playerPoints);
        
        skillTreeView.Init(skillTree, OnSelectSkill);
        hud.Init(playerPoints, OnLearn, OnUnlearn, OnOBLIVION, OnEarn);
    }

    private void OnSelectSkill(Skill skill)
    {
        targetSkill = skill;
        var skillNode = skillTree.Tree[skillTree.FindSkillIndex(skill)];
        if (skill == skillTree.Root)
            skillNode.skillAvailability = ESkillAvailability.Lock;
        hud.Repaint(playerPoints, skillNode);
    }

    private void OnLearn()
    {
        skillTree.Learn(targetSkill, ref playerPoints);
        RepaintUI();
    }

    private void OnUnlearn()
    {
        skillTree.UnLearn(targetSkill, ref playerPoints);
        RepaintUI();
    }
    
    private void OnOBLIVION()
    {
        skillTree.OBLIVION(ref playerPoints);
        RepaintUI();
    }
    
    private void OnEarn()
    {
        playerPoints++;
        skillTree.RefreshAvailability(playerPoints);
        hud.RepaintPlayerInfo(playerPoints);
        skillTreeView.Repaint(skillTree);
    }

    private void RepaintUI()
    {
        skillTreeView.Repaint(skillTree);
        hud.Repaint(playerPoints, skillTree.Tree[skillTree.FindSkillIndex(targetSkill)]);
    }
}
