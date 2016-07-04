using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AttackDexList : MonoBehaviour {

    public GameObject prefab;
    public GameObject content;
    public Moves movesData;

	// Use this for initialization
	void Start () {

        movesData = GameObject.FindGameObjectWithTag("Moves").GetComponent<Moves>();
        GenerateList();
	}

    private void GenerateList()
    {
        for(int i = 0; i < movesData.PokemonMoves.Count; i++)
        {
            GameObject go = Instantiate(prefab, content.transform.position, Quaternion.identity) as GameObject;
            go.transform.SetParent(content.transform);
            Text t = go.GetComponentInChildren<Text>();
            t.text = movesData.PokemonMoves[i].name;

            go.GetComponent<AttackDexButtons>().id = i;
        }
    }
}
