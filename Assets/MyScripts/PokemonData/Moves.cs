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

    private readonly string highCrit = "highcritical"; //:has a 2x critical hit ratio

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
    private readonly string Fairy = "Fairy";


    //Types Of Attacks
    private readonly string Special = "Special";
    private readonly string Physical = "Physical";
    private readonly string Status = "Status";

    //Contest Types
    private readonly string c_Clever = "Clever";
    private readonly string c_Tough = "Tough";
    private readonly string c_Beautiful = "Beautiful";
    private readonly string c_Cool = "Cool";
    private readonly string c_Cute = "Cute";

    //Target Types
    private readonly string t_normal = "normal";
    private readonly string t_allAdjacent = "allAdjacentFoes";
    private readonly string t_self = "self";
    private readonly string t_allAlly_Self = "adjacentAllyOrSelf";
    private readonly string t_any = "any";
    private readonly string t_allAlly = "allyTeam";
    private readonly string t_adjacentAlly = "adjacentAlly";

    public List<Move> PokemonMoves = new List<Move>();
    private List<string> flags = new List<string>();

    public void Moves_A_E()
    {

        flagsAdd(protect, mirror, heal);
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

        flagsAdd(protect, mirror);
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

        flagsAdd(snatch);
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

        flagsAdd(contact, protect, mirror, distance);
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

        flagsAdd();
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

        flagsAdd(contact, protect, mirror, distance);
        PokemonMoves.Add(new Move(
        /*name          */ "Aerial Ace",
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

        flagsAdd(protect, mirror, distance, highCrit);
        PokemonMoves.Add(new Move(
        /*name          */ "Aeroblast",
        /*num           */  177,
        /*accuracy      */  95,
        /*basePower     */  100,
        /*category      */  Special,
        /*desc          */  "Has a higher chance for a critical hit.",
        /*shortDesc     */  "High critical hit ratio.",
        /*id            */  "aeroblast",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  t_any,
        /*type          */  Flying,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(authentic);
        PokemonMoves.Add(new Move(
        /*name          */ "After You",
        /*num           */  495,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "The target makes its move immediately after the user this turn, no matter"+
        " the priority of its selected move. Fails if the target would have moved next anyway, or if the"+
        " target already moved this turn.",
        /*shortDesc     */  "The target makes its move right after the user.",
        /*id            */  "afteryou",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  t_normal,
        /*type          */  Normal,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(snatch);
        PokemonMoves.Add(new Move(
        /*name          */ "Agility",
        /*num           */  97,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Raises the user's Speed by 2 stages.",
        /*shortDesc     */  "Raises the user's Speed by 2.",
        /*id            */  "agility",
        /*pp            */  30,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  t_self,
        /*type          */  Psychic,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(protect, mirror, highCrit);
        PokemonMoves.Add(new Move(
        /*name          */ "Air Cutter",
        /*num           */  314,
        /*accuracy      */  95,
        /*basePower     */  60,
        /*category      */  Special,
        /*desc          */  "Has a higher chance for a critical hit.",
        /*shortDesc     */  "High critical hit ratio. Hits adjacent foes.",
        /*id            */  "aircutter",
        /*pp            */  25,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  t_allAdjacent,
        /*type          */  Flying,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(protect, mirror, distance);
        PokemonMoves.Add(new Move(
        /*name          */ "Air Slash",
        /*num           */  403,
        /*accuracy      */  95,
        /*basePower     */  75,
        /*category      */  Special,
        /*desc          */  "Has a 30% chance to flinch the target.",
        /*shortDesc     */  "30% chance to flinch the target.",
        /*id            */  "airslash",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  t_any,
        /*type          */  Flying,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd();
        PokemonMoves.Add(new Move(
        /*name          */ "Ally Switch",
        /*num           */  502,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "The user swaps positions with its ally on the opposite side of the field."+
        " Fails if there is no Pokemon at that position, if the user is the only Pokemon on its side, or"+
        " if the user is in the middle.",
        /*shortDesc     */  "Switches position with the ally on the far side.",
        /*id            */  "allyswitch",
        /*pp            */  15,
        /*priority      */  1,
        /*flags         */  flags,
        /*target        */  t_self,
        /*type          */  Psychic,
        /*contesttype   */  c_Clever
        ));
        flags.Clear();

        flagsAdd(snatch);
        PokemonMoves.Add(new Move(
        /*name          */ "Amnesia",
        /*num           */  133,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Raises the user's Special Defense by 2 stages.",
        /*shortDesc     */  "Raises the user's Sp. Def by 2.",
        /*id            */  "amnesia",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  t_self,
        /*type          */  Psychic,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Ancient Power",
        /*num           */  246,
        /*accuracy      */  100,
        /*basePower     */  60,
        /*category      */  Special,
        /*desc          */  "Has a 10% chance to raise the user's Attack, Defense, Special Attack,"+
        " Special Defense, and Speed by 1 stage.",
        /*shortDesc     */  "10% chance to raise all stats by 1 (not acc/eva).",
        /*id            */  "ancientpower",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  t_normal,
        /*type          */  Rock,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Aqua Jet",
        /*num           */  453,
        /*accuracy      */  100,
        /*basePower     */  40,
        /*category      */  Physical,
        /*desc          */  "No additional effect.",
        /*shortDesc     */  "Usually goes first.",
        /*id            */  "aquajet",
        /*pp            */  20,
        /*priority      */  1,
        /*flags         */  flags,
        /*target        */  t_normal,
        /*type          */  Water,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(snatch);
        PokemonMoves.Add(new Move(
        /*name          */ "Aqua Ring",
        /*num           */  392,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "The user has 1/16 of its maximum HP, rounded down, restored at the end of"+
        " each turn while it remains active. If the user uses Baton Pass, the replacement will receive the"+
        " healing effect.",
        /*shortDesc     */  "User recovers 1/16 max HP per turn.",
        /*id            */  "aquaring",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  t_self,
        /*type          */  Water,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Arm Thrust",
        /*num           */  292,
        /*accuracy      */  100,
        /*basePower     */  15,
        /*category      */  Physical,
        /*desc          */  "Hits two to five times. Has a 1/3 chance to hit two or three times, and"+
        " a 1/6 chance to hit four or five times. If one of the hits breaks the target's substitute, it"+
        " will take damage for the remaining hits. If the user has the Ability Skill Link, this move will"+
        "always hit five times.",
        /*shortDesc     */  "Hits 2-5 times in one turn.",
        /*id            */  "armthrust",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  t_normal,
        /*type          */  Fighting,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(snatch, distance);
        PokemonMoves.Add(new Move(
        /*name          */ "Aromatherapy",
        /*num           */  312,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Every Pokemon in the user's party is cured of its major status condition."+
        " Active Pokemon with the Ability Sap Sipper are not cured, unless they are the user.",
        /*shortDesc     */  "Cures the user's party of all status conditions.",
        /*id            */  "aromatherapy",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  t_allAlly,
        /*type          */  Grass,
        /*contesttype   */  c_Clever
        ));
        flags.Clear();

        flagsAdd(authentic);
        PokemonMoves.Add(new Move(
        /*name          */ "Aromatic Mist",
        /*num           */  597,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Raises the target's Special Defense by 1 stage. Fails if there is no ally"+
        " adjacent to the user.",
        /*shortDesc     */  "Raises an ally's Sp. Def by 1.",
        /*id            */  "aromaticmist",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  t_adjacentAlly,
        /*type          */  Fairy,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd();
        PokemonMoves.Add(new Move(
        /*name          */ "Assist",
        /*num           */  274,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "A random move among those known by the user's party members is selected for"+
        " use. Does not select Assist, Belch, Bestow, Bounce, Chatter, Circle Throw, Copycat, Counter, "+
        "Covet, Destiny Bond, Detect, Dig, Dive, Dragon Tail, Endure, Feint, Fly, Focus Punch, Follow Me,"+
        " Helping Hand, Hold Hands, King's Shield, Mat Block, Me First, Metronome, Mimic, Mirror Coat, "+
        "Mirror Move, Nature Power, Phantom Force, Protect, Rage Powder, Roar, Shadow Force, Sketch, "+
        "Sky Drop, Sleep Talk, Snatch, Spiky Shield, Struggle, Switcheroo, Thief, Transform, Trick, or "+
        "Whirlwind.",
        /*shortDesc     */  "Uses a random move known by a team member.",
        /*id            */  "assist",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  t_self,
        /*type          */  t_normal,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();
    }

    private void flagsAdd(params string[] s)
    {
        if(s.Length == 0)
        {
            return;
        }

        for(int i = 0; i < s.Length; i++)
        {
            flags.Add(s[i]);
        }
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
