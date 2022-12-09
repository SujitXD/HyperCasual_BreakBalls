using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [System.Serializable]
    public enum Powerups
    {
        MachineBalls,
        FireBalls,
        SlowMotion
    }

    public Powerups powerUps;
}
