using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PokemonDamageMultipliers : MonoBehaviour
{

    public List<pokemon_dmg_multipliers> master_list = new List<pokemon_dmg_multipliers>();

    #region List of various arrays of damage multipliers
    private dmgMult grass;
    private dmgMult grass_poison;
    private dmgMult grass_psychic;

    private dmgMult fire;
    private dmgMult fire_flying;

    private dmgMult water;
    private dmgMult water_poison;
    private dmgMult water_psychic;
    private dmgMult water_ice;
    private dmgMult water_flying;

    private dmgMult dragon;
    private dmgMult dragon_flying;

    private dmgMult fighting;

    private dmgMult bug;
    private dmgMult bug_flying;
    private dmgMult bug_poison;
    private dmgMult bug_grass;

    private dmgMult normal;
    //private dmgMult normal_fairy;
    private dmgMult normal_flying;

    private dmgMult electric;
    private dmgMult electric_steel;
    private dmgMult electric_flying;

    private dmgMult ground;
    private dmgMult ground_rock;

    private dmgMult rock_ground;
    private dmgMult rock_water;
    private dmgMult rock_flying;

    private dmgMult poison;
    private dmgMult poison_ground;
    private dmgMult poison_flying;

    private dmgMult psychic;
    //private dmgMult psychic_fairy;

    //private dmgMult fairy;

    private dmgMult ghost_poison;

    private dmgMult ice_psychic;
    private dmgMult ice_flying;
    #endregion

    // Use this for initialization
    void Awake()
    {
        damageMultipliers();
        Gen1_DmgMultipliers();
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*Fairy*/
    private void damageMultipliers()
    {
        grass_poison = new dmgMult(
            /*Normal*/  1f,
            /*Fighting*/.5f,
            /*Flying*/2f,
            /*Poison*/1f,
            /*Ground*/1f,
            /*Rock*/1f,
            /*Bug*/1f,
            /*Ghost*/1f,
            /*Steel*/1f,
            /*Fire*/ 2f,
            /*Water*/.5f,
            /*Grass*/.25f,
            /*Electric*/.5f,
            /*Psychic*/2f,
            /*Ice*/2f,
            /*Dragon*/1f,
            /*Dark*/1f,
            /*Fairy*/.5f);

        fire = new dmgMult(
            /*Normal*/  1f,
            /*Fighting*/1f,
            /*Flying*/1f,
            /*Poison*/1f,
            /*Ground*/2f,
            /*Rock*/2f,
            /*Bug*/.5f,
            /*Ghost*/1f,
            /*Steel*/.5f,
            /*Fire*/ .5f,
            /*Water*/2f,
            /*Grass*/.5f,
            /*Electric*/1f,
            /*Psychic*/1f,
            /*Ice*/.5f,
            /*Dragon*/ 1f,
            /*Dark*/1f,
            /*Fairy*/ .5f);

        fire_flying = new dmgMult(
            /*Normal*/  1f,
            /*Fighting*/.5f,
            /*Flying*/1f,
            /*Poison*/1f,
            /*Ground*/0f,
            /*Rock*/4f,
            /*Bug*/.25f,
            /*Ghost*/1f,
            /*Steel*/.5f,
            /*Fire*/.5f,
            /*Water*/2f,
            /*Grass*/ .25f,
            /*Electric*/2f,
            /*Psychic*/ 1f,
            /*Ice*/ 1f,
            /*Dragon*/1f,
            /*Dark*/ 1f,
            /*Fairy*/.5f);

        water = new dmgMult(
            /*Normal*/  1f,
            /*Fighting*/1f,
            /*Flying*/1f,
            /*Poison*/1f,
            /*Ground*/1f,
            /*Rock*/1f,
            /*Bug*/1f,
            /*Ghost*/1f,
            /*Steel*/.5f,
            /*Fire*/ .5f,
            /*Water*/ .5f,
            /*Grass*/ 2f,
            /*Electric*/2f,
            /*Psychic*/ 1f,
            /*Ice*/.5f, 
            /*Dragon*/1f,
            /*Dark*/1f,
            /*Fairy*/1f);

        bug = new dmgMult(
            /*Normal*/  1f,
            /*Fighting*/.5f,
            /*Flying*/2f,
            /*Poison*/1f,
            /*Ground*/.5f,
            /*Rock*/2f,
            /*Bug*/1f,
            /*Ghost*/1f,
            /*Steel*/1f,
            /*Fire*/ 2f,
            /*Water*/ 1f,
            /*Grass*/ .5f,
            /*Electric*/ 1f,
            /*Psychic*/ 1f,
            /*Ice*/ 1f,
            /*Dragon*/1f,
            /*Dark*/1f,
            /*Fairy*/1f);

        bug_flying = new dmgMult(
            /*Normal*/  1f,
            /*Fighting*/.25f,
            /*Flying*/2f,
            /*Poison*/1f,
            /*Ground*/0f,
            /*Rock*/4f,
            /*Bug*/.5f,
            /*Ghost*/1f,
            /*Steel*/1f,
            /*Fire*/ 2f,
            /*Water*/1f,
            /*Grass*/ .25f,
            /*Electric*/ 2f,
            /*Psychic*/ 1f,
            /*Ice*/2f,
            /*Dragon*/1f,
            /*Dark*/1f,
            /*Fairy*/1f);

        bug_poison = new dmgMult(
            /*Normal*/  1f,
            /*Fighting*/.25f,
            /*Flying*/2f,
            /*Poison*/.5f,
            /*Ground*/1f,
            /*Rock*/2f,
            /*Bug*/.5f,
            /*Ghost*/1f,
            /*Steel*/1f,
            /*Fire*/ 2f,
            /*Water*/1f,
            /*Grass*/ .25f,
            /*Electric*/ 1f,
            /*Psychic*/ 2f,
            /*Ice*/1f,
            /*Dragon*/1f,
            /*Dark*/1f,
            /*Fairy*/.5f);

        normal_flying = new dmgMult(
            /*Normal*/  1f,
            /*Fighting*/1f,
            /*Flying*/1f,
            /*Poison*/1f,
            /*Ground*/0f,
            /*Rock*/2f,
            /*Bug*/.5f,
            /*Ghost*/0f,
            /*Steel*/1f,
            /*Fire*/1f,
            /*Water*/1f,
            /*Grass*/ .5f,
            /*Electric*/ 2f,
            /*Psychic*/ 1f,
            /*Ice*/2f,
            /*Dragon*/1f,
            /*Dark*/ 1f,
            /*Fairy*/1f);

        normal = new dmgMult(
            /*Normal*/  1f,
            /*Fighting*/2f,
            /*Flying*/1f,
            /*Poison*/1f,
            /*Ground*/1f,
            /*Rock*/1f,
            /*Bug*/1f,
            /*Ghost*/0f,
            /*Steel*/ 1f,
            /*Fire*/ 1f,
            /*Water*/ 1f,
            /*Grass*/ 1f,
            /*Electric*/ 1f,
            /*Psychic*/ 1f,
            /*Ice*/1f,
            /*Dragon*/ 1f,
            /*Dark*/1f,
            /*Fairy*/1f);

        poison = new dmgMult(
            /*Normal*/  1f,
            /*Fighting*/.5f,
            /*Flying*/1f,
            /*Poison*/.5f,
            /*Ground*/2f,
            /*Rock*/1f,
            /*Bug*/.5f,
            /*Ghost*/1f,
            /*Steel*/1f,
            /*Fire*/1f,
            /*Water*/1f,
            /*Grass*/ .5f,
            /*Electric*/ 1f,
            /*Psychic*/ 2f,
            /*Ice*/1f,
            /*Dragon*/1f,
            /*Dark*/ 1f,
            /*Fairy*/ .5f);

        electric = new dmgMult(
            /*Normal*/  1f,
            /*Fighting*/1f,
            /*Flying*/.5f,
            /*Poison*/1f,
            /*Ground*/2f,
            /*Rock*/1f,
            /*Bug*/1f,
            /*Ghost*/1f,
            /*Steel*/.5f,
            /*Fire*/1f,
            /*Water*/ 1f,
            /*Grass*/ 1f,
            /*Electric*/ .5f,
            /*Psychic*/ 1f,
            /*Ice*/ 1f,
            /*Dragon*/1f,
            /*Dark*/ 1f,
            /*Fairy*/ 1f);

        ground = new dmgMult(
            /*Normal*/  1f,
            /*Fighting*/1f,
            /*Flying*/1f,
            /*Poison*/.5f,
            /*Ground*/1f,
            /*Rock*/.5f,
            /*Bug*/1f,
            /*Ghost*/1f,
            /*Steel*/ 1f,
            /*Fire*/1f,
            /*Water*/2f,
            /*Grass*/ 2f,
            /*Electric*/ 0f,
            /*Psychic*/ 1f,
            /*Ice*/2f,
            /*Dragon*/1f,
            /*Dark*/ 1f,
            /*Fairy*/ 1f);

        poison_ground = new dmgMult(
            /*Normal*/  1f,
            /*Fighting*/.5f,
            /*Flying*/1f,
            /*Poison*/.25f,
            /*Ground*/2f,
            /*Rock*/.5f,
            /*Bug*/.5f,
            /*Ghost*/1f,
            /*Steel*/1f,
            /*Fire*/ 1f,
            /*Water*/2f,
            /*Grass*/ 1f,
            /*Electric*/ 0f,
            /*Psychic*/2f,
            /*Ice*/ 2f,
            /*Dragon*/1f,
            /*Dark*/ 1f,
            /*Fairy*/ .5f);

        fairy = new dmgMult(
            /*Normal*/  1f,
            /*Fighting*/.5f,
            /*Flying*/1f,
            /*Poison*/2f,
            /*Ground*/1f,
            /*Rock*/1f,
            /*Bug*/.5f,
            /*Ghost*/1f,
            /*Steel*/ 2f,
            /*Fire*/ 1f,
            /*Water*/1f,
            /*Grass*/ 1f,
            /*Electric*/ 1f,
            /*Psychic*/ 1f,
            /*Ice*/ 1f,
            /*Dragon*/ 0f,
            /*Dark*/ .5f,
            /*Fairy*/ 1f);

        normal_fairy = new dmgMult(
            /*Normal*/  1f,
            /*Fighting*/1f,
            /*Flying*/1f,
            /*Poison*/2f,
            /*Ground*/1f,
            /*Rock*/1f,
            /*Bug*/.5f,
            /*Ghost*/0f,
            /*Steel*/2f,
            /*Fire*/1f,
            /*Water*/1f,
            /*Grass*/ 1f,
            /*Electric*/ 1f,
            /*Psychic*/ 1f,
            /*Ice*/1f,
            /*Dragon*/0f,
            /*Dark*/.5f,
            /*Fairy*/ 1f);

        poison_flying = new dmgMult(
            /*Normal*/  1f,
            /*Fighting*/.25f,
            /*Flying*/1f,
            /*Poison*/.5f,
            /*Ground*/0f,
            /*Rock*/2f,
            /*Bug*/.25f,
            /*Ghost*/1f,
            /*Steel*/1f,
            /*Fire*/1f,
            /*Water*/1f,
            /*Grass*/ .25f,
            /*Electric*/ 2f,
            /*Psychic*/ 2f,
            /*Ice*/2f,
            /*Dragon*/1f,
            /*Dark*/1f,
            /*Fairy*/ .5f);

        bug_grass = new dmgMult(
            /*Normal*/  1f,
            /*Fighting*/.5f,
            /*Flying*/4f,
            /*Poison*/2f,
            /*Ground*/.25f,
            /*Rock*/2f,
            /*Bug*/2f,
            /*Ghost*/1f,
            /*Steel*/ 1f,
            /*Fire*/ 4f,
            /*Water*/ .5f,
            /*Grass*/ .25f,
            /*Electric*/ .5f,
            /*Psychic*/ 1f,
            /*Ice*/2f,
            /*Dragon*/1f,
            /*Dark*/ 1f,
            /*Fairy*/1f);

        fighting = new dmgMult(
            /*Normal*/1f,
            /*Fighting*/1f,
            /*Flying*/2f,
            /*Poison*/1f,
            /*Ground*/1f,
            /*Rock*/.5f,
            /*Bug*/.5f,
            /*Ghost*/1f,
            /*Steel*/1f,
            /*Fire*/ 1f,
            /*Water*/ 1f,
            /*Grass*/ 1f,
            /*Electric*/ 1f,
            /*Psychic*/ 2f,
            /*Ice*/ 1f,
            /*Dragon*/1f,
            /*Dark*/.5f,
            /*Fairy*/2f);

        psychic = new dmgMult(
            /*Normal*/1f,
            /*Fighting*/.5f,
            /*Flying*/1f,
            /*Poison*/1f,
            /*Ground*/1f,
            /*Rock*/1f,
            /*Bug*/2f,
            /*Ghost*/2f,
            /*Steel*/1f,
            /*Fire*/1f,
            /*Water*/1f,
            /*Grass*/ 1f,
            /*Electric*/ 1f,
            /*Psychic*/ .5f,
            /*Ice*/ 1f,
            /*Dragon*/1f,
            /*Dark*/ 2f,
            /*Fairy*/1f);

        water_poison = new dmgMult(
            /*Normal*/1f,
            /*Fighting*/.5f,
            /*Flying*/1f,
            /*Poison*/.5f,
            /*Ground*/2f,
            /*Rock*/1f,
            /*Bug*/.5f,
            /*Ghost*/1f,
            /*Steel*/.5f,
            /*Fire*/ .5f,
            /*Water*/.5f,
            /*Grass*/ 1f,
            /*Electric*/ 2f,
            /*Psychic*/ 2f,
            /*Ice*/.5f,
            /*Dragon*/1f,
            /*Dark*/ 1f,
            /*Fairy*/ .5f);

        rock_ground = new dmgMult(
            /*Normal*/.5f,
            /*Fighting*/2f,
            /*Flying*/.5f,
            /*Poison*/.25f,
            /*Ground*/2f,
            /*Rock*/.5f,
            /*Bug*/1f,
            /*Ghost*/1f,
            /*Steel*/2f,
            /*Fire*/.5f,
            /*Water*/4f,
            /*Grass*/ 4f,
            /*Electric*/ 0f,
            /*Psychic*/ 1f,
            /*Ice*/ 2f,
            /*Dragon*/1f,
            /*Dark*/1f,
            /*Fairy*/ 1f);

        water_psychic = new dmgMult(
            /*Normal*/1f,
            /*Fighting*/.5f,
            /*Flying*/1f,
            /*Poison*/1f,
            /*Ground*/1f,
            /*Rock*/1f,
            /*Bug*/2f,
            /*Ghost*/2f,
            /*Steel*/.5f,
            /*Fire*/ .5f,
            /*Water*/.5f,
            /*Grass*/ 2f,
            /*Electric*/2f,
            /*Psychic*/ .5f,
            /*Ice*/.5f,
            /*Dragon*/ 1f,
            /*Dark*/ 2f,
            /*Fairy*/ 1f);

        electric_steel = new dmgMult(
            /*Normal*/.5f,
            /*Fighting*/2f,
            /*Flying*/.25f,
            /*Poison*/0f,
            /*Ground*/4f,
            /*Rock*/.5f,
            /*Bug*/.5f,
            /*Ghost*/1f,
            /*Steel*/.25f,
            /*Fire*/ 2f,
            /*Water*/1f,
            /*Grass*/ .5f,
            /*Electric*/ .5f,
            /*Psychic*/ .5f,
            /*Ice*/ .5f,
            /*Dragon*/.5f,
            /*Dark*/1f,
            /*Fairy*/.5f);

        water_ice = new dmgMult(
            /*Normal*/1f,
            /*Fighting*/2f,
            /*Flying*/1f,
            /*Poison*/1f,
            /*Ground*/1f,
            /*Rock*/2f,
            /*Bug*/1f,
            /*Ghost*/1f,
            /*Steel*/1f,
            /*Fire*/ 1f,
            /*Water*/.5f,
            /*Grass*/ 2f,
            /*Electric*/ 2f,
            /*Psychic*/ 1f,
            /*Ice*/.25f,
            /*Dragon*/1f,
            /*Dark*/ 1f,
            /*Fairy*/ 1f);

        ghost_poison = new dmgMult(
            /*Normal*/0f,
            /*Fighting*/0f,
            /*Flying*/1f,
            /*Poison*/.25f,
            /*Ground*/2f,
            /*Rock*/1f,
            /*Bug*/.25f,
            /*Ghost*/2f,
            /*Steel*/ 1f,
            /*Fire*/ 1f,
            /*Water*/1f,
            /*Grass*/ .5f,
            /*Electric*/ 1f,
            /*Psychic*/ 2f,
            /*Ice*/ 1f,
            /*Dragon*/1f,
            /*Dark*/ 2f,
            /*Fairy*/ .5f);

        grass_psychic = new dmgMult(
            /*Normal*/1f,
            /*Fighting*/.5f,
            /*Flying*/2f,
            /*Poison*/2f,
            /*Ground*/.5f,
            /*Rock*/1f,
            /*Bug*/4f,
            /*Ghost*/2f,
            /*Steel*/1f,
            /*Fire*/2f,
            /*Water*/.5f,
            /*Grass*/ .5f,
            /*Electric*/.5f,
            /*Psychic*/ .5f,
            /*Ice*/2f,
            /*Dragon*/1f,
            /*Dark*/ 2f,
            /*Fairy*/ 1f);

        ground_rock = new dmgMult(
            /*Normal*/.5f,
            /*Fighting*/2f,
            /*Flying*/.5f,
            /*Poison*/.25f,
            /*Ground*/2f,
            /*Rock*/.5f,
            /*Bug*/1f,
            /*Ghost*/1f,
            /*Steel*/2f,
            /*Fire*/ .5f,
            /*Water*/4f,
            /*Grass*/ 4f,
            /*Electric*/ 0f,
            /*Psychic*/ 1f,
            /*Ice*/2f,
            /*Dragon*/1f,
            /*Dark*/ 1f,
            /*Fairy*/1f);

        grass = new dmgMult(
            /*Normal*/1f,
            /*Fighting*/1f,
            /*Flying*/2f,
            /*Poison*/2f,
            /*Ground*/.5f,
            /*Rock*/1f,
            /*Bug*/2f,
            /*Ghost*/1f,
            /*Steel*/1f,
            /*Fire*/2f,
            /*Water*/ .5f,
            /*Grass*/ .5f,
            /*Electric*/ .5f,
            /*Psychic*/ 1f,
            /*Ice*/2f,
            /*Dragon*/1f,
            /*Dark*/1f,
            /*Fairy*/ 1f);

        psychic_fairy = new dmgMult(
            /*Normal*/1f,
            /*Fighting*/.25f,
            /*Flying*/1f,
            /*Poison*/2f,
            /*Ground*/1f,
            /*Rock*/1f,
            /*Bug*/1f,
            /*Ghost*/2f,
            /*Steel*/ 2f,
            /*Fire*/1f,
            /*Water*/1f,
            /*Grass*/ 1f,
            /*Electric*/ 1f,
            /*Psychic*/.5f,
            /*Ice*/1f,
            /*Dragon*/0f,
            /*Dark*/ 1f,
            /*Fairy*/ 1f);

        ice_psychic = new dmgMult(
            /*Normal*/1f,
            /*Fighting*/1f,
            /*Flying*/1f,
            /*Poison*/1f,
            /*Ground*/1f,
            /*Rock*/2f,
            /*Bug*/2f,
            /*Ghost*/2f,
            /*Steel*/ 2f,
            /*Fire*/ 2f,
            /*Water*/1f,
            /*Grass*/ 1f,
            /*Electric*/ 1f,
            /*Psychic*/.5f,
            /*Ice*/ .5f,
            /*Dragon*/ 1f,
            /*Dark*/2f,
            /*Fairy*/ 1f);

        water_flying = new dmgMult(
            /*Normal*/1f,
            /*Fighting*/.5f,
            /*Flying*/1f,
            /*Poison*/1f,
            /*Ground*/0f,
            /*Rock*/2f,
            /*Bug*/.5f,
            /*Ghost*/1f,
            /*Steel*/.5f,
            /*Fire*/.5f,
            /*Water*/ .5f,
            /*Grass*/ 1f,
            /*Electric*/ 4f,
            /*Psychic*/1f,
            /*Ice*/1f,
            /*Dragon*/1f,
            /*Dark*/ 1f,
            /*Fairy*/ 1f);

        rock_water = new dmgMult(
            /*Normal*/.5f,
            /*Fighting*/2f,
            /*Flying*/.5f,
            /*Poison*/.5f,
            /*Ground*/2f,
            /*Rock*/1f,
            /*Bug*/1f,
            /*Ghost*/1f,
            /*Steel*/1f,
            /*Fire*/.25f,
            /*Water*/1f,
            /*Grass*/ 4f,
            /*Electric*/ 2f,
            /*Psychic*/1f,
            /*Ice*/.5f,
            /*Dragon*/1f,
            /*Dark*/1f,
            /*Fairy*/1f);

        rock_flying = new dmgMult(
            /*Normal*/.5f,
            /*Fighting*/1f,
            /*Flying*/.5f,
            /*Poison*/.5f,
            /*Ground*/0f,
            /*Rock*/2f,
            /*Bug*/.5f,
            /*Ghost*/1f,
            /*Steel*/2f,
            /*Fire*/.5f,
            /*Water*/ 2f,
            /*Grass*/ 1f,
            /*Electric*/ 2f,
            /*Psychic*/1f,
            /*Ice*/ 2f,
            /*Dragon*/1f,
            /*Dark*/1f,
            /*Fairy*/ 1f);

        ice_flying = new dmgMult(
            /*Normal*/1f,
            /*Fighting*/1f,
            /*Flying*/1f,
            /*Poison*/1f,
            /*Ground*/0f,
            /*Rock*/4f,
            /*Bug*/.5f,
            /*Ghost*/1f,
            /*Steel*/2f,
            /*Fire*/ 2f,
            /*Water*/ 1f,
            /*Grass*/ .5f
            /*Electric*/, 2f,
            /*Psychic*/1f,
            /*Ice*/1f,
            /*Dragon*/1f,
            /*Dark*/1f,
            /*Fairy*/1f);

        electric_flying = new dmgMult(
            /*Normal*/1f,
            /*Fighting*/.5f,
            /*Flying*/.5f,
            /*Poison*/1f,
            /*Ground*/0f,
            /*Rock*/2f,
            /*Bug*/.5f,
            /*Ghost*/1f,
            /*Steel*/.5f,
            /*Fire*/1f,
            /*Water*/1f,
            /*Grass*/ .5f,
            /*Electric*/ 1f,
            /*Psychic*/1f,
            /*Ice*/2f,
            /*Dragon*/1f, 
            /*Dark*/1f,
            /*Fairy*/1f);

        dragon = new dmgMult(
            /*Normal*/1f,
            /*Fighting*/1f,
            /*Flying*/1f,
            /*Poison*/1f,
            /*Ground*/1f,
            /*Rock*/1f,
            /*Bug*/1f,
            /*Ghost*/1f,
            /*Steel*/1f,
            /*Fire*/.5f,
            /*Water*/.5f,
            /*Grass*/ .5f,
            /*Electric*/ .5f,
            /*Psychic*/1f,
            /*Ice*/2f,
            /*Dragon*/ 2f,
            /*Dark*/ 1f,
            /*Fairy*/ 2f);

        dragon_flying = new dmgMult(
            /*Normal*/1f,
            /*Fighting*/.5f,
            /*Flying*/1f,
            /*Poison*/1f,
            /*Ground*/0f,
            /*Rock*/2f,
            /*Bug*/.5f,
            /*Ghost*/1f,
            /*Steel*/1f,
            /*Fire*/.5f,
            /*Water*/.5f,
            /*Grass*/ .25f,
            /*Electric*/ 1f,
            /*Psychic*/1f,
            /*Ice*/4f,
            /*Dragon*/2f,
            /*Dark*/1f,
            /*Fairy*/ 2f);
    }

    private void Gen1_DmgMultipliers()
    {
        master_list.Add(new pokemon_dmg_multipliers("Bulbasaur", grass_poison));
        master_list.Add(new pokemon_dmg_multipliers("Ivysaur", grass_poison));
        master_list.Add(new pokemon_dmg_multipliers("Venusaur", grass_poison));
        master_list.Add(new pokemon_dmg_multipliers("Charmander", fire));
        master_list.Add(new pokemon_dmg_multipliers("Charmeleon", fire));
        master_list.Add(new pokemon_dmg_multipliers("Charizard", fire_flying));
        master_list.Add(new pokemon_dmg_multipliers("Squirtle", water));
        master_list.Add(new pokemon_dmg_multipliers("Wartortle", water));
        master_list.Add(new pokemon_dmg_multipliers("Blastoise", water));
        master_list.Add(new pokemon_dmg_multipliers("Caterpie", bug));
        master_list.Add(new pokemon_dmg_multipliers("Metapod", bug));
        master_list.Add(new pokemon_dmg_multipliers("Butterfree", bug_flying));
        master_list.Add(new pokemon_dmg_multipliers("Weedle", bug_poison));
        master_list.Add(new pokemon_dmg_multipliers("Kakuna", bug_poison));
        master_list.Add(new pokemon_dmg_multipliers("Beedrill", bug_poison));
        master_list.Add(new pokemon_dmg_multipliers("Pidgey", normal_flying));
        master_list.Add(new pokemon_dmg_multipliers("Pidgeotto", normal_flying));
        master_list.Add(new pokemon_dmg_multipliers("Pidgeot", normal_flying));
        master_list.Add(new pokemon_dmg_multipliers("Rattata", normal));
        master_list.Add(new pokemon_dmg_multipliers("Raticate", normal));
        master_list.Add(new pokemon_dmg_multipliers("Spearow", normal_flying));
        master_list.Add(new pokemon_dmg_multipliers("Fearow", normal_flying));
        master_list.Add(new pokemon_dmg_multipliers("Ekans", poison));
        master_list.Add(new pokemon_dmg_multipliers("Arbok", poison));
        master_list.Add(new pokemon_dmg_multipliers("Pikachu", electric));
        master_list.Add(new pokemon_dmg_multipliers("Raichu", electric));
        master_list.Add(new pokemon_dmg_multipliers("Sandshrew", ground));
        master_list.Add(new pokemon_dmg_multipliers("Sandslash", ground));
        master_list.Add(new pokemon_dmg_multipliers("Nidoran_F", poison));
        master_list.Add(new pokemon_dmg_multipliers("Nidorina", poison));
        master_list.Add(new pokemon_dmg_multipliers("Nidoqueen", poison_ground));
        master_list.Add(new pokemon_dmg_multipliers("Nidoran_M", poison));
        master_list.Add(new pokemon_dmg_multipliers("Nidorino", poison));
        master_list.Add(new pokemon_dmg_multipliers("Nidoking", poison_ground));
        master_list.Add(new pokemon_dmg_multipliers("Clefairy", normal));
        master_list.Add(new pokemon_dmg_multipliers("Clefable", normal));
        master_list.Add(new pokemon_dmg_multipliers("Vulpix", fire));
        master_list.Add(new pokemon_dmg_multipliers("Ninetales", fire));
        master_list.Add(new pokemon_dmg_multipliers("Jigglypuff", normal));
        master_list.Add(new pokemon_dmg_multipliers("Wigglytuff", normal));
        master_list.Add(new pokemon_dmg_multipliers("Zubat", poison_flying));
        master_list.Add(new pokemon_dmg_multipliers("Golbat", poison_flying));
        master_list.Add(new pokemon_dmg_multipliers("Oddish", grass_poison));
        master_list.Add(new pokemon_dmg_multipliers("Gloom", grass_poison));
        master_list.Add(new pokemon_dmg_multipliers("Vileplume", grass_poison));
        master_list.Add(new pokemon_dmg_multipliers("Paras", bug_grass));
        master_list.Add(new pokemon_dmg_multipliers("Parasect", bug_grass));
        master_list.Add(new pokemon_dmg_multipliers("Venonat", bug_poison));
        master_list.Add(new pokemon_dmg_multipliers("Venomoth", bug_poison));
        master_list.Add(new pokemon_dmg_multipliers("Diglett", ground));
        master_list.Add(new pokemon_dmg_multipliers("Dugtrio", ground));
        master_list.Add(new pokemon_dmg_multipliers("Meowth", normal));
        master_list.Add(new pokemon_dmg_multipliers("Persian", normal));
        master_list.Add(new pokemon_dmg_multipliers("Psyduck", water));
        master_list.Add(new pokemon_dmg_multipliers("Golduck", water));
        master_list.Add(new pokemon_dmg_multipliers("Mankey", fighting));
        master_list.Add(new pokemon_dmg_multipliers("Primeape", fighting));
        master_list.Add(new pokemon_dmg_multipliers("Growlithe", fire));
        master_list.Add(new pokemon_dmg_multipliers("Arcanine", fire));
        master_list.Add(new pokemon_dmg_multipliers("Poliwag", water));
        master_list.Add(new pokemon_dmg_multipliers("Poliwhirl", water));
        master_list.Add(new pokemon_dmg_multipliers("Poliwrath", water));
        master_list.Add(new pokemon_dmg_multipliers("Abra", psychic));
        master_list.Add(new pokemon_dmg_multipliers("Kadabra", psychic));
        master_list.Add(new pokemon_dmg_multipliers("Alakazam", psychic));
        master_list.Add(new pokemon_dmg_multipliers("Machop", fighting));
        master_list.Add(new pokemon_dmg_multipliers("Machoke", fighting));
        master_list.Add(new pokemon_dmg_multipliers("Machamp", fighting));
        master_list.Add(new pokemon_dmg_multipliers("Bellsprout", grass_poison));
        master_list.Add(new pokemon_dmg_multipliers("Weepinbell", grass_poison));
        master_list.Add(new pokemon_dmg_multipliers("Victreebel", grass_poison));
        master_list.Add(new pokemon_dmg_multipliers("Tentacool", water_poison));
        master_list.Add(new pokemon_dmg_multipliers("Tentacruel", water_poison));
        master_list.Add(new pokemon_dmg_multipliers("Geodude", rock_ground));
        master_list.Add(new pokemon_dmg_multipliers("Graveler", rock_ground));
        master_list.Add(new pokemon_dmg_multipliers("Golem", rock_ground));
        master_list.Add(new pokemon_dmg_multipliers("Ponyta", fire));
        master_list.Add(new pokemon_dmg_multipliers("Rapidash", fire));
        master_list.Add(new pokemon_dmg_multipliers("Slowpoke", water_psychic));
        master_list.Add(new pokemon_dmg_multipliers("Slowbro", water_psychic));
        master_list.Add(new pokemon_dmg_multipliers("Magnemite", electric_steel));
        master_list.Add(new pokemon_dmg_multipliers("Magneton", electric_steel));
        master_list.Add(new pokemon_dmg_multipliers("Farfetch'd", normal_flying));
        master_list.Add(new pokemon_dmg_multipliers("Doduo", normal_flying));
        master_list.Add(new pokemon_dmg_multipliers("Dodrio", normal_flying));
        master_list.Add(new pokemon_dmg_multipliers("Seel", water));
        master_list.Add(new pokemon_dmg_multipliers("Dewgong", water_ice));
        master_list.Add(new pokemon_dmg_multipliers("Grimer", poison));
        master_list.Add(new pokemon_dmg_multipliers("Muk", poison));
        master_list.Add(new pokemon_dmg_multipliers("Shellder", water));
        master_list.Add(new pokemon_dmg_multipliers("Cloyster", water_ice));
        master_list.Add(new pokemon_dmg_multipliers("Gastly", ghost_poison));
        master_list.Add(new pokemon_dmg_multipliers("Haunter", ghost_poison));
        master_list.Add(new pokemon_dmg_multipliers("Gengar", ghost_poison));
        master_list.Add(new pokemon_dmg_multipliers("Onix", rock_ground));
        master_list.Add(new pokemon_dmg_multipliers("Drowzee", psychic));
        master_list.Add(new pokemon_dmg_multipliers("Hypno", psychic));
        master_list.Add(new pokemon_dmg_multipliers("Krabby", water));
        master_list.Add(new pokemon_dmg_multipliers("Kingler", water_ice));
        master_list.Add(new pokemon_dmg_multipliers("Voltorb", electric));
        master_list.Add(new pokemon_dmg_multipliers("Electrode", electric));
        master_list.Add(new pokemon_dmg_multipliers("Exeggcute", grass_psychic));
        master_list.Add(new pokemon_dmg_multipliers("Exeggutor", grass_psychic));
        master_list.Add(new pokemon_dmg_multipliers("Cubone", ground));
        master_list.Add(new pokemon_dmg_multipliers("Marowak", ground));
        master_list.Add(new pokemon_dmg_multipliers("Hitmonlee", fighting));
        master_list.Add(new pokemon_dmg_multipliers("Hitmonchan", fighting));
        master_list.Add(new pokemon_dmg_multipliers("Lickitung", normal));
        master_list.Add(new pokemon_dmg_multipliers("Koffing", poison));
        master_list.Add(new pokemon_dmg_multipliers("Weezing", poison));
        master_list.Add(new pokemon_dmg_multipliers("Rhyhorn", ground_rock));
        master_list.Add(new pokemon_dmg_multipliers("Rhydon", ground_rock));
        master_list.Add(new pokemon_dmg_multipliers("Chansey", normal));
        master_list.Add(new pokemon_dmg_multipliers("Tangela", grass));
        master_list.Add(new pokemon_dmg_multipliers("Kangaskan", normal));
        master_list.Add(new pokemon_dmg_multipliers("Horsea", water));
        master_list.Add(new pokemon_dmg_multipliers("Seadra", water));
        master_list.Add(new pokemon_dmg_multipliers("Goldeen", water));
        master_list.Add(new pokemon_dmg_multipliers("Seaking", water));
        master_list.Add(new pokemon_dmg_multipliers("Staryu", water));
        master_list.Add(new pokemon_dmg_multipliers("Starmie", water_psychic));
        master_list.Add(new pokemon_dmg_multipliers("Mr. Mime", psychic));
        master_list.Add(new pokemon_dmg_multipliers("Scyther", bug_flying));
        master_list.Add(new pokemon_dmg_multipliers("Jynx", ice_psychic));
        master_list.Add(new pokemon_dmg_multipliers("Electrabuzz", electric));
        master_list.Add(new pokemon_dmg_multipliers("Magmar", fire));
        master_list.Add(new pokemon_dmg_multipliers("Pinsir", bug));
        master_list.Add(new pokemon_dmg_multipliers("Tauros", normal));
        master_list.Add(new pokemon_dmg_multipliers("Magikarp", water));
        master_list.Add(new pokemon_dmg_multipliers("Gyarados", water_flying));
        master_list.Add(new pokemon_dmg_multipliers("Lapras", water_ice));
        master_list.Add(new pokemon_dmg_multipliers("Ditto", normal));
        master_list.Add(new pokemon_dmg_multipliers("Eevee", normal));
        master_list.Add(new pokemon_dmg_multipliers("Vaporeon", water));
        master_list.Add(new pokemon_dmg_multipliers("Jolteon", electric));
        master_list.Add(new pokemon_dmg_multipliers("Flareon", fire));
        master_list.Add(new pokemon_dmg_multipliers("Porygon", normal));
        master_list.Add(new pokemon_dmg_multipliers("Omanyte", rock_water));
        master_list.Add(new pokemon_dmg_multipliers("Omastar", rock_water));
        master_list.Add(new pokemon_dmg_multipliers("Kabuto", rock_water));
        master_list.Add(new pokemon_dmg_multipliers("Kabutops", rock_water));
        master_list.Add(new pokemon_dmg_multipliers("Aerodactyl", rock_flying));
        master_list.Add(new pokemon_dmg_multipliers("Snorlax", normal));
        master_list.Add(new pokemon_dmg_multipliers("Articuno", ice_flying));
        master_list.Add(new pokemon_dmg_multipliers("Zapdos", electric_flying));
        master_list.Add(new pokemon_dmg_multipliers("Moltres", fire_flying));
        master_list.Add(new pokemon_dmg_multipliers("Dratini", dragon));
        master_list.Add(new pokemon_dmg_multipliers("Dragonair", dragon));
        master_list.Add(new pokemon_dmg_multipliers("Dragonite", dragon_flying));
        master_list.Add(new pokemon_dmg_multipliers("Mewtwo", psychic));
        master_list.Add(new pokemon_dmg_multipliers("Mew", psychic));
        master_list.Add(new pokemon_dmg_multipliers("Empty", normal));


    }

    public dmgMult name_to_dmgmult(string name)
    {
        for (int i = 0; i < master_list.Count; i++)
        {
            if (name == master_list[i].name)
            {
                return master_list[i].damage;
            }
        }
        Debug.LogError("No name found in dmg multipliers: " + name);
        return master_list[master_list.Count].damage;
    }

    public dmgMult getMultiplier(string name)
    {
        for (int i = 0; i < master_list.Count; i++)
        {
            if (name == master_list[i].name)
            {
                return master_list[i].damage;
            }
        }
        Debug.Log("Couldn't find damage multiplier for " + name);
        return master_list[0].damage;
    }
}
#region Structs
public struct dmgMult
{
    public float normal;
    public float fighting;
    public float flying;
    public float poison;
    public float ground;
    public float rock;
    public float bug;
    public float ghost;
    public float steel;
    public float fire;
    public float water;
    public float grass;
    public float electric;
    public float psychic;
    public float ice;
    public float dragon;
    public float dark;
    public float fairy;

    public dmgMult(float no, float fig, float fl, float po, float gro, float ro, float bu, float gh, float st, float fi,
                   float wa, float gra, float el, float ps, float ic, float dr, float da, float fa)
    {
        normal = no;
        fighting = fig;
        flying = fl;
        poison = po;
        ground = gro;
        rock = ro;
        bug = bu;
        ghost = gh;
        steel = st;
        fire = fi;
        water = wa;
        grass = gra;
        electric = el;
        psychic = ps;
        ice = ic;
        dragon = dr;
        dark = da;
        fairy = fa;
    }
}

public struct pokemon_dmg_multipliers
{
    public string name;
    public dmgMult damage;

    public pokemon_dmg_multipliers(string n, dmgMult b)
    {
        name = n;
        damage = b;
    }
}
#endregion
