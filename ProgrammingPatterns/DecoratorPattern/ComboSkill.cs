public class ComboSkill : SkillDecorator
{
    public ComboSkill(ISkill skill) : base(skill)
    {
    }

    public override void Use()
    {
        base.Use();
        System.Console.WriteLine("콤보 스킬 사용");
    }
}