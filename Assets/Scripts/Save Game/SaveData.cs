
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int health;
    public int score;
    
    public SaveData()
    {
        health = 100;
        score = 0;
    }
}