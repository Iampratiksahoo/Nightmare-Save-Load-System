using System;
using System.IO;
using UnityEngine;

public class SaveDataHandler
{
    private string _dataDirPath = string.Empty;
    private string _dataFileName = string.Empty;

    public SaveDataHandler(string dataDirPath, string dataFileName)
    {
        _dataDirPath = dataDirPath;
        _dataFileName = dataFileName;
    }

    public SaveData Load()
    {
        string path = Path.Combine(_dataDirPath, _dataFileName);

        SaveData loadedData = null;
        if(File.Exists(path)) 
        {
            try
            {
                //string loadedJsonData = string.Empty;
                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        loadedData = JsonUtility.FromJson<SaveData>(streamReader.ReadToEnd());
                    }
                }

                // Deserialize the data
                
            }
            catch (Exception e)
            {
                Debug.LogError("Faied to Load game data with expection: " + e);
            }
        }
        return loadedData;  
    }

    public void Save(SaveData saveData) 
    {
        string path = Path.Combine(_dataDirPath, _dataFileName);

        try
        {
            // Create the directory path if doesn't already exist 
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            // Create JSON of the save data (Serialization)
            string jsonSaveData = JsonUtility.ToJson(saveData, true);

            // Save the file by creating a filestream
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(jsonSaveData); 
                }
            }

        }
        catch(Exception e)
        {
            Debug.LogError("Faied to save game data with expection: " + e);
        }

    }
}