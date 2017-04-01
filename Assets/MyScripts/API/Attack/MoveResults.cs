public class MoveResults
{
    public string name;
    public bool hit;
    public bool crit;
    public bool flinched;
    public move_DmgReport dmgReport;

    public MoveResults(string s)
    {
        name = s;
    }
}