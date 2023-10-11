public interface ISaveGame
{
    void LoadData(SaveData saveData);
    void SaveData(ref SaveData saveData);
}