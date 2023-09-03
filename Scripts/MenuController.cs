using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameController gameController;
    public GameObject menu;
    public Image[] lockImageLevels;
    [SerializeField]private int _lastLevel = 9;
    [SerializeField]private int _selectedLevel;
    public bool isResetData;
    void Start()
    {
        if (isResetData)
        {
            ResetData();
        }
        LoadGame();
        UpdateMenu(_lastLevel);
        if (!menu.activeSelf)
        {
            menu.SetActive(true);
        }
    }

    public int GetLastLevel()
    {
        return _lastLevel;
    }
    // Update is called once per frame
    

    public void LoadLevel(int level) //кнопки в меню
    {
        _selectedLevel = level;
        gameController.LoadLevel(level);
        menu.SetActive(false);
    }

    public void ReloadLevelComplete()
    {
        if (_lastLevel == _selectedLevel && _lastLevel !=14)
        {
            _lastLevel++;
            UpdateMenu(_lastLevel);
            SaveGame();
        }
    }
    public void UpdateMenu(int level)
    {
        _lastLevel = level;
        for (int i = 0; i <= _lastLevel; i++)
        {
            lockImageLevels[i].gameObject.SetActive(false);
        }

        for (int i = _lastLevel+1; i < 15; i++)
        {
            lockImageLevels[i].gameObject.SetActive(true);
        }
    }
    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/System.dat");
        SaveData data = new SaveData();
        data.level = _lastLevel;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Данные сохраннены!");
    }
    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/System.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                File.Open(Application.persistentDataPath+ "/System.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            _lastLevel = data.level;
            Debug.Log("Данные загрузились");
        }
        else
        {
            Debug.Log("Данные пустые");
            //_lastLevel = 0;
        }
          
    }
    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/System.dat"))
        { 
            File.Delete(Application.persistentDataPath + "/System.dat");
            _lastLevel = 0;
        }
    }
    public void OtherGames()
    {
        Application.OpenURL("https://vk.com/fault_matrix");
    }
}
[System.Serializable]

class SaveData
{
    public int level;
}
