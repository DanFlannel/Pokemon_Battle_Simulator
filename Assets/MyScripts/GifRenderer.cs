using UnityEngine;
using System.Collections;
using System;

public class GifRenderer : MonoBehaviour
{

    private SpriteRenderer gifRenderer;
    private Animator anim;
    private bool isPlayer;

    void Start()
    {
        
        anim = this.GetComponent<Animator>();               if (anim == null) Debug.Log("no animator on this obect");
        gifRenderer = this.GetComponent<SpriteRenderer>();  if (gifRenderer == null) Debug.Log("No sprite renderer on this object");

    }

    /// <summary>
    /// This handles the string generation of the pokemon sprite based on direction it is facing, which generation ir belongs to, and its name
    /// The sprite is then fetched from the resources folder and load it into the scene
    /// </summary>
    /// <param name="name">Pokemon's name</param>
    /// <param name="id">Pokemon's ID</param>
    public void ChangeSprite(string name, int id)
    {
        //Debug.Log("called sprite render");
        string gen = checkGen(id);
        string direction = "Front";

        string special  = specialCases(name);
        if(special != null)
        {
            name = special;
        }

        isPlayer = (this.transform.tag == "Player");
        if (isPlayer)
        {
            direction = "Back";
        }


        object o;
        RuntimeAnimatorController control;
        string animationPath;

        animationPath = "Animations/" + gen + "/" + direction + "/" + name.ToUpper() + "_0";


        o = Resources.Load(animationPath);
        if (o == null) Debug.Log("Controller was not there: " + animationPath);
        control = o as RuntimeAnimatorController;
        if (control == null) Debug.Log("Did not load a controller");
        anim.runtimeAnimatorController = control;

        anim.speed = 2f;
        //Debug.Log("Seached Path: " + animationPath);

    }

    /// <summary>
    /// This is to return the proper generation the pokemon were in based off their IDs
    /// </summary>
    /// <param name="id">Pokemon ID</param>
    /// <returns></returns>
    private string checkGen(int id)
    {
        string gen = "";
        if (id <= 151)
        {
            gen = "Gen1";
        }
        else if (id <= 251)
        {
            gen = "Gen2";
        }
        else if (id <= 386)
        {
            gen = "Gen3";
        }
        else if (id <= 493)
        {
            gen = "Gen4";
        }
        else if (id <= 649)
        {
            gen = "Gen5";
        }
        else
        {
            gen = "Gen6";
        }
        return gen;
    }

    /// <summary>
    /// This checks for special cases which the pokemon name doesn't match their sprite sheet and controller
    /// This is for pokemon with special characters in their names
    /// </summary>
    /// <param name="name">Pokemon's nme</param>
    /// <returns></returns>
    private string specialCases(string name)
    {
        string newName = "";
        switch (name)
        {
            default:
                return null;
            case "Mr. Mime":
                newName = "Mr_Mime";
                break;
            case "Farfetch'd":
                newName = "Farfetchd";
                break;
        }
        return newName;
    }

}
