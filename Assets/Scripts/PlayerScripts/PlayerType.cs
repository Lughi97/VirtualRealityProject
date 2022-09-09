using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum typeBall
{
    wood,
    football,
    metal1,
    metal2,
    basketball,
    golf,
    tennis

}

[Serializable]
[CreateAssetMenu(fileName = "TypeSkin", menuName = "Player/Skins")]
public class PlayerType : ScriptableObject
{
    public typeBall type;
    public Material skin;
    public float maxXZVelocity;
    public float mass;
    public float scale;
    public float speedSlopeBoost;

    public bool lineEffect;

    public int costBronze, costSilver, costGold;
    public bool paid = false;
}
