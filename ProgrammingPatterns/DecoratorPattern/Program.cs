public class Program
{
    public static void Main(string[] args)
    {
        ISkill baseSkill = new BaseSkill();
        baseSkill.Use();

        ISkill laserSkill = new LaserSkill(new BaseSkill());
        laserSkill.Use();

        ISkill comboSkill = new ComboSkill(new BaseSkill());
        comboSkill.Use();

        // ---

        ISkill totalSkill = new ComboSkill(new LaserSkill(new BaseSkill()));
        totalSkill.Use();
    }
}