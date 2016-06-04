using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class generates all of the attacks for all of the pokemon
/// It first loads all the attacks, with the attack structure into a list by ID
/// Then it goes through each pokemon and makes a specific list of attacks for that pokemon
/// Finally, it adds that list for each pokemon the the masterList, so that the master list
/// Can be searched by name and return a list of attacks for any given pokemon
/// </summary>
public class PokemonAttacks : MonoBehaviour
{
	public bool completedDatabaseInitalization = false;

	#region Declared Variables
	public List<attacks> attackList = new List<attacks> ();	
	public List<attackIndex> HM_List = new List<attackIndex>();
	public List<attackIndex> TM_List = new List<attackIndex>();
	public List<masterAttackList> master_attack_list = new List<masterAttackList> ();	//master list of the lists of attacks

	#region TM names to number associating
	private const int MegaPunch = 1;
	private const int RazorWind = 2;
	private const int SwordsDance = 3;
	private const int Whirlwind = 4;
	private const int MegaKick = 5;
	private const int Toxic = 6;
	private const int HornDrill = 7;
	private const int BodySlam = 8;
	private const int TakeDown = 9;
	private const int DoubleEdge = 10;
	private const int BubbleBeam = 11;
	private const int WaterGun = 12;
	private const int IceBeam = 13;
	private const int Blizzard = 14;
	private const int HyperBeam = 15;
	private const int Payday = 16;
	private const int Submission = 17;
	private const int Counter = 18;
	private const int SeismicToss = 19;
	private const int Rage = 20;
	private const int MegaDrain = 21;
	private const int SolarBeam = 22;
	private const int DragonRage = 23;
	private const int ThunderBolt = 24;
	private const int Thunder = 25;
	private const int Earthquake = 26;
    private const int Fissure = 27;
	private const int Dig = 28;
	private const int Psychic_TM = 29;
	private const int Teleport = 30;
	private const int Mimic = 31;
	private const int DoubleTeam = 32;
	private const int Reflect = 33;
	private const int Bide = 34;
	private const int Metronome = 35;
	private const int SelfDestruct = 36;
	private const int EggBomb = 37;
	private const int FireBlast = 38;
	private const int Swift = 39;
	private const int SkullBash = 40;
	private const int SoftBoiled = 41;
	private const int DreamEater = 42;
	private const int SkyAttack = 43;
	private const int Rest = 44;
	private const int ThunderWave = 45;
	private const int Psywave = 46;
	private const int Explosion = 47;
	private const int RockSlide = 48;
	private const int TriAttack = 49;
	private const int Substitute = 50;
	#endregion

	#region HM names to number associating
	private const int Cut = 1;
	private const int Fly = 2;
	private const int Surf = 3;
	private const int Strength = 4;
	private const int Flash = 5;
	#endregion
	
	#region Types of attacks
	public string Physical = "Physical";
	public string Special = "Special";
	public string Status = "Status";

	private const string Normal = "Normal";
	private const string Fighting = "Fighting";
	private const string Water = "Water";
	private const string Poison = "Poison";
	private const string Ice = "Ice";
	private const string Ground = "Ground";
	private const string Grass = "Grass";
	private const string Psychic = "Psychic";
	private const string Dark = "Dark";
	private const string Ghost = "Ghost";
	private const string Dragon = "Dragon";
	private const string Flying = "Flying";
	private const string Fire = "Fire";
	private const string Bug = "Bug";
	private const string Rock = "Rock";
	private const string Electric = "Electric";
	#endregion
	
	#region Pokemon List 1-24
	public List<attackIndex> BulbasarAttacks = new List<attackIndex> ();
	public List<attackIndex> IvysaurAttacks = new List<attackIndex> ();
	public List<attackIndex> VenusaurAttacks = new List<attackIndex> ();
	public List<attackIndex> CharmanderAttacks = new List<attackIndex> ();
	public List<attackIndex> CharmeleonAttacks = new List<attackIndex> ();
	public List<attackIndex> CharizardAttacks = new List<attackIndex> ();
	public List<attackIndex> SquirtleAttacks = new List<attackIndex> ();
	public List<attackIndex> WartortleAttacks = new List<attackIndex> ();
	public List<attackIndex> BlastoiseAttacks = new List<attackIndex> ();
	public List<attackIndex> CaterpieAttacks = new List<attackIndex> ();
	public List<attackIndex> MetapodAttacks = new List<attackIndex> ();
	public List<attackIndex> ButterfreeAttacks = new List<attackIndex> ();
	public List<attackIndex> WeedleAttacks = new List<attackIndex> ();
	public List<attackIndex> KakunaAttacks = new List<attackIndex> ();
	public List<attackIndex> BeedrillAttacks = new List<attackIndex> ();
	public List<attackIndex> PidgeyAttacks = new List<attackIndex> ();
	public List<attackIndex> PidgeottoAttacks = new List<attackIndex> ();
	public List<attackIndex> PidgeotAttacks = new List<attackIndex> ();
	public List<attackIndex> RattataAttacks = new List<attackIndex> ();
	public List<attackIndex> RaticateAttacks = new List<attackIndex> ();
	public List<attackIndex> SpearowAttacks = new List<attackIndex> ();
	public List<attackIndex> FearowAttacks = new List<attackIndex> ();
	public List<attackIndex> EkansAttacks = new List<attackIndex> ();
	public List<attackIndex> ArbokAttacks = new List<attackIndex> ();
	#endregion

	#region Pokemon List 25-52
	public List<attackIndex> PikachuAttacks = new List<attackIndex> ();
	public List<attackIndex> RaichuAttacks = new List<attackIndex> ();
	public List<attackIndex> SandshrewAttacks = new List<attackIndex> ();
	public List<attackIndex> SandslashAttacks = new List<attackIndex> ();
	public List<attackIndex> Nidoran_FAttacks = new List<attackIndex> ();
	public List<attackIndex> NidorinaAttacks = new List<attackIndex> ();
	public List<attackIndex> NidoqueenAttacks = new List<attackIndex> ();
	public List<attackIndex> Nidoran_MAttacks = new List<attackIndex> ();
	public List<attackIndex> NidorinoAttacks = new List<attackIndex> ();
	public List<attackIndex> NidokingAttacks = new List<attackIndex> ();
	public List<attackIndex> ClefairyAttacks = new List<attackIndex> ();
	public List<attackIndex> ClefableAttacks = new List<attackIndex> ();
	public List<attackIndex> VulpixAttacks  = new List<attackIndex> ();
	public List<attackIndex> NinetailsAttacks = new List<attackIndex> ();
	public List<attackIndex> JigglypuffAttacks = new List<attackIndex> ();
	public List<attackIndex> WigglytuffAttacks = new List<attackIndex> ();
	public List<attackIndex> ZubatAttacks = new List<attackIndex> ();
	public List<attackIndex> GolbattAttacks = new List<attackIndex> ();
	public List<attackIndex> OddishAttacks = new List<attackIndex> ();
	public List<attackIndex> GloomAttacks = new List<attackIndex> ();
	public List<attackIndex> VileplumeAttacks = new List<attackIndex> ();
	public List<attackIndex> ParasAttacks = new List<attackIndex> ();
	public List<attackIndex> ParasectAttacks = new List<attackIndex> ();
	public List<attackIndex> VenonatAttacks = new List<attackIndex> ();
	public List<attackIndex> VenomothAttacks = new List<attackIndex> ();
	public List<attackIndex> DiglettAttacks = new List<attackIndex> ();
	public List<attackIndex> DugtrioAttacks = new List<attackIndex> ();
	#endregion

	#region Pokemon List 53-76
	public List<attackIndex> MeowthAttacks = new List<attackIndex> ();
	public List<attackIndex> PersianAttacks = new List<attackIndex> ();
	public List<attackIndex> PsyduckAttacks = new List<attackIndex> ();
	public List<attackIndex> GolduckAttacks = new List<attackIndex> ();
	public List<attackIndex> MankeyAttacks = new List<attackIndex> ();
	public List<attackIndex> PrimeapeAttacks = new List<attackIndex> ();
	public List<attackIndex> GrowlitheAttacks = new List<attackIndex> ();
	public List<attackIndex> ArcanineAttacks = new List<attackIndex> ();
	public List<attackIndex> PoliwagAttacks = new List<attackIndex> ();
	public List<attackIndex> PoliwhirlAttacks = new List<attackIndex>();
	public List<attackIndex> PoliwrathAttacks = new List<attackIndex> ();
	public List<attackIndex> AbraAttacks = new List<attackIndex>();
	public List<attackIndex> KadabraAttacks = new List<attackIndex>();
	public List<attackIndex> AlakazamAttacks = new List<attackIndex>();
	public List<attackIndex> MachopAttacks = new List<attackIndex>();
	public List<attackIndex> MachokeAttacks = new List<attackIndex>();
	public List<attackIndex> MachampAttacks = new List<attackIndex>();
	public List<attackIndex> BellsproutAttacks = new List<attackIndex>();
	public List<attackIndex> WeepinbellAttacks = new List<attackIndex>();
	public List<attackIndex> VictreebelAttacks = new List<attackIndex>();
	public List<attackIndex> TentacoolAttacks = new List<attackIndex>();
	public List<attackIndex> TentacruelAttacks = new List<attackIndex>();
	public List<attackIndex> GeodudeAttacks = new List<attackIndex>();
	public List<attackIndex> GravlerAttacks = new List<attackIndex>();
	public List<attackIndex> GolemAttacks = new List<attackIndex>();
	#endregion

	#region Pokemon List 77-101
	public List<attackIndex> PonytaAttacks = new List<attackIndex> ();
	public List<attackIndex> RapidashAttacks = new List<attackIndex> ();
	public List<attackIndex> SlowpokeAttacks = new List<attackIndex> ();
	public List<attackIndex> SlowbroAttacks = new List<attackIndex> ();
	public List<attackIndex> MagnemiteAttacks = new List<attackIndex> ();
	public List<attackIndex> MagnetonAttacks = new List<attackIndex> ();
	public List<attackIndex> FarfetchdAttacks = new List<attackIndex> ();
	public List<attackIndex> DoduoAttacks = new List<attackIndex> ();
	public List<attackIndex> DodrioAttacks = new List<attackIndex> ();
	public List<attackIndex> SeelAttacks = new List<attackIndex> ();
	public List<attackIndex> DewgongAttacks = new List<attackIndex> ();
	public List<attackIndex> GrimerAttacks = new List<attackIndex> ();
	public List<attackIndex> MukAttacks = new List<attackIndex> ();
	public List<attackIndex> ShellderAttacks = new List<attackIndex> ();
	public List<attackIndex> CloysterAttacks = new List<attackIndex> ();
	public List<attackIndex> GastlyAttacks = new List<attackIndex> ();
	public List<attackIndex> HaunterAttacks = new List<attackIndex> ();
	public List<attackIndex> GengarAttacks = new List<attackIndex> ();
	public List<attackIndex> OnixAttacks = new List<attackIndex> ();
	public List<attackIndex> DrowzeeAttacks = new List<attackIndex> ();
	public List<attackIndex> HypnoAttacks = new List<attackIndex> ();
	public List<attackIndex> KrabbyAttacks = new List<attackIndex> ();
	public List<attackIndex> KinglerAttacks = new List<attackIndex> ();
	public List<attackIndex> VoltorbAttacks = new List<attackIndex> ();
	public List<attackIndex> ElectrodeAttacks = new List<attackIndex> ();
	#endregion

	#region Pokemon List 102-126
	public List<attackIndex> ExeggcuteAttacks = new List<attackIndex>();
	public List<attackIndex> ExegutorAttacks = new List<attackIndex>();
	public List<attackIndex> CuboneAttacks = new List<attackIndex>();
	public List<attackIndex> MarowakAttacks = new List<attackIndex>();
	public List<attackIndex> HitmonleeAttacks = new List<attackIndex>();
	public List<attackIndex> HitmonchanAttacks = new List<attackIndex>();
	public List<attackIndex> LickitungAttacks = new List<attackIndex>();
	public List<attackIndex> KoffingAttacks = new List<attackIndex>();
	public List<attackIndex> WeezingAttacks = new List<attackIndex>();
	public List<attackIndex> RhyhornAttacks = new List<attackIndex>();
	public List<attackIndex> RhydonAttacks = new List<attackIndex> ();
	public List<attackIndex> ChanseyAttacks = new List<attackIndex>();
	public List<attackIndex> TangelaAttacks = new List<attackIndex>();
	public List<attackIndex> KangaskhanAttacks = new List<attackIndex>();
	public List<attackIndex> HorseaAttacks = new List<attackIndex>();
	public List<attackIndex> KingdraAttacks = new List<attackIndex> ();
	public List<attackIndex> SeadraAttacks = new List<attackIndex>();
	public List<attackIndex> GoldeenAttacks = new List<attackIndex>();
	public List<attackIndex> SeakingAttacks = new List<attackIndex>();
	public List<attackIndex> StaryuAttacks = new List<attackIndex>();
	public List<attackIndex> StarmieAttacks = new List<attackIndex>();
	public List<attackIndex> MrMimeAttacks = new List<attackIndex>();
	public List<attackIndex> ScytherAttacks = new List<attackIndex>();
	public List<attackIndex> JynxAttacks = new List<attackIndex>();
	public List<attackIndex> ElectrabuzzAttacks = new List<attackIndex>();
	public List<attackIndex> MagmarAttacks = new List<attackIndex>();
	#endregion

	#region Pokemon List 127-151
	public List<attackIndex> PinsirAttacks = new List<attackIndex>();
	public List<attackIndex> TaurosAttacks = new List<attackIndex>();
	public List<attackIndex> MagikarpAttacks = new List<attackIndex>();
	public List<attackIndex> GyaradosAttacks = new List<attackIndex>();
	public List<attackIndex> LaprasAttacks = new List<attackIndex>();
	public List<attackIndex> DittoAttacks = new List<attackIndex>();
	public List<attackIndex> EeveeAttacks = new List<attackIndex>();
	public List<attackIndex> VaporeonAttacks = new List<attackIndex>();
	public List<attackIndex> JolteonAttacks = new List<attackIndex>();
	public List<attackIndex> FlareonAttacks = new List<attackIndex>();
	public List<attackIndex> PorygonAttacks = new List<attackIndex>();
	public List<attackIndex> OmanyteAttacks = new List<attackIndex>();
	public List<attackIndex> OmastarAttacks = new List<attackIndex>();
	public List<attackIndex> KabutoAttacks = new List<attackIndex>();
	public List<attackIndex> KabutopsAttacks = new List<attackIndex>();
	public List<attackIndex> AeordactylAttacks = new List<attackIndex>();
	public List<attackIndex> SnorlaxAttacks = new List<attackIndex>();
	public List<attackIndex> ArticunoAttacks = new List<attackIndex>();
	public List<attackIndex> ZapdosAttacks = new List<attackIndex>();
	public List<attackIndex> MoltresAttacks = new List<attackIndex>();
	public List<attackIndex> DratiniAttacks = new List<attackIndex>();
	public List<attackIndex> DragonairAttacks = new List<attackIndex>();
	public List<attackIndex> DragoniteAttacks = new List<attackIndex>();
	public List<attackIndex> MewtwoAttacks = new List<attackIndex>();
	public List<attackIndex> MewAttacks = new List<attackIndex>();
	#endregion


	#endregion

	void Awake(){
        Console.WriteLine("PK : Attack Data: Initalizing");
		attackLibrary ();
		TMLibrary();
		HMLibrary();
		init1_24();
		init25_52();
		init53_76();
		init77_101();
		init102_126();
		init127_151();
        Console.WriteLine("PK : Attack Data: Initalized");
    }
	// Use this for initialization
	void Start ()
	{


	}

	#region Init for Pokemon Attack Lists
	private void init1_24 (){
		Bulbasar ();
		Ivysaur ();
		Venusaur ();
		Charmander ();
		Charmeleon ();
		Charizard ();
		Squirtle ();
		Wartortle ();
		Blastoise ();
		Caterpie();
		Metapod();
		Butterfree();
		Weedle();
		Kakuna();
		Beedrill();
		Pidgey();
		Pidgeotto();
		Pidgeot();
		Rattata();
		Raticate();
		Spearow();
		Fearow();
		Ekans();
		Arbok();
	}

	private void init25_52(){
		Pikachu();
		Raichu();
		Sandshrew();
		Sandslash();
		Nidoran_F();
		Nidorina();
		NidoQueen();
		Nidoran_M();
		Nidorino();
		NidoKing();
		Clefairy();
		Clefable();
		Vulpix();
		Ninetails();
		Jigglypuff();
		Wigglytuff();
		Zubat();
		Golbat();
		Oddish();
		Gloom();
		Vileplume();
		Paras();
		Parasect();
		Venonat();
		Venomoth();
		Digglet();
		Dugtrio();
	}

	private void init53_76(){
		Meowth();
		Persian();
		Psyduck();
		Golduck();
		Mankey();
		Primeape();
		Growlithe();
		Arcanine();
		Poliwag();
		Poliwhirl();
		Poliwrath();
		Abra();
		Kadabra();
		Alakazam();
		Machop();
		Machoke();
		Machamp();
		Bellsprout();
		Weepinbell();
		Victreebel();
		Tentacool();
		Tentacruel();
		Geodude();
		Gravler();
		Golem();
	}

	private void init77_101(){
		Ponyta();
		Rapidash();
		Slowpoke();
		Slowbro();
		Magnemite();
		Magneton();
		Farfetchd();
		Doduo();
		Dodrio();
		Seel();
		Dewgong();
		Grimer();
		Muk();
		Shellder();
		Cloyster();
		Gastly();
		Haunter();
		Gengar();
		Onix();
		Drowzee();
		Hypno();
		Krabby();
		Kingler();
		Voltorb();
		Electrode();
	}

	private void init102_126(){
		Exeggcute();
		Exeggutor();
		Cubone();
		Marowak();
		Hitmonlee();
		Hitmonchan();
		Lickitung();
		Koffing();
		Weezing();
		Rhyhorn();
		Rhydon();
		Chansey();
		Tangela();
		Kangaskhan();
		Horsea();
		Seadra();
		Goldeen();
		Seaking();
		Staryu();
		Starmie();
		MrMime();
		Scyther();
		Jynx();
		Electrabuzz();
		Magmar();
	}

	private void init127_151(){
		Pinsir();
		Tauros();
		Magikarp();
		Gyarados();
		Lapras();
		Dittio();
		Eevee();
		Vaporeon();
		Jolteon();
		Flareon();
		Porygon();
		Omanyte();
		Omastar();
		Kabuto();
		Kabutops();
		Aeordactyl();
		Snorlax();
		Articuno();
		Zapdos();
		Moltres();
		Dratini();
		Dragonair();
		Dragonite();
		MewTwo();
		Mew();
	}
	#endregion

	#region Attack Libraries (HM, TM, Library)
	private void HMLibrary(){
		//There are a total of 5 HM moves in Generation 1 Pokemon
		int hm = 50;
		HM_List.Add (new attackIndex (searchAttackList("Cut"), hm));
		HM_List.Add (new attackIndex (searchAttackList("Fly"), hm));
		HM_List.Add (new attackIndex (searchAttackList("Surf"), hm));
		HM_List.Add (new attackIndex (searchAttackList("Strength"), hm));
		HM_List.Add (new attackIndex (searchAttackList("Flash"), hm));
	}

	private void TMLibrary(){
		//There are a total of 50 TM moves in Generation 1 Pokemon
		int tm = 50;
		TM_List.Add (new attackIndex (searchAttackList("Mega Punch"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Razor Wind"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Swords Dance"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Whirlwind"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Mega Kick"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Toxic"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Horn Drill"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Body Slam"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Take Down"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Double Edge"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Bubble Beam"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Water Gun"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Ice Beam"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Blizzard"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Hyper Beam"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Pay Day"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Submission"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Counter"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Seismic Toss"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Rage"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Mega Drain"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Solar Beam"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Dragon Rage"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Thunderbolt"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Thunder"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Earthquake"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Fissure"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Dig"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Psychic"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Teleport"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Mimic"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Double Team"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Reflect"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Bide"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Metronome"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Self Destruct"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Egg Bomb"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Fire Blast"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Swift"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Skull Bash"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Soft Boiled"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Dream Eater"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Sky Attack"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Rest"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Thunder Wave"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Psywave"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Explosion"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Rock Slide"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Tri Attack"),tm));
		TM_List.Add (new attackIndex(searchAttackList("Substitute"),tm));
	}

	private void attackLibrary ()
	{
		//attackList.Add (new attacks ());
		
		#region Attacks 1 - 25
		attackList.Add (new attacks ("Absorb", Grass, Special, 20, 100, 25));
		attackList.Add (new attacks ("Acid", Poison, Special, 40, 100, 30));
		attackList.Add (new attacks ("Acid Armor", Poison, Status, 0, 0, 20));
		attackList.Add (new attacks ("Agility", Psychic, Status, 0, 0, 30));
		attackList.Add (new attacks ("Amnesia", Psychic, Status, 0, 0, 20));
		attackList.Add (new attacks ("Aurora Beam", Ice, Special, 65, 100, 20));
		attackList.Add (new attacks ("Barrage", Normal, Physical, 15, 85, 20));
		attackList.Add (new attacks ("Barrier", Psychic, Status, 0, 0, 20));
		attackList.Add (new attacks ("Bide", Normal, Physical, 0, 0, 10));
		attackList.Add (new attacks ("Bind", Normal, Physical, 15, 85, 20));
		attackList.Add (new attacks ("Bite", Dark, Physical, 60, 100, 25));
		attackList.Add (new attacks ("Blizzard", Ice, Special, 110, 70, 5));
		attackList.Add (new attacks ("Body Slam", Normal, Physical, 85, 100, 15));
		attackList.Add (new attacks ("Bone Club", Ground, Physical, 65, 85, 20));
		attackList.Add (new attacks ("Bonemerang", Ground, Physical, 50, 90, 10));
		attackList.Add (new attacks ("Bubble", Water, Special, 40, 100, 30));
		attackList.Add (new attacks ("Bubble Beam", Water, Special, 65, 100, 20));
		attackList.Add (new attacks ("Clamp", Water, Physical, 35, 85, 10));
		attackList.Add (new attacks ("Comet Punch", Normal, Physical, 18, 85, 15));
		attackList.Add (new attacks ("Confuse Ray", Ghost, Status, 0, 100, 10));
		attackList.Add (new attacks ("Confusion", Physical, Special, 50, 100, 25));
		attackList.Add (new attacks ("Constrict", Normal, Physical, 10, 100, 35));
		attackList.Add (new attacks ("Conversion", Normal, Status, 0, 0, 30));
		attackList.Add (new attacks ("Counter", Fighting, Physical, 0, 100, 20));
		attackList.Add (new attacks ("Crabhammer", Water, Physical, 100, 90, 10));
		#endregion
		
		#region Attacks 26-50
		attackList.Add (new attacks ("Cut", Normal, Physical, 50, 95, 30));
		attackList.Add (new attacks ("Defense Curl", Normal, Status, 0, 0, 40));
		attackList.Add (new attacks ("Dig", Ground, Physical, 80, 100, 10));
		attackList.Add (new attacks ("Disable", Normal, Status, 0, 100, 20));
		attackList.Add (new attacks ("Dizzy Punch", Normal, Physical, 70, 100, 10));
		attackList.Add (new attacks ("Double Kick", Fighting, Physical, 30, 100, 30));
		attackList.Add (new attacks ("Double Slap", Normal, Physical, 15, 85, 10));
		attackList.Add (new attacks ("Double Team", Normal, Status, 0, 0, 15));
		attackList.Add (new attacks ("Double Edge", Normal, Physical, 120, 100, 15));
		attackList.Add (new attacks ("Dragon Rage", Dragon, Special, 0, 100, 10));
		attackList.Add (new attacks ("Dream Eater", Psychic, Special, 100, 100, 15));
		attackList.Add (new attacks ("Drill Peck", Flying, Physical, 80, 100, 20));
		attackList.Add (new attacks ("Earthquake", Ground, Physical, 100, 100, 10));
		attackList.Add (new attacks ("Egg Bomb", Normal, Physical, 100, 75, 10));
		attackList.Add (new attacks ("Ember", Fire, Special, 40, 100, 25));
		attackList.Add (new attacks ("Explosion", Normal, Physical, 250, 100, 5));
		attackList.Add (new attacks ("Fire Blast", Fire, Special, 110, 85, 5));
		attackList.Add (new attacks ("Fire Punch", Fire, Physical, 75, 100, 15));
		attackList.Add (new attacks ("Fire Spin", Fire, Special, 35, 85, 15));
		attackList.Add (new attacks ("Fissure", Ground, Physical, 0, 0, 5));
		attackList.Add (new attacks ("Flamethrower", Fire, Special, 90, 100, 15));
		attackList.Add (new attacks ("Flash", Normal, Status, 0, 100, 20));
		attackList.Add (new attacks ("Fly", Flying, Physical, 90, 95, 15));
		attackList.Add (new attacks ("Focus Energy", Normal, Status, 0, 0, 30));
		attackList.Add (new attacks ("Fury Attack", Normal, Physical, 15, 85, 20));
		#endregion
		
		#region Attacks 51-75
		attackList.Add (new attacks ("Fury Swipes", Normal, Physical, 18, 80, 15));
		attackList.Add (new attacks ("Glare", Normal, Status, 0, 75, 30));
		attackList.Add (new attacks ("Growl", Normal, Status, 0, 100, 40));
		attackList.Add (new attacks ("Growth", Normal, Status, 0, 0, 40));
		attackList.Add (new attacks ("Guillotine", Normal, Physical, 0, 0, 5));
		attackList.Add (new attacks ("Gust", Normal, Special, 40, 100, 35));
		attackList.Add (new attacks ("Harden", Normal, Status, 0, 100, 30));
		attackList.Add (new attacks ("Haze", Ice, Status, 0, 0, 30));
		attackList.Add (new attacks ("Headbutt", Normal, Physical, 70, 100, 15));
		attackList.Add (new attacks ("High Jump Kick", Fighting, Physical, 70, 100, 15));
		attackList.Add (new attacks ("Horn Attack", Normal, Physical, 65, 100, 25));
		attackList.Add (new attacks ("Horn Drill", Normal, Physical, 0, 0, 5));
		attackList.Add (new attacks ("Hydro Pump", Water, Special, 100, 80, 5));
		attackList.Add (new attacks ("Hyper Beam", Normal, Special, 150, 90, 5));
		attackList.Add (new attacks ("Hyper Fang", Normal, Physical, 80, 90, 15));
		attackList.Add (new attacks ("Hypnosis", Psychic, Status, 0, 60, 20));
		attackList.Add (new attacks ("Ice Beam", Ice, Special, 90, 100, 10));
		attackList.Add (new attacks ("Ice Punch", Ice, Physical, 75, 100, 15));
		attackList.Add (new attacks ("Jump Kick", Fighting, Physical, 100, 95, 10));
		attackList.Add (new attacks ("Karate Chop", Fighting, Physical, 50, 100, 25));
		attackList.Add (new attacks ("Kinesis", Psychic, Status, 0, 80, 15));
		attackList.Add (new attacks ("Leech Life", Bug, Physical, 100, 95, 10));
		attackList.Add (new attacks ("Leech Seed", Grass, Status, 0, 90, 10));
		attackList.Add (new attacks ("Leer", Normal, Status, 0, 100, 30));
		attackList.Add (new attacks ("Lick", Ghost, Physical, 30, 100, 30));
		#endregion
		
		#region Attacks 76 - 100
		attackList.Add (new attacks ("Light Screen", Psychic, Status, 0, 0, 30));
		attackList.Add (new attacks ("Lovely Kiss", Normal, Status, 0, 75, 10));
		attackList.Add (new attacks ("Low Kick", Fighting, Physical, 50, 100, 20));
		attackList.Add (new attacks ("Meditate", Psychic, Status, 0, 0, 40));
		attackList.Add (new attacks ("Mega Drain", Grass, Special, 40, 100, 15));
		attackList.Add (new attacks ("Mega Kick", Normal, Physical, 120, 75, 5));
		attackList.Add (new attacks ("Mega Punch", Normal, Physical, 80, 85, 20));
		attackList.Add (new attacks ("Metronome", Normal, Status, 0, 0, 10));
		attackList.Add (new attacks ("Mimic", Normal, Status, 0, 0, 10));
		attackList.Add (new attacks ("Minimize", Normal, Status, 0, 0, 10));
		attackList.Add (new attacks ("Mirror Move", Flying, Status, 0, 0, 20));
		attackList.Add (new attacks ("Mist", Ice, Status, 0, 0, 30));
		attackList.Add (new attacks ("Night Shade", Ghost, Special, 0, 100, 15));
		attackList.Add (new attacks ("Pay Day", Normal, Physical, 40, 100, 20));
		attackList.Add (new attacks ("Peck", Flying, Physical, 35, 100, 35));
		attackList.Add (new attacks ("Petal Dance", Grass, Special, 120, 100, 10));
		attackList.Add (new attacks ("Pin Missile", Bug, Physical, 25, 85, 20));
		attackList.Add (new attacks ("Poison Gas", Poison, Status, 0, 90, 40));
		attackList.Add (new attacks ("Poison Powder", Poison, Status, 0, 75, 35));
		attackList.Add (new attacks ("Poison Sting", Poison, Physical, 15, 100, 35));
		attackList.Add (new attacks ("Pound", Normal, Physical, 40, 100, 35));
		attackList.Add (new attacks ("Psybeam", Psychic, Special, 65, 100, 20));
		attackList.Add (new attacks ("Psychic", Psychic, Special, 90, 100, 10));
		attackList.Add (new attacks ("Psywave", Psychic, Special, 0, 80, 15));
		attackList.Add (new attacks ("Quick Attack", Normal, Physical, 40, 100, 30));
		#endregion
		
		#region Attacks 101-125
		attackList.Add (new attacks ("Rage", Normal, Physical, 20, 100, 20));
		attackList.Add (new attacks ("Razor Leaf", Grass, Physical, 55, 95, 25));
		attackList.Add (new attacks ("Razor Wind", Normal, Special, 80, 100, 10));
		attackList.Add (new attacks ("Recover", Normal, Status, 0, 0, 10));
		attackList.Add (new attacks ("Reflect", Psychic, Status, 0, 0, 20));
		attackList.Add (new attacks ("Rest", Psychic, Status, 0, 0, 10));
		attackList.Add (new attacks ("Roar", Normal, Status, 0, 0, 20));
		attackList.Add (new attacks ("Rock Slide", Rock, Physical, 75, 90, 10));
		attackList.Add (new attacks ("Rock Throw", Rock, Physical, 50, 90, 15));
		attackList.Add (new attacks ("Rolling Kick", Fighting, Physical, 60, 85, 15));
		attackList.Add (new attacks ("Sand Attack", Ground, Status, 0, 100, 15));
		attackList.Add (new attacks ("Scratch", Normal, Physical, 40, 100, 35));
		attackList.Add (new attacks ("Screech", Normal, Status, 0, 85, 40));
		attackList.Add (new attacks ("Seismic Toss", Fighting, Physical, 0, 100, 20));
		attackList.Add (new attacks ("Self Destruct", Normal, Physical, 200, 100, 5));
		attackList.Add (new attacks ("Sharpen", Normal, Status, 0, 0, 30));
		attackList.Add (new attacks ("Sing", Normal, Status, 0, 55, 15));
		attackList.Add (new attacks ("Skull Bash", Normal, Physical, 130, 100, 10));
		attackList.Add (new attacks ("Sky Attack", Flying, Physical, 140, 90, 5));
		attackList.Add (new attacks ("Slam", Normal, Physical, 80, 75, 20));
		attackList.Add (new attacks ("Slash", Normal, Physical, 70, 100, 20));
		attackList.Add (new attacks ("Sleep Powder", Grass, Status, 0, 75, 15));
		attackList.Add (new attacks ("Sludge", Poison, Special, 65, 100, 20));
		attackList.Add (new attacks ("Smog", Poison, Special, 30, 70, 20));
		attackList.Add (new attacks ("SmokeScreen", Normal, Status, 0, 100, 20));
		#endregion
		
		#region Attacks 126-150
		attackList.Add (new attacks ("Soft Boiled", Normal, Status, 0, 100, 20));
		attackList.Add (new attacks ("Solar Beam", Grass, Special, 120, 100, 10));
		attackList.Add (new attacks ("Sonic Boom", Normal, Special, 0, 90, 20));
		attackList.Add (new attacks ("Spike Cannon", Normal, Physical, 20, 100, 15));
		attackList.Add (new attacks ("Splash", Normal, Status, 0, 0, 40));
		attackList.Add (new attacks ("Spore", Grass, Status, 0, 100, 15));
		attackList.Add (new attacks ("Stomp", Normal, Physical, 80, 100, 15));
		attackList.Add (new attacks ("Strength", Normal, Physical, 80, 100, 15));
		attackList.Add (new attacks ("String Shot", Bug, Status, 0, 95, 40));
		attackList.Add (new attacks ("Struggle", Normal, Physical, 50, 100, 0));
		attackList.Add (new attacks ("Stun Spore", Grass, Status, 0, 75, 30));
		attackList.Add (new attacks ("Submission", Fighting, Physical, 80, 80, 25));
		attackList.Add (new attacks ("Substitute", Normal, Status, 0, 0, 10));
		attackList.Add (new attacks ("Super Fang", Normal, Physical, 0, 90, 10));
		attackList.Add (new attacks ("Supersonic", Normal, Status, 0, 55, 20));
		attackList.Add (new attacks ("Surf", Water, Special, 90, 100, 15));
		attackList.Add (new attacks ("Swift", Normal, Special, 60, 100, 20));
		attackList.Add (new attacks ("Swords Dance", Normal, Status, 0, 0, 20));
		attackList.Add (new attacks ("Tackle", Normal, Physical, 50, 100, 35));
		attackList.Add (new attacks ("Tail Whip", Normal, Status, 0, 100, 30));
		attackList.Add (new attacks ("Take Down", Normal, Physical, 90, 85, 20));
		attackList.Add (new attacks ("Teleport", Psychic, Status, 0, 0, 20));
		attackList.Add (new attacks ("Thrash", Normal, Physical, 120, 100, 10));
		attackList.Add (new attacks ("Thunder", Electric, Special, 110, 70, 10));
		attackList.Add (new attacks ("Thunder Punch", Electric, Physical, 75, 100, 15));
		#endregion
		
		#region Attacks 151-165
		attackList.Add (new attacks ("Thunder Shock", Electric, Special, 40, 100, 30));
		attackList.Add (new attacks ("Thunder Wave", Electric, Status, 0, 100, 20));
		attackList.Add (new attacks ("Thunderbolt", Electric, Special, 90, 100, 15));
		attackList.Add (new attacks ("Toxic", Poison, Status, 0, 90, 10));
		attackList.Add (new attacks ("Transform", Normal, Status, 0, 0, 10));
		attackList.Add (new attacks ("Tri Attack", Normal, Special, 80, 100, 10));
		attackList.Add (new attacks ("Twineedle", Bug, Physical, 25, 100, 20));
		attackList.Add (new attacks ("Vice Grip", Normal, Physical, 55, 100, 30));
		attackList.Add (new attacks ("Vine Whip", Grass, Physical, 45, 100, 25));
		attackList.Add (new attacks ("Water Gun", Water, Special, 40, 100, 25));
		attackList.Add (new attacks ("Waterfall", Water, Physical, 80, 100, 15));
		attackList.Add (new attacks ("Whirlwind", Normal, Status, 0, 0, 20));
		attackList.Add (new attacks ("Wing Attack", Flying, Physical, 60, 100, 35));
		attackList.Add (new attacks ("Withdraw", Water, Status, 0, 0, 40));
		attackList.Add (new attacks ("Wrap", Normal, Physical, 15, 90, 20));
		
		#endregion
	}
	#endregion

	#region Pokemon 1-24
	private void Bulbasar ()
	{
		BulbasarAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		BulbasarAttacks.Add (new attackIndex (searchAttackList ("Growl"), 1));
		BulbasarAttacks.Add (new attackIndex (searchAttackList ("Leech Seed"), 7));
		BulbasarAttacks.Add (new attackIndex (searchAttackList ("Vine Whip"), 13));
		BulbasarAttacks.Add (new attackIndex (searchAttackList ("Poison Powder"), 20));
		BulbasarAttacks.Add (new attackIndex (searchAttackList ("Razor Leaf"), 27));
		BulbasarAttacks.Add (new attackIndex (searchAttackList ("Growth"), 34));
		BulbasarAttacks.Add (new attackIndex (searchAttackList ("Sleep Powder"), 41));
		BulbasarAttacks.Add (new attackIndex (searchAttackList ("Solar Beam"), 48));

		int[] tm = {SwordsDance, Toxic, BodySlam, TakeDown, DoubleEdge, Rage, MegaDrain, SolarBeam, Mimic, 
						DoubleTeam, Reflect, Bide, Rest, Substitute};
		searchTMList(BulbasarAttacks, tm);

		int[] hm = {Cut};
		searchHMList(BulbasarAttacks, hm);
		master_attack_list.Add(new masterAttackList("Bulbasar", BulbasarAttacks));
	}
	
	private void Ivysaur ()
	{
		
		IvysaurAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		IvysaurAttacks.Add (new attackIndex (searchAttackList ("Growl"), 1));
		IvysaurAttacks.Add (new attackIndex (searchAttackList ("Leech Seed"), 1));
		IvysaurAttacks.Add (new attackIndex (searchAttackList ("Vine Whip"), 13));
		IvysaurAttacks.Add (new attackIndex (searchAttackList ("Poison Powder"), 22));
		IvysaurAttacks.Add (new attackIndex (searchAttackList ("Razor Leaf"), 30));
		IvysaurAttacks.Add (new attackIndex (searchAttackList ("Growth"), 38));
		IvysaurAttacks.Add (new attackIndex (searchAttackList ("Sleep Powder"), 46));
		IvysaurAttacks.Add (new attackIndex (searchAttackList ("Solar Beam"), 54));

		int[] tm = {SwordsDance, Toxic, BodySlam, TakeDown, DoubleEdge, Rage, MegaDrain, SolarBeam, Mimic, 
			DoubleTeam, Reflect, Bide, Rest, Substitute};
		searchTMList(IvysaurAttacks, tm);
		int[] hm = {Cut};
		searchHMList(IvysaurAttacks, hm);
		master_attack_list.Add(new masterAttackList("Ivysaur", IvysaurAttacks));
	}
	
	private void Venusaur ()
	{
		VenusaurAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		VenusaurAttacks.Add (new attackIndex (searchAttackList ("Growl"), 1));
		VenusaurAttacks.Add (new attackIndex (searchAttackList ("Leech Seed"), 1));
		VenusaurAttacks.Add (new attackIndex (searchAttackList ("Vine Whip"), 13));
		VenusaurAttacks.Add (new attackIndex (searchAttackList ("Poison Powder"), 22));
		VenusaurAttacks.Add (new attackIndex (searchAttackList ("Razor Leaf"), 30));
		VenusaurAttacks.Add (new attackIndex (searchAttackList ("Growth"), 38));
		VenusaurAttacks.Add (new attackIndex (searchAttackList ("Sleep Powder"), 46));
		VenusaurAttacks.Add (new attackIndex (searchAttackList ("Solar Beam"), 54));

		int[] tm = {SwordsDance, Toxic, BodySlam, TakeDown, DoubleEdge, Rage, MegaDrain, SolarBeam, Mimic, 
			DoubleTeam, Reflect, Bide, Rest, Substitute};
		searchTMList(VenusaurAttacks, tm);
		int[] hm = {Cut};
		searchHMList(VenusaurAttacks, hm);

		master_attack_list.Add(new masterAttackList("Venusaur", VenusaurAttacks));
	}

	private void Charmander ()
	{
		CharmanderAttacks.Add (new attackIndex (searchAttackList ("Scratch"), 1));
		CharmanderAttacks.Add (new attackIndex (searchAttackList ("Growl"), 1));
		CharmanderAttacks.Add (new attackIndex (searchAttackList ("Ember"), 9));
		CharmanderAttacks.Add (new attackIndex (searchAttackList ("Leer"), 15));
		CharmanderAttacks.Add (new attackIndex (searchAttackList ("Rage"), 22));
		CharmanderAttacks.Add (new attackIndex (searchAttackList ("Slash"), 30));
		CharmanderAttacks.Add (new attackIndex (searchAttackList ("Flamethrower"), 38));
		CharmanderAttacks.Add (new attackIndex (searchAttackList ("Fire Spin"), 46));

		int[] tm = {MegaPunch, SwordsDance, MegaKick, Toxic, BodySlam, TakeDown, DoubleEdge, 
			Submission, Counter, SeismicToss, Rage, DragonRage,Dig, Mimic, DoubleTeam, Reflect, Bide, FireBlast, Swift,
			SkullBash, Rest, Substitute};

		searchTMList(CharmanderAttacks, tm);
		int[] hm = {Cut, Strength};
		searchHMList(CharmanderAttacks, hm);

		master_attack_list.Add(new masterAttackList("Charmander", CharmanderAttacks));
	}
	
	private void Charmeleon ()
	{
		CharmeleonAttacks.Add (new attackIndex (searchAttackList ("Scratch"), 1));
		CharmeleonAttacks.Add (new attackIndex (searchAttackList ("Growl"), 1));
		CharmeleonAttacks.Add (new attackIndex (searchAttackList ("Ember"), 1));
		CharmeleonAttacks.Add (new attackIndex (searchAttackList ("Leer"), 15));
		CharmeleonAttacks.Add (new attackIndex (searchAttackList ("Rage"), 24));
		CharmeleonAttacks.Add (new attackIndex (searchAttackList ("Slash"), 33));
		CharmeleonAttacks.Add (new attackIndex (searchAttackList ("Flamethrower"), 42));
		CharmeleonAttacks.Add (new attackIndex (searchAttackList ("Fire Spin"), 56));

		int[] tm = {MegaPunch, SwordsDance, MegaKick, Toxic, BodySlam, TakeDown, DoubleEdge, 
			Submission, Counter, SeismicToss, Rage, DragonRage, Dig, Mimic, DoubleTeam, Reflect, Bide, FireBlast, Swift,
			SkullBash, Rest, Substitute};
		searchTMList(CharmeleonAttacks, tm);
		int[] hm = {Cut, Strength};
		searchHMList(CharmeleonAttacks, hm);

		master_attack_list.Add(new masterAttackList("Charmeleon", CharmeleonAttacks));
	}
	
	private void Charizard ()
	{
		CharizardAttacks.Add (new attackIndex (searchAttackList ("Scratch"), 1));
		CharizardAttacks.Add (new attackIndex (searchAttackList ("Growl"), 1));
		CharizardAttacks.Add (new attackIndex (searchAttackList ("Ember"), 1));
		CharizardAttacks.Add (new attackIndex (searchAttackList ("Leer"), 15));
		CharizardAttacks.Add (new attackIndex (searchAttackList ("Rage"), 24));
		CharizardAttacks.Add (new attackIndex (searchAttackList ("Slash"), 36));
		CharizardAttacks.Add (new attackIndex (searchAttackList ("Flamethrower"), 46));
		CharizardAttacks.Add (new attackIndex (searchAttackList ("Fire Spin"), 55));

		int[] tm = {MegaPunch, SwordsDance, MegaKick, Toxic, BodySlam, TakeDown, DoubleEdge, HyperBeam,
			Submission, Counter, SeismicToss, Rage, DragonRage, Earthquake, Fissure, Dig, Mimic, DoubleTeam, Reflect, 
			Bide, FireBlast, Swift,	SkullBash, Rest, Substitute};
		searchTMList(CharmeleonAttacks, tm);
		int[] hm = {Cut, Fly, Strength};
		searchHMList(CharmeleonAttacks, hm);

		master_attack_list.Add(new masterAttackList("Charizard", CharizardAttacks));
	}

	private void Squirtle ()
	{
		SquirtleAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		SquirtleAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 1));
		SquirtleAttacks.Add (new attackIndex (searchAttackList ("Bubble"), 8));
		SquirtleAttacks.Add (new attackIndex (searchAttackList ("Water Gun"), 15));
		SquirtleAttacks.Add (new attackIndex (searchAttackList ("Bite"), 22));
		SquirtleAttacks.Add (new attackIndex (searchAttackList ("Withdraw"), 28));
		SquirtleAttacks.Add (new attackIndex (searchAttackList ("Skull Bash"), 35));
		SquirtleAttacks.Add (new attackIndex (searchAttackList ("Hydro Pump"), 42));

		int[] tm = {MegaPunch, MegaKick, Toxic, BodySlam, TakeDown, DoubleEdge, BubbleBeam,IceBeam,Blizzard,
			Submission, Counter, SeismicToss, Rage, Dig, Mimic, DoubleTeam, Reflect, 
			Bide, SkullBash, Rest, Substitute};
		searchTMList(SquirtleAttacks, tm);
		int[] hm = {Surf, Strength};
		searchHMList(SquirtleAttacks, hm);
		master_attack_list.Add(new masterAttackList("Squirtle", SquirtleAttacks));
	}
	
	private void Wartortle ()
	{
		WartortleAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		WartortleAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 1));
		WartortleAttacks.Add (new attackIndex (searchAttackList ("Bubble"), 1));
		WartortleAttacks.Add (new attackIndex (searchAttackList ("Water Gun"), 15));
		WartortleAttacks.Add (new attackIndex (searchAttackList ("Bite"), 24));
		WartortleAttacks.Add (new attackIndex (searchAttackList ("Withdraw"), 31));
		WartortleAttacks.Add (new attackIndex (searchAttackList ("Skull Bash"), 39));
		WartortleAttacks.Add (new attackIndex (searchAttackList ("Hydro Pump"), 47));

		int[] tm = {MegaPunch, MegaKick, Toxic, BodySlam, TakeDown, DoubleEdge, BubbleBeam,IceBeam,Blizzard,
			Submission, Counter, SeismicToss, Rage, Dig, Mimic, DoubleTeam, Reflect, 
			Bide, SkullBash, Rest, Substitute};
		searchTMList(WartortleAttacks, tm);
		int[] hm = {Surf, Strength};
		searchHMList(WartortleAttacks, hm);
		master_attack_list.Add(new masterAttackList("Wartortle", WartortleAttacks));
	}
	
	private void Blastoise ()
	{
		BlastoiseAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		BlastoiseAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 1));
		BlastoiseAttacks.Add (new attackIndex (searchAttackList ("Bubble"), 1));
		BlastoiseAttacks.Add (new attackIndex (searchAttackList ("Water Gun"), 1));
		BlastoiseAttacks.Add (new attackIndex (searchAttackList ("Bite"), 24));
		BlastoiseAttacks.Add (new attackIndex (searchAttackList ("Withdraw"), 31));
		BlastoiseAttacks.Add (new attackIndex (searchAttackList ("Skull Bash"), 42));
		BlastoiseAttacks.Add (new attackIndex (searchAttackList ("Hydro Pump"), 52));
		
		int[] tm = {MegaPunch, MegaKick, Toxic, BodySlam, TakeDown, DoubleEdge, BubbleBeam,IceBeam,Blizzard, HyperBeam,
			Submission, Counter, SeismicToss, Rage,Earthquake,Fissure, Dig, Mimic, DoubleTeam, Reflect, 
			Bide, SkullBash, Rest, Substitute};
		searchTMList(BlastoiseAttacks, tm);
		int[] hm = {Surf, Strength};
		searchHMList(BlastoiseAttacks, hm);
		master_attack_list.Add(new masterAttackList("Blastoise", BlastoiseAttacks));
	}

	private void Caterpie ()
	{
		CaterpieAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		CaterpieAttacks.Add (new attackIndex (searchAttackList ("String Shot"), 1));
		master_attack_list.Add(new masterAttackList("Caterpie", CaterpieAttacks));
	}
	
	private void Metapod ()
	{
		MetapodAttacks.Add (new attackIndex (searchAttackList ("Harden"), 1));
		master_attack_list.Add(new masterAttackList("Metapod", MetapodAttacks));
	}
	
	private void Butterfree ()
	{
		ButterfreeAttacks.Add (new attackIndex (searchAttackList ("Confusion"), 1));
		ButterfreeAttacks.Add (new attackIndex (searchAttackList ("Poison Powder"), 15));
		ButterfreeAttacks.Add (new attackIndex (searchAttackList ("Stun Spore"), 16));
		ButterfreeAttacks.Add (new attackIndex (searchAttackList ("Sleep Powder"), 17));
		ButterfreeAttacks.Add (new attackIndex (searchAttackList ("Supersonic"), 21));
		ButterfreeAttacks.Add (new attackIndex (searchAttackList ("Whirlwind"), 26));
		ButterfreeAttacks.Add (new attackIndex (searchAttackList ("Psybeam"), 32));

		int[] tm = {RazorWind,Whirlwind, Toxic, TakeDown, DoubleEdge,HyperBeam,Rage,MegaDrain,SolarBeam,Psychic_TM,Teleport,
			Mimic, DoubleTeam, Reflect, Bide, Swift, Rest,Psywave, Substitute};
		searchTMList(ButterfreeAttacks, tm);
		master_attack_list.Add(new masterAttackList("Butterfree", ButterfreeAttacks));
	}

	private void Weedle ()
	{
		WeedleAttacks.Add (new attackIndex (searchAttackList ("Poison Sting"), 1));
		WeedleAttacks.Add (new attackIndex (searchAttackList ("String Shot"), 1));
		master_attack_list.Add(new masterAttackList("Weedle", WeedleAttacks));
	}
	
	private void Kakuna ()
	{
		KakunaAttacks.Add (new attackIndex (searchAttackList ("Harden"), 1));
		master_attack_list.Add(new masterAttackList("Kakuna", KakunaAttacks));
	}
	
	private void Beedrill ()
	{
		BeedrillAttacks.Add (new attackIndex (searchAttackList ("Fury Attack"), 1));
		BeedrillAttacks.Add (new attackIndex (searchAttackList ("Focus Energy"), 1));
		BeedrillAttacks.Add (new attackIndex (searchAttackList ("Twineedle"), 1));
		BeedrillAttacks.Add (new attackIndex (searchAttackList ("Rage"), 1));
		BeedrillAttacks.Add (new attackIndex (searchAttackList ("Pin Missile"), 1));
		BeedrillAttacks.Add (new attackIndex (searchAttackList ("Agility"), 1));

		int[] tm = {SwordsDance, Toxic, TakeDown, DoubleEdge,HyperBeam,Rage,MegaDrain,
			Mimic, DoubleTeam, Reflect, Bide, Swift,SkullBash, Rest,Psywave, Substitute};
		searchTMList(BeedrillAttacks, tm);
		int[] hm = {Cut};
		searchHMList (BeedrillAttacks, hm);
		master_attack_list.Add(new masterAttackList("Beedrill", BeedrillAttacks));
	}

	private void Pidgey ()
	{
		PidgeyAttacks.Add (new attackIndex (searchAttackList ("Gust"), 1));
		PidgeyAttacks.Add (new attackIndex (searchAttackList ("Sand Attack"), 5));
		PidgeyAttacks.Add (new attackIndex (searchAttackList ("Quick Attack"), 12));
		PidgeyAttacks.Add (new attackIndex (searchAttackList ("Whirlwind"), 19));
		PidgeyAttacks.Add (new attackIndex (searchAttackList ("Wing Attack"), 28));
		PidgeyAttacks.Add (new attackIndex (searchAttackList ("Agility"), 36));
		PidgeyAttacks.Add (new attackIndex (searchAttackList ("Mirror Move"), 44));

		int[] tm = {RazorWind,Whirlwind, Toxic, TakeDown, DoubleEdge,Rage,
			Mimic, DoubleTeam, Reflect, Bide, Swift,SkyAttack, Rest, Substitute};
		searchTMList(PidgeyAttacks, tm);
		int[] hm = {Fly};
		searchHMList (PidgeyAttacks, hm);
		master_attack_list.Add(new masterAttackList("Pidgey", PidgeyAttacks));
	}
	
	private void Pidgeotto ()
	{
		PidgeottoAttacks.Add (new attackIndex (searchAttackList ("Gust"), 1));
		PidgeottoAttacks.Add (new attackIndex (searchAttackList ("Sand Attack"), 5));
		PidgeottoAttacks.Add (new attackIndex (searchAttackList ("Quick Attack"), 13));
		PidgeottoAttacks.Add (new attackIndex (searchAttackList ("Whirlwind"), 21));
		PidgeottoAttacks.Add (new attackIndex (searchAttackList ("Wing Attack"), 31));
		PidgeottoAttacks.Add (new attackIndex (searchAttackList ("Agility"), 40));
		PidgeottoAttacks.Add (new attackIndex (searchAttackList ("Mirror Move"), 49));

		int[] tm = {RazorWind,Whirlwind, Toxic, TakeDown, DoubleEdge,Rage,
			Mimic, DoubleTeam, Reflect, Bide, Swift,SkyAttack, Rest, Substitute};
		searchTMList(PidgeottoAttacks, tm);
		int[] hm = {Fly};
		searchHMList (PidgeottoAttacks, hm);
		master_attack_list.Add(new masterAttackList("Pidgeotto", PidgeottoAttacks));
	}
	
	private void Pidgeot ()
	{
		PidgeotAttacks.Add (new attackIndex (searchAttackList ("Gust"), 1));
		PidgeotAttacks.Add (new attackIndex (searchAttackList ("Sand Attack"), 1));
		PidgeotAttacks.Add (new attackIndex (searchAttackList ("Quick Attack"), 1));
		PidgeotAttacks.Add (new attackIndex (searchAttackList ("Whirlwind"), 21));
		PidgeotAttacks.Add (new attackIndex (searchAttackList ("Wing Attack"), 31));
		PidgeotAttacks.Add (new attackIndex (searchAttackList ("Agility"), 44));
		PidgeotAttacks.Add (new attackIndex (searchAttackList ("Mirror Move"), 54));

		int[] tm = {RazorWind,Whirlwind, Toxic, TakeDown, DoubleEdge,Rage,
			Mimic, DoubleTeam, Reflect, Bide, Swift,SkyAttack, Rest, Substitute};
		searchTMList(PidgeotAttacks, tm);
		int[] hm = {Fly};
		searchHMList (PidgeotAttacks, hm);
		master_attack_list.Add(new masterAttackList("Pidgeot", PidgeotAttacks));
	}

	private void Rattata ()
	{
		RattataAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		RattataAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 1));
		RattataAttacks.Add (new attackIndex (searchAttackList ("Quick Attack"), 7));
		RattataAttacks.Add (new attackIndex (searchAttackList ("Hyper Fang"), 14));
		RattataAttacks.Add (new attackIndex (searchAttackList ("Focus Energy"), 23));
		RattataAttacks.Add (new attackIndex (searchAttackList ("Super Fang"), 34));

		int[] tm = {Toxic, BodySlam,TakeDown, DoubleEdge,WaterGun,BubbleBeam,Blizzard, Rage,ThunderBolt,Thunder,
			Mimic, DoubleTeam, Bide, Swift,SkullBash, Rest, Substitute};
		searchTMList(RattataAttacks, tm);
		master_attack_list.Add(new masterAttackList("Rattata", RattataAttacks));
	}
	
	private void Raticate ()
	{
		RaticateAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		RaticateAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 1));
		RaticateAttacks.Add (new attackIndex (searchAttackList ("Quick Attack"), 7));
		RaticateAttacks.Add (new attackIndex (searchAttackList ("Hyper Fang"), 14));
		RaticateAttacks.Add (new attackIndex (searchAttackList ("Focus Energy"), 27));
		RaticateAttacks.Add (new attackIndex (searchAttackList ("Super Fang"), 41));

		int[] tm = {Toxic, BodySlam,TakeDown, DoubleEdge,WaterGun,BubbleBeam,IceBeam, Blizzard,HyperBeam,
			Rage,ThunderBolt,Thunder,Dig,Mimic, DoubleTeam, Bide, Swift,SkullBash, Rest, Substitute};
		searchTMList(RaticateAttacks, tm);
		master_attack_list.Add(new masterAttackList("Raticate", RaticateAttacks));
	}
	
	private void Spearow ()
	{
		SpearowAttacks.Add (new attackIndex (searchAttackList ("Growl"), 1));
		SpearowAttacks.Add (new attackIndex (searchAttackList ("Peck"), 1));
		SpearowAttacks.Add (new attackIndex (searchAttackList ("Leer"), 9));
		SpearowAttacks.Add (new attackIndex (searchAttackList ("Fury Attack"), 15));
		SpearowAttacks.Add (new attackIndex (searchAttackList ("Mirror Move"), 22));
		SpearowAttacks.Add (new attackIndex (searchAttackList ("Drill Peck"), 29));
		SpearowAttacks.Add (new attackIndex (searchAttackList ("Agility"), 39));

		int[] tm = {RazorWind,Whirlwind, Toxic, TakeDown, DoubleEdge,Rage,
			Mimic, Bide, Swift,SkyAttack, Rest, Substitute};
		searchTMList(SpearowAttacks, tm);
		int[] hm = {Fly};
		searchHMList (SpearowAttacks, hm);
		master_attack_list.Add(new masterAttackList("Spearow", SpearowAttacks));
	}
	
	private void Fearow ()
	{
		FearowAttacks.Add (new attackIndex (searchAttackList ("Growl"), 1));
		FearowAttacks.Add (new attackIndex (searchAttackList ("Peck"), 1));
		FearowAttacks.Add (new attackIndex (searchAttackList ("Leer"), 1));
		FearowAttacks.Add (new attackIndex (searchAttackList ("Fury Attack"), 15));
		FearowAttacks.Add (new attackIndex (searchAttackList ("Mirror Move"), 25));
		FearowAttacks.Add (new attackIndex (searchAttackList ("Drill Peck"), 34));
		FearowAttacks.Add (new attackIndex (searchAttackList ("Agility"), 43));

		int[] tm = {RazorWind,Whirlwind, Toxic, TakeDown, DoubleEdge,HyperBeam, Rage,
			Mimic, Bide, Swift,SkyAttack, Rest, Substitute};
		searchTMList(FearowAttacks, tm);
		int[] hm = {Fly};
		searchHMList (FearowAttacks, hm);
		master_attack_list.Add(new masterAttackList("Fearow", FearowAttacks));
	}
	
	private void Ekans ()
	{
		EkansAttacks.Add (new attackIndex (searchAttackList ("Leer"), 1));
		EkansAttacks.Add (new attackIndex (searchAttackList ("Wrap"), 1));
		EkansAttacks.Add (new attackIndex (searchAttackList ("Poison Sting"), 10));
		EkansAttacks.Add (new attackIndex (searchAttackList ("Bite"), 17));
		EkansAttacks.Add (new attackIndex (searchAttackList ("Glare"), 24));
		EkansAttacks.Add (new attackIndex (searchAttackList ("Screech"), 31));
		EkansAttacks.Add (new attackIndex (searchAttackList ("Acid"), 38));

		int[] tm = {Toxic, BodySlam,TakeDown, DoubleEdge, Rage,MegaDrain,Earthquake,Fissure,
			Dig,Mimic, DoubleTeam, Bide,SkullBash, Rest,RockSlide, Substitute};
		searchTMList(EkansAttacks, tm);
		int[] hm = {Strength};
		searchHMList(EkansAttacks, hm);
		master_attack_list.Add(new masterAttackList("Ekans", EkansAttacks));
	}
	
	private void Arbok ()
	{
		ArbokAttacks.Add (new attackIndex (searchAttackList ("Leer"), 1));
		ArbokAttacks.Add (new attackIndex (searchAttackList ("Wrap"), 1));
		ArbokAttacks.Add (new attackIndex (searchAttackList ("Poison Sting"), 1));
		ArbokAttacks.Add (new attackIndex (searchAttackList ("Bite"), 17));
		ArbokAttacks.Add (new attackIndex (searchAttackList ("Glare"), 27));
		ArbokAttacks.Add (new attackIndex (searchAttackList ("Screech"), 36));
		ArbokAttacks.Add (new attackIndex (searchAttackList ("Acid"), 47));

		int[] tm = {Toxic, BodySlam,TakeDown, DoubleEdge, Rage,MegaDrain,Earthquake,Fissure,
			Dig,Mimic, DoubleTeam, Bide,SkullBash, Rest,RockSlide, Substitute};
		searchTMList(ArbokAttacks, tm);
		int[] hm = {Strength};
		searchHMList(ArbokAttacks, hm);
		master_attack_list.Add(new masterAttackList("Arbok", ArbokAttacks));
	}
	#endregion
	
	#region Pokemon 25-51
	
	public void Pikachu ()
	{
		PikachuAttacks.Add (new attackIndex (searchAttackList ("Growl"), 1));
		PikachuAttacks.Add (new attackIndex (searchAttackList ("Thunder Shock"), 1));
		PikachuAttacks.Add (new attackIndex (searchAttackList ("Thunder Wave"), 9));
		PikachuAttacks.Add (new attackIndex (searchAttackList ("Quick Attack"), 16));
		PikachuAttacks.Add (new attackIndex (searchAttackList ("Swift"), 26));
		PikachuAttacks.Add (new attackIndex (searchAttackList ("Agility"), 33));
		PikachuAttacks.Add (new attackIndex (searchAttackList ("Thunder"), 43));

		int[] tm = {MegaPunch, MegaKick, Toxic, BodySlam,DoubleEdge,Payday,
			Submission, Counter, SeismicToss, Rage,ThunderBolt,Thunder,Mimic, DoubleTeam, Reflect, 
			Bide, Swift, SkullBash, Rest,ThunderWave, Substitute};
		searchTMList(PikachuAttacks, tm);
		int[] hm = {Flash};
		searchHMList(PikachuAttacks, hm);
		master_attack_list.Add(new masterAttackList("Pikachu", PikachuAttacks));
	}
	
	public void Raichu ()
	{
		RaichuAttacks.Add (new attackIndex (searchAttackList ("Growl"), 1));
		RaichuAttacks.Add (new attackIndex (searchAttackList ("Thunder Shock"), 1));
		RaichuAttacks.Add (new attackIndex (searchAttackList ("Thunder Wave"), 1));

		int[] tm = {MegaPunch, MegaKick, Toxic, BodySlam,DoubleEdge,HyperBeam, Payday,
			Submission,Counter, SeismicToss, Rage,ThunderBolt,Thunder,Mimic, DoubleTeam, Reflect, 
			Bide, Swift, SkullBash, Rest,ThunderWave, Substitute};
		searchTMList(RaichuAttacks, tm);
		int[] hm = {Flash};
		searchHMList(RaichuAttacks, hm);
		master_attack_list.Add(new masterAttackList("Raichu", RaichuAttacks));
	}
	
	public void Sandshrew ()
	{
		SandshrewAttacks.Add (new attackIndex (searchAttackList ("Scratch"), 1));
		SandshrewAttacks.Add (new attackIndex (searchAttackList ("Sand Attack"), 10));
		SandshrewAttacks.Add (new attackIndex (searchAttackList ("Slash"), 17));
		SandshrewAttacks.Add (new attackIndex (searchAttackList ("Poison Sting"), 24));
		SandshrewAttacks.Add (new attackIndex (searchAttackList ("Swift"), 31));
		SandshrewAttacks.Add (new attackIndex (searchAttackList ("Fury Swipes"), 38));

		int[] tm = {SwordsDance, Toxic, BodySlam,DoubleEdge,Submission, SeismicToss, Rage,Earthquake,
			Fissure,Dig,Mimic, DoubleTeam,Bide, Swift, SkullBash, Rest,RockSlide, Substitute};
		searchTMList(SandshrewAttacks, tm);
		int[] hm = {Cut,Strength};
		searchHMList(SandshrewAttacks, hm);
		master_attack_list.Add(new masterAttackList("Sandshrew", SandshrewAttacks));
	}
	
	public void Sandslash ()
	{
		SandslashAttacks.Add (new attackIndex (searchAttackList ("Scratch"), 1));
		SandslashAttacks.Add (new attackIndex (searchAttackList ("Sand Attack"), 1));
		SandslashAttacks.Add (new attackIndex (searchAttackList ("Slash"), 17));
		SandslashAttacks.Add (new attackIndex (searchAttackList ("Poison Sting"), 27));
		SandslashAttacks.Add (new attackIndex (searchAttackList ("Swift"), 36));
		SandslashAttacks.Add (new attackIndex (searchAttackList ("Fury Swipes"), 47));

		int[] tm = {SwordsDance, Toxic, BodySlam,DoubleEdge,HyperBeam, Submission, SeismicToss, Rage,Earthquake,
			Fissure,Dig,Mimic, DoubleTeam,Bide, Swift, SkullBash, Rest,RockSlide, Substitute};
		searchTMList(SandshrewAttacks, tm);
		int[] hm = {Cut,Strength};
		searchHMList(SandshrewAttacks, hm);
		master_attack_list.Add(new masterAttackList("Sandslash", SandslashAttacks));
	}
	
	public void Nidoran_F ()
	{
		Nidoran_FAttacks.Add (new attackIndex (searchAttackList ("Growl"), 1));
		Nidoran_FAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		Nidoran_FAttacks.Add (new attackIndex (searchAttackList ("Scratch"), 8));
		Nidoran_FAttacks.Add (new attackIndex (searchAttackList ("Poison Sting"), 14));
		Nidoran_FAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 21));
		Nidoran_FAttacks.Add (new attackIndex (searchAttackList ("Bite"), 29));
		Nidoran_FAttacks.Add (new attackIndex (searchAttackList ("Fury Swipes"), 36));
		Nidoran_FAttacks.Add (new attackIndex (searchAttackList ("Double Kick"), 43));

		int[] tm = {SwordsDance, Toxic, BodySlam,DoubleEdge,Blizzard,Rage, Mimic, DoubleTeam,Reflect, Bide,SkullBash,
			Rest, Substitute};
		searchTMList(Nidoran_FAttacks, tm);
		master_attack_list.Add(new masterAttackList("Nidoran_F", Nidoran_FAttacks));
	}
	
	public void Nidorina ()
	{
		NidorinaAttacks.Add (new attackIndex (searchAttackList ("Growl"), 1));
		NidorinaAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		NidorinaAttacks.Add (new attackIndex (searchAttackList ("Scratch"), 1));
		NidorinaAttacks.Add (new attackIndex (searchAttackList ("Poison Sting"), 14));
		NidorinaAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 23));
		NidorinaAttacks.Add (new attackIndex (searchAttackList ("Bite"), 32));
		NidorinaAttacks.Add (new attackIndex (searchAttackList ("Fury Swipes"), 41));
		NidorinaAttacks.Add (new attackIndex (searchAttackList ("Double Kick"), 50));

		int[] tm = {Toxic,HornDrill, BodySlam,TakeDown, DoubleEdge,WaterGun,IceBeam, Blizzard,Rage,ThunderBolt,Thunder,
			Mimic, DoubleTeam,Reflect, Bide,SkullBash, Rest, Substitute};
		searchTMList(NidorinaAttacks, tm);
		master_attack_list.Add(new masterAttackList("Nidorina", NidorinaAttacks));
	}
	
	public void NidoQueen ()
	{
		NidoqueenAttacks.Add (new attackIndex (searchAttackList ("Scratch"), 1));
		NidoqueenAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		NidoqueenAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 1));
		NidoqueenAttacks.Add (new attackIndex (searchAttackList ("Body Slam"), 1));
		NidoqueenAttacks.Add (new attackIndex (searchAttackList ("Poison Sting"), 14));

		int[] tm = {MegaPunch,MegaKick, Toxic,HornDrill, BodySlam,TakeDown, DoubleEdge,WaterGun,IceBeam, Blizzard,
			HyperBeam,Payday,Submission,Counter,SeismicToss,ThunderBolt,Thunder,Earthquake,	Mimic, DoubleTeam,Reflect,
			Bide,FireBlast, SkullBash,RockSlide, Rest, Substitute};
		searchTMList(NidoqueenAttacks, tm);
		int[] hm = {Surf,Strength};
		searchHMList(NidoqueenAttacks,hm);
		master_attack_list.Add(new masterAttackList("Nidoqueen", NidoqueenAttacks));
	}
	
	public void Nidoran_M ()
	{
		Nidoran_MAttacks.Add (new attackIndex (searchAttackList ("Leer"), 1));
		Nidoran_MAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		Nidoran_MAttacks.Add (new attackIndex (searchAttackList ("Horn Attack"), 8));
		Nidoran_MAttacks.Add (new attackIndex (searchAttackList ("Poison Sting"), 14));
		Nidoran_MAttacks.Add (new attackIndex (searchAttackList ("Focus Energy"), 21));
		Nidoran_MAttacks.Add (new attackIndex (searchAttackList ("Fury Attack"), 29));
		Nidoran_MAttacks.Add (new attackIndex (searchAttackList ("Horn Drill"), 36));
		Nidoran_MAttacks.Add (new attackIndex (searchAttackList ("Double Kick"), 43));

		int[] tm = {Toxic, BodySlam,TakeDown, DoubleEdge,Blizzard,Rage,ThunderBolt,Thunder, Mimic, DoubleTeam,Reflect, 
			Bide,SkullBash,	Rest, Substitute};
		searchTMList(Nidoran_MAttacks, tm);
		master_attack_list.Add(new masterAttackList("Nidoran_M", Nidoran_MAttacks));
	}
	
	public void Nidorino ()
	{
		NidorinoAttacks.Add (new attackIndex (searchAttackList ("Growl"), 1));
		NidorinoAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		NidorinoAttacks.Add (new attackIndex (searchAttackList ("Scratch"), 1));
		NidorinoAttacks.Add (new attackIndex (searchAttackList ("Poison Sting"), 14));
		NidorinoAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 23));
		NidorinoAttacks.Add (new attackIndex (searchAttackList ("Bite"), 32));
		NidorinoAttacks.Add (new attackIndex (searchAttackList ("Fury Swipes"), 41));
		NidorinoAttacks.Add (new attackIndex (searchAttackList ("Double Kick"), 50));

		int[] tm = {Toxic,HornDrill, BodySlam,TakeDown, DoubleEdge,WaterGun,BubbleBeam, IceBeam, Blizzard,Rage,ThunderBolt,Thunder,
			Mimic, DoubleTeam,Reflect, Bide,SkullBash, Rest, Substitute};
		searchTMList(NidorinoAttacks, tm);	
		master_attack_list.Add(new masterAttackList("Nidorino", NidorinoAttacks));
	}
	
	public void NidoKing ()
	{
		NidokingAttacks.Add (new attackIndex (searchAttackList ("Horn Attack"), 1));
		NidokingAttacks.Add (new attackIndex (searchAttackList ("Poison Sting"), 1));
		NidokingAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		NidokingAttacks.Add (new attackIndex (searchAttackList ("Thrash"), 1));

		int[] tm = {MegaPunch,MegaKick, Toxic,HornDrill, BodySlam,TakeDown, DoubleEdge,WaterGun,IceBeam, Blizzard,
			HyperBeam,Payday,Submission,Counter,SeismicToss,ThunderBolt,Thunder,Earthquake,Fissure,	Mimic, DoubleTeam,
			Reflect,Bide,FireBlast, SkullBash,RockSlide, Rest, Substitute};
		searchTMList(NidokingAttacks, tm);
		int[] hm = {Surf,Strength};
		searchHMList(NidokingAttacks,hm);
		master_attack_list.Add(new masterAttackList("Nidoking", NidoqueenAttacks));
	}
	
	public void Clefairy ()
	{
		ClefairyAttacks.Add (new attackIndex (searchAttackList ("Growl"), 1));
		ClefairyAttacks.Add (new attackIndex (searchAttackList ("Pound"), 1));
		ClefairyAttacks.Add (new attackIndex (searchAttackList ("Sing"), 13));
		ClefairyAttacks.Add (new attackIndex (searchAttackList ("Double Slap"), 1));
		ClefairyAttacks.Add (new attackIndex (searchAttackList ("Minimize"), 24));
		ClefairyAttacks.Add (new attackIndex (searchAttackList ("Metronome"), 31));
		ClefairyAttacks.Add (new attackIndex (searchAttackList ("Defense Curl"), 39));
		ClefairyAttacks.Add (new attackIndex (searchAttackList ("Light Screen"), 48));

		int[] tm = {MegaPunch,MegaKick, Toxic, BodySlam,TakeDown, DoubleEdge,WaterGun,IceBeam, Blizzard,
			Submission,Counter,SeismicToss,Rage,SolarBeam, ThunderBolt,Thunder,Psychic_TM,Teleport,Mimic, DoubleTeam,
			Reflect,Bide,Metronome, FireBlast, SkullBash,Rest,ThunderWave,Psywave,TriAttack, Substitute};
		searchTMList(ClefairyAttacks, tm);
		int[] hm = {Strength,Flash};
		searchHMList(ClefairyAttacks,hm);
		master_attack_list.Add(new masterAttackList("Clefairy", ClefairyAttacks));
	}
	
	public void Clefable ()
	{
		ClefableAttacks.Add (new attackIndex (searchAttackList ("Sing"), 1));
		ClefableAttacks.Add (new attackIndex (searchAttackList ("Double Slap"), 1));
		ClefableAttacks.Add (new attackIndex (searchAttackList ("Minimize"), 1));
		ClefableAttacks.Add (new attackIndex (searchAttackList ("Metronome"), 1));

		int[] tm = {MegaPunch,MegaKick, Toxic, BodySlam,TakeDown, DoubleEdge,WaterGun,IceBeam, Blizzard, HyperBeam,
			Submission,Counter,SeismicToss,Rage,SolarBeam, ThunderBolt,Thunder,Psychic_TM,Teleport,Mimic, DoubleTeam,
			Reflect,Bide,Metronome, FireBlast, SkullBash,Rest,ThunderWave,Psywave,TriAttack, Substitute};
		searchTMList(ClefableAttacks, tm);
		int[] hm = {Strength,Flash};
		searchHMList(ClefableAttacks,hm);
		master_attack_list.Add(new masterAttackList("Clefable", ClefableAttacks));
	}
	
	public void Vulpix ()
	{
		VulpixAttacks.Add (new attackIndex (searchAttackList ("Ember"), 1));
		VulpixAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 1));
		VulpixAttacks.Add (new attackIndex (searchAttackList ("Quick Attack"), 16));
		VulpixAttacks.Add (new attackIndex (searchAttackList ("Roar"), 21));
		VulpixAttacks.Add (new attackIndex (searchAttackList ("Confuse Ray"), 28));
		VulpixAttacks.Add (new attackIndex (searchAttackList ("Flamethrower"), 35));
		VulpixAttacks.Add (new attackIndex (searchAttackList ("Fire Spin"), 42));

		int[] tm = {Toxic, BodySlam,TakeDown, DoubleEdge,Rage,Dig,Mimic, DoubleTeam, Reflect,Bide,FireBlast, Swift,Substitute};
		searchTMList(VulpixAttacks, tm);
		master_attack_list.Add(new masterAttackList("Vulpix", VulpixAttacks));
	}
	
	public void Ninetails ()
	{
		NinetailsAttacks.Add (new attackIndex (searchAttackList ("Ember"), 1));
		NinetailsAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 1));
		NinetailsAttacks.Add (new attackIndex (searchAttackList ("Quick Attack"), 1));
		NinetailsAttacks.Add (new attackIndex (searchAttackList ("Roar"), 1));

		int[] tm = {Toxic, BodySlam,TakeDown, DoubleEdge,HyperBeam, Rage,Dig,Mimic, DoubleTeam, Reflect,
			Bide,FireBlast, Swift,Substitute};
		searchTMList(NinetailsAttacks, tm);
		master_attack_list.Add(new masterAttackList("Ninetails", NinetailsAttacks));
	}
	
	public void Jigglypuff ()
	{
		JigglypuffAttacks.Add (new attackIndex (searchAttackList ("Sing"), 1));
		JigglypuffAttacks.Add (new attackIndex (searchAttackList ("Pound"), 9));
		JigglypuffAttacks.Add (new attackIndex (searchAttackList ("Disable"), 14));
		JigglypuffAttacks.Add (new attackIndex (searchAttackList ("Defense Curl"), 19));
		JigglypuffAttacks.Add (new attackIndex (searchAttackList ("Double Slap"), 24));
		JigglypuffAttacks.Add (new attackIndex (searchAttackList ("Rest"), 29));
		JigglypuffAttacks.Add (new attackIndex (searchAttackList ("Body Slam"), 34));
		JigglypuffAttacks.Add (new attackIndex (searchAttackList ("Double Edge"), 39));

		int[] tm = {MegaPunch,MegaKick, Toxic, BodySlam,TakeDown, DoubleEdge,BubbleBeam, WaterGun,IceBeam, Blizzard,
			Submission,Counter,SeismicToss,Rage,SolarBeam, ThunderBolt,Thunder,Psychic_TM,Teleport,Mimic, DoubleTeam,
			Reflect,Bide,Metronome, FireBlast, SkullBash,Rest,ThunderWave,Psywave,TriAttack, Substitute};
		searchTMList(JigglypuffAttacks, tm);
		int[] hm = {Strength,Flash};
		searchHMList(JigglypuffAttacks,hm);
		master_attack_list.Add(new masterAttackList("Jigglypuff", JigglypuffAttacks));
	}
	
	public void Wigglytuff ()
	{
		WigglytuffAttacks.Add (new attackIndex (searchAttackList ("Sing"), 1));
		WigglytuffAttacks.Add (new attackIndex (searchAttackList ("Disable"), 1));
		WigglytuffAttacks.Add (new attackIndex (searchAttackList ("Defense Curl"), 1));
		WigglytuffAttacks.Add (new attackIndex (searchAttackList ("Double Slap"), 1));

		int[] tm = {MegaPunch,MegaKick, Toxic, BodySlam,TakeDown, DoubleEdge,BubbleBeam, WaterGun,IceBeam, Blizzard,
			HyperBeam, Submission,Counter,SeismicToss,Rage,SolarBeam, ThunderBolt,Thunder,Psychic_TM,Teleport,Mimic, DoubleTeam,
			Reflect,Bide,Metronome, FireBlast, SkullBash,Rest,ThunderWave,Psywave,TriAttack, Substitute};
		searchTMList(WigglytuffAttacks, tm);
		int[] hm = {Strength,Flash};
		searchHMList(WigglytuffAttacks,hm);
		master_attack_list.Add(new masterAttackList("Wigglytuff", WigglytuffAttacks));
	}
	
	public void Zubat ()
	{
		ZubatAttacks.Add (new attackIndex (searchAttackList ("Leech Life"), 1));
		ZubatAttacks.Add (new attackIndex (searchAttackList ("Supersonic"), 10));
		ZubatAttacks.Add (new attackIndex (searchAttackList ("Bite"), 15));
		ZubatAttacks.Add (new attackIndex (searchAttackList ("Confuse Ray"), 21));
		ZubatAttacks.Add (new attackIndex (searchAttackList ("Wing Attack"), 28));
		ZubatAttacks.Add (new attackIndex (searchAttackList ("Haze"), 36));

		int[] tm = {RazorWind,Whirlwind, Toxic,TakeDown, DoubleEdge,Rage,MegaDrain,Mimic, DoubleTeam, Bide,Swift,Rest,Substitute};
		searchTMList(ZubatAttacks, tm);
		master_attack_list.Add(new masterAttackList("Zubat", ZubatAttacks));
	}
	
	public void Golbat ()
	{
		GolbattAttacks.Add (new attackIndex (searchAttackList ("Bite"), 1));
		GolbattAttacks.Add (new attackIndex (searchAttackList ("Leech Life"), 1));
		GolbattAttacks.Add (new attackIndex (searchAttackList ("Screech"), 1));
		GolbattAttacks.Add (new attackIndex (searchAttackList ("Supersonic"), 10));
		GolbattAttacks.Add (new attackIndex (searchAttackList ("Confuse Ray"), 21));
		GolbattAttacks.Add (new attackIndex (searchAttackList ("Wing Attack"), 32));
		GolbattAttacks.Add (new attackIndex (searchAttackList ("Haze"), 43));

		int[] tm = {RazorWind,Whirlwind, Toxic,TakeDown, DoubleEdge,Rage,MegaDrain,Mimic, DoubleTeam, HyperBeam,
			Bide,Swift,Rest,Substitute};
		searchTMList(GolbattAttacks, tm);
		master_attack_list.Add(new masterAttackList("Golbat", GolbattAttacks));
	}
	
	public void Oddish ()
	{
		OddishAttacks.Add (new attackIndex (searchAttackList ("Absorb"), 1));
		OddishAttacks.Add (new attackIndex (searchAttackList ("Poison Powder"), 15));
		OddishAttacks.Add (new attackIndex (searchAttackList ("Stun Spore"), 17));
		OddishAttacks.Add (new attackIndex (searchAttackList ("Sleep Powder"), 19));
		OddishAttacks.Add (new attackIndex (searchAttackList ("Acid"), 24));
		OddishAttacks.Add (new attackIndex (searchAttackList ("Petal Dance"), 33));
		OddishAttacks.Add (new attackIndex (searchAttackList ("Solar Beam"), 46));

		int[] tm = {SwordsDance, Toxic,TakeDown, DoubleEdge,Rage,MegaDrain, SolarBeam, Mimic, DoubleTeam,
			Reflect,Bide,Rest, Substitute};
		searchTMList(OddishAttacks, tm);
		int[] hm = {Cut};
		searchHMList(OddishAttacks,hm);
		master_attack_list.Add(new masterAttackList("Oddish", OddishAttacks));
	}
	
	public void Gloom ()
	{
		GloomAttacks.Add (new attackIndex (searchAttackList ("Absorb"), 1));
		GloomAttacks.Add (new attackIndex (searchAttackList ("Poison Powder"), 1));
		GloomAttacks.Add (new attackIndex (searchAttackList ("Stun Spore"), 1));
		GloomAttacks.Add (new attackIndex (searchAttackList ("Sleep Powder"), 19));
		GloomAttacks.Add (new attackIndex (searchAttackList ("Acid"), 28));
		GloomAttacks.Add (new attackIndex (searchAttackList ("Petal Dance"), 38));
		GloomAttacks.Add (new attackIndex (searchAttackList ("Solar Beam"), 52));

		int[] tm = {SwordsDance, Toxic,TakeDown, DoubleEdge,Rage,MegaDrain, SolarBeam, Mimic, DoubleTeam,
			Reflect,Bide,Rest, Substitute};
		searchTMList(GloomAttacks, tm);
		int[] hm = {Cut};
		searchHMList(GloomAttacks,hm);
		master_attack_list.Add(new masterAttackList("Gloom", GloomAttacks));
	}
	
	public void Vileplume ()
	{
		VileplumeAttacks.Add (new attackIndex (searchAttackList ("Acid"), 1));
		VileplumeAttacks.Add (new attackIndex (searchAttackList ("Petal Dance"), 1));
		VileplumeAttacks.Add (new attackIndex (searchAttackList ("Sleep Powder"), 1));
		VileplumeAttacks.Add (new attackIndex (searchAttackList ("Stun Spore"), 1));
		VileplumeAttacks.Add (new attackIndex (searchAttackList ("Poison Powder"), 15));

		int[] tm = {SwordsDance, Toxic,TakeDown, DoubleEdge,HyperBeam, Rage,MegaDrain, SolarBeam, Mimic, DoubleTeam,
			Reflect,Bide,Rest, Substitute};
		searchTMList(VileplumeAttacks, tm);
		int[] hm = {Cut};
		searchHMList(VileplumeAttacks,hm);
		master_attack_list.Add(new masterAttackList("Vileplume", VileplumeAttacks));
	}
	
	public void Paras ()
	{
		ParasAttacks.Add (new attackIndex (searchAttackList ("Scratch"), 1));
		ParasAttacks.Add (new attackIndex (searchAttackList ("Stun Spore"), 13));
		ParasAttacks.Add (new attackIndex (searchAttackList ("Leech Life"), 20));
		ParasAttacks.Add (new attackIndex (searchAttackList ("Spore"), 27));
		ParasAttacks.Add (new attackIndex (searchAttackList ("Slash"), 34));
		ParasAttacks.Add (new attackIndex (searchAttackList ("Growth"), 41));

		int[] tm = {SwordsDance, Toxic,TakeDown, DoubleEdge, Rage,MegaDrain, SolarBeam,Dig, Mimic, DoubleTeam,
			Reflect,Bide,SkullBash, Rest, Substitute};
		searchTMList(ParasAttacks, tm);
		int[] hm = {Cut};
		searchHMList(ParasAttacks,hm);
		master_attack_list.Add(new masterAttackList("Paras", ParasAttacks));
	}
	
	public void Parasect ()
	{
		ParasectAttacks.Add (new attackIndex (searchAttackList ("Scratch"), 1));
		ParasectAttacks.Add (new attackIndex (searchAttackList ("Stun Spore"), 1));
		ParasectAttacks.Add (new attackIndex (searchAttackList ("Leech Life"), 1));
		ParasectAttacks.Add (new attackIndex (searchAttackList ("Spore"), 30));
		ParasectAttacks.Add (new attackIndex (searchAttackList ("Slash"), 39));
		ParasectAttacks.Add (new attackIndex (searchAttackList ("Growth"), 48));

		int[] tm = {SwordsDance, Toxic,TakeDown, DoubleEdge,HyperBeam, Rage,MegaDrain, SolarBeam,Dig, Mimic, DoubleTeam,
			Reflect,Bide,SkullBash, Rest, Substitute};
		searchTMList(ParasectAttacks, tm);
		int[] hm = {Cut};
		searchHMList(ParasectAttacks,hm);
		master_attack_list.Add(new masterAttackList("Parasect", ParasectAttacks));
	}
	
	public void Venonat ()
	{
		VenonatAttacks.Add (new attackIndex (searchAttackList ("Disable"), 1));
		VenonatAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		VenonatAttacks.Add (new attackIndex (searchAttackList ("Poison Powder"), 24));
		VenonatAttacks.Add (new attackIndex (searchAttackList ("Leech Life"), 27));
		VenonatAttacks.Add (new attackIndex (searchAttackList ("Stun Spore"), 30));
		VenonatAttacks.Add (new attackIndex (searchAttackList ("Psybeam"), 35));
		VenonatAttacks.Add (new attackIndex (searchAttackList ("Sleep Powder"), 38));
		VenonatAttacks.Add (new attackIndex (searchAttackList ("Psychic"), 43));

		int[] tm = {Toxic,TakeDown, DoubleEdge,Rage,MegaDrain, SolarBeam,Psychic_TM,Mimic, DoubleTeam,
			Reflect,Bide, Rest,Psywave, Substitute};
		searchTMList(VenonatAttacks, tm);
		master_attack_list.Add(new masterAttackList("Venonat", VenonatAttacks));
	}
	
	public void Venomoth ()
	{
		VenomothAttacks.Add (new attackIndex (searchAttackList ("Disable"), 1));
		VenomothAttacks.Add (new attackIndex (searchAttackList ("Poison Powder"), 1));
		VenomothAttacks.Add (new attackIndex (searchAttackList ("Leech Life"), 1));
		VenomothAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		VenomothAttacks.Add (new attackIndex (searchAttackList ("Stun Spore"), 30));
		VenomothAttacks.Add (new attackIndex (searchAttackList ("Psybeam"), 38));
		VenomothAttacks.Add (new attackIndex (searchAttackList ("Sleep Powder"), 43));
		VenomothAttacks.Add (new attackIndex (searchAttackList ("Psychic"), 50));

		int[] tm = {RazorWind,Whirlwind,Toxic,TakeDown, DoubleEdge,HyperBeam, Rage,MegaDrain, SolarBeam,Psychic_TM,Teleport,
			Mimic, DoubleTeam, Reflect,Bide, Rest,Psywave, Substitute};
		searchTMList(VenomothAttacks, tm);
		master_attack_list.Add(new masterAttackList("Venomoth", VenomothAttacks));
	}
	
	public void Digglet ()
	{
		DiglettAttacks.Add (new attackIndex (searchAttackList ("Scratch"), 1));
		DiglettAttacks.Add (new attackIndex (searchAttackList ("Growl"), 15));
		DiglettAttacks.Add (new attackIndex (searchAttackList ("Dig"), 19));
		DiglettAttacks.Add (new attackIndex (searchAttackList ("Sand Attack"), 24));
		DiglettAttacks.Add (new attackIndex (searchAttackList ("Slash"), 31));
		DiglettAttacks.Add (new attackIndex (searchAttackList ("Earthquake"), 40));

		int[] tm = {Toxic,BodySlam, TakeDown, DoubleEdge, Rage,Earthquake,Fissure,Dig,
			Mimic, DoubleTeam,Bide, Rest,RockSlide, Substitute};
		searchTMList(DiglettAttacks, tm);
		master_attack_list.Add(new masterAttackList("Digglet", DiglettAttacks));
	}
	
	public void Dugtrio ()
	{
		DugtrioAttacks.Add (new attackIndex (searchAttackList ("Growl"), 1));
		DugtrioAttacks.Add (new attackIndex (searchAttackList ("Scratch"), 1));
		DugtrioAttacks.Add (new attackIndex (searchAttackList ("Dig"), 1));
		DugtrioAttacks.Add (new attackIndex (searchAttackList ("Sand Attack"), 24));
		DugtrioAttacks.Add (new attackIndex (searchAttackList ("Slash"), 35));
		DugtrioAttacks.Add (new attackIndex (searchAttackList ("Earthquake"), 47));

		int[] tm = {Toxic,BodySlam, TakeDown, DoubleEdge, HyperBeam,Rage,Earthquake,Fissure,Dig,
			Mimic, DoubleTeam,Bide, Rest,RockSlide, Substitute};
		searchTMList(DugtrioAttacks, tm);
		master_attack_list.Add(new masterAttackList("Dugtrio", DugtrioAttacks));
	}
	#endregion
	
	#region Pokemon 52-76
	private void Meowth ()
	{
		MeowthAttacks.Add (new attackIndex (searchAttackList ("Growl"), 1));
		MeowthAttacks.Add (new attackIndex (searchAttackList ("Scratch"), 1));
		MeowthAttacks.Add (new attackIndex (searchAttackList ("Bite"), 12));
		MeowthAttacks.Add (new attackIndex (searchAttackList ("Pay Day"), 17));
		MeowthAttacks.Add (new attackIndex (searchAttackList ("Screech"), 24));
		MeowthAttacks.Add (new attackIndex (searchAttackList ("Fury Swipes"), 33));
		MeowthAttacks.Add (new attackIndex (searchAttackList ("Slash"), 44));

		int[] tm = {Toxic,BodySlam, TakeDown, DoubleEdge,BubbleBeam,WaterGun,Payday,Rage,ThunderBolt,Thunder,
			Mimic, DoubleTeam,Bide,Swift,SkullBash, Rest,Substitute};
		searchTMList(MeowthAttacks, tm);
		master_attack_list.Add(new masterAttackList("Meowth", MeowthAttacks));
	}
	
	private void Persian ()
	{
		PersianAttacks.Add (new attackIndex (searchAttackList ("Bite"), 1));
		PersianAttacks.Add (new attackIndex (searchAttackList ("Growl"), 1));
		PersianAttacks.Add (new attackIndex (searchAttackList ("Scratch"), 1));
		PersianAttacks.Add (new attackIndex (searchAttackList ("Screech"), 24));
		PersianAttacks.Add (new attackIndex (searchAttackList ("Pay Day"), 17));
		PersianAttacks.Add (new attackIndex (searchAttackList ("Fury Swipes"), 37));
		PersianAttacks.Add (new attackIndex (searchAttackList ("Slash"), 51));

		int[] tm = {Toxic,BodySlam, TakeDown, DoubleEdge,HyperBeam, BubbleBeam,WaterGun,Payday,Rage,ThunderBolt,Thunder,
			Mimic, DoubleTeam,Bide,Swift,SkullBash, Rest,Substitute};
		searchTMList(PersianAttacks, tm);
		master_attack_list.Add(new masterAttackList("Persian", PersianAttacks));
	}
	
	private void Psyduck ()
	{
		PsyduckAttacks.Add (new attackIndex (searchAttackList ("Scratch"), 1));
		PsyduckAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 28));
		PsyduckAttacks.Add (new attackIndex (searchAttackList ("Disable"), 31));
		PsyduckAttacks.Add (new attackIndex (searchAttackList ("Confusion"), 36));
		PsyduckAttacks.Add (new attackIndex (searchAttackList ("Fury Swipes"), 43));
		PsyduckAttacks.Add (new attackIndex (searchAttackList ("Hydro Pump"), 52));

		int[] tm = {MegaPunch,MegaKick, Toxic,BodySlam, TakeDown, DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard,
			Payday,Submission,Counter,SeismicToss,Rage,Dig,	Mimic, DoubleTeam,Bide,Swift,SkullBash, Rest,Substitute};
		searchTMList(PsyduckAttacks, tm);
		int[] hm = {Surf,Strength};
		searchHMList(PsyduckAttacks,hm);
		master_attack_list.Add(new masterAttackList("Psyduck", PsyduckAttacks));
	}
	
	private void Golduck ()
	{
		GolduckAttacks.Add (new attackIndex (searchAttackList ("Disable"), 1));
		GolduckAttacks.Add (new attackIndex (searchAttackList ("Scratch"), 1));
		GolduckAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 1));
		GolduckAttacks.Add (new attackIndex (searchAttackList ("Confusion"), 39));
		GolduckAttacks.Add (new attackIndex (searchAttackList ("Fury Swipes"), 48));
		GolduckAttacks.Add (new attackIndex (searchAttackList ("Hydro Pump"), 59));

		int[] tm = {MegaPunch,MegaKick, Toxic,BodySlam, TakeDown, DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard, HyperBeam,
			Payday,Submission,Counter,SeismicToss,Rage,Dig,	Mimic, DoubleTeam,Bide,Swift,SkullBash, Rest,Substitute};
		searchTMList(GolduckAttacks, tm);
		int[] hm = {Surf,Strength};
		searchHMList(GolduckAttacks,hm);
		master_attack_list.Add(new masterAttackList("Golduck", GolduckAttacks));
	}
	
	private void Mankey ()
	{
		MankeyAttacks.Add (new attackIndex (searchAttackList ("Leer"), 1));
		MankeyAttacks.Add (new attackIndex (searchAttackList ("Scratch"), 1));
		MankeyAttacks.Add (new attackIndex (searchAttackList ("Karate Chop"), 15));
		MankeyAttacks.Add (new attackIndex (searchAttackList ("Fury Swipes"), 21));
		MankeyAttacks.Add (new attackIndex (searchAttackList ("Focus Energy"), 27));
		MankeyAttacks.Add (new attackIndex (searchAttackList ("Seismic Toss"), 33));
		MankeyAttacks.Add (new attackIndex (searchAttackList ("Thrash"), 39));

		int[] tm = {MegaPunch,MegaKick, Toxic,BodySlam, TakeDown, DoubleEdge,Payday,Submission,Counter,SeismicToss,
			Rage,Dig,	Mimic, DoubleTeam,Bide,Swift,SkullBash, Rest,RockSlide, Substitute};
		searchTMList(MankeyAttacks, tm);
		int[] hm = {Strength};
		searchHMList(MankeyAttacks,hm);
		master_attack_list.Add(new masterAttackList("Mankey", MankeyAttacks));
	}
	
	private void Primeape ()
	{
		PrimeapeAttacks.Add (new attackIndex (searchAttackList ("Fury Swipes"), 21));
		PrimeapeAttacks.Add (new attackIndex (searchAttackList ("Karate Chop"), 15));
		PrimeapeAttacks.Add (new attackIndex (searchAttackList ("Leer"), 1));
		PrimeapeAttacks.Add (new attackIndex (searchAttackList ("Scratch"), 1));
		PrimeapeAttacks.Add (new attackIndex (searchAttackList ("Focus Energy"), 27));
		PrimeapeAttacks.Add (new attackIndex (searchAttackList ("Seismic Toss"), 37));
		PrimeapeAttacks.Add (new attackIndex (searchAttackList ("Thrash"), 46));

		int[] tm = {MegaPunch,MegaKick, Toxic,BodySlam, TakeDown, DoubleEdge,HyperBeam, Payday,Submission,Counter,
			SeismicToss,Rage,Dig,Mimic, DoubleTeam,Bide,Swift,SkullBash, Rest,RockSlide, Substitute};
		searchTMList(PrimeapeAttacks, tm);
		int[] hm = {Strength};
		searchHMList(PrimeapeAttacks,hm);
		master_attack_list.Add(new masterAttackList("Primeape", PrimeapeAttacks));
	}
	
	private void Growlithe ()
	{
		GrowlitheAttacks.Add (new attackIndex (searchAttackList ("Bite"), 1));
		GrowlitheAttacks.Add (new attackIndex (searchAttackList ("Roar"), 1));
		GrowlitheAttacks.Add (new attackIndex (searchAttackList ("Ember"), 18));
		GrowlitheAttacks.Add (new attackIndex (searchAttackList ("Leer"), 23));
		GrowlitheAttacks.Add (new attackIndex (searchAttackList ("Take Down"), 30));
		GrowlitheAttacks.Add (new attackIndex (searchAttackList ("Agility"), 39));
		GrowlitheAttacks.Add (new attackIndex (searchAttackList ("Flamethrower"), 50));

		int[] tm = {Toxic,BodySlam, TakeDown, DoubleEdge,Rage,DragonRage,Dig,Mimic, DoubleTeam,Reflect, Bide,FireBlast,
			Swift,SkullBash, Rest, Substitute};
		searchTMList(GrowlitheAttacks, tm);
		master_attack_list.Add(new masterAttackList("Growlithe", GrowlitheAttacks));
	}
	
	private void Arcanine ()
	{
		ArcanineAttacks.Add (new attackIndex (searchAttackList ("Ember"), 1));
		ArcanineAttacks.Add (new attackIndex (searchAttackList ("Leer"), 1));
		ArcanineAttacks.Add (new attackIndex (searchAttackList ("Roar"), 1));
		ArcanineAttacks.Add (new attackIndex (searchAttackList ("Take Down"), 1));

		int[] tm = {Toxic,BodySlam, TakeDown, DoubleEdge,HyperBeam, Rage,DragonRage,Dig,Mimic, DoubleTeam,Reflect, 
			Bide,FireBlast,	Swift,SkullBash, Rest, Substitute};
		searchTMList(ArcanineAttacks, tm);
		master_attack_list.Add(new masterAttackList("Arcanine", ArcanineAttacks));
	}
	
	private void Poliwag ()
	{
		PoliwagAttacks.Add (new attackIndex (searchAttackList ("Bubble"), 1));
		PoliwagAttacks.Add (new attackIndex (searchAttackList ("Hypnosis"), 16));
		PoliwagAttacks.Add (new attackIndex (searchAttackList ("Water Gun"), 19));
		PoliwagAttacks.Add (new attackIndex (searchAttackList ("Double Slap"), 25));
		PoliwagAttacks.Add (new attackIndex (searchAttackList ("Body Slam"), 31));
		PoliwagAttacks.Add (new attackIndex (searchAttackList ("Amnesia"), 38));
		PoliwagAttacks.Add (new attackIndex (searchAttackList ("Hydro Pump"), 45));

		int[] tm = {Toxic,BodySlam, TakeDown, DoubleEdge,BubbleBeam,IceBeam,Blizzard, Rage,Psychic_TM,Mimic, DoubleTeam, 
			Bide,SkullBash, Rest,Psywave, Substitute};
		searchTMList(PoliwagAttacks, tm);
		int[] hm = {Surf};
		searchHMList(PoliwagAttacks,hm);
		master_attack_list.Add(new masterAttackList("Poliwag", PoliwagAttacks));
	}
	
	private void Poliwhirl ()
	{
		PoliwhirlAttacks.Add (new attackIndex (searchAttackList ("Bubble"), 1));
		PoliwhirlAttacks.Add (new attackIndex (searchAttackList ("Hypnosis"), 1));
		PoliwhirlAttacks.Add (new attackIndex (searchAttackList ("Water Gun"), 1));
		PoliwhirlAttacks.Add (new attackIndex (searchAttackList ("Double Slap"), 26));
		PoliwhirlAttacks.Add (new attackIndex (searchAttackList ("Body Slam"), 33));
		PoliwhirlAttacks.Add (new attackIndex (searchAttackList ("Amnesia"), 44));
		PoliwhirlAttacks.Add (new attackIndex (searchAttackList ("Hydro Pump"), 49));

		int[] tm = {MegaPunch,MegaKick, Toxic,BodySlam, TakeDown, DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard,
			Submission,Counter,SeismicToss, Rage,Earthquake,Fissure,Psychic_TM,Mimic, DoubleTeam, 
			Bide,Metronome,SkullBash, Rest,Psywave, Substitute};
		searchTMList(PoliwhirlAttacks, tm);
		int[] hm = {Surf,Strength};
		searchHMList(PoliwhirlAttacks,hm);
		master_attack_list.Add(new masterAttackList("Poliwhirl", PoliwhirlAttacks));
	}
	
	private void Poliwrath ()
	{
		PoliwrathAttacks.Add (new attackIndex (searchAttackList ("Double Slap"), 1));
		PoliwrathAttacks.Add (new attackIndex (searchAttackList ("Hypnosis"), 1));
		PoliwrathAttacks.Add (new attackIndex (searchAttackList ("Body Slam"), 1));
		PoliwrathAttacks.Add (new attackIndex (searchAttackList ("Water Gun"), 1));

		int[] tm = {MegaPunch,MegaKick, Toxic,BodySlam, TakeDown, DoubleEdge,HyperBeam, BubbleBeam,WaterGun,IceBeam,
			Blizzard, Submission,Counter,SeismicToss, Rage,Earthquake,Fissure,Psychic_TM,Mimic, DoubleTeam, 
			Bide,Metronome,SkullBash, Rest,Psywave, Substitute};
		searchTMList(PoliwhirlAttacks, tm);
		int[] hm = {Surf,Strength};
		searchHMList(PoliwhirlAttacks,hm);
		master_attack_list.Add(new masterAttackList("Poliwrath", PoliwrathAttacks));
	}
	
	private void Abra ()
	{
		AbraAttacks.Add (new attackIndex (searchAttackList ("Teleport"), 1));

		int[] tm = {MegaPunch,MegaKick, Toxic,BodySlam, TakeDown, DoubleEdge,Submission,Counter,SeismicToss, Rage,
			Psychic_TM,Teleport, Mimic, DoubleTeam,Bide,Metronome,SkullBash, Rest,ThunderWave,Psywave,TriAttack,Substitute};
		searchTMList(AbraAttacks, tm);
		int[] hm = {Flash};
		searchHMList(AbraAttacks,hm);
		master_attack_list.Add(new masterAttackList("Abra", AbraAttacks));
	}
	
	private void Kadabra ()
	{
		KadabraAttacks.Add (new attackIndex (searchAttackList ("Confusion"), 1));
		KadabraAttacks.Add (new attackIndex (searchAttackList ("Disable"), 1));
		KadabraAttacks.Add (new attackIndex (searchAttackList ("Teleport"), 1));
		KadabraAttacks.Add (new attackIndex (searchAttackList ("Psybeam"), 27));
		KadabraAttacks.Add (new attackIndex (searchAttackList ("Recover"), 31));
		KadabraAttacks.Add (new attackIndex (searchAttackList ("Psychic"), 38));
		KadabraAttacks.Add (new attackIndex (searchAttackList ("Reflect"), 42));

		int[] tm = {MegaPunch,MegaKick, Toxic,BodySlam, TakeDown, DoubleEdge,Submission,Counter,SeismicToss, Rage,Dig,
			Psychic_TM,Teleport, Mimic, DoubleTeam,Reflect, Bide,Metronome,SkullBash, Rest,ThunderWave,Psywave,
			TriAttack,Substitute};
		searchTMList(KadabraAttacks, tm);
		int[] hm = {Flash};
		searchHMList(KadabraAttacks,hm);
		master_attack_list.Add(new masterAttackList("Kadabra", KadabraAttacks));
	}
	
	private void Alakazam ()
	{
		AlakazamAttacks.Add (new attackIndex (searchAttackList ("Confusion"), 1));
		AlakazamAttacks.Add (new attackIndex (searchAttackList ("Disable"), 1));
		AlakazamAttacks.Add (new attackIndex (searchAttackList ("Teleport"), 1));
		AlakazamAttacks.Add (new attackIndex (searchAttackList ("Psybeam"), 27));
		AlakazamAttacks.Add (new attackIndex (searchAttackList ("Recover"), 31));
		AlakazamAttacks.Add (new attackIndex (searchAttackList ("Psychic"), 38));
		AlakazamAttacks.Add (new attackIndex (searchAttackList ("Reflect"), 42));

		int[] tm = {MegaPunch,MegaKick, Toxic,BodySlam, TakeDown, DoubleEdge,HyperBeam, Submission,Counter,SeismicToss, 
			Rage,Dig,Psychic_TM,Teleport, Mimic, DoubleTeam,Reflect, Bide,Metronome,SkullBash, Rest,ThunderWave,Psywave,
			TriAttack,Substitute};
		searchTMList(AlakazamAttacks, tm);
		int[] hm = {Flash};
		searchHMList(AlakazamAttacks,hm);
		master_attack_list.Add(new masterAttackList("Alakazam", AlakazamAttacks));
	}
	
	private void Machop ()
	{
		MachopAttacks.Add (new attackIndex (searchAttackList ("Karate Chop"), 1));
		MachopAttacks.Add (new attackIndex (searchAttackList ("Low Kick"), 20));
		MachopAttacks.Add (new attackIndex (searchAttackList ("Leer"), 25));
		MachopAttacks.Add (new attackIndex (searchAttackList ("Focus Energy"), 32));
		MachopAttacks.Add (new attackIndex (searchAttackList ("Seismic Toss"), 39));
		MachopAttacks.Add (new attackIndex (searchAttackList ("Submission"), 46	));

		int[] tm = {MegaPunch,MegaKick, Toxic,BodySlam, TakeDown, DoubleEdge, Submission,Counter,SeismicToss, 
			Rage,Earthquake,Fissure,Dig,Mimic, DoubleTeam, Bide,Metronome,FireBlast, SkullBash, Rest, RockSlide,
			Substitute};
		searchTMList(MachopAttacks, tm);
		int[] hm = {Strength};
		searchHMList(MachopAttacks,hm);
		master_attack_list.Add(new masterAttackList("Machop", MachopAttacks));
	}
	
	private void Machoke ()
	{
		MachokeAttacks.Add (new attackIndex (searchAttackList ("Karate Chop"), 1));
		MachokeAttacks.Add (new attackIndex (searchAttackList ("Low Kick"), 1));
		MachokeAttacks.Add (new attackIndex (searchAttackList ("Leer"), 1));
		MachokeAttacks.Add (new attackIndex (searchAttackList ("Focus Energy"), 36));
		MachokeAttacks.Add (new attackIndex (searchAttackList ("Seismic Toss"), 44));
		MachokeAttacks.Add (new attackIndex (searchAttackList ("Submission"), 52));

		int[] tm = {MegaPunch,MegaKick, Toxic,BodySlam, TakeDown, DoubleEdge, Submission,Counter,SeismicToss, 
			Rage,Earthquake,Fissure,Dig,Mimic, DoubleTeam, Bide,Metronome,FireBlast, SkullBash, Rest, RockSlide,
			Substitute};
		searchTMList(MachokeAttacks, tm);
		int[] hm = {Strength};
		searchHMList(MachokeAttacks,hm);
		master_attack_list.Add(new masterAttackList("Machoke", MachokeAttacks));
	}
	
	private void Machamp ()
	{
		MachokeAttacks.Add (new attackIndex (searchAttackList ("Karate Chop"), 1));
		MachokeAttacks.Add (new attackIndex (searchAttackList ("Low Kick"), 1));
		MachokeAttacks.Add (new attackIndex (searchAttackList ("Leer"), 1));
		MachokeAttacks.Add (new attackIndex (searchAttackList ("Focus Energy"), 36));
		MachokeAttacks.Add (new attackIndex (searchAttackList ("Seismic Toss"), 44));
		MachokeAttacks.Add (new attackIndex (searchAttackList ("Submission"), 52));
		
		int[] tm = {MegaPunch,MegaKick, Toxic,BodySlam, TakeDown, DoubleEdge,HyperBeam, Submission,Counter,SeismicToss, 
			Rage,Earthquake,Fissure,Dig,Mimic, DoubleTeam, Bide,Metronome,FireBlast, SkullBash, Rest, RockSlide,
			Substitute};
		searchTMList(MachokeAttacks, tm);
		int[] hm = {Strength};
		searchHMList(MachokeAttacks,hm);
		master_attack_list.Add(new masterAttackList("Machamp", MachampAttacks));
	}
	
	private void Bellsprout ()
	{
		BellsproutAttacks.Add (new attackIndex (searchAttackList ("Growth"), 1));
		BellsproutAttacks.Add (new attackIndex (searchAttackList ("Vine Whip"), 1));
		BellsproutAttacks.Add (new attackIndex (searchAttackList ("Wrap"), 13));
		BellsproutAttacks.Add (new attackIndex (searchAttackList ("Poison Powder"), 15));
		BellsproutAttacks.Add (new attackIndex (searchAttackList ("Sleep Powder"), 18));
		BellsproutAttacks.Add (new attackIndex (searchAttackList ("Stun Spore"), 21));
		BellsproutAttacks.Add (new attackIndex (searchAttackList ("Acid"), 26));
		BellsproutAttacks.Add (new attackIndex (searchAttackList ("Razor Leaf"), 33));
		BellsproutAttacks.Add (new attackIndex (searchAttackList ("Slam"), 42));
		
		int[] tm = {SwordsDance, Toxic, TakeDown, DoubleEdge,Rage,MegaDrain,SolarBeam,Mimic, DoubleTeam,Reflect, Bide,
			Rest, Substitute};
		searchTMList(BellsproutAttacks, tm);
		int[] hm = {Cut};
		searchHMList(BellsproutAttacks,hm);
		master_attack_list.Add(new masterAttackList("Bellsprout", BellsproutAttacks));
	}
	
	private void Weepinbell ()
	{
		WeepinbellAttacks.Add (new attackIndex (searchAttackList ("Growth"), 1));
		WeepinbellAttacks.Add (new attackIndex (searchAttackList ("Vine Whip"), 1));
		WeepinbellAttacks.Add (new attackIndex (searchAttackList ("Wrap"), 1));
		WeepinbellAttacks.Add (new attackIndex (searchAttackList ("Poison Powder"), 15));
		WeepinbellAttacks.Add (new attackIndex (searchAttackList ("Sleep Powder"), 18));
		WeepinbellAttacks.Add (new attackIndex (searchAttackList ("Stun Spore"), 23));
		WeepinbellAttacks.Add (new attackIndex (searchAttackList ("Acid"), 29));
		WeepinbellAttacks.Add (new attackIndex (searchAttackList ("Razor Leaf"), 38));
		WeepinbellAttacks.Add (new attackIndex (searchAttackList ("Slam"), 49));
		
		int[] tm = {SwordsDance, Toxic, TakeDown, DoubleEdge,Rage,MegaDrain,SolarBeam,Mimic, DoubleTeam,Reflect, Bide,
			Rest, Substitute};
		searchTMList(WeepinbellAttacks, tm);
		int[] hm = {Cut};
		searchHMList(WeepinbellAttacks,hm);
		master_attack_list.Add(new masterAttackList("Weepinbell", WeepinbellAttacks));
	}
	
	private void Victreebel ()
	{
		VictreebelAttacks.Add (new attackIndex (searchAttackList ("Acid"), 1));
		VictreebelAttacks.Add (new attackIndex (searchAttackList ("Razor Leaf"), 1));
		VictreebelAttacks.Add (new attackIndex (searchAttackList ("Sleep Powder"), 1));
		VictreebelAttacks.Add (new attackIndex (searchAttackList ("Stun Spore"), 1));
		VictreebelAttacks.Add (new attackIndex (searchAttackList ("Wrap"), 13));
		VictreebelAttacks.Add (new attackIndex (searchAttackList ("Poison Powder"), 15));
		
		int[] tm = {SwordsDance, Toxic, TakeDown, DoubleEdge,HyperBeam, Rage,MegaDrain,SolarBeam,Mimic, DoubleTeam,
			Reflect, Bide, Rest, Substitute};
		searchTMList(VictreebelAttacks, tm);
		int[] hm = {Cut};
		searchHMList(VictreebelAttacks,hm);
		master_attack_list.Add(new masterAttackList("Victreebel", VictreebelAttacks));
	}
	
	private void Tentacool ()
	{
		TentacoolAttacks.Add (new attackIndex (searchAttackList ("Acid"), 1));
		TentacoolAttacks.Add (new attackIndex (searchAttackList ("Supersonic"), 7));
		TentacoolAttacks.Add (new attackIndex (searchAttackList ("Wrap"), 13));
		TentacoolAttacks.Add (new attackIndex (searchAttackList ("Poison Sting"), 18));
		TentacoolAttacks.Add (new attackIndex (searchAttackList ("Water Gun"), 22));
		TentacoolAttacks.Add (new attackIndex (searchAttackList ("Constrict"), 27));
		TentacoolAttacks.Add (new attackIndex (searchAttackList ("Barrier"), 33));
		TentacoolAttacks.Add (new attackIndex (searchAttackList ("Screech"), 40));
		TentacoolAttacks.Add (new attackIndex (searchAttackList ("Hydro Pump"), 48));
		
		int[] tm = {SwordsDance, Toxic, TakeDown, DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard, Rage,MegaDrain,
			Mimic, DoubleTeam, Reflect, Bide, SkullBash, Rest, Substitute};
		searchTMList(TentacoolAttacks, tm);
		int[] hm = {Cut, Surf};
		searchHMList(TentacoolAttacks,hm);
		master_attack_list.Add(new masterAttackList("Tentacool", TentacoolAttacks));
	}
	
	private void Tentacruel ()
	{
		TentacruelAttacks.Add (new attackIndex (searchAttackList ("Acid"), 1));
		TentacruelAttacks.Add (new attackIndex (searchAttackList ("Supersonic"), 1));
		TentacruelAttacks.Add (new attackIndex (searchAttackList ("Wrap"), 1));
		TentacruelAttacks.Add (new attackIndex (searchAttackList ("Poison Sting"), 18));
		TentacruelAttacks.Add (new attackIndex (searchAttackList ("Water Gun"), 22));
		TentacruelAttacks.Add (new attackIndex (searchAttackList ("Constrict"), 27));
		TentacruelAttacks.Add (new attackIndex (searchAttackList ("Barrier"), 35));
		TentacruelAttacks.Add (new attackIndex (searchAttackList ("Screech"), 43));
		TentacruelAttacks.Add (new attackIndex (searchAttackList ("Hydro Pump"), 50));
		
		int[] tm = {SwordsDance, Toxic, TakeDown, DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard,HyperBeam, Rage,MegaDrain,
			Mimic, DoubleTeam, Reflect, Bide, SkullBash, Rest, Substitute};
		searchTMList(TentacoolAttacks, tm);
		int[] hm = {Cut, Surf};
		searchHMList(TentacoolAttacks,hm);
		master_attack_list.Add(new masterAttackList("Tentacruel", TentacruelAttacks));
	}
	
	private void Geodude ()
	{
		GeodudeAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		GeodudeAttacks.Add (new attackIndex (searchAttackList ("Defense Curl"), 11));
		GeodudeAttacks.Add (new attackIndex (searchAttackList ("Rock Throw"), 16));
		GeodudeAttacks.Add (new attackIndex (searchAttackList ("Self Destruct"), 21));
		GeodudeAttacks.Add (new attackIndex (searchAttackList ("Harden"), 26));
		GeodudeAttacks.Add (new attackIndex (searchAttackList ("Earthquake"), 31));
		GeodudeAttacks.Add (new attackIndex (searchAttackList ("Explosion"), 36));
		
		int[] tm = {MegaPunch, Toxic,BodySlam, TakeDown, DoubleEdge,Submission,Counter,SeismicToss,Rage,Earthquake,Fissure,Dig,
			Mimic, DoubleTeam, Bide,Metronome, SkullBash,SelfDestruct,FireBlast, Rest,Explosion,RockSlide, Substitute};
		searchTMList(GeodudeAttacks, tm);
		int[] hm = {Strength};
		searchHMList(GeodudeAttacks,hm);
		master_attack_list.Add(new masterAttackList("Geodude", GeodudeAttacks));
	}
	
	private void Gravler ()
	{
		GravlerAttacks.Add (new attackIndex (searchAttackList ("Defense Curl"), 1));
		GravlerAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		GravlerAttacks.Add (new attackIndex (searchAttackList ("Rock Throw"), 16));
		GravlerAttacks.Add (new attackIndex (searchAttackList ("Self Destruct"), 21));
		GravlerAttacks.Add (new attackIndex (searchAttackList ("Harden"), 29));
		GravlerAttacks.Add (new attackIndex (searchAttackList ("Earthquake"), 36));
		GravlerAttacks.Add (new attackIndex (searchAttackList ("Explosion"), 43));
		
		int[] tm = {MegaPunch, Toxic,BodySlam, TakeDown, DoubleEdge,Submission,Counter,SeismicToss,Rage,Earthquake,Fissure,Dig,
			Mimic, DoubleTeam, Bide,Metronome, SkullBash,SelfDestruct,FireBlast, Rest,Explosion,RockSlide, Substitute};
		searchTMList(GravlerAttacks, tm);
		int[] hm = {Strength};
		searchHMList(GravlerAttacks,hm);
		master_attack_list.Add(new masterAttackList("Gravler", GravlerAttacks));
	}
	
	private void Golem ()
	{
		GolemAttacks.Add (new attackIndex (searchAttackList ("Defense Curl"), 1));
		GolemAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		GolemAttacks.Add (new attackIndex (searchAttackList ("Rock Throw"), 16));
		GolemAttacks.Add (new attackIndex (searchAttackList ("Self Destruct"), 21));
		GolemAttacks.Add (new attackIndex (searchAttackList ("Harden"), 29));
		GolemAttacks.Add (new attackIndex (searchAttackList ("Earthquake"), 36));
		GolemAttacks.Add (new attackIndex (searchAttackList ("Explosion"), 43));
		
		int[] tm = {MegaPunch,MegaKick, Toxic,BodySlam, TakeDown, DoubleEdge,HyperBeam, Submission,Counter,SeismicToss,Rage,Earthquake,Fissure,Dig,
			Mimic, DoubleTeam, Bide,Metronome,SelfDestruct,FireBlast, Rest,Explosion,RockSlide, Substitute};
		searchTMList(GolemAttacks, tm);
		int[] hm = {Strength};
		searchHMList(GolemAttacks,hm);
		master_attack_list.Add(new masterAttackList("Golem", GolemAttacks));
	}
	#endregion

	#region Pokemon 76-101
	private void Ponyta(){
		PonytaAttacks.Add (new attackIndex (searchAttackList ("Ember"), 1));
		PonytaAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 30));
		PonytaAttacks.Add (new attackIndex (searchAttackList ("Stomp"), 32));
		PonytaAttacks.Add (new attackIndex (searchAttackList ("Growl"), 35));
		PonytaAttacks.Add (new attackIndex (searchAttackList ("Fire Spin"), 37));
		PonytaAttacks.Add (new attackIndex (searchAttackList ("Take Down"), 43));
		PonytaAttacks.Add (new attackIndex (searchAttackList ("Agility"), 48));
		
		int[] tm = {Toxic,HornDrill, BodySlam, TakeDown, DoubleEdge,Rage,Mimic, DoubleTeam,Reflect, Bide,FireBlast,
			Swift,SkullBash, Rest,Substitute};
		searchTMList(PonytaAttacks, tm);
		master_attack_list.Add(new masterAttackList("Ponyta", PonytaAttacks));
	}

	private void Rapidash(){
		RapidashAttacks.Add (new attackIndex (searchAttackList ("Ember"), 1));
		RapidashAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 1));
		RapidashAttacks.Add (new attackIndex (searchAttackList ("Stomp"), 1));
		RapidashAttacks.Add (new attackIndex (searchAttackList ("Growl"), 1));
		RapidashAttacks.Add (new attackIndex (searchAttackList ("Fire Spin"), 39));
		RapidashAttacks.Add (new attackIndex (searchAttackList ("Take Down"), 47));
		RapidashAttacks.Add (new attackIndex (searchAttackList ("Agility"), 55));
		
		int[] tm = {Toxic,HornDrill, BodySlam, TakeDown, DoubleEdge,HyperBeam, Rage,Mimic, DoubleTeam,Reflect, 
			Bide,FireBlast,	Swift,SkullBash, Rest,Substitute};
		searchTMList(RapidashAttacks, tm);
		master_attack_list.Add(new masterAttackList("Rapidash", RapidashAttacks));
	}

	private void Slowpoke(){
		SlowpokeAttacks.Add (new attackIndex (searchAttackList ("Confusion"), 1));
		SlowpokeAttacks.Add (new attackIndex (searchAttackList ("Disable"), 18));
		SlowpokeAttacks.Add (new attackIndex (searchAttackList ("Headbutt"), 22));
		SlowpokeAttacks.Add (new attackIndex (searchAttackList ("Growl"), 27));
		SlowpokeAttacks.Add (new attackIndex (searchAttackList ("Water Gun"), 33));
		SlowpokeAttacks.Add (new attackIndex (searchAttackList ("Amnesia"), 40));
		SlowpokeAttacks.Add (new attackIndex (searchAttackList ("Psychic"), 48));
		
		int[] tm = {Toxic,BodySlam, TakeDown, DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard,Payday,Rage,Earthquake,Fissure,
			Dig,Psychic_TM,Teleport, Mimic, DoubleTeam,Reflect, Bide,FireBlast,Swift,SkullBash, Rest,ThunderWave, Psywave,
			TriAttack, Substitute};
		searchTMList(SlowpokeAttacks, tm);
		int[] hm = {Surf,Strength,Flash};
		searchHMList(SlowpokeAttacks, hm);
		master_attack_list.Add(new masterAttackList("Slowpoke", SlowpokeAttacks));
	}

	private void Slowbro(){
		SlowbroAttacks.Add (new attackIndex (searchAttackList ("Confusion"), 1));
		SlowbroAttacks.Add (new attackIndex (searchAttackList ("Disable"), 1));
		SlowbroAttacks.Add (new attackIndex (searchAttackList ("Headbutt"), 1));
		SlowbroAttacks.Add (new attackIndex (searchAttackList ("Growl"), 27));
		SlowbroAttacks.Add (new attackIndex (searchAttackList ("Water Gun"), 33));
		SlowbroAttacks.Add (new attackIndex (searchAttackList ("Withdraw"), 37));
		SlowbroAttacks.Add (new attackIndex (searchAttackList ("Amnesia"), 44));
		SlowbroAttacks.Add (new attackIndex (searchAttackList ("Psychic"), 55));
		
		int[] tm = {MegaPunch,MegaKick, Toxic,BodySlam, TakeDown, DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard,Payday,
			Submission,Counter,SeismicToss, Rage,Earthquake,Fissure,Dig,Psychic_TM,Teleport, Mimic, DoubleTeam,Reflect, 
			Bide,FireBlast,Swift,SkullBash, Rest,ThunderWave,Psywave,TriAttack, Substitute};
		searchTMList(SlowbroAttacks, tm);
		int[] hm = {Surf,Strength,Flash};
		searchHMList(SlowbroAttacks, hm);
		master_attack_list.Add(new masterAttackList("Slowbro", SlowbroAttacks));
	}

	private void Magnemite(){
		MagnemiteAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		MagnemiteAttacks.Add (new attackIndex (searchAttackList ("Sonic Boom"), 21));
		MagnemiteAttacks.Add (new attackIndex (searchAttackList ("Thunder Shock"), 25));
		MagnemiteAttacks.Add (new attackIndex (searchAttackList ("Supersonic"), 29));
		MagnemiteAttacks.Add (new attackIndex (searchAttackList ("Thunder Wave"), 35));
		MagnemiteAttacks.Add (new attackIndex (searchAttackList ("Swift"), 41));
		MagnemiteAttacks.Add (new attackIndex (searchAttackList ("Screech"), 47));
		
		int[] tm = {Toxic, TakeDown, DoubleEdge,Rage,ThunderBolt,Thunder,Teleport, Mimic, 
			DoubleTeam,Reflect, Bide,Swift, Rest,ThunderWave,Substitute};
		searchTMList(MagnemiteAttacks, tm);
		int[] hm = {Flash};
		searchHMList(MagnemiteAttacks, hm);
		master_attack_list.Add(new masterAttackList("Magnemite", MagnemiteAttacks));
	}

	private void Magneton(){
		MagnemiteAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		MagnemiteAttacks.Add (new attackIndex (searchAttackList ("Sonic Boom"), 1));
		MagnemiteAttacks.Add (new attackIndex (searchAttackList ("Thunder Shock"), 1));
		MagnemiteAttacks.Add (new attackIndex (searchAttackList ("Supersonic"), 29));
		MagnemiteAttacks.Add (new attackIndex (searchAttackList ("Thunder Wave"), 38));
		MagnemiteAttacks.Add (new attackIndex (searchAttackList ("Swift"), 46));
		MagnemiteAttacks.Add (new attackIndex (searchAttackList ("Screech"), 54));
		
		int[] tm = {Toxic, TakeDown, DoubleEdge,HyperBeam, Rage,ThunderBolt,Thunder,Teleport, Mimic, 
			DoubleTeam,Reflect, Bide,Swift, Rest,ThunderWave,Substitute};
		searchTMList(MagnemiteAttacks, tm);
		int[] hm = {Flash};
		searchHMList(MagnemiteAttacks, hm);
		master_attack_list.Add(new masterAttackList("Magneton", MagnetonAttacks));
	}

	private void Farfetchd(){
		FarfetchdAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		FarfetchdAttacks.Add (new attackIndex (searchAttackList ("Sonic Boom"), 1));
		FarfetchdAttacks.Add (new attackIndex (searchAttackList ("Thunder Shock"), 1));
		FarfetchdAttacks.Add (new attackIndex (searchAttackList ("Supersonic"), 29));
		FarfetchdAttacks.Add (new attackIndex (searchAttackList ("Thunder Wave"), 38));
		FarfetchdAttacks.Add (new attackIndex (searchAttackList ("Swift"), 46));
		FarfetchdAttacks.Add (new attackIndex (searchAttackList ("Screech"), 54));
		
		int[] tm = {RazorWind,SwordsDance,Whirlwind, Toxic,BodySlam, TakeDown, DoubleEdge,Rage,Mimic, 
			DoubleTeam,Reflect, Bide,Swift,SkullBash, Rest,Substitute};
		searchTMList(FarfetchdAttacks, tm);
		int[] hm = {Cut,Fly};
		searchHMList(FarfetchdAttacks, hm);
		master_attack_list.Add(new masterAttackList("Farfetchd", FarfetchdAttacks));
	}

	private void Doduo (){
		DoduoAttacks.Add (new attackIndex (searchAttackList ("Peck"), 1));
		DoduoAttacks.Add (new attackIndex (searchAttackList ("Growl"), 20));
		DoduoAttacks.Add (new attackIndex (searchAttackList ("Fury Attack"), 24));
		DoduoAttacks.Add (new attackIndex (searchAttackList ("Drill Peck"), 30));
		DoduoAttacks.Add (new attackIndex (searchAttackList ("Rage"), 36));
		DoduoAttacks.Add (new attackIndex (searchAttackList ("Tri Attack"), 40));
		DoduoAttacks.Add (new attackIndex (searchAttackList ("Agility"), 44));
		
		int[] tm = {Whirlwind, Toxic,BodySlam, TakeDown, DoubleEdge,Rage,Mimic,DoubleTeam,Reflect, Bide,SkullBash,
			SkyAttack,Rest,TriAttack, Substitute};
		searchTMList(DoduoAttacks, tm);
		int[] hm = {Fly};
		searchHMList(DoduoAttacks, hm);
		master_attack_list.Add(new masterAttackList("Doduo", DoduoAttacks));
	}

	private void Dodrio(){
		DodrioAttacks.Add (new attackIndex (searchAttackList ("Peck"), 1));
		DodrioAttacks.Add (new attackIndex (searchAttackList ("Growl"),1));
		DodrioAttacks.Add (new attackIndex (searchAttackList ("Fury Attack"), 1));
		DodrioAttacks.Add (new attackIndex (searchAttackList ("Drill Peck"), 30));
		DodrioAttacks.Add (new attackIndex (searchAttackList ("Rage"), 39));
		DodrioAttacks.Add (new attackIndex (searchAttackList ("Tri Attack"), 45));
		DodrioAttacks.Add (new attackIndex (searchAttackList ("Agility"), 51));
		
		int[] tm = {Whirlwind, Toxic,BodySlam, TakeDown, DoubleEdge,HyperBeam, Rage,Mimic,DoubleTeam,Reflect, 
			Bide,SkullBash,	SkyAttack,Rest,TriAttack, Substitute};
		searchTMList(DodrioAttacks, tm);
		int[] hm = {Fly};
		searchHMList(DodrioAttacks, hm);
		master_attack_list.Add(new masterAttackList("Dodrio", DodrioAttacks));
	}

	private void Seel(){
		SeelAttacks.Add (new attackIndex (searchAttackList ("Headbutt"), 1));
		SeelAttacks.Add (new attackIndex (searchAttackList ("Growl"),30));
		SeelAttacks.Add (new attackIndex (searchAttackList ("Aurora Beam"), 35));
		SeelAttacks.Add (new attackIndex (searchAttackList ("Rest"), 40));
		SeelAttacks.Add (new attackIndex (searchAttackList ("Take Down"), 45));
		SeelAttacks.Add (new attackIndex (searchAttackList ("Ice Beam"), 50));
		
		int[] tm = {Toxic,HornDrill, BodySlam, TakeDown, DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard,Payday, Rage,
			Mimic,DoubleTeam, Bide,SkullBash,Rest,Substitute};
		searchTMList(SeelAttacks, tm);
		int[] hm = {Surf,Strength};
		searchHMList(SeelAttacks, hm);
		master_attack_list.Add(new masterAttackList("Seel", SeelAttacks));
	}

	private void Dewgong(){
		DewgongAttacks.Add (new attackIndex (searchAttackList ("Headbutt"), 1));
		DewgongAttacks.Add (new attackIndex (searchAttackList ("Growl"),1));
		DewgongAttacks.Add (new attackIndex (searchAttackList ("Aurora Beam"), 1));
		DewgongAttacks.Add (new attackIndex (searchAttackList ("Rest"), 44));
		DewgongAttacks.Add (new attackIndex (searchAttackList ("Take Down"), 50));
		DewgongAttacks.Add (new attackIndex (searchAttackList ("Ice Beam"), 56));
		
		int[] tm = {Toxic,HornDrill, BodySlam, TakeDown, DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard,HyperBeam, 
			Payday, Rage, Mimic,DoubleTeam, Bide,SkullBash,Rest,Substitute};
		searchTMList(DewgongAttacks, tm);
		int[] hm = {Surf,Strength};
		searchHMList(DewgongAttacks, hm);
		master_attack_list.Add(new masterAttackList("Dewgong", DewgongAttacks));
	}

	private void Grimer(){
		GrimerAttacks.Add (new attackIndex (searchAttackList ("Disable"), 1));
		GrimerAttacks.Add (new attackIndex (searchAttackList ("Pound"),1));
		GrimerAttacks.Add (new attackIndex (searchAttackList ("Poison Gas"), 30));
		GrimerAttacks.Add (new attackIndex (searchAttackList ("Minimize"), 33));
		GrimerAttacks.Add (new attackIndex (searchAttackList ("Sludge"), 37));
		GrimerAttacks.Add (new attackIndex (searchAttackList ("Harden"), 42));
		GrimerAttacks.Add (new attackIndex (searchAttackList ("Screech"), 48));
		GrimerAttacks.Add (new attackIndex (searchAttackList ("Acid Armor"), 55));
		
		int[] tm = {Toxic, BodySlam,Rage,MegaDrain,ThunderBolt,Thunder, Mimic,DoubleTeam, Bide,SelfDestruct,FireBlast,
			Rest,Explosion, Substitute};
		searchTMList(GrimerAttacks, tm);
		master_attack_list.Add(new masterAttackList("Grimer", GrimerAttacks));
	}

	private void Muk(){
		MukAttacks.Add (new attackIndex (searchAttackList ("Disable"), 1));
		MukAttacks.Add (new attackIndex (searchAttackList ("Pound"),1));
		MukAttacks.Add (new attackIndex (searchAttackList ("Poison Gas"), 30));
		MukAttacks.Add (new attackIndex (searchAttackList ("Minimize"), 33));
		MukAttacks.Add (new attackIndex (searchAttackList ("Sludge"), 37));
		MukAttacks.Add (new attackIndex (searchAttackList ("Harden"), 45));
		MukAttacks.Add (new attackIndex (searchAttackList ("Screech"), 53));
		MukAttacks.Add (new attackIndex (searchAttackList ("Acid Armor"), 60));
		
		int[] tm = {Toxic, BodySlam,HyperBeam, Rage,MegaDrain,ThunderBolt,Thunder, Mimic,DoubleTeam, Bide,
			SelfDestruct,FireBlast,	Rest,Explosion, Substitute};
		searchTMList(MukAttacks, tm);
		master_attack_list.Add(new masterAttackList("Muk", MukAttacks));
	}

	private void Shellder(){
		ShellderAttacks.Add (new attackIndex (searchAttackList ("Headbutt"), 1));
		ShellderAttacks.Add (new attackIndex (searchAttackList ("Growl"),1));
		ShellderAttacks.Add (new attackIndex (searchAttackList ("Aurora Beam"), 1));
		ShellderAttacks.Add (new attackIndex (searchAttackList ("Rest"), 44));
		ShellderAttacks.Add (new attackIndex (searchAttackList ("Take Down"), 50));
		ShellderAttacks.Add (new attackIndex (searchAttackList ("Ice Beam"), 56));
		
		int[] tm = {Toxic, TakeDown, DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard, Rage, Mimic,DoubleTeam, Bide,
			SelfDestruct,Swift,Rest,Explosion,TriAttack,Substitute};
		searchTMList(ShellderAttacks, tm);
		int[] hm = {Strength};
		searchHMList(ShellderAttacks, hm);
		master_attack_list.Add(new masterAttackList("Shellder", ShellderAttacks));
	}

	private void Cloyster(){
		CloysterAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		CloysterAttacks.Add (new attackIndex (searchAttackList ("Withdraw"),1));
		CloysterAttacks.Add (new attackIndex (searchAttackList ("Supersonic"), 18));
		CloysterAttacks.Add (new attackIndex (searchAttackList ("Clamp"), 23));
		CloysterAttacks.Add (new attackIndex (searchAttackList ("Aurora Beam"), 30));
		CloysterAttacks.Add (new attackIndex (searchAttackList ("Leer"), 39));
		CloysterAttacks.Add (new attackIndex (searchAttackList ("Ice Beam"), 50));
		
		int[] tm = {Toxic, TakeDown, DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard, Rage, Mimic,DoubleTeam, Bide,
			SelfDestruct,Swift,Rest,Explosion,TriAttack,Substitute};
		searchTMList(CloysterAttacks, tm);
		int[] hm = {Strength};
		searchHMList(CloysterAttacks, hm);
		master_attack_list.Add(new masterAttackList("Cloyster", CloysterAttacks));
	}

	private void Gastly(){
		GastlyAttacks.Add (new attackIndex (searchAttackList ("Confuse Ray"), 1));
		GastlyAttacks.Add (new attackIndex (searchAttackList ("Lick"),1));
		GastlyAttacks.Add (new attackIndex (searchAttackList ("Night Shade"), 18));
		GastlyAttacks.Add (new attackIndex (searchAttackList ("Hypnosis"), 23));
		GastlyAttacks.Add (new attackIndex (searchAttackList ("Dream Eater"), 30));
		
		int[] tm = {Toxic,Rage,MegaDrain,ThunderBolt,Thunder,Psychic_TM,Mimic,DoubleTeam, Bide,SelfDestruct, DreamEater,
			Rest,Psywave,Explosion,Substitute};
		searchTMList(GastlyAttacks, tm);
		master_attack_list.Add(new masterAttackList("Gastly", GastlyAttacks));
	}

	private void Haunter(){
		HaunterAttacks.Add (new attackIndex (searchAttackList ("Confuse Ray"), 1));
		HaunterAttacks.Add (new attackIndex (searchAttackList ("Lick"),1));
		HaunterAttacks.Add (new attackIndex (searchAttackList ("Night Shade"), 1));
		HaunterAttacks.Add (new attackIndex (searchAttackList ("Hypnosis"), 29));
		HaunterAttacks.Add (new attackIndex (searchAttackList ("Dream Eater"), 38));
		
		int[] tm = {Toxic,Rage,MegaDrain,ThunderBolt,Thunder,Psychic_TM,Mimic,DoubleTeam, Bide,SelfDestruct, DreamEater,
			Rest,Psywave,Explosion,Substitute};
		searchTMList(HaunterAttacks, tm);
		master_attack_list.Add(new masterAttackList("Haunter", HaunterAttacks));
	}

	private void Gengar(){
		GengarAttacks.Add (new attackIndex (searchAttackList ("Confuse Ray"), 1));
		GengarAttacks.Add (new attackIndex (searchAttackList ("Lick"),1));
		GengarAttacks.Add (new attackIndex (searchAttackList ("Night Shade"), 1));
		GengarAttacks.Add (new attackIndex (searchAttackList ("Hypnosis"), 29));
		GengarAttacks.Add (new attackIndex (searchAttackList ("Dream Eater"), 38));
		
		int[] tm = {MegaPunch,MegaKick, Toxic,BodySlam,TakeDown,DoubleEdge,HyperBeam,Submission,Counter,SeismicToss,
			Rage,MegaDrain,ThunderBolt,Thunder,Psychic_TM,Mimic,DoubleTeam, Bide,Metronome,SelfDestruct,SkullBash,DreamEater,
			Rest,Psywave,Explosion,Substitute};
		searchTMList(GengarAttacks, tm);
		int[] hm = {Strength};
		searchHMList(GengarAttacks, hm);
		master_attack_list.Add(new masterAttackList("Gengar", GengarAttacks));
	}

	private void Onix(){
		OnixAttacks.Add (new attackIndex (searchAttackList ("Confuse Ray"), 1));
		OnixAttacks.Add (new attackIndex (searchAttackList ("Lick"),1));
		OnixAttacks.Add (new attackIndex (searchAttackList ("Night Shade"), 1));
		OnixAttacks.Add (new attackIndex (searchAttackList ("Hypnosis"), 29));
		OnixAttacks.Add (new attackIndex (searchAttackList ("Dream Eater"), 38));
		
		int[] tm = {Toxic,BodySlam,TakeDown,DoubleEdge,Rage,MegaDrain,Earthquake,Fissure,Dig, Mimic,DoubleTeam, Bide,
			SelfDestruct,SkullBash,Rest,Explosion,RockSlide, Substitute};
		searchTMList(OnixAttacks, tm);
		int[] hm = {Strength};
		searchHMList(OnixAttacks, hm);
		master_attack_list.Add(new masterAttackList("Onix", OnixAttacks));
	}

	private void Drowzee(){
		DrowzeeAttacks.Add (new attackIndex (searchAttackList ("Hypnosis"), 1));
		DrowzeeAttacks.Add (new attackIndex (searchAttackList ("Pound"),1));
		DrowzeeAttacks.Add (new attackIndex (searchAttackList ("Disable"),12));
		DrowzeeAttacks.Add (new attackIndex (searchAttackList ("Confusion"),17));
		DrowzeeAttacks.Add (new attackIndex (searchAttackList ("Headbutt"),24));
		DrowzeeAttacks.Add (new attackIndex (searchAttackList ("Poison Gas"),29));
		DrowzeeAttacks.Add (new attackIndex (searchAttackList ("Psychic"),32));
		DrowzeeAttacks.Add (new attackIndex (searchAttackList ("Meditate"),37));

		
		int[] tm = {MegaPunch,MegaKick, Toxic,BodySlam,TakeDown,DoubleEdge,Submission,Counter,SeismicToss,
			Rage,Psychic_TM,Teleport, Mimic,DoubleTeam,Reflect, Bide,Metronome,SkullBash,DreamEater,
			Rest,ThunderWave, Psywave,TriAttack, Substitute};
		searchTMList(DrowzeeAttacks, tm);
		int[] hm = {Flash};
		searchHMList(DrowzeeAttacks, hm);
		master_attack_list.Add(new masterAttackList("Drowzee", DrowzeeAttacks));
	}

	private void Hypno(){
		HypnoAttacks.Add (new attackIndex (searchAttackList ("Hypnosis"), 1));
		HypnoAttacks.Add (new attackIndex (searchAttackList ("Pound"),1));
		HypnoAttacks.Add (new attackIndex (searchAttackList ("Disable"),1));
		HypnoAttacks.Add (new attackIndex (searchAttackList ("Confusion"),1));
		HypnoAttacks.Add (new attackIndex (searchAttackList ("Headbutt"),24));
		HypnoAttacks.Add (new attackIndex (searchAttackList ("Poison Gas"),33));
		HypnoAttacks.Add (new attackIndex (searchAttackList ("Psychic"),37));
		HypnoAttacks.Add (new attackIndex (searchAttackList ("Meditate"),43));
		
		int[] tm = {MegaPunch,MegaKick, Toxic,BodySlam,TakeDown,DoubleEdge,HyperBeam, Submission,Counter,SeismicToss,
			Rage,Psychic_TM,Teleport, Mimic,DoubleTeam,Reflect, Bide,Metronome,SkullBash,DreamEater,
			Rest,ThunderWave, Psywave,TriAttack, Substitute};
		searchTMList(HypnoAttacks, tm);
		int[] hm = {Flash};
		searchHMList(HypnoAttacks, hm);
		master_attack_list.Add(new masterAttackList("Hypno", HypnoAttacks));
	}

	private void Krabby(){
		KrabbyAttacks.Add (new attackIndex (searchAttackList ("Bubble"), 1));
		KrabbyAttacks.Add (new attackIndex (searchAttackList ("Leer"),1));
		KrabbyAttacks.Add (new attackIndex (searchAttackList ("Vice Grip"),20));
		KrabbyAttacks.Add (new attackIndex (searchAttackList ("Guillotine"),25));
		KrabbyAttacks.Add (new attackIndex (searchAttackList ("Stomp"),30));
		KrabbyAttacks.Add (new attackIndex (searchAttackList ("Crabhammer"),35));
		KrabbyAttacks.Add (new attackIndex (searchAttackList ("Harden"),40));
		
		int[] tm = {SwordsDance, Toxic,BodySlam,TakeDown,DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard,
			Rage,Mimic,DoubleTeam, Bide,Rest,Substitute};
		searchTMList(KrabbyAttacks, tm);
		int[] hm = {Cut,Surf,Strength};
		searchHMList(KrabbyAttacks, hm);
		master_attack_list.Add(new masterAttackList("Krabby", KrabbyAttacks));
	}

	private void Kingler(){
		KinglerAttacks.Add (new attackIndex (searchAttackList ("Bubble"), 1));
		KinglerAttacks.Add (new attackIndex (searchAttackList ("Leer"),1));
		KinglerAttacks.Add (new attackIndex (searchAttackList ("Vice Grip"),20));
		KinglerAttacks.Add (new attackIndex (searchAttackList ("Guillotine"),25));
		KinglerAttacks.Add (new attackIndex (searchAttackList ("Stomp"),34));
		KinglerAttacks.Add (new attackIndex (searchAttackList ("Crabhammer"),42));
		KinglerAttacks.Add (new attackIndex (searchAttackList ("Harden"),49));
		
		int[] tm = {SwordsDance, Toxic,BodySlam,TakeDown,DoubleEdge,HyperBeam, BubbleBeam,WaterGun,IceBeam,Blizzard,
			Rage,Mimic,DoubleTeam, Bide,Rest,Substitute};
		searchTMList(KinglerAttacks, tm);
		int[] hm = {Cut,Surf,Strength};
		searchHMList(KinglerAttacks, hm);
		master_attack_list.Add(new masterAttackList("Kingler", KinglerAttacks));
	}

	private void Voltorb(){
		VoltorbAttacks.Add (new attackIndex (searchAttackList ("Screech"), 1));
		VoltorbAttacks.Add (new attackIndex (searchAttackList ("Tackle"),1));
		VoltorbAttacks.Add (new attackIndex (searchAttackList ("Sonic Boom"),17));
		VoltorbAttacks.Add (new attackIndex (searchAttackList ("Self Destruct"),22));
		VoltorbAttacks.Add (new attackIndex (searchAttackList ("Light Screen"),29));
		VoltorbAttacks.Add (new attackIndex (searchAttackList ("Swift"),36));
		VoltorbAttacks.Add (new attackIndex (searchAttackList ("Explosion"),43));
		
		int[] tm = {Toxic,TakeDown,Rage,ThunderBolt,Thunder,Mimic,DoubleTeam,Reflect,Bide,SelfDestruct,Swift,
			Rest,ThunderWave,Explosion, Substitute};
		searchTMList(VoltorbAttacks, tm);
		int[] hm = {Flash};
		searchHMList(VoltorbAttacks, hm);
		master_attack_list.Add(new masterAttackList("Voltorb", VoltorbAttacks));
	}

	private void Electrode(){
		VoltorbAttacks.Add (new attackIndex (searchAttackList ("Screech"), 1));
		VoltorbAttacks.Add (new attackIndex (searchAttackList ("Tackle"),1));
		VoltorbAttacks.Add (new attackIndex (searchAttackList ("Sonic Boom"),1));
		VoltorbAttacks.Add (new attackIndex (searchAttackList ("Self Destruct"),22));
		VoltorbAttacks.Add (new attackIndex (searchAttackList ("Light Screen"),29));
		VoltorbAttacks.Add (new attackIndex (searchAttackList ("Swift"),40));
		VoltorbAttacks.Add (new attackIndex (searchAttackList ("Explosion"),50));
		
		int[] tm = {Toxic,TakeDown,HyperBeam,Rage,ThunderBolt,Thunder,Mimic,DoubleTeam,Reflect,Bide,SelfDestruct,Swift,
			SkullBash,Rest,ThunderWave,Explosion, Substitute};
		searchTMList(VoltorbAttacks, tm);
		int[] hm = {Flash};
		searchHMList(VoltorbAttacks, hm);
		master_attack_list.Add(new masterAttackList("Electrode", ElectrodeAttacks));
	}
	#endregion

	#region Pokemon 102-126
	private void Exeggcute(){
		ExeggcuteAttacks.Add (new attackIndex (searchAttackList ("Barrage"), 1));
		ExeggcuteAttacks.Add (new attackIndex (searchAttackList ("Hypnosis"), 1));
		ExeggcuteAttacks.Add (new attackIndex (searchAttackList ("Reflect"), 25));
		ExeggcuteAttacks.Add (new attackIndex (searchAttackList ("Leech Seed"), 28));
		ExeggcuteAttacks.Add (new attackIndex (searchAttackList ("Stun Spore"), 32));
		ExeggcuteAttacks.Add (new attackIndex (searchAttackList ("Poison Powder"), 37));
		ExeggcuteAttacks.Add (new attackIndex (searchAttackList ("Solar Beam"), 42));
		ExeggcuteAttacks.Add (new attackIndex (searchAttackList ("Sleep Powder"), 48));
		
		int[] tm = {Toxic, TakeDown, DoubleEdge,Rage,Psychic_TM,Teleport, Mimic, DoubleTeam,Reflect, Bide,SelfDestruct,
			EggBomb,Rest,Psywave,Explosion,Substitute};
		searchTMList(ExeggcuteAttacks, tm);
		master_attack_list.Add(new masterAttackList("Exeggcute", ExeggcuteAttacks));
	}

	private void Exeggutor(){
		ExegutorAttacks.Add (new attackIndex (searchAttackList ("Barrage"), 1));
		ExegutorAttacks.Add (new attackIndex (searchAttackList ("Hypnosis"), 1));
		ExegutorAttacks.Add (new attackIndex (searchAttackList ("Stomp"), 28));
		
		int[] tm = {Toxic, TakeDown,DoubleEdge,HyperBeam,Rage,MegaDrain,SolarBeam, Psychic_TM,Teleport, Mimic, DoubleTeam,
			Reflect, Bide,SelfDestruct,	EggBomb,Rest,Psywave,Explosion,Substitute};
		searchTMList(ExegutorAttacks, tm);
		int[] hm = {Strength};
		searchHMList(ExegutorAttacks, hm);
		master_attack_list.Add(new masterAttackList("Exeggutor", ExegutorAttacks));
	}

	private  void Cubone(){
		CuboneAttacks.Add (new attackIndex (searchAttackList ("Bone Club"), 1));
		CuboneAttacks.Add (new attackIndex (searchAttackList ("Growl"), 1));
		CuboneAttacks.Add (new attackIndex (searchAttackList ("Leer"), 25));
		CuboneAttacks.Add (new attackIndex (searchAttackList ("Focus Energy"), 31));
		CuboneAttacks.Add (new attackIndex (searchAttackList ("Thrash"), 38));
		CuboneAttacks.Add (new attackIndex (searchAttackList ("Bonemerang"), 43));
		CuboneAttacks.Add (new attackIndex (searchAttackList ("Rage"), 46));
		
		int[] tm = {MegaPunch,MegaKick, Toxic,BodySlam, TakeDown,DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard,
			Submission,Counter,SeismicToss,Rage,Earthquake,Fissure,Dig,Mimic,DoubleTeam,Bide,FireBlast,SkullBash,Rest,
			Substitute};
		searchTMList(CuboneAttacks, tm);
		int[] hm = {Strength};
		searchHMList(CuboneAttacks, hm);
		master_attack_list.Add(new masterAttackList("Cubone", CuboneAttacks));
	}

	private void Marowak(){
		MarowakAttacks.Add (new attackIndex (searchAttackList ("Bone Club"), 1));
		MarowakAttacks.Add (new attackIndex (searchAttackList ("Growl"), 1));
		MarowakAttacks.Add (new attackIndex (searchAttackList ("Leer"), 1));
		MarowakAttacks.Add (new attackIndex (searchAttackList ("Focus Energy"), 1));
		MarowakAttacks.Add (new attackIndex (searchAttackList ("Thrash"), 41));
		MarowakAttacks.Add (new attackIndex (searchAttackList ("Bonemerang"), 48));
		MarowakAttacks.Add (new attackIndex (searchAttackList ("Rage"), 55));
		
		int[] tm = {MegaPunch,MegaKick, Toxic,BodySlam, TakeDown,DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard,HyperBeam,
			Submission,Counter,SeismicToss,Rage,Earthquake,Fissure,Dig,Mimic,DoubleTeam,Bide,FireBlast,SkullBash,Rest,
			Substitute};
		searchTMList(MarowakAttacks, tm);
		int[] hm = {Strength};
		searchHMList(MarowakAttacks, hm);
		master_attack_list.Add(new masterAttackList("Marowak", MarowakAttacks));
	}

	private void Hitmonlee(){
		HitmonleeAttacks.Add (new attackIndex (searchAttackList ("Double Kick"), 1));
		HitmonleeAttacks.Add (new attackIndex (searchAttackList ("Meditate"), 1));
		HitmonleeAttacks.Add (new attackIndex (searchAttackList ("Rolling Kick"), 33));
		HitmonleeAttacks.Add (new attackIndex (searchAttackList ("Jump Kick"), 38));
		HitmonleeAttacks.Add (new attackIndex (searchAttackList ("Focus Energy"), 43));
		HitmonleeAttacks.Add (new attackIndex (searchAttackList ("High Jump Kick"), 48));
		HitmonleeAttacks.Add (new attackIndex (searchAttackList ("Mega Kick"), 55));
		
		int[] tm = {MegaPunch,MegaKick, Toxic,BodySlam, TakeDown,DoubleEdge,Submission,Counter,SeismicToss,
			Rage,Mimic,DoubleTeam,Bide,Metronome,Swift,SkullBash,Rest,Substitute};
		searchTMList(HitmonleeAttacks, tm);
		int[] hm = {Strength};
		searchHMList(HitmonleeAttacks, hm);
		master_attack_list.Add(new masterAttackList("Hitmonlee", HitmonleeAttacks));
	}

	private void Hitmonchan(){
		HitmonchanAttacks.Add (new attackIndex (searchAttackList ("Agility"), 1));
		HitmonchanAttacks.Add (new attackIndex (searchAttackList ("Comet Punch"), 1));
		HitmonchanAttacks.Add (new attackIndex (searchAttackList ("Fire Punch"), 33));
		HitmonchanAttacks.Add (new attackIndex (searchAttackList ("Ice Punch"), 38));
		HitmonchanAttacks.Add (new attackIndex (searchAttackList ("Thunder Punch"), 43));
		HitmonchanAttacks.Add (new attackIndex (searchAttackList ("Mega Punch"), 48));
		HitmonchanAttacks.Add (new attackIndex (searchAttackList ("Counter"), 53));
		
		int[] tm = {MegaPunch,MegaKick, Toxic,BodySlam, TakeDown,DoubleEdge,Submission,Counter,SeismicToss,
			Rage,Mimic,DoubleTeam,Bide,Metronome,Swift,SkullBash,Rest,Substitute};
		searchTMList(HitmonchanAttacks, tm);
		int[] hm = {Strength};
		searchHMList(HitmonchanAttacks, hm);
		master_attack_list.Add(new masterAttackList("Hitmonchan", HitmonchanAttacks));
	}

	private void Lickitung(){
		LickitungAttacks.Add (new attackIndex (searchAttackList ("Supersonic"), 1));
		LickitungAttacks.Add (new attackIndex (searchAttackList ("Wrap"), 1));
		LickitungAttacks.Add (new attackIndex (searchAttackList ("Stomp"), 7));
		LickitungAttacks.Add (new attackIndex (searchAttackList ("Disable"), 15));
		LickitungAttacks.Add (new attackIndex (searchAttackList ("Defense Curl"), 23));
		LickitungAttacks.Add (new attackIndex (searchAttackList ("Slam"), 31));
		LickitungAttacks.Add (new attackIndex (searchAttackList ("Screech"), 39));
		
		int[] tm = {MegaPunch,SwordsDance, MegaKick, Toxic,BodySlam, TakeDown,DoubleEdge,BubbleBeam,WaterGun,IceBeam,
			Blizzard,Submission,Counter,SeismicToss,Rage,ThunderBolt,Thunder,Earthquake,Fissure,Mimic,DoubleTeam,Bide,
			FireBlast,SkullBash,Rest,Substitute};
		searchTMList(LickitungAttacks, tm);
		int[] hm = {Cut, Surf, Strength};
		searchHMList(LickitungAttacks, hm);
		master_attack_list.Add(new masterAttackList("Lickitung", LickitungAttacks));
	}

	private void Koffing(){
		KoffingAttacks.Add (new attackIndex (searchAttackList ("Smog"), 1));
		KoffingAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		KoffingAttacks.Add (new attackIndex (searchAttackList ("Sludge"), 32));
		KoffingAttacks.Add (new attackIndex (searchAttackList ("SmokeScreen"), 37));
		KoffingAttacks.Add (new attackIndex (searchAttackList ("Self Destruct"), 40));
		KoffingAttacks.Add (new attackIndex (searchAttackList ("Haze"), 45));
		KoffingAttacks.Add (new attackIndex (searchAttackList ("Explosion"), 48));
		
		int[] tm = {Toxic,Rage,ThunderBolt,Thunder,Mimic,DoubleTeam,Bide,SelfDestruct,FireBlast,Rest,Explosion,
			Substitute};
		searchTMList(KoffingAttacks, tm);
		master_attack_list.Add(new masterAttackList("Koffing", KoffingAttacks));
	}

	private void Weezing(){
		WeezingAttacks.Add (new attackIndex (searchAttackList ("Smog"), 1));
		WeezingAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		WeezingAttacks.Add (new attackIndex (searchAttackList ("Sludge"), 1));
		WeezingAttacks.Add (new attackIndex (searchAttackList ("SmokeScreen"), 39));
		WeezingAttacks.Add (new attackIndex (searchAttackList ("Self Destruct"), 43));
		WeezingAttacks.Add (new attackIndex (searchAttackList ("Haze"), 49));
		WeezingAttacks.Add (new attackIndex (searchAttackList ("Explosion"), 53));
		
		int[] tm = {Toxic,Rage,ThunderBolt,Thunder,Mimic,DoubleTeam,Bide,SelfDestruct,FireBlast,Rest,Explosion,
			Substitute};
		searchTMList(WeezingAttacks, tm);
		master_attack_list.Add(new masterAttackList("Weezing", WeezingAttacks));
	}

	private void Rhyhorn(){
		RhyhornAttacks.Add (new attackIndex (searchAttackList ("Horn Attack"), 1));
		RhyhornAttacks.Add (new attackIndex (searchAttackList ("Stomp"), 30));
		RhyhornAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 35));
		RhyhornAttacks.Add (new attackIndex (searchAttackList ("Fury Attack"), 40));
		RhyhornAttacks.Add (new attackIndex (searchAttackList ("Horn Drill"), 45));
		RhyhornAttacks.Add (new attackIndex (searchAttackList ("Leer"), 50));
		RhyhornAttacks.Add (new attackIndex (searchAttackList ("Take Down"), 55));
		
		int[] tm = {Toxic,HornDrill, BodySlam, TakeDown,DoubleEdge,Rage,ThunderBolt,Thunder,Earthquake,Fissure,Dig, 
			Mimic,DoubleTeam,Bide,FireBlast,SkullBash,Rest,RockSlide, Substitute};
		searchTMList(RhyhornAttacks, tm);
		int[] hm = {Strength};
		searchHMList(RhyhornAttacks, hm);
		master_attack_list.Add(new masterAttackList("Rhyhorn", RhyhornAttacks));
	}

	private void Rhydon(){
		RhydonAttacks.Add (new attackIndex (searchAttackList ("Horn Attack"), 1));
		RhydonAttacks.Add (new attackIndex (searchAttackList ("Stomp"), 1));
		RhydonAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 1));
		RhydonAttacks.Add (new attackIndex (searchAttackList ("Fury Attack"), 1));
		RhydonAttacks.Add (new attackIndex (searchAttackList ("Horn Drill"), 48));
		RhydonAttacks.Add (new attackIndex (searchAttackList ("Leer"), 55));
		RhydonAttacks.Add (new attackIndex (searchAttackList ("Take Down"), 64));
		
		int[] tm = {MegaPunch,MegaKick,Toxic,HornDrill, BodySlam, TakeDown,DoubleEdge,BubbleBeam,WaterGun,IceBeam,
			Blizzard,HyperBeam,Payday,Submission,Counter,SeismicToss,Rage,ThunderBolt,Thunder,Earthquake,Fissure,Dig, 
			Mimic,DoubleTeam,Bide,FireBlast,SkullBash,Rest,RockSlide, Substitute};
		searchTMList(RhydonAttacks, tm);
		int[] hm = {Surf, Strength};
		searchHMList(RhydonAttacks, hm);
		master_attack_list.Add(new masterAttackList("Rhydon", RhydonAttacks));
	}

	private void Chansey(){
		ChanseyAttacks.Add (new attackIndex (searchAttackList ("Double Slap"), 1));
		ChanseyAttacks.Add (new attackIndex (searchAttackList ("Pound"), 1));
		ChanseyAttacks.Add (new attackIndex (searchAttackList ("Sing"), 24));
		ChanseyAttacks.Add (new attackIndex (searchAttackList ("Growl"), 30));
		ChanseyAttacks.Add (new attackIndex (searchAttackList ("Minimize"), 38));
		ChanseyAttacks.Add (new attackIndex (searchAttackList ("Defense Curl"), 44));
		ChanseyAttacks.Add (new attackIndex (searchAttackList ("Light Screen"), 48));
		ChanseyAttacks.Add (new attackIndex (searchAttackList ("Double Edge"), 54));
		
		int[] tm = {MegaPunch,MegaKick,Toxic,HornDrill, BodySlam, TakeDown,DoubleEdge,BubbleBeam,WaterGun,IceBeam,
			Blizzard,HyperBeam,Submission,Counter,SeismicToss,Rage,SolarBeam,ThunderBolt,Thunder,Psychic_TM,Teleport, 
			Mimic,DoubleTeam,Bide,Metronome,EggBomb,FireBlast,SkullBash,SoftBoiled,Rest,ThunderWave,Psywave,TriAttack,
			Substitute};
		searchTMList(ChanseyAttacks, tm);
		int[] hm = {Strength,Flash};
		searchHMList(ChanseyAttacks, hm);
		master_attack_list.Add(new masterAttackList("Chansey", ChanseyAttacks));
	}

	private void Tangela(){
		TangelaAttacks.Add (new attackIndex (searchAttackList ("Bind"), 1));
		TangelaAttacks.Add (new attackIndex (searchAttackList ("Constrict"), 1));
		TangelaAttacks.Add (new attackIndex (searchAttackList ("Absorb"), 29));
		TangelaAttacks.Add (new attackIndex (searchAttackList ("Poison Powder"), 32));
		TangelaAttacks.Add (new attackIndex (searchAttackList ("Stun Spore"), 36));
		TangelaAttacks.Add (new attackIndex (searchAttackList ("Sleep Powder"), 39));
		TangelaAttacks.Add (new attackIndex (searchAttackList ("Slam"), 45));
		TangelaAttacks.Add (new attackIndex (searchAttackList ("Growth"), 49));
		
		int[] tm = {SwordsDance,Toxic,BodySlam,TakeDown,DoubleEdge,HyperBeam,Rage,MegaDrain,SolarBeam,
			Mimic,DoubleTeam,Bide,SkullBash,Rest,Substitute};
		searchTMList(TangelaAttacks, tm);
		int[] hm = {Cut};
		searchHMList(TangelaAttacks, hm);
		master_attack_list.Add(new masterAttackList("Tangela", TangelaAttacks));
	}

	private void Kangaskhan(){
		KangaskhanAttacks.Add (new attackIndex (searchAttackList ("Comet Punch"), 1));
		KangaskhanAttacks.Add (new attackIndex (searchAttackList ("Rage"), 1));
		KangaskhanAttacks.Add (new attackIndex (searchAttackList ("Bite"), 26));
		KangaskhanAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 31));
		KangaskhanAttacks.Add (new attackIndex (searchAttackList ("Mega Punch"), 36));
		KangaskhanAttacks.Add (new attackIndex (searchAttackList ("Leer"), 41));
		KangaskhanAttacks.Add (new attackIndex (searchAttackList ("Dizzy Punch"), 46));
		
		int[] tm = {MegaPunch,MegaKick,Toxic, BodySlam, TakeDown,DoubleEdge,BubbleBeam,WaterGun,IceBeam,
			Blizzard,HyperBeam,Submission,Counter,SeismicToss,Rage,ThunderBolt,Thunder,Earthquake,Fissure, 
			Mimic,DoubleTeam,Bide,FireBlast,SkullBash,Rest,RockSlide,Substitute};
		searchTMList(KangaskhanAttacks, tm);
		int[] hm = {Surf,Strength};
		searchHMList(KangaskhanAttacks, hm);
		master_attack_list.Add(new masterAttackList("Kangaskhan", KangaskhanAttacks));
	}

	private void Horsea(){
		HorseaAttacks.Add (new attackIndex (searchAttackList ("Bubble"), 1));
		HorseaAttacks.Add (new attackIndex (searchAttackList ("SmokeScreen"), 19));
		HorseaAttacks.Add (new attackIndex (searchAttackList ("Leer"), 24));
		HorseaAttacks.Add (new attackIndex (searchAttackList ("Water Gun"), 30));
		HorseaAttacks.Add (new attackIndex (searchAttackList ("Agility"), 37));
		HorseaAttacks.Add (new attackIndex (searchAttackList ("Hydro Pump"), 45));
		
		int[] tm = {Toxic,TakeDown,DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard,Rage,Mimic,DoubleTeam,Bide,Swift,
			SkullBash,Rest,Substitute};
		searchTMList(HorseaAttacks, tm);
		int[] hm = {Surf};
		searchHMList(HorseaAttacks, hm);
		master_attack_list.Add(new masterAttackList("Horsea", HorseaAttacks));
	}

	private void Seadra(){
		SeadraAttacks.Add (new attackIndex (searchAttackList ("Bubble"), 1));
		SeadraAttacks.Add (new attackIndex (searchAttackList ("SmokeScreen"), 1));
		SeadraAttacks.Add (new attackIndex (searchAttackList ("Leer"), 24));
		SeadraAttacks.Add (new attackIndex (searchAttackList ("Water Gun"), 30));
		SeadraAttacks.Add (new attackIndex (searchAttackList ("Agility"), 41));
		SeadraAttacks.Add (new attackIndex (searchAttackList ("Hydro Pump"), 52));
		
		int[] tm = {Toxic,TakeDown,DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard,HyperBeam, Rage,Mimic,DoubleTeam,
			Bide,Swift,SkullBash,Rest,Substitute};
		searchTMList(SeadraAttacks, tm);
		int[] hm = {Surf};
		searchHMList(SeadraAttacks, hm);
		master_attack_list.Add(new masterAttackList("Seadra", SeadraAttacks));
	}

	private void Goldeen(){
		GoldeenAttacks.Add (new attackIndex (searchAttackList ("Peck"), 1));
		GoldeenAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 1));
		GoldeenAttacks.Add (new attackIndex (searchAttackList ("Supersonic"), 19));
		GoldeenAttacks.Add (new attackIndex (searchAttackList ("Horn Attack"), 24));
		GoldeenAttacks.Add (new attackIndex (searchAttackList ("Fury Attack"), 30));
		GoldeenAttacks.Add (new attackIndex (searchAttackList ("Waterfall"), 37));
		GoldeenAttacks.Add (new attackIndex (searchAttackList ("Horn Drill"), 45));
		GoldeenAttacks.Add (new attackIndex (searchAttackList ("Agility"), 54));
		
		int[] tm = {Toxic,HornDrill,TakeDown,DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard,Rage,Mimic,DoubleTeam,
			Bide,Swift,SkullBash,Rest,Substitute};
		searchTMList(GoldeenAttacks, tm);
		int[] hm = {Surf};
		searchHMList(GoldeenAttacks, hm);
		master_attack_list.Add(new masterAttackList("Goldeen", GoldeenAttacks));
	}

	private void Seaking(){
		SeakingAttacks.Add (new attackIndex (searchAttackList ("Peck"), 1));
		SeakingAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 1));
		SeakingAttacks.Add (new attackIndex (searchAttackList ("Supersonic"), 1));
		SeakingAttacks.Add (new attackIndex (searchAttackList ("Horn Attack"), 24));
		SeakingAttacks.Add (new attackIndex (searchAttackList ("Fury Attack"), 30));
		SeakingAttacks.Add (new attackIndex (searchAttackList ("Waterfall"), 37));
		SeakingAttacks.Add (new attackIndex (searchAttackList ("Horn Drill"), 45));
		SeakingAttacks.Add (new attackIndex (searchAttackList ("Agility"), 54));
		
		int[] tm = {Toxic,HornDrill,TakeDown,DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard,HyperBeam, Rage,Mimic,
			DoubleTeam,	Bide,Swift,SkullBash,Rest,Substitute};
		searchTMList(SeakingAttacks, tm);
		int[] hm = {Surf};
		searchHMList(SeakingAttacks, hm);
		master_attack_list.Add(new masterAttackList("Seaking", SeakingAttacks));
	}

	private void Staryu(){
		StaryuAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		StaryuAttacks.Add (new attackIndex (searchAttackList ("Water Gun"), 17));
		StaryuAttacks.Add (new attackIndex (searchAttackList ("Harden"), 22));
		StaryuAttacks.Add (new attackIndex (searchAttackList ("Recover"), 27));
		StaryuAttacks.Add (new attackIndex (searchAttackList ("Swift"), 32));
		StaryuAttacks.Add (new attackIndex (searchAttackList ("Minimize"), 37));
		StaryuAttacks.Add (new attackIndex (searchAttackList ("Light Screen"), 42));
		StaryuAttacks.Add (new attackIndex (searchAttackList ("Hydro Pump"), 47));
		
		int[] tm = {Toxic,TakeDown,DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard,Rage,ThunderBolt,Thunder,Psychic_TM,
			Teleport,Mimic, DoubleTeam,Reflect,	Bide,Swift,SkullBash,Rest,ThunderWave,Psywave,TriAttack,Substitute};
		searchTMList(StaryuAttacks, tm);
		int[] hm = {Flash,Surf};
		searchHMList(StaryuAttacks, hm);
		master_attack_list.Add(new masterAttackList("Staryu", StaryuAttacks));
	}

	private void Starmie(){
		StarmieAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		StarmieAttacks.Add (new attackIndex (searchAttackList ("Water Gun"), 17));
		StarmieAttacks.Add (new attackIndex (searchAttackList ("Harden"), 22));
		
		int[] tm = {Toxic,TakeDown,DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard,HyperBeam,Rage,ThunderBolt,Thunder,
			Psychic_TM,Teleport,Mimic, DoubleTeam,Reflect,	Bide,Swift,SkullBash,Rest,ThunderWave,Psywave,TriAttack,
			Substitute};
		searchTMList(StarmieAttacks, tm);
		int[] hm = {Flash,Surf};
		searchHMList(StarmieAttacks, hm);
		master_attack_list.Add(new masterAttackList("Starmie", StarmieAttacks));
	}

	private void MrMime(){
		MrMimeAttacks.Add (new attackIndex (searchAttackList ("Barrier"), 1));
		MrMimeAttacks.Add (new attackIndex (searchAttackList ("Confusion"), 1));
		MrMimeAttacks.Add (new attackIndex (searchAttackList ("Light Screen"), 23));
		MrMimeAttacks.Add (new attackIndex (searchAttackList ("Double Slap"), 31));
		MrMimeAttacks.Add (new attackIndex (searchAttackList ("Meditate"), 39));
		MrMimeAttacks.Add (new attackIndex (searchAttackList ("Substitute"), 47));
		
		int[] tm = {MegaPunch,MegaKick,Toxic,TakeDown,DoubleEdge,HyperBeam,Submission,Counter,SeismicToss,Rage,SolarBeam
			,ThunderBolt,Thunder,Psychic_TM,Teleport,Mimic,DoubleTeam,Reflect,	Bide,Metronome,SkullBash,Rest,ThunderWave,
			Psywave,Substitute};
		searchTMList(MrMimeAttacks, tm);
		int[] hm = {Flash};
		searchHMList(MrMimeAttacks, hm);
		master_attack_list.Add(new masterAttackList("MrMime", MrMimeAttacks));
	}

	private void Scyther(){
		ScytherAttacks.Add (new attackIndex (searchAttackList ("Quick Attack"), 1));
		ScytherAttacks.Add (new attackIndex (searchAttackList ("Leer"), 17));
		ScytherAttacks.Add (new attackIndex (searchAttackList ("Focus Energy"), 20));
		ScytherAttacks.Add (new attackIndex (searchAttackList ("Double Team"), 24));
		ScytherAttacks.Add (new attackIndex (searchAttackList ("Slash"), 29));
		ScytherAttacks.Add (new attackIndex (searchAttackList ("Swords Dance"), 35));
		ScytherAttacks.Add (new attackIndex (searchAttackList ("Agility"), 42));
		
		int[] tm = {SwordsDance,Toxic,TakeDown,DoubleEdge,HyperBeam,Rage,Mimic,DoubleTeam,Bide,Swift,SkullBash,Rest,
			Substitute};
		searchTMList(ScytherAttacks, tm);
		int[] hm = {Cut};
		searchHMList(ScytherAttacks, hm);
		master_attack_list.Add(new masterAttackList("Scyther", ScytherAttacks));
	}

	private void Jynx(){
		JynxAttacks.Add (new attackIndex (searchAttackList ("Lovely Kiss"), 1));
		JynxAttacks.Add (new attackIndex (searchAttackList ("Pound"), 1));
		JynxAttacks.Add (new attackIndex (searchAttackList ("Lick"), 18));
		JynxAttacks.Add (new attackIndex (searchAttackList ("Double Slap"), 23));
		JynxAttacks.Add (new attackIndex (searchAttackList ("Ice Punch"), 31));
		JynxAttacks.Add (new attackIndex (searchAttackList ("Body Slam"), 39));
		JynxAttacks.Add (new attackIndex (searchAttackList ("Thrash"), 47));
		JynxAttacks.Add (new attackIndex (searchAttackList ("Blizzard"), 58));
		
		int[] tm = {MegaPunch,MegaKick,Toxic,TakeDown,DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard,HyperBeam,
			Submission,Counter,SeismicToss,Rage,Psychic_TM,Teleport,Mimic,DoubleTeam,Reflect,Bide,Metronome,SkullBash,Rest,
			Psywave,Substitute};
		searchTMList(JynxAttacks, tm);
		master_attack_list.Add(new masterAttackList("Jynx", JynxAttacks));
	}

	private void Electrabuzz(){
		ElectrabuzzAttacks.Add (new attackIndex (searchAttackList ("Leer"), 1));
		ElectrabuzzAttacks.Add (new attackIndex (searchAttackList ("Quick Attack"), 1));
		ElectrabuzzAttacks.Add (new attackIndex (searchAttackList ("Thunder Shock"), 34));
		ElectrabuzzAttacks.Add (new attackIndex (searchAttackList ("Screech"), 37));
		ElectrabuzzAttacks.Add (new attackIndex (searchAttackList ("Thunder Punch"), 42));
		ElectrabuzzAttacks.Add (new attackIndex (searchAttackList ("Light Screen"), 49));
		ElectrabuzzAttacks.Add (new attackIndex (searchAttackList ("Thunder"), 54));
		
		int[] tm = {MegaPunch,MegaKick,Toxic,BodySlam,TakeDown,DoubleEdge,HyperBeam,Submission,Counter,SeismicToss,Rage,
			ThunderBolt,Thunder,Psychic_TM,Teleport,Mimic,DoubleTeam,Reflect,Bide,Metronome,SkullBash,Rest,ThunderWave,
			Psywave,Substitute};
		searchTMList(ElectrabuzzAttacks, tm);
		int[] hm = {Strength,Flash};
		searchHMList(ElectrabuzzAttacks, hm);
		master_attack_list.Add(new masterAttackList("Electrabuzz", ElectrabuzzAttacks));
	}

	private void Magmar(){
		MagmarAttacks.Add (new attackIndex (searchAttackList ("Ember"), 1));
		MagmarAttacks.Add (new attackIndex (searchAttackList ("Leer"), 36));
		MagmarAttacks.Add (new attackIndex (searchAttackList ("Confuse Ray"), 39));
		MagmarAttacks.Add (new attackIndex (searchAttackList ("Fire Punch"), 43));
		MagmarAttacks.Add (new attackIndex (searchAttackList ("SmokeScreen"), 48));
		MagmarAttacks.Add (new attackIndex (searchAttackList ("Smog"), 52));
		MagmarAttacks.Add (new attackIndex (searchAttackList ("Flamethrower"), 55));
				
		int[] tm = {MegaPunch,MegaKick,Toxic,BodySlam,TakeDown,DoubleEdge,HyperBeam,Submission,Counter,SeismicToss,Rage,
			Psychic_TM,Teleport,Mimic,DoubleTeam,Bide,Metronome,FireBlast,SkullBash,Rest,Psywave,Substitute};
		searchTMList(MagmarAttacks, tm);
		int[] hm = {Strength};
		searchHMList(MagmarAttacks, hm);
		master_attack_list.Add(new masterAttackList("Magmar", MagmarAttacks));
	}

	#endregion

	#region Pokemon 127-151
	private void Pinsir(){
		PinsirAttacks.Add (new attackIndex (searchAttackList ("Vice Grip"), 1));
		PinsirAttacks.Add (new attackIndex (searchAttackList ("Seismic Toss"), 25));
		PinsirAttacks.Add (new attackIndex (searchAttackList ("Guillotine"), 30));
		PinsirAttacks.Add (new attackIndex (searchAttackList ("Focus Energy"), 36));
		PinsirAttacks.Add (new attackIndex (searchAttackList ("Harden"), 43));
		PinsirAttacks.Add (new attackIndex (searchAttackList ("Slash"), 49));
		PinsirAttacks.Add (new attackIndex (searchAttackList ("Swords Dance"), 54));
		
		int[] tm = {SwordsDance, Toxic,BodySlam, TakeDown,DoubleEdge,HyperBeam,Submission,SeismicToss,Rage,Mimic, 
			DoubleTeam,Bide,Rest,Substitute};
		searchTMList(PinsirAttacks, tm);
		int[] hm = {Cut,Strength};
		searchHMList(PinsirAttacks, hm);
		master_attack_list.Add(new masterAttackList("Pinsir", PinsirAttacks));
	}

	private void Tauros(){
		TaurosAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		TaurosAttacks.Add (new attackIndex (searchAttackList ("Stomp"), 21));
		TaurosAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 28));
		TaurosAttacks.Add (new attackIndex (searchAttackList ("Leer"), 35));
		TaurosAttacks.Add (new attackIndex (searchAttackList ("Rage"), 44));
		TaurosAttacks.Add (new attackIndex (searchAttackList ("Take Down"), 51));
		
		int[] tm = {Toxic,HornDrill,BodySlam,TakeDown,DoubleEdge,IceBeam,Blizzard,HyperBeam,Rage,ThunderBolt,Thunder,
			Earthquake,Fissure,Mimic,DoubleTeam,Bide,FireBlast,SkullBash,Rest,Substitute}; 
		searchTMList(PinsirAttacks, tm);
		int[] hm = {Strength};
		searchHMList(PinsirAttacks, hm);
		master_attack_list.Add(new masterAttackList("Tauros", TaurosAttacks));
	}

	private void Magikarp(){
		MagikarpAttacks.Add (new attackIndex (searchAttackList ("Splash"), 1));
		MagikarpAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 15));
		master_attack_list.Add(new masterAttackList("Magikarp", MagikarpAttacks));
	}

	private void Gyarados(){
		GyaradosAttacks.Add (new attackIndex (searchAttackList ("Bite"), 1));
		GyaradosAttacks.Add (new attackIndex (searchAttackList ("Hydro Pump"), 1));
		GyaradosAttacks.Add (new attackIndex (searchAttackList ("Leer"), 1));
		GyaradosAttacks.Add (new attackIndex (searchAttackList ("Dragon Rage"), 1));
		GyaradosAttacks.Add (new attackIndex (searchAttackList ("Hyper Beam"), 52));
		
		int[] tm = {Toxic,BodySlam,TakeDown,DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard,HyperBeam,Rage,DragonRage,
			ThunderBolt,Thunder,Mimic,DoubleTeam,Reflect,Bide,FireBlast,SkullBash,Rest,Substitute}; 
		searchTMList(GyaradosAttacks, tm);
		int[] hm = {Surf,Strength};
		searchHMList(GyaradosAttacks, hm);
		master_attack_list.Add(new masterAttackList("Gyarados", GyaradosAttacks));
	}

	private void Lapras(){
		LaprasAttacks.Add (new attackIndex (searchAttackList ("Growl"), 1));
		LaprasAttacks.Add (new attackIndex (searchAttackList ("Water Gun"), 1));
		LaprasAttacks.Add (new attackIndex (searchAttackList ("Sing"), 16));
		LaprasAttacks.Add (new attackIndex (searchAttackList ("Mist"), 20));
		LaprasAttacks.Add (new attackIndex (searchAttackList ("Body Slam"), 25));
		LaprasAttacks.Add (new attackIndex (searchAttackList ("Confuse Ray"), 31));
		LaprasAttacks.Add (new attackIndex (searchAttackList ("Ice Beam"), 38));
		LaprasAttacks.Add (new attackIndex (searchAttackList ("Hydro Pump"), 46	));
		
		int[] tm = {Toxic,HornDrill,BodySlam,TakeDown,DoubleEdge,BubbleBeam,WaterGun,IceBeam,Blizzard,HyperBeam,Rage,
			SolarBeam,DragonRage,ThunderBolt,Thunder,Psychic_TM,Mimic,DoubleTeam,Reflect,Bide,SkullBash,Rest,Psywave,
			Substitute}; 
		searchTMList(LaprasAttacks, tm);
		int[] hm = {Surf,Strength};
		searchHMList(LaprasAttacks, hm);
		master_attack_list.Add(new masterAttackList("Lapras", LaprasAttacks));
	}	

	private void Dittio(){
		DittoAttacks.Add (new attackIndex (searchAttackList ("Transform"), 1));
		master_attack_list.Add(new masterAttackList("Ditto", DittoAttacks));
	}

	private void Eevee(){
		EeveeAttacks.Add (new attackIndex (searchAttackList ("Sand Attack"), 1));
		EeveeAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		EeveeAttacks.Add (new attackIndex (searchAttackList ("Quick Attack"), 27));
		EeveeAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 31));
		EeveeAttacks.Add (new attackIndex (searchAttackList ("Bite"), 37));
		EeveeAttacks.Add (new attackIndex (searchAttackList ("Take Down"), 45));
		
		int[] tm = {Toxic,BodySlam,TakeDown,DoubleEdge,Rage,Mimic,DoubleTeam,Reflect,Bide,Swift,SkullBash,Rest,
			Substitute}; 
		searchTMList(LaprasAttacks, tm);
		master_attack_list.Add(new masterAttackList("Eevee", EeveeAttacks));
	}

	private void Vaporeon(){
		VaporeonAttacks.Add (new attackIndex (searchAttackList ("Sand Attack"), 1));
		VaporeonAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		VaporeonAttacks.Add (new attackIndex (searchAttackList ("Quick Attack"), 1));
		VaporeonAttacks.Add (new attackIndex (searchAttackList ("Water Gun"), 1));
		VaporeonAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 31));
		VaporeonAttacks.Add (new attackIndex (searchAttackList ("Bite"), 37));
		VaporeonAttacks.Add (new attackIndex (searchAttackList ("Acid Armor"), 40));
		VaporeonAttacks.Add (new attackIndex (searchAttackList ("Haze"), 44));
		VaporeonAttacks.Add (new attackIndex (searchAttackList ("Mist"), 48));
		VaporeonAttacks.Add (new attackIndex (searchAttackList ("Hydro Pump"), 54));
		
		int[] tm = {Toxic,BodySlam,TakeDown,DoubleEdge,WaterGun,BubbleBeam,IceBeam,Blizzard,HyperBeam,Rage,Mimic,
			DoubleTeam,Reflect,Bide,Swift,SkullBash,Rest,Substitute}; 
		searchTMList(VaporeonAttacks, tm);
		int[] hm = {Surf};
		searchHMList(VaporeonAttacks, hm);
		master_attack_list.Add(new masterAttackList("Vaporeon", VaporeonAttacks));
	}

	private void Jolteon(){
		JolteonAttacks.Add (new attackIndex (searchAttackList ("Sand Attack"), 1));
		JolteonAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		JolteonAttacks.Add (new attackIndex (searchAttackList ("Quick Attack"), 1));
		JolteonAttacks.Add (new attackIndex (searchAttackList ("Thunder Shock"), 1));
		JolteonAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 37));
		JolteonAttacks.Add (new attackIndex (searchAttackList ("Thunder Wave"), 40));
		JolteonAttacks.Add (new attackIndex (searchAttackList ("Double Kick"), 42));
		JolteonAttacks.Add (new attackIndex (searchAttackList ("Agility"), 44));
		JolteonAttacks.Add (new attackIndex (searchAttackList ("Pin Missile"), 48));
		JolteonAttacks.Add (new attackIndex (searchAttackList ("Thunder"), 54));
		
		int[] tm = {Toxic,BodySlam,TakeDown,DoubleEdge,HyperBeam,Rage,ThunderBolt,Thunder,Mimic,DoubleTeam,Reflect,Bide,
			Swift,SkullBash,Rest,ThunderWave,Substitute}; 
		searchTMList(JolteonAttacks, tm);
		int[] hm = {Flash};
		searchHMList(JolteonAttacks, hm);
		master_attack_list.Add(new masterAttackList("Jolteon", JolteonAttacks));
	}

	private void Flareon(){
		FlareonAttacks.Add (new attackIndex (searchAttackList ("Sand Attack"), 1));
		FlareonAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		FlareonAttacks.Add (new attackIndex (searchAttackList ("Quick Attack"), 1));
		FlareonAttacks.Add (new attackIndex (searchAttackList ("Ember"), 1));
		FlareonAttacks.Add (new attackIndex (searchAttackList ("Tail Whip"), 37));
		FlareonAttacks.Add (new attackIndex (searchAttackList ("Bite"), 40));
		FlareonAttacks.Add (new attackIndex (searchAttackList ("Leer"), 42));
		FlareonAttacks.Add (new attackIndex (searchAttackList ("Fire Spin"), 44));
		FlareonAttacks.Add (new attackIndex (searchAttackList ("Rage"), 48));
		FlareonAttacks.Add (new attackIndex (searchAttackList ("Flamethrower"), 54));
		
		int[] tm = {Toxic,BodySlam,TakeDown,DoubleEdge,HyperBeam,Rage,Mimic,DoubleTeam,Reflect,Bide,FireBlast,Swift,
			SkullBash,Rest,Substitute}; 
		searchTMList(FlareonAttacks, tm);
		int[] hm = {Flash};
		searchHMList(FlareonAttacks, hm);
		master_attack_list.Add(new masterAttackList("Flareon", FlareonAttacks));
	}

	private void Porygon(){
		PorygonAttacks.Add (new attackIndex (searchAttackList ("Tackle"), 1));
		PorygonAttacks.Add (new attackIndex (searchAttackList ("Confusion"), 1));
		PorygonAttacks.Add (new attackIndex (searchAttackList ("Sharpen"), 1));
		PorygonAttacks.Add (new attackIndex (searchAttackList ("Psybeam"), 23));
		PorygonAttacks.Add (new attackIndex (searchAttackList ("Recover"), 28));
		PorygonAttacks.Add (new attackIndex (searchAttackList ("Agility"), 35));
		PorygonAttacks.Add (new attackIndex (searchAttackList ("Tri Attack"), 42));


		int[] tm = {Toxic,BodySlam,TakeDown,DoubleEdge,IceBeam,Blizzard,HyperBeam,Rage,ThunderBolt,Thunder,Psychic_TM,Teleport,Mimic,DoubleTeam,Reflect,Bide,
			Swift,SkullBash,Rest,ThunderWave,Psywave,TriAttack,	Substitute}; 
		searchTMList(PorygonAttacks, tm);
		int[] hm = {Flash};
		searchHMList(PorygonAttacks, hm);
		master_attack_list.Add(new masterAttackList("Porygon", PorygonAttacks));
	}

	private void Omanyte(){
		OmanyteAttacks.Add (new attackIndex (searchAttackList ("Withdraw"), 1));
		OmanyteAttacks.Add (new attackIndex (searchAttackList ("Water Gun"), 1));
		OmanyteAttacks.Add (new attackIndex (searchAttackList ("Horn Attack"), 34));
		OmanyteAttacks.Add (new attackIndex (searchAttackList ("Leer"), 39));
		OmanyteAttacks.Add (new attackIndex (searchAttackList ("Spike Cannon"), 46));
		OmanyteAttacks.Add (new attackIndex (searchAttackList ("Hydro Pump"), 53));

		int[] tm = {Toxic,BodySlam,TakeDown,DoubleEdge,WaterGun,BubbleBeam,IceBeam,Blizzard,Rage,Mimic,DoubleTeam,
			Reflect,Bide,Rest,Substitute}; 
		searchTMList(OmanyteAttacks, tm);
		int[] hm = {Surf};
		searchHMList(OmanyteAttacks, hm);
		master_attack_list.Add(new masterAttackList("Omanyte", OmanyteAttacks));
	}

	private void Omastar(){
		OmastarAttacks.Add (new attackIndex (searchAttackList ("Withdraw"), 1));
		OmastarAttacks.Add (new attackIndex (searchAttackList ("Water Gun"), 1));
		OmastarAttacks.Add (new attackIndex (searchAttackList ("Horn Attack"), 1));
		OmastarAttacks.Add (new attackIndex (searchAttackList ("Leer"), 39));
		OmastarAttacks.Add (new attackIndex (searchAttackList ("Spike Cannon"), 44));
		OmastarAttacks.Add (new attackIndex (searchAttackList ("Hydro Pump"), 49));
		
		int[] tm = {Toxic,BodySlam,TakeDown,DoubleEdge,WaterGun,BubbleBeam,IceBeam,Blizzard,HyperBeam,Submission,
			SeismicToss,Rage,Mimic,DoubleTeam,Reflect,Bide,SkullBash,Rest,Substitute}; 
		searchTMList(OmastarAttacks, tm);
		int[] hm = {Surf};
		searchHMList(OmastarAttacks, hm);
		master_attack_list.Add(new masterAttackList("Omastar", OmastarAttacks));
	}

	private void Kabuto(){
		KabutoAttacks.Add (new attackIndex (searchAttackList ("Harden"), 1));
		KabutoAttacks.Add (new attackIndex (searchAttackList ("Scratch"), 1));
		KabutoAttacks.Add (new attackIndex (searchAttackList ("Absorb"), 34));
		KabutoAttacks.Add (new attackIndex (searchAttackList ("Slash"), 39));
		KabutoAttacks.Add (new attackIndex (searchAttackList ("Leer"), 44));
		KabutoAttacks.Add (new attackIndex (searchAttackList ("Hydro Pump"), 49));
		
		int[] tm = {Toxic,BodySlam,TakeDown,DoubleEdge,WaterGun,BubbleBeam,IceBeam,Rage,Mimic,DoubleTeam,Reflect,Bide,
			Rest,Substitute}; 
		searchTMList(KabutoAttacks, tm);
		int[] hm = {Surf};
		searchHMList(KabutoAttacks, hm);
		master_attack_list.Add(new masterAttackList("Kabuto", KabutoAttacks));
	}

	private void Kabutops(){
		KabutopsAttacks.Add (new attackIndex (searchAttackList ("Harden"), 1));
		KabutopsAttacks.Add (new attackIndex (searchAttackList ("Scratch"), 1));
		KabutopsAttacks.Add (new attackIndex (searchAttackList ("Absorb"), 1));
		KabutopsAttacks.Add (new attackIndex (searchAttackList ("Slash"), 39));
		KabutopsAttacks.Add (new attackIndex (searchAttackList ("Leer"), 46));
		KabutopsAttacks.Add (new attackIndex (searchAttackList ("Hydro Pump"), 53));
		
		int[] tm = {RazorWind,SwordsDance,MegaKick,Toxic,BodySlam,TakeDown,DoubleEdge,WaterGun,BubbleBeam,IceBeam,
			Blizzard,HyperBeam,Submission,SeismicToss,Rage,Mimic,DoubleTeam,Reflect,Bide,SkullBash,Rest,Substitute}; 
		searchTMList(KabutopsAttacks, tm);
		int[] hm = {Surf};
		searchHMList(KabutopsAttacks, hm);
		master_attack_list.Add(new masterAttackList("Kabutops", KabutopsAttacks));
	}

	private void Aeordactyl(){
		AeordactylAttacks.Add (new attackIndex (searchAttackList ("Agility"), 1));
		AeordactylAttacks.Add (new attackIndex (searchAttackList ("Wing Attack"), 1));
		AeordactylAttacks.Add (new attackIndex (searchAttackList ("Supersonic"), 33));
		AeordactylAttacks.Add (new attackIndex (searchAttackList ("Bite"), 38));
		AeordactylAttacks.Add (new attackIndex (searchAttackList ("Take Down"), 45));
		AeordactylAttacks.Add (new attackIndex (searchAttackList ("Hyper Beam"), 54));
		
		int[] tm = {RazorWind,Whirlwind,Toxic,TakeDown,DoubleEdge,HyperBeam,Rage,DragonRage,Mimic,DoubleTeam,Reflect,
			Bide,FireBlast,Swift,SkyAttack,Rest,Substitute}; 
		searchTMList(AeordactylAttacks, tm);
		int[] hm = {Fly};
		searchHMList(AeordactylAttacks, hm);
		master_attack_list.Add(new masterAttackList("Aerodactyl", AeordactylAttacks));
	}

	private void Snorlax(){
		SnorlaxAttacks.Add (new attackIndex (searchAttackList ("Amnesia"), 1));
		SnorlaxAttacks.Add (new attackIndex (searchAttackList ("Headbutt"), 1));
		SnorlaxAttacks.Add (new attackIndex (searchAttackList ("Rest"), 1));
		SnorlaxAttacks.Add (new attackIndex (searchAttackList ("Body Slam"), 35));
		SnorlaxAttacks.Add (new attackIndex (searchAttackList ("Harden"), 41));
		SnorlaxAttacks.Add (new attackIndex (searchAttackList ("Double Edge"), 48));
		SnorlaxAttacks.Add (new attackIndex (searchAttackList ("Hyper Beam"), 56));
		
		int[] tm = {MegaPunch,MegaKick,Toxic,BodySlam,TakeDown,DoubleEdge,WaterGun,BubbleBeam,IceBeam,Blizzard,
			HyperBeam,Payday,Submission,Counter,SeismicToss,Rage,SolarBeam,ThunderBolt,Thunder,Earthquake,Fissure,
			Psychic_TM,Mimic,DoubleTeam,Reflect,Bide,Metronome,SelfDestruct,FireBlast,SkullBash,Rest,Psywave,RockSlide,
			Substitute}; 
		searchTMList(SnorlaxAttacks, tm);
		int[] hm = {Surf,Strength};
		searchHMList(SnorlaxAttacks, hm);
		master_attack_list.Add(new masterAttackList("Snorlax", SnorlaxAttacks));
	}

	private void Articuno(){
		ArticunoAttacks.Add (new attackIndex (searchAttackList ("Peck"), 1));
		ArticunoAttacks.Add (new attackIndex (searchAttackList ("Ice Beam"), 1));
		ArticunoAttacks.Add (new attackIndex (searchAttackList ("Blizzard"), 51));
		ArticunoAttacks.Add (new attackIndex (searchAttackList ("Agility"), 55));
		ArticunoAttacks.Add (new attackIndex (searchAttackList ("Mist"), 60));
		
		int[] tm = {RazorWind,Whirlwind,Toxic,TakeDown,DoubleEdge,WaterGun,BubbleBeam,IceBeam,Blizzard,
			HyperBeam,Rage,Mimic,DoubleTeam,Reflect,Bide,Swift,SkyAttack,Rest,Substitute}; 
		searchTMList(ArticunoAttacks, tm);
		int[] hm = {Surf,Strength};
		searchHMList(ArticunoAttacks, hm);
		master_attack_list.Add(new masterAttackList("Articuno", ArticunoAttacks));
	}

	private void Zapdos(){
		ZapdosAttacks.Add (new attackIndex (searchAttackList ("Drill Peck"), 1));
		ZapdosAttacks.Add (new attackIndex (searchAttackList ("Thunder Shock"), 1));
		ZapdosAttacks.Add (new attackIndex (searchAttackList ("Thunder"), 51));
		ZapdosAttacks.Add (new attackIndex (searchAttackList ("Agility"), 55));
		ZapdosAttacks.Add (new attackIndex (searchAttackList ("Light Screen"), 60));
		
		int[] tm = {RazorWind,Whirlwind,Toxic,TakeDown,DoubleEdge,Rage,ThunderBolt,Thunder,Mimic,DoubleTeam,Reflect,
			Bide,Metronome,Swift,SkyAttack,Rest,ThunderWave,Substitute}; 
		searchTMList(ZapdosAttacks, tm);
		int[] hm = {Fly,Flash};
		searchHMList(ZapdosAttacks, hm);
		master_attack_list.Add(new masterAttackList("Zapados", ZapdosAttacks));
	}

	private void Moltres(){
		MoltresAttacks.Add (new attackIndex (searchAttackList ("Fire Spin"), 1));
		MoltresAttacks.Add (new attackIndex (searchAttackList ("Peck"), 1));
		MoltresAttacks.Add (new attackIndex (searchAttackList ("Leer"), 51));
		MoltresAttacks.Add (new attackIndex (searchAttackList ("Agility"), 55));
		MoltresAttacks.Add (new attackIndex (searchAttackList ("Sky Attack"), 60));
		
		int[] tm = {RazorWind,Whirlwind,Toxic,TakeDown,DoubleEdge,Rage,Mimic,DoubleTeam,Reflect,
			Bide,FireBlast,Swift,SkyAttack,Rest,ThunderWave,Substitute};
		searchTMList(MoltresAttacks, tm);
		int[] hm = {Fly};
		searchHMList(MoltresAttacks, hm);
		master_attack_list.Add(new masterAttackList("Moltres", MoltresAttacks));
	}

	private void Dratini(){
		DratiniAttacks.Add (new attackIndex (searchAttackList ("Leer"), 1));
		DratiniAttacks.Add (new attackIndex (searchAttackList ("Wrap"), 1));
		DratiniAttacks.Add (new attackIndex (searchAttackList ("Thunder Wave"), 10));
		DratiniAttacks.Add (new attackIndex (searchAttackList ("Agility"), 20));
		DratiniAttacks.Add (new attackIndex (searchAttackList ("Slam"), 30));
		DratiniAttacks.Add (new attackIndex (searchAttackList ("Dragon Rage"), 40));
		DratiniAttacks.Add (new attackIndex (searchAttackList ("Hyper Beam"), 50));
		
		int[] tm = {Toxic,BodySlam,TakeDown,DoubleEdge,WaterGun,BubbleBeam,IceBeam,Blizzard,Rage,DragonRage,ThunderBolt,
			Thunder,Mimic,DoubleTeam,Reflect,Bide,FireBlast,Swift,SkullBash,Rest,ThunderWave,Substitute};
		searchTMList(DratiniAttacks, tm);
		int[] hm = {Surf};
		searchHMList(DratiniAttacks, hm);
		master_attack_list.Add(new masterAttackList("Dratini", DratiniAttacks));
	}

	private void Dragonair(){
		DragonairAttacks.Add (new attackIndex (searchAttackList ("Leer"), 1));
		DragonairAttacks.Add (new attackIndex (searchAttackList ("Wrap"), 1));
		DragonairAttacks.Add (new attackIndex (searchAttackList ("Thunder Wave"), 1));
		DragonairAttacks.Add (new attackIndex (searchAttackList ("Agility"), 20));
		DragonairAttacks.Add (new attackIndex (searchAttackList ("Slam"), 35));
		DragonairAttacks.Add (new attackIndex (searchAttackList ("Dragon Rage"), 45));
		DragonairAttacks.Add (new attackIndex (searchAttackList ("Hyper Beam"), 55));
		
		int[] tm = {Toxic,HornDrill,BodySlam,TakeDown,DoubleEdge,WaterGun,BubbleBeam,IceBeam,Blizzard,Rage,DragonRage,
			ThunderBolt,Thunder,Mimic,DoubleTeam,Reflect,Bide,FireBlast,Swift,SkullBash,Rest,ThunderWave,Substitute};
		searchTMList(DragonairAttacks, tm);
		int[] hm = {Surf};
		searchHMList(DragonairAttacks, hm);
		master_attack_list.Add(new masterAttackList("Dragonair", DragonairAttacks));
	}

	private void Dragonite(){
		DragoniteAttacks.Add (new attackIndex (searchAttackList ("Leer"), 1));
		DragoniteAttacks.Add (new attackIndex (searchAttackList ("Wrap"), 1));
		DragoniteAttacks.Add (new attackIndex (searchAttackList ("Thunder Wave"), 1));
		DragoniteAttacks.Add (new attackIndex (searchAttackList ("Agility"), 1));
		DragoniteAttacks.Add (new attackIndex (searchAttackList ("Slam"), 35));
		DragoniteAttacks.Add (new attackIndex (searchAttackList ("Dragon Rage"), 45));
		DragoniteAttacks.Add (new attackIndex (searchAttackList ("Hyper Beam"), 60));
		
		int[] tm = {RazorWind,Toxic,HornDrill,BodySlam,TakeDown,DoubleEdge,WaterGun,BubbleBeam,IceBeam,Blizzard,HyperBeam,
			Rage,DragonRage,ThunderBolt,Thunder,Mimic,DoubleTeam,Reflect,Bide,FireBlast,Swift,SkullBash,Rest,ThunderWave,
			Substitute};
		searchTMList(DragoniteAttacks, tm);
		int[] hm = {Surf,Strength};
		searchHMList(DragoniteAttacks, hm);
		master_attack_list.Add(new masterAttackList("Dragonite", DragoniteAttacks));
	}

	private void MewTwo(){
		MewtwoAttacks.Add (new attackIndex (searchAttackList ("Confusion"), 1));
		MewtwoAttacks.Add (new attackIndex (searchAttackList ("Disable"), 1));
		MewtwoAttacks.Add (new attackIndex (searchAttackList ("Psychic"), 1));
		MewtwoAttacks.Add (new attackIndex (searchAttackList ("Swift"), 1));
		MewtwoAttacks.Add (new attackIndex (searchAttackList ("Barrier"), 63));
		MewtwoAttacks.Add (new attackIndex (searchAttackList ("Psychic"), 66));
		MewtwoAttacks.Add (new attackIndex (searchAttackList ("Recover"), 70));
		MewtwoAttacks.Add (new attackIndex (searchAttackList ("Mist"), 75));
		MewtwoAttacks.Add (new attackIndex (searchAttackList ("Amnesia"), 81));

		int[] tm = {MegaPunch,MegaKick,Toxic,BodySlam,TakeDown,DoubleEdge,WaterGun,BubbleBeam,IceBeam,Blizzard,HyperBeam,
			Payday,Submission,Counter,SeismicToss,Rage,SolarBeam,ThunderBolt,Thunder,Psychic_TM,Teleport,Mimic,DoubleTeam,
			Reflect,Metronome,SelfDestruct,FireBlast,SkullBash,Rest,ThunderWave,Psywave,TriAttack,Substitute};
		searchTMList(MewtwoAttacks, tm);
		int[] hm = {Strength,Flash};
		searchHMList(MewtwoAttacks, hm);
		master_attack_list.Add(new masterAttackList("Mewtwo", MewtwoAttacks));
	}

	private void Mew(){
		MewAttacks.Add (new attackIndex (searchAttackList ("Pound"), 1));
		MewAttacks.Add (new attackIndex (searchAttackList ("Transform"), 10));
		MewAttacks.Add (new attackIndex (searchAttackList ("Mega Punch"), 20));
		MewAttacks.Add (new attackIndex (searchAttackList ("Metronome"), 30));
		MewAttacks.Add (new attackIndex (searchAttackList ("Psychic"), 40));
		
		int[] tm = {MegaPunch,RazorWind,SwordsDance,Whirlwind,MegaKick,Toxic,HornDrill,BodySlam,TakeDown,DoubleEdge,
			WaterGun,BubbleBeam,IceBeam,Blizzard,HyperBeam,Payday,Submission,Counter,SeismicToss,Rage,MegaDrain,
			SolarBeam,DragonRage,ThunderBolt,Thunder,Earthquake,Fissure,Dig,Psychic_TM,Teleport,Mimic,DoubleTeam,Reflect,
			Bide,Metronome,SelfDestruct,EggBomb,FireBlast,Swift,SkullBash,SoftBoiled,DreamEater,SkyAttack,Rest,
			ThunderWave,Psywave,Explosion,RockSlide,TriAttack,Substitute};
		searchTMList(MewAttacks, tm);
		int[] hm = {Cut,Fly,Surf,Strength,Flash};
		searchHMList(MewAttacks, hm);
		master_attack_list.Add(new masterAttackList("Mew", MewAttacks));

		completedDatabaseInitalization = true;
	}
	#endregion

	#region Searching Methods, Getters and Setters
	private attacks searchAttackList (string sName)
	{
		for (int i = 0; i < attackList.Count; i++) {
			string temp = attackList [i].name;
			if (temp == sName) {
				return attackList [i];
			}
		}
		Debug.LogError ("Could not find: " + sName);
		return attackList [0];
	}
	
	public List<attackIndex> masterGetAttacks(int id){
		return master_attack_list[id].attackIndex;
	}

	public string masterGetName(int id){
		return master_attack_list[id].name;
	}
	
	private void searchTMList (List<attackIndex> pokemon,int[] attacks){
		for(int i = 0; i < attacks.Length; i++){
			pokemon.Add (getTMList(attacks[i]));
		}
	}
	
	private attackIndex getTMList(int i){
		i-= 1;
		return TM_List[i];
	}
	
	
	private void searchHMList (List<attackIndex> pokemon, int[] attacks){
		for(int i = 0; i < attacks.Length; i++){
			pokemon.Add (getHMList(attacks[i]));
		}
	}
	
	private attackIndex getHMList(int i){
		i-= 1;
		return HM_List[i];
	}
	

	#endregion
}
#region Structs
public struct attacks
{
	public string name;
	public string type;
	public string cat;
	public int power;
	public int accuracy;
	public int pp;
	
	public attacks (string aName, string aType, string aCat, int aPower, int aAccuracy, int aPP)
	{
		name = aName;
		type = aType;
		cat = aCat;
		power = aPower;
		accuracy = aAccuracy;
		pp = aPP;
	}
	
}

public struct attackIndex
{
	public attacks attack;
	public int level;
	
	public attackIndex (attacks a, int l)
	{
		attack = a;
		level = l;
	}
}

public struct masterAttackList{
	public string name;
	public List<attackIndex> attackIndex;

	public masterAttackList(string n, List<attackIndex> a){
		name = n;
		attackIndex = a;
	}
}
#endregion
