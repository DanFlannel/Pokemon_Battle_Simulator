using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Pokemon Library for Generation 1 
/// </summary>
public class PokemonLibrary : MonoBehaviour
{
	//our pokemon library of stats and attributes
	public List<Pokemon> pokemonList = new List<Pokemon> ();
	
	//string for reference of all of the pokemon types
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
	//private string fairy = "Fairy";
	
	// Use this for initialization
	void Awake ()
	{
        libraryBaseStats ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	/// <summary>
	/// This creates a list of all the pokemon withe their id, name, hp, attack, defense, speed, special, if they can 
	/// eveolve and their 2 types, for non dual type pokemon the second attribute is empty so when we check for that
	///	during battle calculations we can see if they are dual type or single type and their weaknesses
	/// </summary>
	private void libraryBaseStats ()
	{
		/*Important note was the jigglypuff, wigglytuff, cleafiry and clefable were all normal type pokemon in gen 1*/
		#region Generation 1 Pokemon with most recent stats
		pokemonList.Add (new Pokemon (001, "Bulbasaur", 45, 49, 49, 65, 65, 45, true, grass, poison));
		pokemonList.Add (new Pokemon (002, "Ivysaur", 60, 62, 63, 80, 80, 60, true, grass, poison));
		pokemonList.Add (new Pokemon (003, "Venusaur", 80, 82, 83, 100, 100, 80, false, grass, poison));
		
		pokemonList.Add (new Pokemon (004, "Charmander", 39, 52, 43, 60, 50, 65, true, fire, empty));
		pokemonList.Add (new Pokemon (005, "Charmeleon", 58, 64, 58, 80, 65, 80, true, fire, empty));
		pokemonList.Add (new Pokemon (006, "Charizard", 78, 84, 78, 109, 85, 100, false, fire, flying));
		
		pokemonList.Add (new Pokemon (007, "Squirtle", 44, 48, 65, 50, 64, 43, true, water, empty));
		pokemonList.Add (new Pokemon (008, "Wartortle", 59, 63, 80, 65, 80, 58, true, water, empty));
		pokemonList.Add (new Pokemon (009, "Blastoise", 79, 83, 100, 85, 105, 78, false, water, empty));
		
		pokemonList.Add (new Pokemon (010, "Caterpie", 45, 30, 35, 20, 20, 45, true, bug, empty));
		pokemonList.Add (new Pokemon (011, "Metapod", 50, 20, 55, 25, 25, 30, true, bug, empty));
		pokemonList.Add (new Pokemon (012, "Butterfree", 60, 45, 50, 90, 80, 70, false, flying, bug));
		
		pokemonList.Add (new Pokemon (013, "Weedle", 40, 35, 30, 20, 20, 50, true, poison, bug));
		pokemonList.Add (new Pokemon (014, "Kakuna", 45, 25, 50, 25, 25, 35, true, poison, bug));
		pokemonList.Add (new Pokemon (015, "Beedrill", 65, 80, 40, 45, 80, 75, false, poison, bug));
		
		pokemonList.Add (new Pokemon (016, "Pidgey", 40, 45, 40, 35, 35, 56, true, normal, flying));
		pokemonList.Add (new Pokemon (017, "Pidgeotto", 63, 60, 55, 50, 50, 71, true, normal, flying));
		pokemonList.Add (new Pokemon (018, "Pidgeot", 83, 80, 75, 70, 70, 101, false, normal, flying));
		
		pokemonList.Add (new Pokemon (019, "Rattata", 30, 56, 35, 25, 35, 72, true, normal, empty));
		pokemonList.Add (new Pokemon (020, "Raticate", 55, 81, 60, 50, 70, 97, false, normal, empty));
		
		pokemonList.Add (new Pokemon (021, "Spearow", 40, 60, 30, 31, 31, 70, true, normal, flying));
		pokemonList.Add (new Pokemon (022, "Fearow", 65, 90, 65, 61, 61, 100, false, normal, flying));
		
		pokemonList.Add (new Pokemon (023, "Ekans", 35, 60, 44, 40, 54, 55, true, poison, empty));
		pokemonList.Add (new Pokemon (024, "Arbok", 60, 85, 69, 65, 79, 80, false, poison, empty));
		
		pokemonList.Add (new Pokemon (025, "Pikachu", 35, 55, 30, 50, 50, 90, true, electric, empty));
		pokemonList.Add (new Pokemon (026, "Raichu", 60, 90, 55, 90, 80, 100, false, electric, empty));
		
		pokemonList.Add (new Pokemon (027, "Sandshrew", 50, 75, 85, 20, 30, 40, true, ground, empty));
		pokemonList.Add (new Pokemon (028, "Sandslash", 75, 100, 110, 45, 55, 65, false, ground, empty));
		
		pokemonList.Add (new Pokemon (029, "Nidoran_F", 55, 47, 52, 40, 40, 41, true, poison, empty));
		pokemonList.Add (new Pokemon (030, "Nidorina", 70, 62, 67, 55, 55, 56, true, poison, empty));
		pokemonList.Add (new Pokemon (031, "Nidoqueen", 90, 82, 87, 75, 85, 76, false, poison, ground));
		
		pokemonList.Add (new Pokemon (032, "Nidoran_M", 46, 57, 40, 40, 40, 50, true, poison, empty));
		pokemonList.Add (new Pokemon (033, "Nidorino", 61, 72, 57, 55, 55, 65, true, poison, empty));
		pokemonList.Add (new Pokemon (034, "Nidoking", 81, 92, 77, 85, 75, 85, false, poison, ground));
		
		pokemonList.Add (new Pokemon (035, "Clefairy", 70, 45, 48, 60, 65, 35, true,normal , empty));
		pokemonList.Add (new Pokemon (036, "Clefable", 95, 70, 73, 95, 90, 60, false, normal, empty));
		
		pokemonList.Add (new Pokemon (037, "Vulpix", 38, 41, 40, 50, 65, 65, true, fire, empty));
		pokemonList.Add (new Pokemon (038, "Ninetales", 73, 76, 75, 81, 100, 100, false, fire, empty));
		
		pokemonList.Add (new Pokemon (039, "Jigglypuff", 115, 45, 20, 45, 25, 20, true, normal, empty));
		pokemonList.Add (new Pokemon (040, "Wigglytuff", 140, 70, 45, 85, 50, 45, false, normal, empty));
		
		pokemonList.Add (new Pokemon (041, "Zubat", 40, 45, 35, 30, 40, 55, true, poison, flying));
		pokemonList.Add (new Pokemon (042, "Golbat", 75, 80, 70, 65, 75, 90, false, poison, flying));
		
		pokemonList.Add (new Pokemon (043, "Oddish", 45, 50, 55, 75, 65, 30, true, grass, poison));
		pokemonList.Add (new Pokemon (044, "Gloom", 60, 65, 70, 85, 75, 40, true, grass, poison));
		pokemonList.Add (new Pokemon (045, "Vileplume", 75, 80, 85, 110, 90, 50, false, grass, poison));
		
		pokemonList.Add (new Pokemon (046, "Paras", 35, 70, 55, 45, 55, 25, true, grass, bug));
		pokemonList.Add (new Pokemon (047, "Parasect", 60, 95, 80, 60, 80, 30, false, grass, bug));
		
		pokemonList.Add (new Pokemon (048, "Venonat", 60, 55, 50, 40, 55, 45, true, poison, bug));
		pokemonList.Add (new Pokemon (049, "Venomoth", 70, 65, 60, 90, 75, 90, false, poison, bug));
		
		pokemonList.Add (new Pokemon (050, "Diglett", 10, 55, 25, 35, 45, 95, true, ground, empty));
		pokemonList.Add (new Pokemon (051, "Dugtrio", 35, 80, 50, 50, 70, 120, false, ground, empty));
		
		pokemonList.Add (new Pokemon (052, "Meowth", 40, 45, 35, 40, 40, 90, true, normal, empty));
		pokemonList.Add (new Pokemon (053, "Persian", 65, 70, 60, 65, 65, 115, false, normal, empty));
		
		pokemonList.Add (new Pokemon (054, "Psyduck", 50, 52, 48, 65, 50, 55, true, water, empty));
		pokemonList.Add (new Pokemon (055, "Golduck", 80, 82, 78, 95, 80, 85, false, water, empty));
		
		pokemonList.Add (new Pokemon (056, "Mankey", 40, 80, 35, 35, 45, 70, true, fighting, empty));
		pokemonList.Add (new Pokemon (057, "Primeape", 65, 105, 60, 60, 70, 95, false, fighting, empty));
		
		pokemonList.Add (new Pokemon (058, "Growlithe", 55, 70, 45, 70, 50, 60, true, fire, empty));
		pokemonList.Add (new Pokemon (059, "Arcanine", 90, 110, 80, 100, 80, 95, false, fire, empty));
		
		pokemonList.Add (new Pokemon (060, "Poliwag", 40, 50, 40, 40, 40, 90, true, water, empty));
		pokemonList.Add (new Pokemon (061, "Poliwhirl", 65, 65, 65, 50, 50, 90, true, water, empty));
		pokemonList.Add (new Pokemon (062, "Poliwrath", 90, 85, 95, 70, 90, 70, false, water, fighting));
		
		pokemonList.Add (new Pokemon (063, "Abra", 25, 20, 15, 105, 55, 90, true, psychic, empty));
		pokemonList.Add (new Pokemon (064, "Kadabra", 40, 35, 30, 120, 70, 105, true, psychic, empty));
		pokemonList.Add (new Pokemon (065, "Alakazam", 55, 50, 45, 135, 95, 120, false, psychic, empty));
		
		pokemonList.Add (new Pokemon (066, "Machop", 70, 80, 50, 35, 35, 35, true, fighting, empty));
		pokemonList.Add (new Pokemon (067, "Machoke", 80, 100, 70, 50, 60, 45, true, fighting, empty));
		pokemonList.Add (new Pokemon (068, "Machamp", 90, 130, 80, 65, 85, 55, false, fighting, empty));
		
		pokemonList.Add (new Pokemon (069, "Bellsprout", 50, 75, 35, 70, 30, 40, true, grass, poison));
		pokemonList.Add (new Pokemon (070, "Weepinbell", 65, 90, 50, 85, 45, 55, true, grass, poison));
		pokemonList.Add (new Pokemon (071, "Victreebel", 80, 105, 65, 100, 70, 70, false, grass, poison));
		
		pokemonList.Add (new Pokemon (072, "Tentacool", 40, 40, 35, 50, 100, 70, true, water, poison));
		pokemonList.Add (new Pokemon (073, "Tentacruel", 80, 70, 65, 80, 120, 100, false, water, poison));
		
		pokemonList.Add (new Pokemon (074, "Geodude", 40, 80, 100, 30, 30, 20, true, ground, rock));
		pokemonList.Add (new Pokemon (075, "Graveler", 55, 95, 115, 45, 45, 35, true, ground, rock));
		pokemonList.Add (new Pokemon (076, "Golem", 80, 110, 130, 55, 65, 45, false, ground, rock));
		
		pokemonList.Add (new Pokemon (077, "Ponyta", 50, 85, 55, 65, 65, 90, true, fire, empty));
		pokemonList.Add (new Pokemon (078, "Rapidash", 65, 100, 70, 80, 80, 105, false, fire, empty));
		
		pokemonList.Add (new Pokemon (079, "Slowpoke", 90, 65, 65, 40, 40, 15, true, water, psychic));
		pokemonList.Add (new Pokemon (080, "Slowbro", 95, 75, 110, 100, 80, 30, false, water, psychic));
		
		pokemonList.Add (new Pokemon (081, "Magnemite", 25, 35, 70, 95, 55, 45, true, electric, empty));
		pokemonList.Add (new Pokemon (082, "Magneton", 50, 60, 95, 120, 70, 70, false, electric, empty));
		
		pokemonList.Add (new Pokemon (083, "Farfetch'd", 52, 65, 55, 58, 62, 60, false, normal, flying));
		
		pokemonList.Add (new Pokemon (084, "Doduo", 35, 85, 45, 35, 35, 75, true, normal, flying));
		pokemonList.Add (new Pokemon (085, "Dodrio", 60, 110, 70, 60, 60, 100, false, normal, flying));
		
		pokemonList.Add (new Pokemon (086, "Seel", 65, 45, 55, 45, 70, 45, true, water, empty));
		pokemonList.Add (new Pokemon (087, "Dewgong", 90, 70, 80, 70, 95, 70, false, water, ice));
		
		pokemonList.Add (new Pokemon (088, "Grimer", 80, 80, 50, 40, 50, 25, true, poison, empty));
		pokemonList.Add (new Pokemon (089, "Muk", 105, 105, 75, 65, 100, 50, false, poison, empty));
		
		pokemonList.Add (new Pokemon (090, "Shellder", 30, 65, 100, 45, 25, 40, true, water, empty));
		pokemonList.Add (new Pokemon (091, "Cloyster", 50, 95, 180, 85, 45, 70, false, water, ice));
		
		pokemonList.Add (new Pokemon (092, "Gastly", 30, 35, 30, 100, 35, 80, true, poison, ghost));
		pokemonList.Add (new Pokemon (093, "Haunter", 45, 50, 45, 115, 55, 95, true, poison, ghost));
		pokemonList.Add (new Pokemon (094, "Gengar", 60, 65, 60, 130, 75, 110, false, poison, ghost));
		
		pokemonList.Add (new Pokemon (095, "Onix", 35, 45, 160, 30, 45, 70, false, ground, rock));
		
		pokemonList.Add (new Pokemon (096, "Drowzee", 60, 48, 45, 43, 90, 92, true, psychic, empty));
		pokemonList.Add (new Pokemon (097, "Hypno", 85, 73, 70, 73, 115, 67, false, psychic, empty));
		
		pokemonList.Add (new Pokemon (098, "Krabby", 30, 105, 90, 25, 25, 50, true, water, empty));
		pokemonList.Add (new Pokemon (099, "Kingler", 55, 130, 115, 50, 50, 75, false, water, empty));
		
		pokemonList.Add (new Pokemon (100, "Voltorb", 40, 30, 50, 55, 55, 100, true, electric, empty));
		pokemonList.Add (new Pokemon (101, "Electrode", 60, 50, 70, 80, 80, 140, false, electric, empty));
		
		pokemonList.Add (new Pokemon (102, "Exeggcute", 60, 40, 80, 60, 45, 40, true, grass, psychic));
		pokemonList.Add (new Pokemon (103, "Exeggutor", 95, 95, 85, 125, 65, 55, false, grass, psychic));
		
		pokemonList.Add (new Pokemon (104, "Cubone", 50, 50, 95, 40, 50, 35, true, ground, empty));
		pokemonList.Add (new Pokemon (105, "Marowak", 60, 80, 110, 50, 80, 45, false, ground, empty));
		
		pokemonList.Add (new Pokemon (106, "Hitmonlee", 50, 120, 53, 35, 110, 76, false, fighting, empty));
		
		pokemonList.Add (new Pokemon (107, "Hitmonchan", 50, 105, 79, 35, 110, 76, false, fighting, empty));
		
		pokemonList.Add (new Pokemon (108, "Lickitung", 90, 55, 75, 60, 75, 30, false, normal, empty));
		
		pokemonList.Add (new Pokemon (109, "Koffing", 40, 65, 95, 60, 45, 35, true, poison, empty));
		pokemonList.Add (new Pokemon (110, "Weezing", 65, 90, 120, 85, 70, 60, false, poison, empty));
		
		pokemonList.Add (new Pokemon (111, "Rhyhorn", 80, 85, 95, 30, 30, 25, true, ground, rock));
		pokemonList.Add (new Pokemon (112, "Rhydon", 105, 130, 120, 45, 45, 40, false, ground, rock));
		
		pokemonList.Add (new Pokemon (113, "Chansey", 250, 5, 5, 35, 105, 50, false, normal, empty));
		
		pokemonList.Add (new Pokemon (114, "Tangela", 65, 55, 115, 100, 40, 60, false, grass, empty));
		
		pokemonList.Add (new Pokemon (115, "Kangaskhan", 105, 95, 80, 40, 80, 90, false, normal, empty));
		
		pokemonList.Add (new Pokemon (116, "Horsea", 30, 40, 70, 70, 25, 60, true, water, empty));
		pokemonList.Add (new Pokemon (117, "Seadra", 55, 65, 95, 95, 45, 85, false, water, empty));
		
		pokemonList.Add (new Pokemon (118, "Goldeen", 45, 67, 60, 35, 50, 63, true, water, empty));
		pokemonList.Add (new Pokemon (119, "Seaking", 80, 92, 65, 65, 80, 68, false, water, empty));
		
		pokemonList.Add (new Pokemon (120, "Staryu", 30, 45, 55, 70, 55, 85, true, water, empty));
		pokemonList.Add (new Pokemon (121, "Starmie", 60, 75, 85, 100, 85, 115, false, water, psychic));
		
		pokemonList.Add (new Pokemon (122, "Mr. Mime", 40, 45, 65, 100, 120, 90, false, psychic, empty));
		
		pokemonList.Add (new Pokemon (123, "Scyther", 70, 110, 80, 55, 80, 105, false, flying, bug));
		
		pokemonList.Add (new Pokemon (124, "Jynx", 65, 50, 35, 115, 95, 95, false, ice, psychic));
		
		pokemonList.Add (new Pokemon (125, "Electrabuzz", 65, 83, 57, 95, 85, 105, false, electric, empty));
		
		pokemonList.Add (new Pokemon (126, "Magmar", 65, 95, 57, 100, 85, 93, false, fire, empty));
		
		pokemonList.Add (new Pokemon (127, "Pinsir", 65, 125, 100, 55, 70, 85, false, flying, bug));
		
		pokemonList.Add (new Pokemon (128, "Tauros", 75, 100, 95, 40, 70, 110, false, normal, empty));
		
		pokemonList.Add (new Pokemon (129, "Magikarp", 20, 10, 55, 15, 20, 80, true, water, empty));
		pokemonList.Add (new Pokemon (130, "Gyarados", 95, 125, 79, 60, 100, 81, false, water, flying));
		
		pokemonList.Add (new Pokemon (131, "Lapras", 130, 85, 80, 85, 95, 60, false, water, ice));
		
		pokemonList.Add (new Pokemon (132, "Ditto", 48, 48, 48, 48, 48, 48, false, normal, empty));
		
		pokemonList.Add (new Pokemon (133, "Eevee", 55, 55, 50, 45, 65, 55, true, normal, empty));
		pokemonList.Add (new Pokemon (134, "Vaporeon", 130, 65, 60, 110, 95, 65, false, water, empty));
		pokemonList.Add (new Pokemon (135, "Jolteon", 65, 65, 60, 110, 95, 130, false, electric, empty));
		pokemonList.Add (new Pokemon (136, "Flareon", 65, 130, 60, 95, 110, 65, false, fire, empty));
		
		pokemonList.Add (new Pokemon (137, "Porygon", 65, 60, 70, 85, 75, 40, false, normal, empty));
		
		pokemonList.Add (new Pokemon (138, "Omanyte", 35, 40, 100, 90, 55, 35, true, water, rock));
		pokemonList.Add (new Pokemon (139, "Omastar", 70, 60, 125, 115, 70, 55, false, water, rock));
		
		pokemonList.Add (new Pokemon (140, "Kabuto", 30, 80, 90, 55, 45, 55, true, water, rock));
		pokemonList.Add (new Pokemon (141, "Kabutops", 60, 115, 105, 65, 70, 80, false, water, rock));
		
		pokemonList.Add (new Pokemon (142, "Aerodactyl", 80, 105, 65, 60, 75, 130, false, flying, rock));
		
		pokemonList.Add (new Pokemon (143, "Snorlax", 160, 110, 65, 65, 110, 30, false, normal, empty));
		
		pokemonList.Add (new Pokemon (144, "Articuno", 90, 85, 100, 95, 125, 85, false, ice, flying));
		
		pokemonList.Add (new Pokemon (145, "Zapdos", 90, 90, 85, 125, 90, 100, false, electric, flying));
		
		pokemonList.Add (new Pokemon (146, "Moltres", 90, 100, 90, 125, 85, 90, false, fire, flying));
		
		pokemonList.Add (new Pokemon (147, "Dratini", 41, 64, 45, 50, 50, 50, true, dragon, empty));
		pokemonList.Add (new Pokemon (148, "Dragonair", 61, 84, 65, 70, 70, 70, false, dragon, empty));
		pokemonList.Add (new Pokemon (149, "Dragonite", 91, 134, 95, 100, 100, 80, false, flying, dragon));
		
		pokemonList.Add (new Pokemon (150, "Mewtwo", 106, 110, 90, 154, 100, 130, false, psychic, empty));
		
		pokemonList.Add (new Pokemon (151, "Mew", 100, 100, 100, 100, 100, 100, false, psychic, empty));
		#endregion
		
		
		
		
		/*for (int i = 0; i < pokemonList.Count; i++) {
			Debug.Log (pokemonList [i].name);
		}*/
		
	}
	
	#region Getters to return data to the Pokemon Creator Script
	public string GetName(int id){
		return pokemonList[id].name;
	}
	
	public int GetHP(int id){
		return pokemonList[id].hp;
	}
	
	public int GetAttack(int id){
		return pokemonList[id].attack;
	}
	
	public int GetDefense(int id){
		return pokemonList[id].defense;
	}
	
	public int GetSpecialAttack(int id){
		return pokemonList[id].specialAttack;
	}
	
	public int GetSpecialDefense(int id){
		return pokemonList[id].specialDefense;
	}
	
	public int GetSpeed(int id){
		return pokemonList[id].speed;
	}
	
	public bool GetCanEvolve(int id){
		return pokemonList[id].canEvolve;
	}
	
	public string GetType1(int id){
		return pokemonList[id].type1;
	}
	
	public string GetType2(int id){
		return pokemonList[id].type2;
	}
	#endregion
}