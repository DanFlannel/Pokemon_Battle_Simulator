using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Moves : MonoBehaviour
{

    //List of flags and their descriptions:
    private readonly string authentic = "authentic"; //: Ignores a target's substitute.
    private readonly string bite = "bite";          //: Power is multiplied by 1.5 when used by a Pokemon with the Ability Strong Jaw.
    private readonly string bullet = "bullet";      //: Has no effect on Pokemon with the Ability Bulletproof.
    private readonly string charge = "charge";      //: The user is unable to make a move between turns.
    private readonly string contact = "contact";    //: Makes contact.
    private readonly string defrost = "defrost";    //: Thaws the user if executed successfully while the user is frozen.
    private readonly string distance = "distance";  //: Can target a Pokemon positioned anywhere in a Triple Battle.
    private readonly string gravity = "gravity";    //: Prevented from being executed or selected during Gravity's effect.
    private readonly string heal = "heal";          //: Prevented from being executed or selected during Heal Block's effect.
    private readonly string mirror = "mirror";      //: Can be copied by Mirror Move.
    private readonly string nonsky = "nonsky";      //: Prevented from being executed or selected in a Sky Battle.
    private readonly string powder = "powder";      //: Has no effect on Grass-type Pokemon, Pokemon with the Ability Overcoat, and Pokemon holding Safety Goggles.
    private readonly string protect = "protect";    //: Blocked by Detect, Protect, Spiky Shield, and if not a Status move, King's Shield.
    private readonly string pulse = "pulse";        //: Power is multiplied by 1.5 when used by a Pokemon with the Ability Mega Launcher.
    private readonly string punch = "punch";        //: Power is multiplied by 1.2 when used by a Pokemon with the Ability Iron Fist.
    private readonly string recharge = "recharge";  //: If this move is successful, the user must recharge on the following turn and cannot make a move.
    private readonly string reflectable = "reflectable";    //: Bounced back to the original user by Magic Coat or the Ability Magic Bounce.
    private readonly string snatch = "snatch";      //: Can be stolen from the original user and instead used by another Pokemon using Snatch.
    private readonly string sound = "sound";        //: Has no effect on Pokemon with the Ability Soundproof.

    //Categories Of Attacks
    private readonly string Normal = "Normal";
    private readonly string Fighting = "Fighting";
    private readonly string Water = "Water";
    private readonly string Poison = "Poison";
    private readonly string Ice = "Ice";
    private readonly string Ground = "Ground";
    private readonly string Grass = "Grass";
    private readonly string Psychic = "Psychic";
    private readonly string Dark = "Dark";
    private readonly string Ghost = "Ghost";
    private readonly string Dragon = "Dragon";
    private readonly string Flying = "Flying";
    private readonly string Fire = "Fire";
    private readonly string Bug = "Bug";
    private readonly string Rock = "Rock";
    private readonly string Electric = "Electric";


    //Types Of Attacks
    private readonly string Special = "Special";
    private readonly string Physical = "Physical";
    private readonly string Status = "Status";

    //Contest Types
    private readonly string c_Clever = "Clever";
    private readonly string c_Tough = "Tough";
    private readonly string c_Beautiful = "Beautiful";
    private readonly string c_Cool = "Cool";

    //Target Types
    private readonly string t_normal = "normal";
    private readonly string t_allAdjacent = "allAdjacentFoes";
    private readonly string t_self = "self";
    private readonly string t_allAlly_Self = "adjacentAllyOrSelf";
    private readonly string t_any = "any";

    public List<Move> PokemonMoves = new List<Move>();

    public void Moves_A_E()
    {
        List<string> flags = new List<string>();

        flags.Add(protect); flags.Add(mirror);  flags.Add(heal);
        PokemonMoves.Add(new Move(
        /*name          */ "Absorb",
        /*num           */  71,
        /*accuracy      */  100,
        /*basePower     */  20,
        /*category      */  Special,
        /*desc          */  "The user recovers 1/2 the HP lost by the target, rounded half up. If Big Root is held by the user, the HP recovered is 1.3x normal, rounded half down.",
        /*shortDesc     */  "User recovers 50% of the damage dealt.",
        /*id            */  "absorb",
        /*pp            */  25,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  t_normal,
        /*type          */  Grass,
        /*contesttype   */  c_Clever
        ));
        flags.Clear();

        flags.Add(protect); flags.Add(mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Acid",
        /*num           */  51,
        /*accuracy      */  100,
        /*basePower     */  40,
        /*category      */  Special,
        /*desc          */  "Has a 10% chance to lower the target's Special Defense by 1 stage.",
        /*shortDesc     */  "10% chance to lower the foe(s) Sp. Def by 1.",
        /*id            */  "acid",
        /*pp            */  30,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  t_allAdjacent,
        /*type          */  Poison,
        /*contesttype   */  "Clever"
        ));
        flags.Clear();

        flags.Add(snatch);
        PokemonMoves.Add(new Move(
        /*name          */ "Acid Armor",
        /*num           */  151,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Raises the user's Defense by 2 stages.",
        /*shortDesc     */  "Raises the user's Defense by 2.",
        /*id            */  "acidarmor",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  t_self,
        /*type          */  Poison,
        /*contesttype   */  c_Tough
        ));

        flags.Add(contact); flags.Add(protect); flags.Add(mirror); flags.Add(distance);
        PokemonMoves.Add(new Move(
        /*name          */ "Acrobatics",
        /*num           */  512,
        /*accuracy      */  100,
        /*basePower     */  55,
        /*category      */  Physical,
        /*desc          */  "Power doubles if the user has no held item.",
        /*shortDesc     */  "Power doubles if the user has no held item.",
        /*id            */  "acrobatics",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  t_normal,
        /*type          */  Flying,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();


        PokemonMoves.Add(new Move(
        /*name          */ "Acupressure",
        /*num           */  367,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Raises a random stat by 2 stages as long as the stat is not" + 
        " already at stage 6. The user can choose to use this move on itself or an adjacent ally."+
        " Fails if no stat stage can be raised or if used on an ally with a substitute.",
        /*shortDesc     */  "Raises a random stat of the user or an ally by 2.",
        /*id            */  "acupressure",
        /*pp            */  30,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  t_allAlly_Self,
        /*type          */  Normal,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flags.Add(contact); flags.Add(protect); flags.Add(mirror); flags.Add(distance);
        PokemonMoves.Add(new Move(
        /*name          */ "Aerialace",
        /*num           */  332,
        /*accuracy      */  100,
        /*basePower     */  60,
        /*category      */  Physical,
        /*desc          */  "This move does not check accuracy.",
        /*shortDesc     */  "This move does not check accuracy.",
        /*id            */  "aerialace",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  t_any,
        /*type          */  Flying,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();
    }

    public struct Move
    {
        string name;
        int num;
        int accuracy;
        int basePower;
        string category;
        string desc;
        string shortDesc;
        string id;
        int pp;
        int priority;
        List<string> flags;
        string target;
        string type;
        string contestType;

        public Move(string m_name, int m_num, int m_accuracy, int m_basePower, string m_category,
            string m_desc, string m_shortDesc, string m_id, int m_pp,
            int m_priority, List<string> m_flags, string m_target, string m_type,
            string m_contestType)
        {
            num = m_num;
            accuracy = m_accuracy;
            basePower = m_basePower;
            category = m_category;
            desc = m_desc;
            shortDesc = m_shortDesc;
            id = m_id;
            name = m_name;
            pp = m_pp;
            priority = m_priority;
            flags = m_flags;
            target = m_target;
            type = m_type;
            contestType = m_contestType;
        }
    }
}
