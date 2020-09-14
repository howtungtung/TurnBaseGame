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

    public event UnityAction<int> onHPAdd;
    public event UnityAction<int> onHPReduce;

    public void AddHP(int amount)
    {
        hp += amount;
        onHPAdd?.Invoke(amount);
    }

    public void ReduceHP(int amount)
    {
        hp -= amount;
        onHPReduce?.Invoke(amount);
    }

    public CharacterData Clone()
    {
        return (CharacterData)this.MemberwiseClone();
    }
}
