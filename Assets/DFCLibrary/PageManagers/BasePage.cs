using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BasePage : MonoBehaviour, IPage {

    public GameObject root;
    public List<GameObject> defaultOn;
    public List<GameObject> defaultOff;
    public List<IPageElement> pageElements = new List<IPageElement>(); //we should have a solution that makes this serizable in the editor.....
    public float baseTimer = 60f;

    private bool isInitalized = false;

    public virtual void Initalize()
    {
        if(isInitalized) { return; }
        isInitalized = true;

        root = (root == null) ? this.gameObject : root;
        pageElements = root.transform.GetComponentsInChildren<IPageElement>().ToList(); //I don't like this but it will work. We should look into the ODIN asset?
    }

    public virtual void Open()
    {
        if (!isInitalized)
        {
            Initalize();
        }
        enableObjects(true);
        for(int i = 0; i < pageElements.Count; i++) { pageElements[i].onOpen(); }
    }

    public virtual void Close()
    {
        enableObjects(false);
        for (int i = 0; i < pageElements.Count; i++) { pageElements[i].onClose(); }
    }

    public virtual void Reload()
    {
        enableObjects(true);
        for (int i = 0; i < pageElements.Count; i++) { pageElements[i].onReload(); }
    }

    public virtual void doUpdate()
    {
        for (int i = 0; i < pageElements.Count; i++) { pageElements[i].doUpdate(); }
    }

    public virtual void doFixedUpdate()
    {

    }

    public virtual void doLateUpdate()
    {

    }

    //.. Helpers

    public void enableObjects(bool b)
    {
        //Debug.Log(string.Format("Enabling objects on page {0} {1}", root.name, b));
        for (int i = 0; i < defaultOn.Count; i++)
        {
            if (defaultOn[i].GetComponent<Canvas>() != null)
            {
                defaultOn[i].GetComponent<Canvas>().enabled = b;
            }
            else
            {
                defaultOn[i].SetActive(b);
            }
            //Debug.Log(string.Format("setting {0} to {1}", defaultOn[i].name, b));
        }

        for (int i = 0; i < defaultOff.Count; i++)
        {
            if (defaultOff[i].GetComponent<Canvas>() != null)
            {
                defaultOff[i].GetComponent<Canvas>().enabled = false;
            }
            else
            {
                defaultOff[i].SetActive(false);
            }
        }
    }
}
