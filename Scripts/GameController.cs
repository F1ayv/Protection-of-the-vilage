using System;
using System.Collections;
using System.Collections.Generic;
using DataLevels;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public LoadGame loadGame;
    public EmeraldController emeraldController;
    public MenuController menuController;
    
    //[Header("Button Settings")]
    [SerializeField] private DataExample[] listLevels;
    //private int[] _timeListEnemy = new int [20];
    private List<int> _loadValueEnemy = new List<int>();
    private List<int> _ListAllEnemyOnLevel = new List<int>();
    private List<GameObject>_listEnemyForSpawn = new List<GameObject>();
    public GameObject[] allEnemy;
    public Transform folderEnemy;
   // private Vector3 _folderEnemyPosition;
    public int selectedLevel;
    public bool isPause;
    private bool _isTheEndSpawn;
    

    [SerializeField] private float time;
    [SerializeField] private float _timeBeforeTheStart = 5; //время перед началом уровня //20 поставить
    private float _cooldownSpawnEnemy = 4; //кд между спавном врагов
    private int _requiredWave; 
    private int _valueMobsInLevel; //количество мобов в уровне //15 по 3
    private int _valueSpawn; //количество уже заспавленных мобов
    private int _tierMobsSpawned; //текущий максимальный тир спавна мобов
    [SerializeField] private List<Vector3> _listSpawnEnemyPos = new List<Vector3>();
    [SerializeField] private List<GameObject> _mobs = new List<GameObject>();

     private int[] _startFastSpawn;
    private int[] _theEndFastSpawn;
    private int[] _ratioFastSpawn;
    private int _timeRatio;
    private bool _isLevelComplete;
    private void Start()
    {
        isPause = true;
        //LoadLevel(selectedLevel);
        for (int i = 0; i < loadGame.sizeZ; i++)
        {
            var _folderEnemyPosition = new Vector3(loadGame.sizePlaneX * (loadGame.sizeX + 1), 3, (loadGame.sizePlaneZ) / 2 + (loadGame.sizePlaneZ) * i);;
            _listSpawnEnemyPos.Add(_folderEnemyPosition);
        }
    }
    private void FixedUpdate()
    {
        if (_isTheEndSpawn)
        {
            if (folderEnemy.childCount != 0 || _isLevelComplete) return;
            _isLevelComplete = true;
            menuController.ReloadLevelComplete();
            return;
        }
        if (isPause) return;
        _timeRatio = 1;
        for (int i = 0; i < _startFastSpawn.Length; i++)
        {
            if (_startFastSpawn[i] <= _valueSpawn && _theEndFastSpawn[i] > _valueSpawn)
            {
                _timeRatio = _ratioFastSpawn[i];
                break;
            }
        }

        time += Time.fixedDeltaTime;
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        while (time >= _timeBeforeTheStart && _listEnemyForSpawn.Count != 0)
        {
            int random = Random.Range(0, 3);
            int randomPos = Random.Range(0, 5);
            if (_listEnemyForSpawn.Count > random)
            {
                GameObject sd = Instantiate(_listEnemyForSpawn[random], folderEnemy) as GameObject;
                sd.transform.position = new Vector3(_listSpawnEnemyPos[randomPos].x, sd.transform.position.y, _listSpawnEnemyPos[randomPos].z);
                _mobs.Add(sd);
                _listEnemyForSpawn.Remove(_listEnemyForSpawn[random]);
                _valueSpawn++;
                _cooldownSpawnEnemy -= 0.01f;
                _timeBeforeTheStart += _cooldownSpawnEnemy / _timeRatio;
            }
            else
                return;
            if (_listEnemyForSpawn.Count == 0)
            {
                _isTheEndSpawn = true;
            }
        }
    }

    public void LoadLevel(int level)
    {
        Debug.Log("загружаю уровень: " + level);
        ClearAllLists();
        for (int i = 0; i < listLevels[level].Mobs.Length; i++)
        {
            _loadValueEnemy.Add(listLevels[level].Mobs[i]);//импортируем количество каждого моба на данном уровне
        }

        _startFastSpawn = listLevels[level].StartFastSpawn;
        _theEndFastSpawn = listLevels[level].TheEndFastSpawn;
        _ratioFastSpawn = listLevels[level].RatioFastSpawn;
        for (int i = 0; i < listLevels[level].Mobs.Length; i++)
        {
            _ListAllEnemyOnLevel.Add(_loadValueEnemy[i]);
            for (int n = 0; _ListAllEnemyOnLevel[i] > n; n++)
            {
                _listEnemyForSpawn.Add(allEnemy[i]);
            }
            
        }
        _valueMobsInLevel = _ListAllEnemyOnLevel.Count;
        if (menuController.GetLastLevel() >= 7)
            emeraldController.OpenCloseMobs(10);
        else
            emeraldController.OpenCloseMobs(menuController.GetLastLevel()+2);
        //ClearLists();
    }

    private void ClearAllLists()
    {
        _loadValueEnemy.Clear();
        _ListAllEnemyOnLevel.Clear();
        _listEnemyForSpawn.Clear();
        _mobs.Clear();
        isPause = false;
    }
}
