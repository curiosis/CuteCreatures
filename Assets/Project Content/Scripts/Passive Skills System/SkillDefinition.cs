public class SkillDefinition
{
    public ESkillType Type { get; private set; }
    public int Level { get; private set; } = 0;
    public float CurrentEXP { get; private set; } = 0;

    private float[] expThresholds;
    private string[] levelBonuses;

    public SkillDefinition(SerializablePassiveSkill skillData)
    {
        Type = skillData.Type;
        expThresholds = skillData.ExpThresholds;
        levelBonuses = skillData.LevelBonuses;
    }

    public void AddEXP(float amount)
    {
        if (Level >= expThresholds.Length) 
            return;

        CurrentEXP += amount;

        while (Level < expThresholds.Length && CurrentEXP >= expThresholds[Level])
        {
            CurrentEXP -= expThresholds[Level];
            LevelUp();
        }
    }

    private void LevelUp()
    {
        Level++;
        DebugTool.Instance.CreateLogInfo($"Skill {Type} leveled up to {Level}! Bonus: {levelBonuses[Level - 1]}", 5.0f);
    }

    public string GetCurrentBonus()
    {
        return Level > 0 && Level <= levelBonuses.Length ? levelBonuses[Level - 1] : "No bonus available.";
    }
}