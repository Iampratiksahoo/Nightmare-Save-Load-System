using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    private const string SAVE_FILE_NAME = "shooter_save_data.ssgd";

    private static SaveDataHandler _saveDataHandler;
    private static SaveData _saveData;
    private static List<ISaveGame> _saveGameObject = new List<ISaveGame>();

    private void Start()
    {
        // Create a new SaveDataHandler Object for future operations
        _saveDataHandler = new SaveDataHandler(Application.persistentDataPath, SAVE_FILE_NAME);

        // Find and add all the save game objects in the scene 
        IEnumerable<ISaveGame> saveGameObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveGame>();
        _saveGameObject.AddRange(saveGameObjects);

        // Load Game if data is present
        LoadGame();
    }

    public static void NewGame()
    {
        _saveData = new SaveData();

        // Save the Game Data in case of a new game to override the save data if any. Used in case of Player Death. Applicable to current game scenario, can change as per games.
        _saveDataHandler.Save(_saveData);
    }

    public static void LoadGame()
    {
        // Try to load the Save Data from the Application persistant data path, NULL if failed
        _saveData = _saveDataHandler.Load();

        // Case when no save data is retrieved from the SaveDataHandler Object
        if ( _saveData == null ) 
        {
            NewGame();
        }

        // Call Load Data for all ISaveGame Object
        foreach(ISaveGame saveObject in _saveGameObject)
        {
            saveObject.LoadData(_saveData);
        }
    }

    public static void SaveGame()
    {
        // Call Save Data for all ISaveGame Object to save the data in the SaveData object before any save game file operation is done.
        foreach (ISaveGame saveObject in _saveGameObject)
        {
            saveObject.SaveData(ref _saveData);
        }

        // Save the game data using Data Handler.
        _saveDataHandler.Save(_saveData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}