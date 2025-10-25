using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Abilities", menuName = "Scriptable Objects/Abilities")]
public class Abilities : ScriptableObject
{
    [Serializable]
    public struct Abilities_Data
    {
        public int damage;
        public Ability script;
    }

    [SerializeField] public System.Collections.Generic.List<Abilities_Data> data;
}
