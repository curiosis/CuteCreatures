using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PassiveSkillsTable", menuName = "Game/Passive Skills Table")]
public class PassiveSkillsTable : ScriptableObject
{
    public List<SerializablePassiveSkill> Skills = new List<SerializablePassiveSkill>();
}