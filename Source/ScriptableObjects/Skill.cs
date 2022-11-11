using UnityEngine;

namespace GameStateSkillTree
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "GameStateSkillTree/Create Skill")]
    public class Skill : ScriptableObject
    {
        [SerializeField] private new string name;
        public string Name => name;
    }
}
    
