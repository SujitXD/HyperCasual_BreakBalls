using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Multiplier : MonoBehaviour
{
    public bool PlusNo;
    public int num;

    // Start is called before the first frame update
    void Start()
    {
        // num = Random.Range(2, 6);
        num = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlusNo)
        {
            transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "+" + num.ToString();
        }
        else
        {
            transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "-" + num.ToString();
        }
    }
}
