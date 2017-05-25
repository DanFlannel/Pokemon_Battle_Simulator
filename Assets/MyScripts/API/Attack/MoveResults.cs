public class MoveResults
{
    /// <summary>
    /// Name of the attack move
    /// </summary>
    public string name;

    /// <summary>
    /// Did the attack hit
    /// </summary>
    public bool hit;

    /// <summary>
    /// Did the attack crit
    /// </summary>
    public bool crit;

    /// <summary>
    /// Did the attacker flinch
    /// </summary>
    public bool flinched;

    /// <summary>
    /// Did the attacker fail
    /// </summary>
    public bool failed;

    /// <summary>
    /// Damage report
    /// </summary>
    public move_DmgReport dmgReport;

    /// <summary>
    /// the name of the status affect affed during the move
    /// </summary>
    public string statusEffect;

    public bool ignoreSemiInvulerable;

    public MoveResults(string s)
    {
        name = s;

        hit = false;
        crit = false;
        flinched = false;
        failed = false;

        statusEffect = "";

        ignoreSemiInvulerable = false;

    }
}