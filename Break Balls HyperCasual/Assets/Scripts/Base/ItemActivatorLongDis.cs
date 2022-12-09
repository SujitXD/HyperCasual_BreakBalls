using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemActivatorLongDis : MonoBehaviour
{

    // --------------------------------------------------
    // Variables:

    [SerializeField]
    private int distanceFromPlayer;

    private GameObject player;

    public List<ActivatorItemLongDis> activatorItems;

    // --------------------------------------------------

    void Start()
    {
        player = GameObject.Find("Player");
        activatorItems = new List<ActivatorItemLongDis>();

        StartCoroutine("CheckActivation");
    }

    IEnumerator CheckActivation()
    {
        List<ActivatorItemLongDis> removeList = new List<ActivatorItemLongDis>();

        if (activatorItems.Count > 0)
        {
            foreach (ActivatorItemLongDis item in activatorItems)
            {
                if (Vector3.Distance(player.transform.position, item.itemPos) > distanceFromPlayer || player.transform.position.z>item.itemPos.z + 2f)
                {
                    if (item.item == null)
                    {
                        removeList.Add(item);
                    }
                    else
                    {
                        //if (player.transform.position.z > item.itemPos.z + 2f)
                        //    Destroy(item.item);
                        //else
                            item.item.SetActive(false);
                    }
                }
                else
                {
                    if (item.item == null)
                    {
                        removeList.Add(item);
                    }
                    else
                    {
                        item.item.SetActive(true);
                    }
                }
            }
        }

        yield return new WaitForSeconds(0.01f);

        if (removeList.Count > 0)
        {
            foreach (ActivatorItemLongDis item in removeList)
            {
                activatorItems.Remove(item);
            }
        }

        yield return new WaitForSeconds(0.01f);
        StartCoroutine("CheckActivation");
    }
}

public class ActivatorItemLongDis
{
    public GameObject item;
    public Vector3 itemPos;
}