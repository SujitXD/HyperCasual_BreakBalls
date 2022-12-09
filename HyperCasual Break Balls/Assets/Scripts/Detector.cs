using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Obstacle"))
        {
            Crystal crystal = col.transform.parent.parent.GetComponent<Crystal>();
            if(crystal != null)
            {
                if(!crystal.isShattered)
                {
                    GameManager.instance.ballCountFillImg.fillAmount = 0f;
                    CharacterController.instance.ballSpawnCount = 1;

                    for (int i = 0; i < GameManager.instance.ballCountImgs.Count; i++)
                    {
                        if (i == CharacterController.instance.ballSpawnCount - 1)
                        {
                            GameManager.instance.ballCountImgs[i].SetActive(true);
                        }
                        else
                        {
                            GameManager.instance.ballCountImgs[i].SetActive(false);
                        }
                    }
                }
            }
        }
    }
}
