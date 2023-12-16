using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupType
{
    None,
    Bomb,
    Shield,
}
public class Powerup : MonoBehaviour
{
    public PowerupType powerupType;
}
