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
        
        anim = this.GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("no animator on this obect");
        }
        gifRenderer = this.GetComponent<SpriteRenderer>();
        if (gifRenderer == null)
        {
            Debug.LogError("No sprite renderer on this object");
        }

    }

    public void ChangeSprite(string name, int id)
    {
        Debug.Log("called sprite render");
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
            Debug.Log("Rendering Player Sprite");
            direction = "Back";
        }
        else
        {
            Debug.Log("Rendering Enemy Sprite");
        }


        object o;
        RuntimeAnimatorController control;
        string animationPath;

        animationPath = "Animations/" + gen + "/" + direction + "/" + name.ToUpper() + "_0";


        o = Resources.Load(animationPath);
        if (o == null)
        {
            Debug.Log("Controller was not there: " + animationPath);
        }
        control = o as RuntimeAnimatorController;
        if (control == null)
        {
            Debug.Log("Did not load a controller");
        }
        anim.runtimeAnimatorController = control;

        anim.speed = 2f;
        Debug.Log("Seached Path: " + animationPath);

    }

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
