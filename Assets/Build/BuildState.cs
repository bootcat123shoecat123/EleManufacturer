using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="newState",menuName ="Build")]
public class BuildState:ScriptableObject
{
    public Vector2 size;
    public Vector3 localPositionSetting;
    public Sprite buildTexture;

}
