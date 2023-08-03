public abstract class SkillDecorator : ISkill
{
    private ISkill _skill;

    public SkillDecorator(ISkill skill)
    {
        _skill = skill;
    }

    public virtual void Use()
    {
        _skill.Use();
    }
}