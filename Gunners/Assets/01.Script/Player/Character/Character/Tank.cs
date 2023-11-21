public class Tank : ICharacter
{
    protected void Awake()
    {
        maxHp = 200;
        hp = maxHp;
        armor = 40;
        speed = 4;
    }
}
