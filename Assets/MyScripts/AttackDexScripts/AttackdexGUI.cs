using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AttackdexGUI : MonoBehaviour {

    private Moves moves;
    public int index;

    public Text tName;
    public Text tNum;
    public Text tCategory;
    public Text tPP;
    public Text tPower;
    public Text tAccuracy;
    public Text tDesc;
    public Text tPriority;
    public Text tContest;
    public Text tType;

	// Use this for initialization
	void Start () {
        index = 0;
        moves = GameObject.FindGameObjectWithTag("Moves").GetComponent<Moves>();
        UpdateInformation();
	}

    public void UpdateInformation()
    {
        tName.text = Name();
        tNum.text = Number();
        tPP.text = PP();
        tCategory.text = Category();
        tPower.text = BasePower();
        tAccuracy.text = Accuracy();
        tDesc.text = Description();
        tPriority.text = Priority();
        //tContest.text = ContestType();
        tType.text = AttackType();

    }

    public void UpdateInformation(int n)
    {
        index = n;

        tName.text = Name();
        tNum.text = Number();
        tPP.text = PP();
        tCategory.text = Category();
        tPower.text = BasePower();
        tAccuracy.text = Accuracy();
        tDesc.text = Description();
        tPriority.text = Priority();
        tContest.text = ContestType();
    }

    private string Name()
    {
        return moves.PokemonMoves[index].name.ToString();
    }

    private string Number()
    {
        return moves.PokemonMoves[index].num.ToString();
    }

    private string Category()
    {
        return moves.PokemonMoves[index].category.ToString();
    }

    private string PP()
    {
        return moves.PokemonMoves[index].pp.ToString();
    }

    private string BasePower()
    {
        return moves.PokemonMoves[index].basePower.ToString();
    }

    private string AttackType()
    {
        return moves.PokemonMoves[index].type.ToString();
    }

    private string Accuracy()
    {
        string final = moves.PokemonMoves[index].accuracy.ToString() + "%";
        if(moves.PokemonMoves[index].category == moves.Status)
        {
            final = "-";
        }
        return final;
    }

    private string Description()
    {
        string final = "Description: " + moves.PokemonMoves[index].shortDesc;
        return final;
    }

    private string Priority()
    {
        string final;
        int priority = moves.PokemonMoves[index].priority;
        if(priority == 0)
        {
            final = "none";
        }
        else
        {
            final = moves.PokemonMoves[index].priority.ToString();
        }
        return final;
    }

    private string ContestType()
    {
        return moves.PokemonMoves[index].contestType.ToString();
    }

    public void NextAttack()
    {
        index++;
        if(index >= moves.PokemonMoves.Count)
        {
            index = 0;
        }
        UpdateInformation();
    }

    public void PreviousAttack()
    {
        index--;
        if (index < 0)
        {
            index = moves.PokemonMoves.Count - 1;
        }
        UpdateInformation();
    }
}
