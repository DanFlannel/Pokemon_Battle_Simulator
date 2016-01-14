using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerSprite : MonoBehaviour {

    private SpriteRenderer renderer;
    private Animator anim;
    public string pokemonName { get; set; }
    public bool isPlayer;

    void Start()
    {
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite(string name)
    {
        Debug.Log(name.ToUpper());

        object o = Resources.Load("Front/Animations/ABRA_0");
        RuntimeAnimatorController control = o as RuntimeAnimatorController;
        anim.runtimeAnimatorController = control;

        o = Resources.Load("Front/SpriteSheets/ABRA_0");
        Sprite pokemon = o as Sprite;
        renderer.sprite = pokemon;
        
        if (isPlayer)
        {
        }   
        else
        {

        }
    }

}
