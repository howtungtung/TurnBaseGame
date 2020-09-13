using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CharacterData
{
    public int id;
    public string name;
    public int hp;
    public int atk;
    public int actionTime;

    public event UnityAction onHPChanged;
    public void SetHP(int hp)
    {
        this.hp = hp;
        onHPChanged?.Invoke();
    }

    public CharacterData Clone()
    {
        return (CharacterData)this.MemberwiseClone();
    }
}
