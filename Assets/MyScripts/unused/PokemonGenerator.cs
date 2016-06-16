using UnityEngine;
using System.Collections;

public class PokemonGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GeneratePokemon(bool isPlayer)
    {

    }

    /// <summary>
    /// Attempting to make each indiviual pokemon a structure so that we can have multiple of them for possible teams later.
    /// </summary>
    public struct Pokemon
    {
        string name;
        int level;

        int curHp;
        int maxHp;

        int attack;
        int defense;
        int spDefense;
        int spAttack;
        int speed;

        int attack_Stage;
        int defense_Stage;
        int speed_Stage;
        int spAttack_Stage;
        int spDefense_Stage;

        string type1;
        string type2;

        string attack1;
        string attack2;
        string attack3;
        string attack4;


    }
}
