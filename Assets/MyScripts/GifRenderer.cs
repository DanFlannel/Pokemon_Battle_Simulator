using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class GifRenderer : MonoBehaviour {

    private SpriteRenderer renderer;
    private Animator anim;
    public string pokemonName { get; set; }
    public bool isPlayer;

    void Start()
    {
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite(string name, int id)
    {
        string gen = checkGen(id);

        string direction = "Front";
        if (isPlayer)
        {
            direction = "Back";
        }

        //Debug.Log("Path: " + "Animations/" + gen + "/" + direction + "/" + name.ToUpper());   //for the controller

        /*
        object o = Resources.Load("Animations/ABRA_0");
        RuntimeAnimatorController control = o as RuntimeAnimatorController;
        anim.runtimeAnimatorController = control;

        o = Resources.Load("Front/SpriteSheets/ABRA_0");
        Sprite pokemon = o as Sprite;
        renderer.sprite = pokemon;*/
        

    }

    private string checkGen(int id)
    {
        string gen = "";
        if(id <= 151)
        {
            gen = "Gen1";
        }else if (id <= 251)
        {
            gen = "Gen2";
        }
        else if(id <= 386)
        {
            gen = "Gen3";
        }
        else if(id <= 493)
        {
            gen = "Gen4";
        }
        else if(id <= 649)
        {
            gen = "Gen5";
        }
        else
        {
            gen = "Gen6";
        }
        return gen;
    }

}
