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
    private readonly string tAllAdjacent = "allAdjacent";
    private readonly string tScripted = "scripted";
    private readonly string tAll = "all";
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
        moves_C();
        moves_D();
        moves_E();
        moves_F();

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
        /*desc          */  "The user recovers 1/2 the HP lost by the target, rounded half up. If Big" +
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
        " already at stage 6. The user can choose to use this move on itself or an adjacent ally." +
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
        /*desc          */  "The target makes its move immediately after the user this turn, no matter" +
        " the priority of its selected move. Fails if the target would have moved next anyway, or if the" +
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
        /*desc          */  "The user swaps positions with its ally on the opposite side of the field." +
        " Fails if there is no Pokemon at that position, if the user is the only Pokemon on its side, or" +
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
        /*desc          */  "Has a 10% chance to raise the user's Attack, Defense, Special Attack," +
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
        /*desc          */  "The user has 1/16 of its maximum HP, rounded down, restored at the end of" +
        " each turn while it remains active. If the user uses Baton Pass, the replacement will receive the" +
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
        /*desc          */  "Hits two to five times. Has a 1/3 chance to hit two or three times, and" +
        " a 1/6 chance to hit four or five times. If one of the hits breaks the target's substitute, it" +
        " will take damage for the remaining hits. If the user has the Ability Skill Link, this move will" +
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
        /*desc          */  "Every Pokemon in the user's party is cured of its major status condition." +
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
        /*desc          */  "Raises the target's Special Defense by 1 stage. Fails if there is no ally" +
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
        /*desc          */  "A random move among those known by the user's party members is selected for" +
        " use. Does not select Assist, Belch, Bestow, Bounce, Chatter, Circle Throw, Copycat, Counter, " +
        "Covet, Destiny Bond, Detect, Dig, Dive, Dragon Tail, Endure, Feint, Fly, Focus Punch, Follow Me," +
        " Helping Hand, Hold Hands, King's Shield, Mat Block, Me First, Metronome, Mimic, Mirror Coat, " +
        "Mirror Move, Nature Power, Phantom Force, Protect, Rage Powder, Roar, Shadow Force, Sketch, " +
        "Sky Drop, Sleep Talk, Snatch, Spiky Shield, Struggle, Switcheroo, Thief, Transform, Trick, or " +
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

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Assurance",
        /*num           */  372,
        /*accuracy      */  100,
        /*basePower     */  60,
        /*category      */  Physical,
        /*desc          */  "Power doubles if the target has already taken damage this turn, other than" +
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
        /*desc          */  "Causes the target to become infatuated, making it unable to attack 50% of" +
        " the time. Fails if both the user and the target are the same gender, if either is genderless," +
        " or if the target is already infatuated. The effect ends when either the user or the target is " +
        "no longer active. Pokemon with the Ability Oblivious or protected by the Ability Aroma Veil are " +
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
        /*desc          */  "Raises the user's Speed by 2 stages. If the user's Speed was changed, the" +
        " user's weight is reduced by 100kg as long as it remains active. This effect is stackable but" +
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
        /*desc          */  "Hits two to five times. Has a 1/3 chance to hit two or three times, and" +
        " a 1/6 chance to hit four or five times. If one of the hits breaks the target's substitute, it" +
        " will take damage for the remaining hits. If the user has the Ability Skill Link, this move will" +
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
        /*desc          */  "The user is replaced with another Pokemon in its party. The selected" +
        " Pokemon has the user's stat stage changes, confusion, and certain move effects transferred" +
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
        /*desc          */  "Hits one time for the user and one time for each unfainted Pokemon" +
        " without a major status condition in the user's party. The power of each hit is equal to 5+(X/10)" +
        ", where X is each participating Pokemon's base Attack; each hit is considered to come from the" +
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
        /*desc          */  "This move cannot be selected until the user eats a Berry, either by eating" +
        " one that was held, stealing and eating one off another Pokemon with Bug Bite or Pluck, or eating" +
        " one that was thrown at it with Fling. Once the condition is met, this move can be selected and" +
        " used for the rest of the battle even if the user gains or uses another item or switches out." +
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
        /*desc          */  "Raises the user's Attack by 12 stages in exchange for the user losing 1/2" +
        " of its maximum HP, rounded down. Fails if the user would faint or if its Attack stat stage is" +
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
        /*desc          */  "The target receives the user's held item. Fails if the user has no item or" +
        " is holding a Mail, if the target is already holding an item, if the user is a Kyogre holding a" +
        " Blue Orb, a Groudon holding a Red Orb, a Giratina holding a Griseous Orb, an Arceus holding a" +
        " Plate, a Genesect holding a Drive, a Pokemon that can Mega Evolve holding the Mega Stone for its" +
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
        /*desc          */  "The user spends two turns locked into this move and then, on the " +
        " turn after using this move, the user attacks the last Pokemon that hit it, inflicting" +
        " double the damage in HP it lost during the two turns. If the last Pokemon that hit it is" +
        " no longer on the field, the user attacks a random foe instead. If the user is prevented from" +
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
        /*desc          */  "Prevents the target from switching for four or five turns; seven turns if the" +
        " user is holding Grip Claw. Causes damage to the target equal to 1/8 of its maximum HP" +
        " (1/6 if the user is holding Binding Band), rounded down, at the end of each turn during effect." +
        " The target can still switch out if it is holding Shed Shell or uses Baton Pass, Parting Shot," +
        " U-turn, or Volt Switch. The effect ends if either the user or the target leaves the field, or if" +
        " the target uses Rapid Spin or Substitute. This effect is not stackable or reset by using this or" +
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
        /*desc          */  "If this move is successful, the user must recharge on the following turn " +
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
        /*desc          */  "Has a 10% chance to freeze the target. If the weather is Hail, this move does" +
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
        /*desc          */  "Prevents the target from switching out. The target can still switch out if it" +
        " is holding Shed Shell or uses Baton Pass, Parting Shot, U-turn, or Volt Switch. If the target" +
        " leaves the field using Baton Pass, the replacement will remain trapped. The effect ends if the " +
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
        /*desc          */  "Has a 30% chance to paralyze the target. Damage doubles and no accuracy" +
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

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Bolt Strike",
        /*num           */  550,
        /*accuracy      */  85,
        /*basePower     */  130,
        /*category      */  Physical,
        /*desc          */  "Has a 20% chance to paralyze the target.",
        /*shortDesc     */  "20% chance to paralyze the target.",
        /*id            */  "boltstrike",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Electric,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Bone Club",
        /*num           */  125,
        /*accuracy      */  85,
        /*basePower     */  65,
        /*category      */  Physical,
        /*desc          */  "Has a 10% chance to flinch the target.",
        /*shortDesc     */  "10% chance to flinch the target.",
        /*id            */  "boneclub",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Ground,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Bone Rush",
        /*num           */  198,
        /*accuracy      */  90,
        /*basePower     */  25,
        /*category      */  Physical,
        /*desc          */  "Hits two to five times. Has a 1/3 chance to hit two or three times, and a" +
        " 1/6 chance to hit four or five times. If one of the hits breaks the target's substitute, it " +
        "will take damage for the remaining hits. If the user has the Ability Skill Link, this move will" +
        " always hit five times.",
        /*shortDesc     */  "Hits 2-5 times in one turn.",
        /*id            */  "bonerush",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Ground,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Bonemerang",
        /*num           */  155,
        /*accuracy      */  90,
        /*basePower     */  50,
        /*category      */  Special,
        /*desc          */  "Hits twice. If the first hit breaks the target's substitute, it will take" +
        " damage for the second hit.",
        /*shortDesc     */  "Hits 2 times in one turn.",
        /*id            */  "bonemerang",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Ground,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(contact, charge, protect, mirror, gravity, distance);
        PokemonMoves.Add(new Move(
        /*name          */ "Bounce",
        /*num           */  340,
        /*accuracy      */  85,
        /*basePower     */  85,
        /*category      */  Physical,
        /*desc          */  "Has a 30% chance to paralyze the target. This attack charges on the first" +
        " turn and executes on the second. On the first turn, the user avoids all attacks other than Gust," +
        " Hurricane, Sky Uppercut, Smack Down, Thousand Arrows, Thunder, and Twister. If the user is" +
        " holding a Power Herb, the move completes in one turn.",
        /*shortDesc     */  "Bounces turn 1. Hits turn 2. 30% paralyze.",
        /*id            */  "bounce",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAny,
        /*type          */  Flying,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror, distance);
        PokemonMoves.Add(new Move(
        /*name          */ "Brave Bird",
        /*num           */  413,
        /*accuracy      */  100,
        /*basePower     */  120,
        /*category      */  Physical,
        /*desc          */  "If the target lost HP, the user takes recoil damage equal to 33% the HP" +
        " lost by the target, rounded half up, but not less than 1 HP.",
        /*shortDesc     */  "Has 33% recoil.",
        /*id            */  "bravebird",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAny,
        /*type          */  Flying,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Brick Break",
        /*num           */  280,
        /*accuracy      */  100,
        /*basePower     */  75,
        /*category      */  Physical,
        /*desc          */  "If this attack does not miss, the effects of Reflect and Light Screen end" +
        " for the target's side of the field before damage is calculated.",
        /*shortDesc     */  "Destroys screens, unless the target is immune.",
        /*id            */  "brickbreak",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Fighting,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Brine",
        /*num           */  362,
        /*accuracy      */  100,
        /*basePower     */  65,
        /*category      */  Physical,
        /*desc          */  "Power doubles if the target has less than or equal to half of its maximum" +
        " HP remaining.",
        /*shortDesc     */  "Power doubles if the target's HP is 50% or less.",
        /*id            */  "brine",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Water,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Bubble",
        /*num           */  145,
        /*accuracy      */  100,
        /*basePower     */  40,
        /*category      */  Special,
        /*desc          */  "Has a 10% chance to lower the target's Speed by 1 stage.",
        /*shortDesc     */  "10 % chance to lower the foe(s) Speed by 1.",
        /*id            */  "bubble",
        /*pp            */  30,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAllAdjacentFoes,
        /*type          */  Water,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Bubble Beam",
        /*num           */  61,
        /*accuracy      */  100,
        /*basePower     */  65,
        /*category      */  Special,
        /*desc          */  "Has a 10% chance to lower the target's Speed by 1 stage.",
        /*shortDesc     */  "10 % chance to lower the target's Speed by 1.",
        /*id            */  "bubblebeam",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Water,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(protect, mirror, sound, authentic);
        PokemonMoves.Add(new Move(
        /*name          */ "Bug Buzz",
        /*num           */  405,
        /*accuracy      */  100,
        /*basePower     */  90,
        /*category      */  Special,
        /*desc          */  "Has a 10% chance to lower the target's Special Defense by 1 stage.",
        /*shortDesc     */  "10% chance to lower the target's Sp. Def. by 1.",
        /*id            */  "bugbuzz",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Bug,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(snatch);
        PokemonMoves.Add(new Move(
        /*name          */ "Bulk Up",
        /*num           */  339,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Raises the user's Attack and Defense by 1 stage.",
        /*shortDesc     */  "Raises the user's Attack and Defense by 1.",
        /*id            */  "bulkup",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tSelf,
        /*type          */  Fighting,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(protect, mirror, nonsky);
        PokemonMoves.Add(new Move(
        /*name          */ "Bulldoze",
        /*num           */  523,
        /*accuracy      */  100,
        /*basePower     */  60,
        /*category      */  Physical,
        /*desc          */  "Has a 100% chance to lower the target's Speed by 1 stage.",
        /*shortDesc     */  "100% chance to lower adjacent Pkmn Speed by 1.",
        /*id            */  "bulldoze",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAllAdjacent,
        /*type          */  Ground,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror, punch);
        PokemonMoves.Add(new Move(
        /*name          */ "Bullet Punch",
        /*num           */  418,
        /*accuracy      */  100,
        /*basePower     */  40,
        /*category      */  Physical,
        /*desc          */  "No additional effect.",
        /*shortDesc     */  "Usually goes first.",
        /*id            */  "bulletpunch",
        /*pp            */  30,
        /*priority      */  1,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Steel,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(bullet, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Bullet Seed",
        /*num           */  331,
        /*accuracy      */  100,
        /*basePower     */  25,
        /*category      */  Physical,
        /*desc          */  "Hits two to five times. Has a 1/3 chance to hit two or three times, and a" +
        " 1/6 chance to hit four or five times. If one of the hits breaks the target's substitute, it " +
        "will take damage for the remaining hits. If the user has the Ability Skill Link, this move will" +
        " always hit five times.",
        /*shortDesc     */  "Hits 2-5 times in one turn.",
        /*id            */  "bulletseed",
        /*pp            */  30,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Ground,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();
    }

    /// <summary>
    /// All the moves that start with the letter C
    /// </summary>
    private void moves_C()
    {
        flagsAdd(snatch);
        PokemonMoves.Add(new Move(
        /*name          */ "Calm Mind",
        /*num           */  347,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Raises the user's Special Attack and Special Defense by 1 stage.",
        /*shortDesc     */  "Raises the user's Sp. Atk and Sp. Def by 1.",
        /*id            */  "calmmind",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tSelf,
        /*type          */  Psychic,
        /*contesttype   */  c_Clever
        ));
        flags.Clear();

        flagsAdd(snatch);
        PokemonMoves.Add(new Move(
        /*name          */ "Camouflage",
        /*num           */  293,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "The user's type changes based on the battle terrain. Normal type on the" +
        " regular Wi-Fi terrain, Electric type during Electric Terrain, Fairy type during Misty Terrain," +
        " and Grass type during Grassy Terrain. Fails if the user's type cannot be changed or if the user" +
        " is already purely that type.",
        /*shortDesc     */  "Changes user's type by terrain (default Normal).",
        /*id            */  "camouflage",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tSelf,
        /*type          */  Normal,
        /*contesttype   */  c_Clever
        ));
        flags.Clear();

        flagsAdd(protect, reflectable, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Captivate",
        /*num           */  445,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Lowers the target's Special Attack by 2 stages. The target is unaffected" +
        " if both the user and the target are the same gender, or if either is genderless. Pokemon with" +
        " the Ability Oblivious are immune.",
        /*shortDesc     */  "Lowers the foe(s) Sp. Atk by 2 if opposite gender.",
        /*id            */  "captivate",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAllAdjacentFoes,
        /*type          */  Normal,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd();
        PokemonMoves.Add(new Move(
        /*name          */ "Celebrate",
        /*num           */  606,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "It is your birthday.",
        /*shortDesc     */  "No competitive use. Or any use.",
        /*id            */  "celebrate",
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
        /*name          */ "Charge Beam",
        /*num           */  451,
        /*accuracy      */  90,
        /*basePower     */  50,
        /*category      */  Special,
        /*desc          */  "Has a 70% chance to raise the user's Special Attack by 1 stage.",
        /*shortDesc     */  "70% chance to raise the user's Sp. Atk by 1.",
        /*id            */  "chargebeam",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Electric,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(protect, reflectable, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Charm",
        /*num           */  204,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Lowers the target's Attack by 2 stages.",
        /*shortDesc     */  "Lowers the target's Attack by 2 stages.",
        /*id            */  "charm",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Fairy,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(protect, mirror, sound, distance, authentic);
        PokemonMoves.Add(new Move(
        /*name          */ "Chatter",
        /*num           */  448,
        /*accuracy      */  100,
        /*basePower     */  65,
        /*category      */  Special,
        /*desc          */  "Has a 100% chance to confuse the target.",
        /*shortDesc     */  "100% chance to confuse the target.",
        /*id            */  "chatter",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAny,
        /*type          */  Flying,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Chip Away",
        /*num           */  498,
        /*accuracy      */  100,
        /*basePower     */  70,
        /*category      */  Physical,
        /*desc          */  "Ignores the target's stat stage changes, including evasiveness.",
        /*shortDesc     */  "Ignores the target's stat stage changes.",
        /*id            */  "chipaway",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Circle Throw",
        /*num           */  509,
        /*accuracy      */  90,
        /*basePower     */  60,
        /*category      */  Physical,
        /*desc          */  "If both the user and the target have not fainted, the target is forced to" +
        " switch out and be replaced with a random unfainted ally. This effect fails if the target used" +
        " Ingrain previously, has the Ability Suction Cups, or this move hit a substitute.",
        /*shortDesc     */  "Forces the target to switch to a random ally.",
        /*id            */  "circlethrow",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Fighting,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Clamp",
        /*num           */  128,
        /*accuracy      */  85,
        /*basePower     */  35,
        /*category      */  Physical,
        /*desc          */  "Prevents the target from switching for four or five turns; seven turns if" +
        " the user is holding Grip Claw. Causes damage to the target equal to 1/8 of its maximum HP " +
        "(1/6 if the user is holding Binding Band), rounded down, at the end of each turn during effect." +
        " The target can still switch out if it is holding Shed Shell or uses Baton Pass, Parting Shot," +
        " U-turn, or Volt Switch. The effect ends if either the user or the target leaves the field, or " +
        "if the target uses Rapid Spin or Substitute. This effect is not stackable or reset by using this" +
        " or another partial-trapping move.",
        /*shortDesc     */  "Traps and damages the target for 4-5 turns.",
        /*id            */  "clamp",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Water,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Clear Smog",
        /*num           */  499,
        /*accuracy      */  100,
        /*basePower     */  50,
        /*category      */  Special,
        /*desc          */  "Resets all of the target's stat stages to 0.",
        /*shortDesc     */  "Eliminates the target's stat changes.",
        /*id            */  "clearsmog",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Poison,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Close Combat",
        /*num           */  370,
        /*accuracy      */  100,
        /*basePower     */  120,
        /*category      */  Physical,
        /*desc          */  "Lowers the user's Defense and Special Defense by 1 stage.",
        /*shortDesc     */  "Lowers the user's Defense and Sp. Def by 1.",
        /*id            */  "closecombat",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Fighting,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(snatch);
        PokemonMoves.Add(new Move(
        /*name          */ "Coil",
        /*num           */  489,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Raises the user's Attack, Defense, and accuracy by 1 stage.",
        /*shortDesc     */  "Raises the user's Attack, Defense, and accuracy by 1.",
        /*id            */  "coil",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tSelf,
        /*type          */  Poison,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror, punch);
        PokemonMoves.Add(new Move(
        /*name          */ "Comet Punch",
        /*num           */  4,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Hits two to five times. Has a 1/3 chance to hit two or three times, and a" +
        " 1/6 chance to hit four or five times. If one of the hits breaks the target's substitute, it" +
        " will take damage for the remaining hits. If the user has the Ability Skill Link, this move will" +
        " always hit five times.",
        /*shortDesc     */  "Hits 2-5 times in one turn.",
        /*id            */  "cometpunch",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(reflectable, mirror, sound, authentic);
        PokemonMoves.Add(new Move(
        /*name          */ "Confide",
        /*num           */  590,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Lowers the target's Special Attack by 1 stage.",
        /*shortDesc     */  "Lowers the target's Special Attack by 1.",
        /*id            */  "confide",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(protect, reflectable, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Confuse Ray",
        /*num           */  109,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Causes the target to become confused.",
        /*shortDesc     */  "Confuses the target.",
        /*id            */  "confuseray",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Ghost,
        /*contesttype   */  c_Clever
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Confusion",
        /*num           */  93,
        /*accuracy      */  100,
        /*basePower     */  50,
        /*category      */  Special,
        /*desc          */  "Has a 10% chance to confuse the target.",
        /*shortDesc     */  "10% chance to confuse the target.",
        /*id            */  "confusion",
        /*pp            */  25,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Psychic,
        /*contesttype   */  c_Clever
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Constrict",
        /*num           */  132,
        /*accuracy      */  100,
        /*basePower     */  10,
        /*category      */  Physical,
        /*desc          */  "Has a 10% chance to lower the target's Speed by 1 stage.",
        /*shortDesc     */  "10% chance to lower the target's Speed by 1.",
        /*id            */  "constrict",
        /*pp            */  35,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(snatch);
        PokemonMoves.Add(new Move(
        /*name          */ "Conversion",
        /*num           */  160,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "The user's type changes to match the original type of the move in its first" +
        " move slot. Fails if the user cannot change its type, or if the type is one of the user's current" +
        " types.",
        /*shortDesc     */  "Changes user's type to match its first move.",
        /*id            */  "conversion",
        /*pp            */  30,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tSelf,
        /*type          */  Normal,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(authentic);
        PokemonMoves.Add(new Move(
        /*name          */ "Conversion2",
        /*num           */  176,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "The user's type changes to match a type that resists or is immune to the type" +
        " of the last move used by the target, but not either of its current types. The determined type of" +
        " the move is used rather than the original type. Fails if the target has not made a move, if the" +
        " user cannot change its type, or if this move would only be able to select one of the user's " +
        "current types.",
        /*shortDesc     */  "Changes user's type to resist target's last move.",
        /*id            */  "conversion2",
        /*pp            */  30,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd();
        PokemonMoves.Add(new Move(
        /*name          */ "Copycat",
        /*num           */  383,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "The user uses the last move used by any Pokemon, including itself. Fails if" +
        " no move has been used, or if the last move used was Assist, Belch, Bestow, Chatter, Circle Throw," +
        " Copycat, Counter, Covet, Destiny Bond, Detect, Dragon Tail, Endure, Feint, Focus Punch, Follow Me" +
        ", Helping Hand, Hold Hands, King's Shield, Mat Block, Me First, Metronome, Mimic, Mirror Coat, " +
        "Mirror Move, Nature Power, Protect, Rage Powder, Roar, Sketch, Sleep Talk, Snatch, Spiky Shield, " +
        "Struggle, Switcheroo, Thief, Transform, Trick, or Whirlwind.",
        /*shortDesc     */  "Uses the last move used in the battle.",
        /*id            */  "copycat",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tSelf,
        /*type          */  Normal,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(snatch);
        PokemonMoves.Add(new Move(
        /*name          */ "Cosmic Power",
        /*num           */  332,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Raises the user's Defense and Special Defense by 1 stage.",
        /*shortDesc     */  "Raises the user's Defense and Sp. Def by 1.",
        /*id            */  "cosmicpower",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tSelf,
        /*type          */  Psychic,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(snatch);
        PokemonMoves.Add(new Move(
        /*name          */ "Cotton Guard",
        /*num           */  583,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Raises the user's Defense by 3 stages.",
        /*shortDesc     */  "Raises the user's Defense by 3.",
        /*id            */  "cottonguard",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tSelf,
        /*type          */  Grass,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(powder, protect, reflectable, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Cotton Spore",
        /*num           */  178,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Lowers the target's Speed by 2 stages.",
        /*shortDesc     */  "Lowers the target's Speed by 2.",
        /*id            */  "cottonspore",
        /*pp            */  40,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAllAdjacentFoes,
        /*type          */  Grass,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(contact, protect);
        PokemonMoves.Add(new Move(
        /*name          */ "Counter",
        /*num           */  68,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Physical,
        /*desc          */  "Deals damage to the last foe to hit the user with a physical attack this" +
        " turn equal to twice the HP lost by the user from that attack. If the user did not lose HP from" +
        " the attack, this move deals damage with a Base Power of 1 instead. If that foe's position is no" +
        " longer in use, the damage is done to a random foe in range. Only the last hit of a multi-hit" +
        " attack is counted. Fails if the user was not hit by a foe's physical attack this turn.",
        /*shortDesc     */  "If hit by physical attack, returns double damage.",
        /*id            */  "counter",
        /*pp            */  20,
        /*priority      */  -5,
        /*flags         */  flags,
        /*target        */  tScripted,
        /*type          */  Fighting,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Covet",
        /*num           */  343,
        /*accuracy      */  100,
        /*basePower     */  60,
        /*category      */  Physical,
        /*desc          */  "If this attack was successful and the user has not fainted, it steals the" +
        " target's held item if the user is not holding one. The target's item is not stolen if it is a" +
        " Mail, or if the target is a Kyogre holding a Blue Orb, a Groudon holding a Red Orb, a Giratina" +
        " holding a Griseous Orb, an Arceus holding a Plate, a Genesect holding a Drive, or a Pokemon that" +
        " can Mega Evolve holding the Mega Stone for its species. Items lost to this move cannot be" +
        " regained with Recycle or the Ability Harvest.",
        /*shortDesc     */  "If the user has no item, it steals the target's.",
        /*id            */  "covet",
        /*pp            */  25,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror, highCrit);
        PokemonMoves.Add(new Move(
        /*name          */ "Crabhammer",
        /*num           */  152,
        /*accuracy      */  90,
        /*basePower     */  100,
        /*category      */  Physical,
        /*desc          */  "Has a higher chance for a critical hit.",
        /*shortDesc     */  "High critical hit ratio.",
        /*id            */  "crabhammer",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Water,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd();
        PokemonMoves.Add(new Move(
        /*name          */ "Crafty Shield",
        /*num           */  578,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "The user and its party members are protected from non-damaging attacks made" +
        " by other Pokemon, including allies, during this turn. Fails if the user moves last this turn or" +
        " if this move is already in effect for the user's side.",
        /*shortDesc     */  "Protects allies from Status moves this turn.",
        /*id            */  "craftyshield",
        /*pp            */  10,
        /*priority      */  3,
        /*flags         */  flags,
        /*target        */  tAllAlly,
        /*type          */  Fairy,
        /*contesttype   */  c_Clever
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror, highCrit);
        PokemonMoves.Add(new Move(
        /*name          */ "Cross Chop",
        /*num           */  238,
        /*accuracy      */  80,
        /*basePower     */  100,
        /*category      */  Physical,
        /*desc          */  "Has a higher chance for a critical hit.",
        /*shortDesc     */  "High critical hit ratio.",
        /*id            */  "crosschop",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Fighting,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror, highCrit);
        PokemonMoves.Add(new Move(
        /*name          */ "Cross Poison",
        /*num           */  440,
        /*accuracy      */  100,
        /*basePower     */  70,
        /*category      */  Physical,
        /*desc          */  "Has a 10% chance to poison the target and a higher chance for a critical hit.",
        /*shortDesc     */  "High critical hit ratio. 10% chance to poison.",
        /*id            */  "crosspoison",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Poison,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(bite, contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Crunch",
        /*num           */  242,
        /*accuracy      */  100,
        /*basePower     */  80,
        /*category      */  Physical,
        /*desc          */  "Has a 20% chance to lower the target's Defense by 1 stage.",
        /*shortDesc     */  "20% chance to lower the target's Defense by 1.",
        /*id            */  "crunch",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Dark,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Crush Claw",
        /*num           */  306,
        /*accuracy      */  95,
        /*basePower     */  75,
        /*category      */  Physical,
        /*desc          */  "Has a 50% chance to lower the target's Defense by 1 stage.",
        /*shortDesc     */  "50% chance to lower the target's Defense by 1.",
        /*id            */  "crushclaw",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Crush Grip",
        /*num           */  462,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Physical,
        /*desc          */  "Power is equal to 120 * (target's current HP / target's maximum HP), " +
        "rounded half down, but not less than 1.",
        /*shortDesc     */  "More power the more HP the target has left.",
        /*id            */  "crushgrip",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(authentic);
        PokemonMoves.Add(new Move(
        /*name          */ "Curse",
        /*num           */  174,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "If the user is not a Ghost type, lowers the user's Speed by 1 stage and " +
        "raises the user's Attack and Defense by 1 stage. If the user is a Ghost type, the user loses" +
        " 1/2 of its maximum HP, rounded down and even if it would cause fainting, in exchange for the" +
        " target losing 1/4 of its maximum HP, rounded down, at the end of each turn while it is active." +
        " If the target uses Baton Pass, the replacement will continue to be affected. Fails if there is" +
        " no target or if the target is already affected.",
        /*shortDesc     */  "Curses if Ghost, else +1 Atk, +1 Def, -1 Spe.",
        /*id            */  "curse",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Ghost,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Cut",
        /*num           */  15,
        /*accuracy      */  95,
        /*basePower     */  50,
        /*category      */  Physical,
        /*desc          */  "No additional effect.",
        /*shortDesc     */  "No additional effect.",
        /*id            */  "cut",
        /*pp            */  30,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();
    }

    private void moves_D()
    {
        flagsAdd(protect, pulse, mirror, distance);
        PokemonMoves.Add(new Move(
        /*name          */ "Dark Pulse",
        /*num           */  399,
        /*accuracy      */  100,
        /*basePower     */  80,
        /*category      */  Special,
        /*desc          */  "Has a 20% chance to flinch the target.",
        /*shortDesc     */  "20% chance to flinch the target.",
        /*id            */  "darkpulse",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAny,
        /*type          */  Dark,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(protect, reflectable, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Dark Void",
        /*num           */  464,
        /*accuracy      */  80,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Causes the target to fall asleep.",
        /*shortDesc     */  "Puts the foe(s) to sleep.",
        /*id            */  "darkvoid",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAllAdjacentFoes,
        /*type          */  Dark,
        /*contesttype   */  c_Clever
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Dazzling Gleam",
        /*num           */  605,
        /*accuracy      */  100,
        /*basePower     */  80,
        /*category      */  Special,
        /*desc          */  "No additional effect.",
        /*shortDesc     */  "No additional effect.",
        /*id            */  "dazzlinggleam",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAllAdjacentFoes,
        /*type          */  Fairy,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(snatch);
        PokemonMoves.Add(new Move(
        /*name          */ "Defend Order",
        /*num           */  455,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Raises the user's Defense and Special Defense by 1 stage.",
        /*shortDesc     */  "Raises the user's Defense and Sp. Def by 1.",
        /*id            */  "defendorder",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tSelf,
        /*type          */  Bug,
        /*contesttype   */  c_Clever
        ));
        flags.Clear();

        flagsAdd(snatch);
        PokemonMoves.Add(new Move(
        /*name          */ "Defense Curl",
        /*num           */  111,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Raises the user's Defense by 1 stage. As long as the user remains active," +
        " the power of the user's Ice Ball and Rollout will be doubled (this effect is not stackable).",
        /*shortDesc     */  "Raises the user's Defense by 1.",
        /*id            */  "defensecurl",
        /*pp            */  40,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tSelf,
        /*type          */  Normal,
        /*contesttype   */  c_Cute
        ));

        flagsAdd(protect, reflectable, mirror, authentic);
        PokemonMoves.Add(new Move(
        /*name          */ "Defog",
        /*num           */  432,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Lowers the target's evasiveness by 1 stage. If this move is successful and" +
        " whether or not the target's evasiveness was affected, the effects of Reflect, Light Screen," +
        " Safeguard, Mist, Spikes, Toxic Spikes, Stealth Rock, and Sticky Web end for the target's side," +
        " and the effects of Spikes, Toxic Spikes, Stealth Rock, and Sticky Web end for the user's side." +
        " Ignores a target's substitute, although a substitute will still block the lowering of " +
        "evasiveness.",
        /*shortDesc     */  "Removes hazards from field. Lowers foe's evasion.",
        /*id            */  "defog",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Flying,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(authentic);
        PokemonMoves.Add(new Move(
        /*name          */ "Destiny Bond",
        /*num           */  194,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Until the user's next turn, if a foe's attack knocks the user out, that foe" +
        " faints as well, unless the attack was Doom Desire or Future Sight.",
        /*shortDesc     */  "Removes hazards from field. Lowers foe's evasion.",
        /*id            */  "destinybond",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tSelf,
        /*type          */  Ghost,
        /*contesttype   */  c_Clever
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Diamond Storm",
        /*num           */  591,
        /*accuracy      */  95,
        /*basePower     */  100,
        /*category      */  Physical,
        /*desc          */  "Has a 50% chance to raise the user's Defense by 1 stage.",
        /*shortDesc     */  "50% chance to raise user's Def by 1 for each hit.",
        /*id            */  "diamondstorm",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAllAdjacentFoes,
        /*type          */  Rock,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(contact, charge, protect, mirror, nonsky);
        PokemonMoves.Add(new Move(
        /*name          */ "Dig",
        /*num           */  91,
        /*accuracy      */  100,
        /*basePower     */  80,
        /*category      */  Physical,
        /*desc          */  "This attack charges on the first turn and executes on the second. On the" +
        " first turn, the user avoids all attacks other than Earthquake and Magnitude but takes double" +
        " damage from them, and is also unaffected by weather. If the user is holding a Power Herb, the" +
        " move completes in one turn.",
        /*shortDesc     */  "Digs underground turn 1, strikes turn 2.",
        /*id            */  "dig",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Ground,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(protect, reflectable, mirror, authentic);
        PokemonMoves.Add(new Move(
        /*name          */ "Disable",
        /*num           */  50,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "For 4 turns, the target's last move used becomes disabled. Fails if one" +
        " of the target's moves is already disabled, if the target has not made a move, or if the target" +
        " no longer knows the move.",
        /*shortDesc     */  "For 4 turns, disables the target's last move used.",
        /*id            */  "disable",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Clever
        ));
        flags.Clear();

        flagsAdd(protect, mirror, sound, authentic);
        PokemonMoves.Add(new Move(
        /*name          */ "Disarming Voice",
        /*num           */  574,
        /*accuracy      */  100,
        /*basePower     */  40,
        /*category      */  Special,
        /*desc          */  "This move does not check accuracy.",
        /*shortDesc     */  "This move does not check accuracy. Hits foes.",
        /*id            */  "disarmingvoice",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAllAdjacentFoes,
        /*type          */  Fairy,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Discharge",
        /*num           */  435,
        /*accuracy      */  100,
        /*basePower     */  80,
        /*category      */  Special,
        /*desc          */  "Has a 30% chance to paralyze the target.",
        /*shortDesc     */  "30% chance to paralyze adjacent Pokemon.",
        /*id            */  "discharge",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAllAdjacent,
        /*type          */  Electric,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(contact, charge, protect, mirror, nonsky);
        PokemonMoves.Add(new Move(
        /*name          */ "Dive",
        /*num           */  291,
        /*accuracy      */  100,
        /*basePower     */  80,
        /*category      */  Physical,
        /*desc          */  "This attack charges on the first turn and executes on the second. On the" +
        " first turn, the user avoids all attacks other than Surf and Whirlpool but takes double damage" +
        " from them, and is also unaffected by weather. If the user is holding a Power Herb, the move" +
        " completes in one turn.",
        /*shortDesc     */  "Dives underwater turn 1, strikes turn 2.",
        /*id            */  "dive",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Water,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd();
        PokemonMoves.Add(new Move(
        /*name          */ "Doom Desire",
        /*num           */  353,
        /*accuracy      */  100,
        /*basePower     */  140,
        /*category      */  Special,
        /*desc          */  "Deals damage two turns after this move is used. At the end of that turn, " +
        "the damage is calculated at that time and dealt to the Pokemon at the position the target had" +
        " when the move was used. If the user is no longer active at the time, damage is calculated based" +
        " on the user's natural Special Attack stat, types, and level, with no boosts from its held item" +
        " or Ability. Fails if this move or Future Sight is already in effect for the target's position.",
        /*shortDesc     */  "Hits two turns after being used.",
        /*id            */  "doomdesire",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Steel,
        /*contesttype   */  c_Beautiful
        ));

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Double-Edge",
        /*num           */  38,
        /*accuracy      */  100,
        /*basePower     */  120,
        /*category      */  Physical,
        /*desc          */  "If the target lost HP, the user takes recoil damage equal to 33 % the HP" +
        " lost by the target, rounded half up, but not less than 1 HP.",
        /*shortDesc     */  "Has 33% recoil.",
        /*id            */  "doubleedge",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Double Hit",
        /*num           */  458,
        /*accuracy      */  90,
        /*basePower     */  35,
        /*category      */  Physical,
        /*desc          */  "Hits twice. If the first hit breaks the target's substitute, it will take " +
        "damage for the second hit.",
        /*shortDesc     */  "Hits 2 times in one turn.",
        /*id            */  "doublehit",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Double Kick",
        /*num           */  24,
        /*accuracy      */  100,
        /*basePower     */  30,
        /*category      */  Physical,
        /*desc          */  "Hits twice. If the first hit breaks the target's substitute, it will take" +
        " damage for the second hit.",
        /*shortDesc     */  "Hits 2 times in one turn.",
        /*id            */  "doublekick",
        /*pp            */  30,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Fighting,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Double Slap",
        /*num           */  3,
        /*accuracy      */  85,
        /*basePower     */  15,
        /*category      */  Physical,
        /*desc          */  "Hits two to five times. Has a 1/3 chance to hit two or three times, and a 1/6" +
        " chance to hit four or five times. If one of the hits breaks the target's substitute, it will take" +
        " damage for the remaining hits. If the user has the Ability Skill Link, this move will always hit" +
        " five times.",
        /*shortDesc     */  "Hits 2-5 times in one turn.",
        /*id            */  "doubleslap",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(snatch);
        PokemonMoves.Add(new Move(
        /*name          */ "Double Team",
        /*num           */  104,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Raises the user's evasiveness by 1 stage.",
        /*shortDesc     */  "Raises the user's evasiveness by 1.",
        /*id            */  "doubleteam",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tSelf,
        /*type          */  Normal,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Draco Meteor",
        /*num           */  434,
        /*accuracy      */  90,
        /*basePower     */  130,
        /*category      */  Special,
        /*desc          */  "Lowers the user's Special Attack by 2 stages.",
        /*shortDesc     */  "Lowers the user's Sp. Atk by 2.",
        /*id            */  "dracometeor",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Dragon,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror, distance);
        PokemonMoves.Add(new Move(
        /*name          */ "Dragon Ascent",
        /*num           */  620,
        /*accuracy      */  100,
        /*basePower     */  120,
        /*category      */  Physical,
        /*desc          */  "Lowers the user's Defense and Special Defense by 1 stage.",
        /*shortDesc     */  "Lowers the user's Defense and Sp. Def by 1.",
        /*id            */  "dragonascent",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAny,
        /*type          */  Flying,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Dragon Breath",
        /*num           */  225,
        /*accuracy      */  100,
        /*basePower     */  160,
        /*category      */  Special,
        /*desc          */  "Has a 30% chance to paralyze the target.",
        /*shortDesc     */  "30% chance to paralyze the target.",
        /*id            */  "dragonbreath",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Dragon,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Dragon Claw",
        /*num           */  337,
        /*accuracy      */  100,
        /*basePower     */  80,
        /*category      */  Physical,
        /*desc          */  "No additional effect.",
        /*shortDesc     */  "No additional effect.",
        /*id            */  "dragonclaw",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Dragon,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(snatch);
        PokemonMoves.Add(new Move(
        /*name          */ "Dragon Dance",
        /*num           */  349,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Raises the user's Attack and Speed by 1 stage.",
        /*shortDesc     */  "Raises the user's Attack and Speed by 1.",
        /*id            */  "dragondance",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tSelf,
        /*type          */  Dragon,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(protect, pulse, mirror, distance);
        PokemonMoves.Add(new Move(
        /*name          */ "Dragon Pulse",
        /*num           */  406,
        /*accuracy      */  100,
        /*basePower     */  85,
        /*category      */  Special,
        /*desc          */  "No additional effect.",
        /*shortDesc     */  "No additional effect.",
        /*id            */  "dragonpulse",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAny,
        /*type          */  Dragon,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Dragon Rage",
        /*num           */  82,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Special,
        /*desc          */  "Deals 40 HP of damage to the target.",
        /*shortDesc     */  "Always does 40 HP of damage.",
        /*id            */  "dragonrage",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Dragon,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Dragon Rush",
        /*num           */  407,
        /*accuracy      */  75,
        /*basePower     */  100,
        /*category      */  Physical,
        /*desc          */  "Has a 20% chance to flinch the target. Damage doubles and no accuracy check is done" +
        " if the target has used Minimize while active.",
        /*shortDesc     */  "Always does 40 HP of damage.",
        /*id            */  "dragonrush",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Dragon,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Dragon Tail",
        /*num           */  525,
        /*accuracy      */  90,
        /*basePower     */  60,
        /*category      */  Physical,
        /*desc          */  "If both the user and the target have not fainted, the target is forced to switch out" +
        " and be replaced with a random unfainted ally. This effect fails if the target used Ingrain previously," +
        " has the Ability Suction Cups, or this move hit a substitute.",
        /*shortDesc     */  "Forces the target to switch to a random ally.",
        /*id            */  "dragontail",
        /*pp            */  10,
        /*priority      */  -6,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Dragon,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror, heal);
        PokemonMoves.Add(new Move(
        /*name          */ "Draining Kiss",
        /*num           */  577,
        /*accuracy      */  100,
        /*basePower     */  50,
        /*category      */  Special,
        /*desc          */  "The user recovers 3/4 the HP lost by the target, rounded half up. If Big Root is" +
        " held by the user, the HP recovered is 1.3x normal, rounded half down.",
        /*shortDesc     */  "User recovers 75% of the damage dealt.",
        /*id            */  "drainingkiss",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Fairy,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror, punch, heal);
        PokemonMoves.Add(new Move(
        /*name          */ "Drain Punch",
        /*num           */  409,
        /*accuracy      */  100,
        /*basePower     */  75,
        /*category      */  Physical,
        /*desc          */  "The user recovers 1/2 the HP lost by the target, rounded half up. If Big Root is" +
        " held by the user, the HP recovered is 1.3x normal, rounded half down.",
        /*shortDesc     */  "User recovers 50% of the damage dealt.",
        /*id            */  "drainpunch",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Fighting,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(protect, mirror, heal);
        PokemonMoves.Add(new Move(
        /*name          */ "Dream Eater",
        /*num           */  138,
        /*accuracy      */  100,
        /*basePower     */  100,
        /*category      */  Special,
        /*desc          */  "The target is unaffected by this move unless it is asleep. The user recovers 1/2 the" +
        " HP lost by the target, rounded half up. If Big Root is held by the user, the HP recovered is 1.3x" +
        " normal, rounded half down.",
        /*shortDesc     */  "User gains 1/2 HP inflicted. Sleeping target only.",
        /*id            */  "dreameater",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Psychic,
        /*contesttype   */  c_Clever
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror, distance);
        PokemonMoves.Add(new Move(
        /*name          */ "Drill Peck",
        /*num           */  65,
        /*accuracy      */  100,
        /*basePower     */  80,
        /*category      */  Physical,
        /*desc          */  "No additional effect.",
        /*shortDesc     */  "No additional effect.",
        /*id            */  "drillpeck",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAny,
        /*type          */  Flying,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Drill Run",
        /*num           */  529,
        /*accuracy      */  95,
        /*basePower     */  80,
        /*category      */  Physical,
        /*desc          */  "Has a higher chance for a critical hit.",
        /*shortDesc     */  "High critical hit ratio.",
        /*id            */  "drillrun",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Ground,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Dual Chop",
        /*num           */  530,
        /*accuracy      */  90,
        /*basePower     */  40,
        /*category      */  Physical,
        /*desc          */  "Hits twice. If the first hit breaks the target's substitute, it will take damage" +
        " for the second hit.",
        /*shortDesc     */  "Hits 2 times in one turn.",
        /*id            */  "dualchop",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Dragon,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror, punch);
        PokemonMoves.Add(new Move(
        /*name          */ "Dynamic Punch",
        /*num           */  233,
        /*accuracy      */  50,
        /*basePower     */  100,
        /*category      */  Physical,
        /*desc          */  "Has a 100% chance to confuse the target.",
        /*shortDesc     */  "100% chance to confuse the target.",
        /*id            */  "dynamicpunch",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Fighting,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

    }

    private void moves_E()
    {
        flagsAdd(protect, mirror, nonsky);
        PokemonMoves.Add(new Move(
        /*name          */ "Earth Power",
        /*num           */  414,
        /*accuracy      */  100,
        /*basePower     */  90,
        /*category      */  Special,
        /*desc          */  "Has a 10% chance to lower the target's Special Defense by 1 stage.",
        /*shortDesc     */  "10% chance to lower the target's Sp. Def. by 1.",
        /*id            */  "earthpower",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Ground,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(protect, mirror, nonsky);
        PokemonMoves.Add(new Move(
        /*name          */ "Earthquake",
        /*num           */  89,
        /*accuracy      */  100,
        /*basePower     */  100,
        /*category      */  Physical,
        /*desc          */  "Damage doubles if the target is using Dig.",
        /*shortDesc     */  "Hits adjacent Pokemon. Power doubles on Dig.",
        /*id            */  "earthquake",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAllAdjacent,
        /*type          */  Ground,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(protect, mirror, sound, authentic);
        PokemonMoves.Add(new Move(
        /*name          */ "Echoed Voice",
        /*num           */  497,
        /*accuracy      */  100,
        /*basePower     */  40,
        /*category      */  Special,
        /*desc          */  "For every consecutive turn that this move is used by at least one Pokemon, this" +
        " move's power is multiplied by the number of turns to pass, but not more than 5.",
        /*shortDesc     */  "Power increases when used on consecutive turns.",
        /*id            */  "echoedvoice",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(protect, reflectable, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Eerie Impulse",
        /*num           */  598,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Lowers the target's Special Attack by 2 stages.",
        /*shortDesc     */  "Lowers the target's Sp. Atk by 2.",
        /*id            */  "eerieimpulse",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Electric,
        /*contesttype   */  c_Clever
        ));
        flags.Clear();

        flagsAdd(bullet, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Egg Bomb",
        /*num           */  121,
        /*accuracy      */  75,
        /*basePower     */  100,
        /*category      */  Physical,
        /*desc          */  "No additional effect.",
        /*shortDesc     */  "No additional effect.",
        /*id            */  "eggbomb",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(nonsky);
        PokemonMoves.Add(new Move(
        /*name          */ "Electric Terrain",
        /*num           */  604,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "For 5 turns, the terrain becomes Electric Terrain. During the effect, the power"+
        " of Electric-type attacks made by grounded Pokemon is multiplied by 1.5 and grounded Pokemon cannot"+
        " fall asleep; Pokemon already asleep do not wake up. Camouflage transforms the user into an Electric"+
        " type, Nature Power becomes Thunderbolt, and Secret Power has a 30% chance to cause paralysis. Fails"+
        " if the current terrain is Electric Terrain.",
        /*shortDesc     */  "5 turns. Grounded: +Electric power, can't sleep.",
        /*id            */  "electricterrain",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAll,
        /*type          */  Electric,
        /*contesttype   */  c_Clever
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Electrify",
        /*num           */  582,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Causes the target's move to become Electric type this turn. Fails if the target"+
        " already moved this turn.",
        /*shortDesc     */  "Changes the target's move to Electric this turn.",
        /*id            */  "electrify",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Electric,
        /*contesttype   */  c_Clever
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Electro Ball",
        /*num           */  486,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Special,
        /*desc          */  "The power of this move depends on (user's current Speed / target's current Speed),"+
        " rounded down. Power is equal to 150 if the result is 4 or more, 120 if 3, 80 if 2, 60 if 1, 40 if less"+
        " than 1.",
        /*shortDesc     */  "More power the faster the user is than the target.",
        /*id            */  "electroball",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Electric,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Electroweb",
        /*num           */  527,
        /*accuracy      */  95,
        /*basePower     */  55,
        /*category      */  Special,
        /*desc          */  "Has a 100% chance to lower the target's Speed by 1 stage.",
        /*shortDesc     */  "100% chance to lower the foe(s) Speed by 1.",
        /*id            */  "electroball",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAllAdjacentFoes,
        /*type          */  Electric,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(protect, reflectable, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Embargo",
        /*num           */  373,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "For 5 turns, the target's held item has no effect. An item's effect of causing"+
        " forme changes is unaffected, but any other effects from such items are negated. During the effect,"+
        " Fling and Natural Gift are prevented from being used by the target. Items thrown at the target with"+
        " Fling will still activate for it. If the target uses Baton Pass, the replacement will remain unable"+
        " to use items.",
        /*shortDesc     */  "For 5 turns, the target can't use any items.",
        /*id            */  "embargo",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Dark,
        /*contesttype   */  c_Clever
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Ember",
        /*num           */  52,
        /*accuracy      */  100,
        /*basePower     */  40,
        /*category      */  Special,
        /*desc          */  "Has a 10% chance to burn the target.",
        /*shortDesc     */  "10% chance to burn the target.",
        /*id            */  "ember",
        /*pp            */  25,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Fire,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(protect, reflectable, mirror, authentic);
        PokemonMoves.Add(new Move(
        /*name          */ "Encore",
        /*num           */  227,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "For 3 turns, the target is forced to repeat its last move used. If the affected"+
        " move runs out of PP, the effect ends. Fails if the target is already under this effect, if it has"+
        " not made a move, if the move has 0 PP, or if the move is Encore, Mimic, Mirror Move, Sketch, Struggle,"+
        " or Transform.",
        /*shortDesc     */  "The target repeats its last move for 3 turns.",
        /*id            */  "encore",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Endeavor",
        /*num           */  283,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Physical,
        /*desc          */  "Deals damage to the target equal to (target's current HP - user's current HP)."+
        " The target is unaffected if its current HP is less than or equal to the user's current HP.",
        /*shortDesc     */  "The target repeats its last move for 3 turns.",
        /*id            */  "endeavor",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd();
        PokemonMoves.Add(new Move(
        /*name          */ "Endure",
        /*num           */  203,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "The user will survive attacks made by other Pokemon during this turn with at "+
        "least 1 HP. This move has a 1/X chance of being successful, where X starts at 1 and triples each"+
        " time this move is successfully used. X resets to 1 if this move fails or if the user's last move"+
        " used is not Detect, Endure, King's Shield, Protect, Quick Guard, Spiky Shield, or Wide Guard. Fails"+
        " if the user moves last this turn.",
        /*shortDesc     */  "The user survives the next hit with at least 1 HP.",
        /*id            */  "endure",
        /*pp            */  10,
        /*priority      */  4,
        /*flags         */  flags,
        /*target        */  tSelf,
        /*type          */  Normal,
        /*contesttype   */  c_Tough
        ));
        flags.Clear();

        flagsAdd(bullet, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Energy Ball",
        /*num           */  412,
        /*accuracy      */  100,
        /*basePower     */  90,
        /*category      */  Special,
        /*desc          */  "Has a 10% chance to lower the target's Special Defense by 1 stage.",
        /*shortDesc     */  "10% chance to lower the target's Sp. Def. by 1.",
        /*id            */  "energyball",
        /*pp            */  10,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Grass,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(protect, reflectable, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Entrainment",
        /*num           */  494,
        /*accuracy      */  100,
        /*basePower     */  0,
        /*category      */  Status,
        /*desc          */  "Causes the target's Ability to become the same as the user's. Fails if the target's"+
        " Ability is Multitype, Stance Change, Truant, or the same Ability as the user, or if the user's Ability"+
        " is Flower Gift, Forecast, Illusion, Imposter, Multitype, Stance Change, Trace, or Zen Mode.",
        /*shortDesc     */  "The target's Ability changes to match the user's.",
        /*id            */  "entrainment",
        /*pp            */  15,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Eruption",
        /*num           */  284,
        /*accuracy      */  100,
        /*basePower     */  150,
        /*category      */  Special,
        /*desc          */  "Power is equal to (user's current HP * 150 / user's maximum HP), rounded down, but"+
        " not less than 1.",
        /*shortDesc     */  "Less power as user's HP decreases. Hits foe(s).",
        /*id            */  "eruption",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAllAdjacentFoes,
        /*type          */  Fire,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Explosion",
        /*num           */  153,
        /*accuracy      */  100,
        /*basePower     */  250,
        /*category      */  Physical,
        /*desc          */  "The user faints after using this move, even if this move fails for having no target."+
        " This move is prevented from executing if any active Pokemon has the Ability Damp.",
        /*shortDesc     */  "Hits adjacent Pokemon. The user faints.",
        /*id            */  "explosion",
        /*pp            */  5,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tAllAdjacent,
        /*type          */  Normal,
        /*contesttype   */  c_Beautiful
        ));
        flags.Clear();

        flagsAdd(protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Extrasensory",
        /*num           */  326,
        /*accuracy      */  100,
        /*basePower     */  80,
        /*category      */  Special,
        /*desc          */  "Has a 10% chance to flinch the target.",
        /*shortDesc     */  "10% chance to flinch the target.",
        /*id            */  "extrasensory",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Psychic,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Extreme Speed",
        /*num           */  245,
        /*accuracy      */  100,
        /*basePower     */  80,
        /*category      */  Physical,
        /*desc          */  "No additional effect.",
        /*shortDesc     */  "Nearly always goes first.",
        /*id            */  "extremespeed",
        /*pp            */  5,
        /*priority      */  2,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Cool
        ));
        flags.Clear();

    }

    private void moves_F()
    {
        flagsAdd(contact, protect, mirror);
        PokemonMoves.Add(new Move(
        /*name          */ "Facade",
        /*num           */  263,
        /*accuracy      */  100,
        /*basePower     */  70,
        /*category      */  Physical,
        /*desc          */  "Power doubles if the user is burned, paralyzed, or poisoned. The physical damage"+
        " halving effect from the user's burn is ignored.",
        /*shortDesc     */  "Power doubles if user is burn/poison/paralyzed.",
        /*id            */  "facade",
        /*pp            */  20,
        /*priority      */  0,
        /*flags         */  flags,
        /*target        */  tNormal,
        /*type          */  Normal,
        /*contesttype   */  c_Cute
        ));
        flags.Clear();
    }

    /// <summary>
    /// Takes in an infinite number of strings that are associated with the flags to add them all in one method
    /// </summary>
    /// <param name="s">flag strings</param>
    private void flagsAdd(params string[] s)
    {
        if (s.Length == 0)
        {
            return;
        }

        for (int i = 0; i < s.Length; i++)
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
