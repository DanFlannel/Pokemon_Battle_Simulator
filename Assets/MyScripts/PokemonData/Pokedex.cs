using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pokedex : MonoBehaviour
{

    private string normal = "Normal";
    private string fire = "Fire";
    private string water = "Water";
    private string electric = "Electric";
    private string grass = "Grass";
    private string ice = "Ice";
    private string fighting = "Fighting";
    private string poison = "Poison";
    private string ground = "Ground";
    private string flying = "Flying";
    private string psychic = "Psychic";
    private string bug = "Bug";
    private string rock = "Rock";
    private string ghost = "Ghost";
    private string dragon = "Dragon";
    private string empty = "Empty";

    private string monster = "Monster";
    private string water_1 = "Water 1";

    private string overgrow = "Overgrow";
    private string chlorophyll = "Chlorophyll";
    private string blaze = "Blaze";
    private string solarPower = "Solar Power";
    private string torrent = "Torrent";
    private string rainDish = "RainDish";


    private List<PokemonClass> pokeDex = new List<PokemonClass>();
    public List<string> Abilities = new List<string>();
    public List<string> EggGroups = new List<string>();

    // Use this for initialization
    void Start()
    {
        //Generation 1
        Generation1_1_25();
        //Generation1_26_50();
        //Generation_1_51_75();
        //Generation1_76_100();
        //Generation1_101_125();
        //Generation1_126_151();
    }


    
    /// <summary>
    /// Number      : Done
    /// Name        : Done
    /// Stats       : Done
    /// Abilities   :
    /// Height      :
    /// Weight      :
    /// Evolution   :
    /// Evo Lvl     :
    /// Egg Group   :
    /// </summary>
    public void Generation1_1_25()
    {
        abilitiesAdd(overgrow, chlorophyll);
        eggGroupAdd(monster, grass);
        pokeDex.Add(new PokemonClass(
            /*Number    */  1,
            /*Name      */  "Bulbasaur",
            /*Type 1    */  grass,
            /*Type 2    */  poison,
            /*MGender%  */  0.875f,
            /*HP        */  45,
            /*Attack    */  49,
            /*Defense   */  49,
            /*SpAttack  */  65,
            /*SpDefense */  65,
            /*Speed     */  45,
            /*Abilities */  Abilities,
            /*Height    */  0.7f,
            /*Weight    */  6.9f,
            /*Evolution */  "Ivysaur",
            /*Evo Level */  13,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(overgrow, chlorophyll);
        eggGroupAdd(monster, grass);
        pokeDex.Add(new PokemonClass(
            /*Number    */  2,
            /*Name      */  "Ivysaur",
            /*Type 1    */  grass,
            /*Type 2    */  poison,
            /*MGender%  */  0.875f,
            /*HP        */  60,
            /*Attack    */  62,
            /*Defense   */  63,
            /*SpAttack  */  80,
            /*SpDefense */  80,
            /*Speed     */  60,
            /*Abilities */  Abilities,
            /*Height    */  1f,
            /*Weight    */  13f,
            /*Evolution */  "Venusaur",
            /*Evo Level */  32,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(overgrow, chlorophyll);
        eggGroupAdd(monster, grass);
        pokeDex.Add(new PokemonClass(
            /*Number    */  3,
            /*Name      */  "Venusaur",
            /*Type 1    */  grass,
            /*Type 2    */  poison,
            /*MGender%  */  0.875f,
            /*HP        */  80,
            /*Attack    */  82,
            /*Defense   */  83,
            /*SpAttack  */  100,
            /*SpDefense */  100,
            /*Speed     */  80,
            /*Abilities */  Abilities,
            /*Height    */  2f,
            /*Weight    */  13f,
            /*Evolution */  "",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(blaze, solarPower);
        eggGroupAdd(monster, dragon);
        pokeDex.Add(new PokemonClass(
            /*Number    */  4,
            /*Name      */  "Charmander",
            /*Type 1    */  fire,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            /*HP        */  39,
            /*Attack    */  52,
            /*Defense   */  43,
            /*SpAttack  */  60,
            /*SpDefense */  50,
            /*Speed     */  64,
            /*Abilities */  Abilities,
            /*Height    */  .6f,
            /*Weight    */  8.5f,
            /*Evolution */  "Charmeleon",
            /*Evo Level */  16,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(blaze, solarPower);
        eggGroupAdd(monster, dragon);
        pokeDex.Add(new PokemonClass(
            /*Number    */  6,
            /*Name      */  "Charizard",
            /*Type 1    */  fire,
            /*Type 2    */  flying,
            /*MGender%  */  0.875f,
            /*HP        */  78,
            /*Attack    */  84,
            /*Defense   */  78,
            /*SpAttack  */  109,
            /*SpDefense */  85,
            /*Speed     */  100,
            /*Abilities */  Abilities,
            /*Height    */  1.7f,
            /*Weight    */  90.5f,
            /*Evolution */  "",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, water_1);
        pokeDex.Add(new PokemonClass(
            /*Number    */  7,
            /*Name      */  "Squirtle",
            /*Type 1    */  water,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            /*HP        */  44,
            /*Attack    */  44,
            /*Defense   */  65,
            /*SpAttack  */  50,
            /*SpDefense */  64,
            /*Speed     */  43,
            /*Abilities */  Abilities,
            /*Height    */  0.5f,
            /*Weight    */  9f,
            /*Evolution */  "Wartortle",
            /*Evo Level */  16,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, water_1);
        pokeDex.Add(new PokemonClass(
            /*Number    */  8,
            /*Name      */  "Wartortle",
            /*Type 1    */  water,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
             59, 63, 80, 65, 80, 58,
            /*Abilities */  Abilities,
            /*Height    */  0.5f,
            /*Weight    */  9f,
            /*Evolution */  "Blastoise",
            /*Evo Level */  36,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  9,
            /*Name      */  "Blastoise",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            79, 83, 100, 85, 105, 78,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  10,
            /*Name      */  "Caterpie",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            45, 30, 35, 20, 20, 45,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Metapod",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  11,
            /*Name      */  "Metapod",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            50, 20, 55, 25, 25, 30,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Butterfree",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  12,
            /*Name      */  "Butterfree",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
             60, 45, 50, 90, 80, 70,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  13,
            /*Name      */  "Weedle",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            40, 35, 30, 20, 20, 50,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Kakuna",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  14,
            /*Name      */  "Kakuna",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            45, 25, 50, 25, 25, 35,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Beedrill",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();


        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  15,
            /*Name      */  "Beedrill",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();


        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  16,
            /*Name      */  "Pidgey",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            40, 45, 40, 35, 35, 56,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Pidgeotto",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();


        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  17,
            /*Name      */  "Pidgeotto",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            63, 60, 55, 50, 50, 71,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Pidgeot",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();


        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  18,
            /*Name      */  "Pidgeot",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            83, 80, 75, 70, 70, 101,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();


        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  19,
            /*Name      */  "Rattata",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
             30, 56, 35, 25, 35, 72,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Raticate",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();


        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  20,
            /*Name      */  "Raticate",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            55, 81, 60, 50, 70, 97,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();


        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  21,
            /*Name      */  "Spearow",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            40, 60, 30, 31, 31, 70,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Fearow",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();


        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  22,
            /*Name      */  "Fearow",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            65, 90, 65, 61, 61, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();


        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  23,
            /*Name      */  "Ekans",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            35, 60, 44, 40, 54, 55,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Arbok",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();


        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  24,
            /*Name      */  "Arbok",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            60, 85, 69, 65, 79, 80,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();


        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  25,
            /*Name      */  "Pikachu",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            35, 55, 30, 50, 50, 90,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();
    }
    
    /// <summary>
    /// Number      : Done
    /// Name        : Done
    /// Stats       :
    /// Abilities   :
    /// Height      :
    /// Weight      :
    /// Evolution   :
    /// Evo Lvl     :
    /// Egg Group   :
    /// </summary>
    public void Generation1_26_50()
    {
        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  26,
            /*Name      */  "Raichu",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();


        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  27,
            /*Name      */  "Sandshrew",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Sandslash",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();


        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  28,
            /*Name      */  "Sandslash",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();


        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  29,
            /*Name      */  "Nidoran_F",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Nidorina",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();


        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  30,
            /*Name      */  "Nidorina",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Nidoqueen",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();


        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  31,
            /*Name      */  "Nidoqueen",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();


        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  32,
            /*Name      */  "Nidoran_M",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Nidorino",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  33,
            /*Name      */  "Nidorino",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Nidoking",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  34,
            /*Name      */  "Nidoking",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  35,
            /*Name      */  "Clefairy",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Clefable",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  36,
            /*Name      */  "Clefable",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  37,
            /*Name      */  "Vulpix",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Ninetales",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  38,
            /*Name      */  "Ninetales",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  39,
            /*Name      */  "Jigglypuff",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Wigglytuff",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  40,
            /*Name      */  "Wigglytuff",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  41,
            /*Name      */  "Zubat",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Golbat",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  42,
            /*Name      */  "Golbat",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  43,
            /*Name      */  "Oddish",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Gloom",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  44,
            /*Name      */  "Gloom",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Vileplume",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  45,
            /*Name      */  "Vileplume",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  46,
            /*Name      */  "Paras",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Parasect",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  47,
            /*Name      */  "Parasect",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  48,
            /*Name      */  "Venonat",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Venomoth",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  49,
            /*Name      */  "Venomoth",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  50,
            /*Name      */  "Diglett",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Dugtrio",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();
    }
    
    /// <summary>
    /// Number      : Done
    /// Name        : Done
    /// Stats       :
    /// Abilities   :
    /// Height      :
    /// Weight      :
    /// Evolution   :
    /// Evo Lvl     :
    /// Egg Group   :
    /// </summary>
    public void Generation_1_51_75()
    {

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  51,
            /*Name      */  "Dugtrio",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  52,
            /*Name      */  "Meowth",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Persian",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  53,
            /*Name      */  "Persian",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  54,
            /*Name      */  "Psyduck",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Golduck",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  55,
            /*Name      */  "Golduck",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  56,
            /*Name      */  "Meowth",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Persian",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  57,
            /*Name      */  "Persian",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  58,
            /*Name      */  "Growlithe",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Arcanine",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  59,
            /*Name      */  "Arcanine",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  60,
            /*Name      */  "Poliwag",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Poliwhirl",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  61,
            /*Name      */  "Poliwhirl",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Poliwrath",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  62,
            /*Name      */  "Poliwrath",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  64,
            /*Name      */  "Abra",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Kadabra",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  65,
            /*Name      */  "Kadabra",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Alakazam",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  66,
            /*Name      */  "Alakazam",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  67,
            /*Name      */  "Machop",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Machoke",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  68,
            /*Name      */  "Machoke",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  69,
            /*Name      */  "Bellsprout",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Weepinbell",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  70,
            /*Name      */  "Weepinbell",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Victreebel",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  71,
            /*Name      */  "Victreebel",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  72,
            /*Name      */  "Tentacool",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Tentacruel",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  73,
            /*Name      */  "Tentacruel",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  74,
            /*Name      */  "Geodude",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Graveler",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  75,
            /*Name      */  "Graveler",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Golem",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

    }
    
    /// <summary>
    /// Number      : Done
    /// Name        :
    /// Stats       :
    /// Abilities   :
    /// Height      :
    /// Weight      :
    /// Evolution   :
    /// Evo Lvl     :
    /// Egg Group   :
    /// </summary>
    public void Generation1_76_100()
    {

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  76,
            /*Name      */  "Golem",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  77,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  78,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  79,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  80,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  81,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  82,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  83,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  84,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  85,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  86,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  87,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  88,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  89,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  90,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  91,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  92,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  93,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  94,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  95,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  96,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  97,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  98,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  99,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  100,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

    }
    
    /// <summary>
    /// Number      : Done
    /// Name        :
    /// Stats       :
    /// Abilities   :
    /// Height      :
    /// Weight      :
    /// Evolution   :
    /// Evo Lvl     :
    /// Egg Group   :
    /// </summary>
    public void Generation1_101_125()
    {

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  100,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  102,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  103,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  104,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  105,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  106,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  107,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  108,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  109,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  110,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  111,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  112,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  113,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  114,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  115,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  116,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  117,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  118,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  119,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  120,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  121,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  122,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  123,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  124,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  125,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

    }
    
    /// <summary>
    /// Number      : Done
    /// Name        :
    /// Stats       :
    /// Abilities   :
    /// Height      :
    /// Weight      :
    /// Evolution   :
    /// Evo Lvl     :
    /// Egg Group   :
    /// </summary>
    public void Generation1_126_151()
    {

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  126,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  127,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  128,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  129,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  130,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  131,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  132,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  133,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  134,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  135,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  136,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  137,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  138,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  139,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  140,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  141,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  142,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  143,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  144,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  145,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  146,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  147,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  148,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  149,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  150,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  151,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();

    }



    public void template()
    {
        abilitiesAdd(torrent, rainDish);
        eggGroupAdd(monster, monster);
        pokeDex.Add(new PokemonClass(
            /*Number    */  100,
            /*Name      */  "Empty",
            /*Type 1    */  null,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            100, 100, 100, 100, 100, 100,
            /*Abilities */  Abilities,
            /*Height    */  100f,
            /*Weight    */  100f,
            /*Evolution */  "Empty",
            /*Evo Level */  0,
            /*EggGroup  */  EggGroups
            ));
        Abilities.Clear();
        EggGroups.Clear();
    }

    private void abilitiesAdd(params string[] s)
    {
        if (s.Length == 0)
        {
            return;
        }

        for (int i = 0; i < s.Length; i++)
        {
            Abilities.Add(s[i]);
        }
    }


    private void eggGroupAdd(params string[] s)
    {
        if (s.Length == 0)
        {
            return;
        }

        for (int i = 0; i < s.Length; i++)
        {
            EggGroups.Add(s[i]);
        }
    }

    public class PokemonClass
    {
        public int Num;
        public string name;
        public string type1;
        public string type2;

        public float GenderRatioM;
        public float GenderRatioF;

        public float hp;
        public float attack;
        public float defense;
        public float specialAttack;
        public float specialDefense;
        public float speed;

        public List<string> Abilities;

        public float HeightM;
        public float WeightKg;

        public string Evo;
        public int EvoLevel;

        public List<string> EggGroups;


        public PokemonClass(int m_num, string m_name, string m_type1, string m_type2, float m_ratio_M, float m_hp, float m_atk, float m_def, float m_spa, float m_spd, float m_spe, List<string> m_Abilities, float m_height, float m_weight, string m_evo, int m_evoLevel, List<string> m_eggGroups)
        {
            Num = m_num;
            name = m_name;
            type1 = m_type1;
            type2 = m_type2;
            GenderRatioM = m_ratio_M;
            GenderRatioF = 1 - m_ratio_M;

            hp = m_hp;
            attack = m_atk;
            defense = m_def;
            specialDefense = m_spd;
            specialAttack = m_spa;
            speed = m_spd;

            Abilities = m_Abilities;

            HeightM = m_height;
            WeightKg = m_weight;

            Evo = m_evo;
            EvoLevel = m_evoLevel;


            EggGroups = m_eggGroups;
        }
    }
}
