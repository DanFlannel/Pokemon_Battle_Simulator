using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pokedex : MonoBehaviour {

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

    private string overgrow = "Overgrow";
    private string chlorophyll = "Chlorophyll";
    private string blaze = "Blaze";
    private string solarPower = "Solar Power";


    private List<PokemonClass> pokeDex = new List<PokemonClass>();
    public List<string> Abilities = new List<string>();
    public List<string> EggGroups = new List<string>();

    // Use this for initialization
    void Start () {
	
	}
	

    public void Generation1()
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
            /*Number    */  5,
            /*Name      */  "Charmeleon",
            /*Type 1    */  fire,
            /*Type 2    */  null,
            /*MGender%  */  0.875f,
            /*HP        */  58,
            /*Attack    */  64,
            /*Defense   */  58,
            /*SpAttack  */  80,
            /*SpDefense */  65,
            /*Speed     */  80,
            /*Abilities */  Abilities,
            /*Height    */  1.1f,
            /*Weight    */  19f,
            /*Evolution */  "Charizard",
            /*Evo Level */  36,
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
        public string Name;
        public string Type1;
        public string Type2;

        public float GenderRatioM;
        public float GenderRatioF;

        public float HP;
        public float Attack;
        public float Defense;
        public float SpAttack;
        public float SpDefense;
        public float Speed;

        public List<string> Abilities;

        public float HeightM;
        public float WeightKg;

        public string Evo;
        public int EvoLevel;

        public List<string> EggGroups;


        public PokemonClass(int m_num, string m_name, string m_type1, string m_type2, float m_ratio_M, float m_hp, float m_atk, float m_def, float m_spa, float m_spd, float m_spe, List<string> m_Abilities, float m_height, float m_weight, string m_evo, int m_evoLevel, List<string> m_eggGroups)
        {
            Num = m_num;
            Name = m_name;
            Type1 = m_type1;
            Type2 = m_type2;
            GenderRatioM = m_ratio_M;
            GenderRatioF = 1 - m_ratio_M;

            HP = m_hp;
            Attack = m_atk;
            Defense = m_def;
            SpDefense = m_spd;
            SpAttack = m_spa;
            Speed = m_spd;

            Abilities = m_Abilities;

            HeightM = m_height;
            WeightKg = m_weight;

            Evo = m_evo;
            EvoLevel = m_evoLevel;


            EggGroups = m_eggGroups;
        }
    }
}
