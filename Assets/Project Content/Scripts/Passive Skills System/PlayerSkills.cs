using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private PassiveSkillsTable skillsTable;

    private Dictionary<ESkillType, SkillDefinition> skills;

    private void Awake()
    {
        skills = new Dictionary<ESkillType, SkillDefinition>();

        foreach (var skillData in skillsTable.Skills)
        {
            skills.Add(skillData.Type, new SkillDefinition(skillData));
        }
    }

    public void AddSkillEXP(ESkillType type, float amount)
    {
        if (skills.TryGetValue(type, out var skill))
        {
            skill.AddEXP(amount);
        }
    }

    public int GetSkillLevel(ESkillType type)
    {
        return skills.TryGetValue(type, out var skill) ? skill.Level : 0;
    }

    public string GetSkillBonus(ESkillType type)
    {
        return skills.TryGetValue(type, out var skill) ? skill.GetCurrentBonus() : "No skill data available.";
    }
}