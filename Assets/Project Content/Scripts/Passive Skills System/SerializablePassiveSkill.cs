using UnityEngine;

[System.Serializable]
public class SerializablePassiveSkill
{
    public string SkillName;
    public ESkillType Type;

    [Tooltip("Lista progów doœwiadczenia potrzebnych na ka¿dy poziom")]
    public float[] ExpThresholds = new float[5];

    [Tooltip("Opis bonusów odblokowywanych na poziomach 1-5")]
    public string[] LevelBonuses = new string[5];
}