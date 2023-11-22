public class Scout : ICharacter
{
    private void Awake()
    {
        maxHp = 100;
        hp = maxHp;
        armor = 30;
        speed = 10;
    }
}
