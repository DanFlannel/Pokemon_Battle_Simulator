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

    #region Categories Of Attacks
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
    private readonly string Steel = "Steel";
    #endregion


    #region Types Of Attacks
    public readonly string Special = "Special";
    public readonly string Physical = "Physical";
    public readonly string Status = "Status";
    #endregion

    #region Contest Types
    private readonly string c_Clever = "Clever";
    private readonly string c_Tough = "Tough";
    private readonly string c_Beautiful = "Beautiful";
    private readonly string c_Cool = "Cool";
    private readonly string c_Cute = "Cute";
    #endregion

    #region Target Types
    private readonly string tNormal = "normal";
    private readonly string tAllAdjacentFoes = "allAdjacentFoes";
    private readonly string tSelf = "self";
    private readonly string tAllAlly_Self = "adjacentAllyOrSelf";
    private readonly string tAny = "any";
    private readonly string tAllAlly = "allyTeam";
    private readonly string tAdjacentAlly = "adjacentAlly";
    #endregion

    public List<Move> PokemonMoves = new List<Move>();
    private List<string> flags = new List<string>();

    void Start()
    {
        InitalizeMovesDataBase();
    }

    private void InitalizeMovesDataBase()
    {
        float time1 = Time.time;

        moves_A();
        moves_B();


        float time2 = Time.time;
        float totaltime = time2 - time1;
        Debug.Log(string.Format("Took {0} seconds", totaltime));
    }

    /// <summary>
    /// All the moves that start with the letter A (30)
    /// </summary>
    private void moves_A()
    {
        flagsAdd(protect, mirror, heal);
        PokemonMoves.Add(new Move(
        /*name          */ "Absorb",
        /*num           */  71,
        /*accuracy      */  100,
        /*basePower     */  20,
        /*category      */  Special,
        /*desc          */  "The user recovers 1/2 the HP lost by the target, rounded half up. If Big"+
        " Root is held by the user, the HP recovered is 1.3x normal, rounded half down.",
        /*shortDesc     */  "User recovers 50% of the damage dealt.",
        /*id            */  "absorb",
        /*pp            */  25,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
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
        /*target        */  tAllAdjacentFoes,
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
        /*target        */  tSelf,
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
        /*target        */  tNormal,
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
        /*target        */  tAllAlly_Self,
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
        /*target        */  tAny,
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
        /*target        */  tAny,
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
        /*target        */  tNormal,
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
        /*target        */  tSelf,
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
        /*target        */  tAllAdjacentFoes,
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
        /*target        */  tAny,
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
        /*target        */  tSelf,
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
        /*target        */  tSelf,
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
        /*target        */  tNormal,
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
        /*target        */  tNormal,
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
        /*target        */  tSelf,
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
        /*target        */  tNormal,
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
        /*target        */  tAllAlly,
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
        /*target        */  tAdjacentAlly,
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
        /*target        */  tSelf,
        /*type          */  Normal,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(contact,protect,mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Assurance",
        /*num           */  372,
        /*accuracy      */  100,
        /*basePower     */  60,
        /*category      */  Physical,
        /*desc          */  "Power doubles if the target has already taken damage this turn, other than"+
        " direct damage from Belly Drum, confusion, Curse, or Pain Split.",
        /*shortDesc     */  "Power doubles if target was damaged this turn.",
        /*id            */  "assurance",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Dark,
        /*contesttype   */  c_Clever
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Astonish",
        /*num           */  310,
        /*accuracy      */  100,
        /*basePower     */  30,
        /*category      */  Physical,
        /*desc          */  "Has a 30% chance to flinch the target.",
        /*shortDesc     */  "30% chance to flinch the target.",
        /*id            */  "astonish",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Ghost,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(highCrit, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Attack Order",
        /*num           */  454,
        /*accuracy      */  100,
        /*basePower     */  90,
        /*category      */  Physical,
        /*desc          */  "Has a higher chance for a critical hit.",
        /*shortDesc     */  "High critical hit ratio.",
        /*id            */  "attackorder",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Bug,
        /*contesttype   */  c_Clever
        ));
        flags.Clear();

        flagsAdd(protect, reflectable, mirror, authentic);
        PokemonMoves.Add(new Move(
        /*name          */ "Attract",
        /*num           */  213,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Causes the target to become infatuated, making it unable to attack 50% of"+
        " the time. Fails if both the user and the target are the same gender, if either is genderless,"+
        " or if the target is already infatuated. The effect ends when either the user or the target is "+
        "no longer active. Pokemon with the Ability Oblivious or protected by the Ability Aroma Veil are "+
        "immune.",
        /*shortDesc     */  "A target of the opposite gender gets infatuated.",
        /*id            */  "attract",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(bullet, protect, pulse, mirror, distance);
        PokemonMoves.Add(new Move(
        /*name          */ "Aura Sphere",
        /*num           */  396,
        /*accuracy      */  100,
        /*basePower     */  80,
        /*category      */  Special,
        /*desc          */  "This move does not check accuracy.",
        /*shortDesc     */  "This move does not check accuracy.",
        /*id            */  "aurasphere",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAny,
        /*type          */  Fighting,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Aurora Beam",
        /*num           */  62,
        /*accuracy      */  100,
        /*basePower     */  80,
        /*category      */  Special,
        /*desc          */  "Has a 10% chance to lower the target's Attack by 1 stage.",
        /*shortDesc     */  "10% chance to lower the foe's Attack by 1.",
        /*id            */  "aurorabeam",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Ice,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(snatch);
        PokemonMoves.Add(new Move(
        /*name          */ "Autotomize",
        /*num           */  475,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Raises the user's Speed by 2 stages. If the user's Speed was changed, the"+
        " user's weight is reduced by 100kg as long as it remains active. This effect is stackable but"+
        " cannot reduce the user's weight to less than 0.1kg.",
        /*shortDesc     */  "Raises the user's Speed by 2; user loses 100 kg.",
        /*id            */  "autotomize",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tSelf,
        /*type          */  Steel,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Avalanche",
        /*num           */  4719,
        /*accuracy      */  100,
        /*basePower     */  60,
        /*category      */  Physical,
        /*desc          */  "Power doubles if the user was hit by the target this turn.",
        /*shortDesc     */  "Power doubles if user is damaged by the target.",
        /*id            */  "avalanche",
        /*pp            */  10,
        /*priority      */  -4,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Ice,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();
    }

    /// <summary>
    /// All the moves that start with the letter B
    /// </summary>
    private void moves_B()
    {
        flagsAdd(protect, reflectable, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Baby-Doll Eyes",
        /*num           */  608,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Lowers the target's Attack by 1 stage.",
        /*shortDesc     */  "Lowers the target's Attack by 1.",
        /*id            */  "babydolleyes",
        /*pp            */  30,
        /*priority      */  1,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Fairy,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(bullet, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Barrage",
        /*num           */  140,
        /*accuracy      */  85,
        /*basePower     */  15,
        /*category      */  Physical,
        /*desc          */  "Hits two to five times. Has a 1/3 chance to hit two or three times, and"+
        " a 1/6 chance to hit four or five times. If one of the hits breaks the target's substitute, it"+
        " will take damage for the remaining hits. If the user has the Ability Skill Link, this move will"+
        " always hit five times.",
        /*shortDesc     */  "Hits 2-5 times in one turn.",
        /*id            */  "barrage",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  tNormal,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(snatch);
        PokemonMoves.Add(new Move(
        /*name          */ "Barrier",
        /*num           */  112,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Raises the user's Defense by 2 stages.",
        /*shortDesc     */  "Raises the user's Defense by 2.",
        /*id            */  "barrier",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tSelf,
        /*type          */  Psychic,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd();
        PokemonMoves.Add(new Move(
        /*name          */ "Baton Pass",
        /*num           */  226,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "The user is replaced with another Pokemon in its party. The selected"+
        " Pokemon has the user's stat stage changes, confusion, and certain move effects transferred"+
        " to it.",
        /*shortDesc     */  "User switches, passing stat changes and more.",
        /*id            */  "batonpass",
        /*pp            */  40,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tSelf,
        /*type          */  Normal,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Beat Up",
        /*num           */  251,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Physical,
        /*desc          */  "Hits one time for the user and one time for each unfainted Pokemon"+
        " without a major status condition in the user's party. The power of each hit is equal to 5+(X/10)"+
        ", where X is each participating Pokemon's base Attack; each hit is considered to come from the"+
        " user.",
        /*shortDesc     */  "All healthy allies aid in damaging the target.",
        /*id            */  "beatup",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Dark,
        /*contesttype   */  c_Clever
        ));
        flags.Clear();

        flagsAdd(protect);
        PokemonMoves.Add(new Move(
        /*name          */ "Belch",
        /*num           */  562,
        /*accuracy      */  90,
        /*basePower     */  120,
        /*category      */  Special,
        /*desc          */  "This move cannot be selected until the user eats a Berry, either by eating"+
        " one that was held, stealing and eating one off another Pokemon with Bug Bite or Pluck, or eating"+
        " one that was thrown at it with Fling. Once the condition is met, this move can be selected and"+
        " used for the rest of the battle even if the user gains or uses another item or switches out."+
        " Consuming a Berry with Natural Gift does not count for the purposes of eating one.",
        /*shortDesc     */  "Cannot be selected until the user eats a Berry.",
        /*id            */  "belch",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Poison,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(snatch);
        PokemonMoves.Add(new Move(
        /*name          */ "Belly Drum",
        /*num           */  187,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Raises the user's Attack by 12 stages in exchange for the user losing 1/2"+
        " of its maximum HP, rounded down. Fails if the user would faint or if its Attack stat stage is"+
        " 6.",
        /*shortDesc     */  "User loses 50% max HP. Maximizes Attack.",
        /*id            */  "bellydrum",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tSelf,
        /*type          */  Normal,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(mirror, authentic);
        PokemonMoves.Add(new Move(
        /*name          */ "Bestow",
        /*num           */  516,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "The target receives the user's held item. Fails if the user has no item or"+
        " is holding a Mail, if the target is already holding an item, if the user is a Kyogre holding a"+
        " Blue Orb, a Groudon holding a Red Orb, a Giratina holding a Griseous Orb, an Arceus holding a"+
        " Plate, a Genesect holding a Drive, a Pokemon that can Mega Evolve holding the Mega Stone for its"+
        " species, or if the target is one of those Pokemon and the user is holding the respective item.",
        /*shortDesc     */  "User passes its held item to the target.",
        /*id            */  "bestow",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(contact, protect);
        PokemonMoves.Add(new Move(
        /*name          */ "Bide",
        /*num           */  117,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Physical,
        /*desc          */  "The user spends two turns locked into this move and then, on the "+
        " turn after using this move, the user attacks the last Pokemon that hit it, inflicting"+
        " double the damage in HP it lost during the two turns. If the last Pokemon that hit it is"+
        " no longer on the field, the user attacks a random foe instead. If the user is prevented from"+
        " moving during this move's use, the effect ends. This move does not check accuracy.",
        /*shortDesc     */  "Waits 2 turns; deals double the damage taken.",
        /*id            */  "bide",
        /*pp            */  10,
        /*priority      */  1,
        /*flags         */  flags,
        /*target        */  tSelf,
        /*type          */  Normal,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Bind",
        /*num           */  20,
        /*accuracy      */  85,
        /*basePower     */  15,
        /*category      */  Physical,
        /*desc          */  "Prevents the target from switching for four or five turns; seven turns if the"+
        " user is holding Grip Claw. Causes damage to the target equal to 1/8 of its maximum HP"+
        " (1/6 if the user is holding Binding Band), rounded down, at the end of each turn during effect."+
        " The target can still switch out if it is holding Shed Shell or uses Baton Pass, Parting Shot,"+
        " U-turn, or Volt Switch. The effect ends if either the user or the target leaves the field, or if"+
        " the target uses Rapid Spin or Substitute. This effect is not stackable or reset by using this or"+
        " another partial-trapping move.",
        /*shortDesc     */  "Traps and damages the target for 4-5 turns.",
        /*id            */  "bide",
        /*pp            */  20,
        /*priority      */  1,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(bite, contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Bite",
        /*num           */  44,
        /*accuracy      */  100,
        /*basePower     */  60,
        /*category      */  Physical,
        /*desc          */  "Has a 30% chance to flinch the target.",
        /*shortDesc     */  "30% chance to flinch the target.",
        /*id            */  "bite",
        /*pp            */  25,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Dark,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(recharge, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Blast Burn",
        /*num           */  307,
        /*accuracy      */  90,
        /*basePower     */  150,
        /*category      */  Special,
        /*desc          */  "If this move is successful, the user must recharge on the following turn "+
        "and cannot make a move.",
        /*shortDesc     */  "User cannot move next turn.",
        /*id            */  "blastburn",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Fire,
        /*contesttype   */  c_Beautiful
        ));

        flagsAdd(contact, protect, mirror, highCrit);
        PokemonMoves.Add(new Move(
        /*name          */ "Blaze Kick",
        /*num           */  299,
        /*accuracy      */  90,
        /*basePower     */  85,
        /*category      */  Physical,
        /*desc          */  "Has a 10% chance to burn the target and a higher chance for a critical hit.",
        /*shortDesc     */  "High critical hit ratio. 10% chance to burn.",
        /*id            */  "blazekick",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Fire,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Blizzard",
        /*num           */  59,
        /*accuracy      */  70,
        /*basePower     */  110,
        /*category      */  Special,
        /*desc          */  "Has a 10% chance to freeze the target. If the weather is Hail, this move does"+
        " not check accuracy.",
        /*shortDesc     */  "High critical hit ratio. 10% chance to burn.",
        /*id            */  "blizzard",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAllAdjacentFoes,
        /*type          */  Ice,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(reflectable, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Block",
        /*num           */  355,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Prevents the target from switching out. The target can still switch out if it"+
        " is holding Shed Shell or uses Baton Pass, Parting Shot, U-turn, or Volt Switch. If the target"+
        " leaves the field using Baton Pass, the replacement will remain trapped. The effect ends if the "+
        "user leaves the field.",
        /*shortDesc     */  "The target cannot switch out.",
        /*id            */  "block",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Blue Flare",
        /*num           */  551,
        /*accuracy      */  85,
        /*basePower     */  130,
        /*category      */  Special,
        /*desc          */  "Has a 20% chance to burn the target.",
        /*shortDesc     */  "20% chance to burn the target.",
        /*id            */  "blueflare",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Fire,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror, nonsky);
        PokemonMoves.Add(new Move(
        /*name          */ "Body Slam",
        /*num           */  34,
        /*accuracy      */  100,
        /*basePower     */  85,
        /*category      */  Physical,
        /*desc          */  "Has a 30% chance to paralyze the target. Damage doubles and no accuracy"+
        " check is done if the target has used Minimize while active.",
        /*shortDesc     */  "30% chance to paralyze the target.",
        /*id            */  "bodyslam",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();
    }

    /// <summary>
    /// Takes in an infinite number of strings that are associated with the flags to add them all in one method
    /// </summary>
    /// <param name="s">flag strings</param>
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
        public string name;
        public int num;
        public int accuracy;
        public int basePower;
        public string category;
        public string desc;
        public string shortDesc;
        public string id;
        public int pp;
        public int priority;
        public List<string> flags;
        public string target;
        public string type;
        public string contestType;

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
