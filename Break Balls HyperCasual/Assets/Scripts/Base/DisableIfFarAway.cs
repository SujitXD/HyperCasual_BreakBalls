using UnityEngine;
using System.Collections;

public class DisableIfFarAway : MonoBehaviour
{
    public bool animOn;
    Animator Anim;
    public string animName;

    // --------------------------------------------------
    // Variables:

    private GameObject itemActivatorObject;
    private ItemActivator activationScript;

    // --------------------------------------------------

    void Start()
    {
        if(GetComponent<Animator>() != null)
            Anim = GetComponent<Animator>();

        if (GameObject.Find("ItemActivatorObject") != null)
        {
            itemActivatorObject = GameObject.Find("ItemActivatorObject");
            activationScript = itemActivatorObject.GetComponent<ItemActivator>();

            StartCoroutine("AddToList");
        }
    }


    IEnumerator AddToList()
    {
        yield return new WaitForSeconds(0.1f);

        activationScript.activatorItems.Add(new ActivatorItem { item = this.gameObject, itemPos = transform.position });
    }

    private void Update()
    {
        if (Anim != null && animOn)
        {
            Anim.Play(animName);
        }
    }
}