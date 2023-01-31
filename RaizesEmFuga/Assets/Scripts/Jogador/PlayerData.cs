using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObject/Player")]
public class PlayerData : ScriptableObject
{
    public string PlayerName;

    public Color color;

    public Sprite image;
}
