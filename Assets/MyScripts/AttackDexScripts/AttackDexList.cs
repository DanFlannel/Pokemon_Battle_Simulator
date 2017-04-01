using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using FBG.Data;
using FBG.JSON;

/// <summary>
/// 
/// </summary>
public class AttackDexList : MonoBehaviour {

    public GameObject prefab;
    public GameObject content;
    private AttackData movesData;

	// Use this for initialization
	void Start () {
        movesData = DexHolder.attackDex;
        GenerateList();
	}

    private void GenerateList()
    {
        for(int i = 0; i < movesData.attacks.Length; i++)
        {
            GameObject go = Instantiate(prefab, content.transform.position, Quaternion.identity) as GameObject;
            go.transform.SetParent(content.transform);

            go.transform.localScale = new Vector3(1, 1, 1);

            Text t = go.GetComponentInChildren<Text>();
            t.text = movesData.attacks[i].name;

            go.GetComponent<AttackDexButtons>().id = i;
        }
    }
}
