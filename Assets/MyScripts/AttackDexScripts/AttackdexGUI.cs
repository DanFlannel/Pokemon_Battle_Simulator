using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using FBG.JSON;
using FBG.Data;

public class AttackdexGUI : MonoBehaviour {

    public int index;
    private AttackData moves;
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
        moves = DexHolder.attackDex;
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
        tType.text = AttackType();
    }

    private string Name()
    {
        return moves.attacks[index].name.ToString();
    }

    private string Number()
    {
        return moves.attacks[index].num.ToString();
    }

    private string Category()
    {
        return moves.attacks[index].category.ToString();
    }

    private string PP()
    {
        return moves.attacks[index].pp.ToString();
    }

    private string BasePower()
    {
        return moves.attacks[index].basePower.ToString();
    }

    private string AttackType()
    {
        return moves.attacks[index].type.ToString();
    }

    private string Accuracy()
    {
        string final = moves.attacks[index].accuracy.ToString() + "%";
        if(moves.attacks[index].category == Consts.Status)
        {
            final = "-";
        }
        return final;
    }

    private string Description()
    {
        string final = "Description: " + moves.attacks[index].shortDesc;
        return final;
    }

    private string Priority()
    {
        string final;
        int priority = moves.attacks[index].priority;
        if(priority == 0)
        {
            final = "none";
        }
        else
        {
            final = moves.attacks[index].priority.ToString();
        }
        return final;
    }

    private string ContestType()
    {
        return moves.attacks[index].contestType.ToString();
    }

    public void NextAttack()
    {
        index++;
        if(index >= moves.attacks.Length)
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
            index = moves.attacks.Length - 1;
        }
        UpdateInformation();
    }
}
