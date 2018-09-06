using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Data;

public class AttackDexList : MonoBehaviour
{
    public GameObject prefab;
    public GameObject content;
    private List<GameObject> buttons = new List<GameObject>();
    public int moveCap;

    // Use this for initialization
    private void Start()
    {
        GenerateList();
        loadEntry(0);
    }

    private void GenerateList()
    {
        for (int i = 0; i < DexHolder.attackDex.attacks.Length; i++)
        {
            if (DexHolder.attackDex.attacks[i].num < moveCap)
            {
                GameObject go = Instantiate(prefab, content.transform.position, Quaternion.identity) as GameObject;
                go.transform.SetParent(content.transform);

                go.transform.localScale = new Vector3(1, 1, 1);

                Text t = go.GetComponentInChildren<Text>();
                t.text = DexHolder.attackDex.attacks[i].name;

                go.GetComponent<AttackDexButtons>().id = i;
                buttons.Add(go);
            }
        }
    }

    private void loadEntry(int n)
    {
        GameObject go = buttons[n];
        Button btn = go.GetComponentInChildren<Button>();
        btn.onClick.Invoke();
    }
}