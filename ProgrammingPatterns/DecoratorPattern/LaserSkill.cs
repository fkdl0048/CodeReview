public class LaserSkill : SkillDecorator
{
    public LaserSkill(ISkill skill) : base(skill)
    {
    }

    public override void Use()
    {
        base.Use();
        System.Console.WriteLine("레이저 스킬 사용");
    }
}