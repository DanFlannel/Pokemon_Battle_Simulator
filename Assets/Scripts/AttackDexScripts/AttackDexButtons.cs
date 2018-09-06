using UnityEngine;
using UnityEngine.UI;

public class AttackDexButtons : MonoBehaviour
{
    public AttackdexGUI gui;
    private Button b;
    public int id;

    private void Awake()
    {
        gui = GameObject.FindGameObjectWithTag("GUIScripts").GetComponent<AttackdexGUI>();
        b = this.GetComponent<Button>();
        AssignOnClick();
    }

    private void AssignOnClick()
    {
        b.onClick.AddListener(delegate
       {
           gui.UpdateInformation(id);
       });
    }

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}