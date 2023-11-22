public class Soldier : ICharacter
{
    private void Awake()
    {
        maxHp = 150;
        hp = maxHp;
        armor = 30;
        speed = 7;
    }
}
