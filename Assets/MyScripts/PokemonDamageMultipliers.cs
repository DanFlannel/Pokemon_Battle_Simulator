using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PokemonDamageMultipliers : MonoBehaviour {

	public List<pokemon_dmg_multipliers> dmg_multipliers = new List<pokemon_dmg_multipliers>();

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
	void Start () {
		damageMultipliers();
		pokemon_damage_list();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void damageMultipliers(){
		grass_poison = 		new dmgMult (1f,.5f,2f,1f,1f,1f,1f,1f,1f,2f,.5f,.25f,.5f,2f,2f,1f,1f,.5f);
		fire =				new dmgMult (1f,1f,1f,1f,2f,2f,.5f,1f,.5f,.5f,2f,.5f,1f,1f,.5f,1f,1f,.5f);
		fire_flying = 		new dmgMult (1f,.5f,1f,1f,0f,4f,.25f,1f,.5f,.5f,2f,.25f,2f,1f,1f,1f,1f,.5f);
		water = 			new dmgMult (1f,1f,1f,1f,1f,1f,1f,1f,.5f,.5f,.5f,2f,2f,1f,.5f,1f,1f,1f);
		bug = 				new dmgMult (1f,.5f,2f,1f,.5f,2f,1f,1f,1f,2f,1f,.5f,1f,1f,1f,1f,1f,1f);
		bug_flying = 		new dmgMult (1f,.25f,2f,1f,0f,4f,.5f,1f,1f,2f,1f,.25f,2f,1f,2f,1f,1f,1f);
		bug_poison = 		new dmgMult (1f,.25f,2f,.5f,1f,2f,.5f,1f,1f,2f,1f,.25f,1f,2f,1f,1f,1f,.5f);
		normal_flying = 	new dmgMult (1f,1f,1f,1f,0f,2f,.5f,0f,1f,1f,1f,.5f,2f,1f,2f,1f,1f,1f);
		normal = 			new dmgMult (1f,2f,1f,1f,1f,1f,1f,0f,1f,1f,1f,1f,1f,1f,1f,1f,1f,1f);
		poison = 			new dmgMult (1f,.5f,1f,.5f,2f,1f,.5f, 1f,1f,1f,1f,.5f,1f,2f,1f,1f,1f,.5f);
		electric = 			new dmgMult (1f,1f,.5f,1f,2f,1f,1f,1f,.5f,1f,1f,1f,.5f,1f,1f,1f,1f,1f);
		ground = 			new dmgMult (1f,1f,1f,.5f,1f,.5f,1f,1f,1f,1f,2f,2f,0f,1f,2f,1f,1f,1f);
		poison_ground = 	new dmgMult (1f,.5f,1f,.25f,2f,.5f,.5f,1f,1f,1f,2f,1f,0f,2f,2f,1f,1f,.5f);
		//fairy = 			new dmgMult (1f,.5f,1f,2f,1f,1f,.5f,1f,2f,1f,1f,1f,1f,1f,1f,0f,.5f,1f);
		//normal_fairy = 		new dmgMult (1f,1f,1f,2f,1f,1f,.5f,0f,2f,1f,1f,1f,1f,1f,1f,0f,.5f,1f);
		poison_flying = 	new dmgMult (1f,.25f,1f,.5f,0f,2f,.25f,1f,1f,1f,1f,.25f,2f,2f,2f,1f,1f,.5f);
		bug_grass = 		new dmgMult (1f,.5f,4f,2f,.25f,2f,2f,1f,1f,4f,.5f,.25f,.5f,1f,2f,1f,1f,1f);
		fighting = 			new dmgMult (1f,1f,2f,1f,1f,.5f,.5f,1f,1f,1f,1f,1f,1f,2f,1f,1f,.5f,2f);
		psychic = 			new dmgMult (1f,.5f,1f,1f,1f,1f,2f,2f,1f,1f,1f,1f,1f,.5f,1f,1f,2f,1f);
		water_poison = 		new dmgMult (1f,.5f,1f,.5f,2f,1f,.5f,1f,.5f,.5f,.5f,1f,2f,2f,.5f,1f,1f,.5f);
		rock_ground = 		new dmgMult (.5f,2f,.5f,.25f,2f,.5f,1f,1f,2f,.5f,4f,4f,0f,1f,2f,1f,1f,1f);
		water_psychic = 	new dmgMult (1f,.5f,1f,1f,1f,1f,2f,2f,.5f,.5f,.5f,2f,2f,.5f,.5f,1f,2f,1f);
		electric_steel = 	new dmgMult (.5f,2f,.25f,0f,4f,.5f,.5f,1f,.25f,2f,1f,.5f,.5f,.5f,.5f,.5f,1f,.5f);
		water_ice = 		new dmgMult (1f,2f,1f,1f,1f,2f,1f,1f,1f,1f,.5f,2f,2f,1f,.25f,1f,1f,1f);
		ghost_poison = 		new dmgMult (0f,0f,1f,.25f,2f,1f,.25f,2f,1f,1f,1f,.5f,1f,2f,1f,1f,2f,.5f);
		grass_psychic = 	new dmgMult (1f,.5f,2f,2f,.5f,1f,4f,2f,1f,2f,.5f,.5f,.5f,.5f,2f,1f,2f,1f);
		ground_rock = 		new dmgMult (.5f,2f,.5f,.25f,2f,.5f,1f,1f,2f,.5f,4f,4f,0f,1f,2f,1f,1f,1f);
		grass = 			new dmgMult (1f,1f,2f,2f,.5f,1f,2f,1f,1f,2f,.5f,.5f,.5f,1f,2f,1f,1f,1f);
		//psychic_fairy = 	new dmgMult (1f,.25f,1f,2f,1f,1f,1f,2f,2f,1f,1f,1f,1f,.5f,1f,0f,1f,1f);
		ice_psychic =		new dmgMult (1f,1f,1f,1f,1f,2f,2f,2f,2f,2f,1f,1f,1f,.5f,.5f,1f,2f,1f);
		water_flying = 		new dmgMult (1f,.5f,1f,1f,0f,2f,.5f,1f,.5f,.5f,.5f,1f,4f,1f,1f,1f,1f,1f);
		rock_water = 		new dmgMult (.5f,2f,.5f,.5f,2f,1f,1f,1f,1f,.25f,1f,4f,2f,1f,.5f,1f,1f,1f);
		rock_flying = 		new dmgMult (.5f,1f,.5f,.5f,0f,2f,.5f,1f,2f,.5f,2f,1f,2f,1f,2f,1f,1f,1f);
		ice_flying = 		new dmgMult (1f,1f,1f,1f,0f,4f,.5f,1f,2f,2f,1f,.5f,2f,1f,1f,1f,1f,1f);
		electric_flying = 	new dmgMult (1f,.5f,.5f,1f,0f,2f,.5f,1f,.5f,1f,1f,.5f,1f,1f,2f,1f,1f,1f);
		dragon = 			new dmgMult (1f,1f,1f,1f,1f,1f,1f,1f,1f,.5f,.5f,.5f,.5f,1f,2f,2f,1f,2f);
		dragon_flying = 	new dmgMult (1f,.5f,1f,1f,0f,2f,.5f,1f,1f,.5f,.5f,.25f,1f,1f,4f,2f,1f,2f);
	}

	private void pokemon_damage_list(){
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Bulbasaur", grass_poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Ivysaur", grass_poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Venusaur", grass_poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Charmander", fire));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Charmeleon", fire));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Charizard", fire_flying));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Squirtle", water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Wartortle", water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Blastoise", water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Caterpie", bug));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Metapod", bug));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Butterfree", bug_flying));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Weedle", bug_poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Kakuna", bug_poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Beedrill", bug_poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Pidgey", normal_flying));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Pidgeotto", normal_flying));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Pidgeot", normal_flying));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Rattata", normal));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Raticate", normal));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Spearow", normal_flying));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Fearow", normal_flying));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Ekans", poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Arbok", poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Pikachu", electric));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Raichu", electric));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Sandshrew", ground));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Sandslash", ground));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Nidoran_F", poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Nidorina", poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Nidoqueen", poison_ground));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Nidoran_M", poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Nidorino", poison));	
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Nidoking", poison_ground));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Clefairy", normal));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Clefable", normal));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Vulpix", fire));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Ninetails",fire));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Jigglypuff", normal));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Wigglytuff", normal));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Zubat", poison_flying));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Golbat", poison_flying));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Oddish", grass_poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Gloom", grass_poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Vileplume", grass_poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Paras", bug_grass));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Parasect", bug_grass));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Venonat", bug_poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Venomoth", bug_poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Diglett", ground ));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Dugtrio", ground));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Meowth", normal));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Persian", normal));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Psyduck", water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Golduck", water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Mankey", fighting));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Primeape", fighting));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Growlithe", fire));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Arcanine", fire));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Poliwag", water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Poliwhirl", water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Poliwrath", water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Abra", psychic));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Kadabra", psychic));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Alakazam", psychic));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Machop", fighting));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Machoke", fighting));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Machamp", fighting));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Bellsprout", grass_poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Weepinbell", grass_poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Victreebel", grass_poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Tentacool", water_poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Tentacruel", water_poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Geodude", rock_ground));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Gravler", rock_ground));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Golem", rock_ground));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Ponyta", fire));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Rapidash", fire));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Slowpoke", water_psychic));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Slowbro", water_psychic));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Magnemite", electric_steel));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Magneton", electric_steel));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Farfetch'd", normal_flying));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Doduo", normal_flying));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Dodrio", normal_flying));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Seel", water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Dewgong", water_ice));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Grimer", poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Muk", poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Shellder", water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Cloyster", water_ice));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Gastly", ghost_poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Haunter", ghost_poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Gengar", ghost_poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Onix", rock_ground));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Drowzee", psychic));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Hypno", psychic));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Krabby", water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Kingler", water_ice));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Voltorb", electric));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Electrode", electric));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Exeggcute", grass_psychic));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Exeggutor", grass_psychic));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Cubone", ground));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Marowak", ground));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Hitmonlee", fighting));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Hitmonchan", fighting));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Lickitung", normal));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Koffing", poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Weezing", poison));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Rhyhorn", ground_rock));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Rhydon", ground_rock));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Chansey", normal));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Tangela", grass));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Kangaskan", normal));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Horsea", water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Seadra", water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Goldeen", water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Seaking", water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Staryu", water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Starmie", water_psychic));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Mr. Mime", psychic));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Scyther", bug_flying));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Jynx", ice_psychic));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Electrabuzz", electric));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Magmar", fire));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Pinsir", bug));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Tauros", normal));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Magikarp", water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Gyrados", water_flying));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Lapras", water_ice));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Ditto", normal));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Eevee", normal));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Vaporeon", water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Jolteon", electric));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Flareon", fire));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Porygon", normal));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Omanyte", rock_water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Omastar", rock_water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Kabuto", rock_water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Kabutops", rock_water));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Aerodactyl", rock_flying));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Snorlax", normal));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Articuno", ice_flying));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Zapados", electric_flying));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Moltres", fire_flying));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Drantini", dragon));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Dragonair", dragon));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Dragonite", dragon_flying));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Mewtwo", psychic));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Mew", psychic));
		dmg_multipliers.Add(new pokemon_dmg_multipliers("Empty", normal));


	}

	public dmgMult name_to_dmgmult(string name){
		for(int i = 0; i < dmg_multipliers.Count; i++){
			if(name == dmg_multipliers[i].name){
				return dmg_multipliers[i].damage;
			}
		}
		Debug.LogError("No name found in dmg multipliers: " + name);
		return dmg_multipliers[dmg_multipliers.Count].damage;
	}

	public dmgMult getMultiplier(string name){
		for(int i = 0; i < dmg_multipliers.Count; i++){
			if(name == dmg_multipliers[i].name){
				return dmg_multipliers[i].damage;
			}
		}
		Debug.Log("Couldn't find damage multiplier for " + name);
		return dmg_multipliers[0].damage;
	}
}
#region Structs
public struct dmgMult {
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
	               float wa, float gra, float el, float ps, float ic, float dr, float da, float fa){
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
		dark  = da;
		fairy = fa;
	}
}

public struct pokemon_dmg_multipliers{
	public string name;
	public dmgMult damage;

	public pokemon_dmg_multipliers (string n, dmgMult b){
		name = n;
		damage = b;
	}
}
#endregion
