using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour, ISaveGame
{

    public static int score;

    Text text;

    public void LoadData(SaveData saveData)
    {
        score = saveData.score;
    }

    public void SaveData(ref SaveData saveData)
    {
        saveData.score = score;
    }

    void Awake ()
    {
        text = GetComponent <Text> ();
        score = 0;
    }

    void Update ()
    {
        text.text = "Score: " + score;
    }

}