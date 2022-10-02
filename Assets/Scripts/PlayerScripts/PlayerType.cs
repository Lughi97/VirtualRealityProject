using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum typeBall
{
    wood,//0
    football,//1
    metal1,//2
    metal2,//3
    basketball,//4
    golf,//5
    tennis//6

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
