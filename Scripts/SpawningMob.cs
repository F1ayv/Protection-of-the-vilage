using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
// ReSharper disable All
public class SpawningMob : MonoBehaviour
{
    public LoadGame loadGame;
    public EmeraldController emeraldController;
    public MenuController menuController;
    [SerializeField] private bool isTakeMob = false;
    private GameObject? _hitObj;

    private GameObject _timeGameObject;
    private int _idSelectedMob;
    private bool[,] _tryArray = new bool[9, 5];
    private Mob[,] _tryArrayGameObjects = new Mob[9, 5];
    private int _sizeX = 9, _sizeZ = 5;

    public GameObject[] mobs;
    public Image timeIconSelectedMob; //иконка моба которую игрок перетаскивает на поле
    public Sprite[] mobSprites;
    public float sizePlaneX, sizePlaneZ;
    private bool _isDestroyMob;

    private void Start()
    {
        this.sizePlaneX = loadGame.sizePlaneX;
        this.sizePlaneZ = loadGame.sizePlaneZ;
        this._sizeX = loadGame.sizeX;
        this._sizeZ = loadGame.sizeZ;
        _reload = true;

    }


   
    public Sprite potionSprite;
 
    private bool _reload;
    private bool _isReload;
    private bool _isTakePotion;
    private float _sizePlaneX, _sizePlaneZ;
    

   
    void Update()
    {
        if (isTakeMob)
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits;
            hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.CompareTag("Plane"))
                {
                    _hitObj = hits[i].collider.gameObject;
                    _timeGameObject.transform.position = new Vector3(_hitObj.transform.position.x, _timeGameObject.transform.position.y, _hitObj.transform.position.z);
                    int x = (int) ((_hitObj.transform.position.x + sizePlaneX / 2) / sizePlaneX - 1);
                    int z = (int) ((_hitObj.transform.position.z + sizePlaneZ / 2) / sizePlaneZ - 1);
                    if (!_tryArray[x, z])
                    {
                        _timeGameObject.SetActive(true);
                        timeIconSelectedMob.gameObject.SetActive(false);
                    }
                    else
                    {
                        _timeGameObject.SetActive(false);
                        timeIconSelectedMob.gameObject.SetActive(true);
                        timeIconSelectedMob.transform.position = Input.mousePosition;
                    }

                    return;
                }
            }

            _timeGameObject.SetActive(false);
            timeIconSelectedMob.gameObject.SetActive(true);
            timeIconSelectedMob.transform.position = Input.mousePosition;
            return;
        }
        else if (_isTakePotion)
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction);
            timeIconSelectedMob.gameObject.SetActive(true);
            timeIconSelectedMob.transform.position = Input.mousePosition;
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.CompareTag("Plane"))
                {
                    _hitObj = hits[i].collider.gameObject;
                    int x = (int) ((_hitObj.transform.position.x + _sizePlaneX / 2) / _sizePlaneX - 1);
                    int z = (int) ((_hitObj.transform.position.z + _sizePlaneZ / 2) / _sizePlaneZ - 1);
                    return;
                }

            }

            _hitObj = null;
        }
        else if (_isDestroyMob)
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction);
            timeIconSelectedMob.gameObject.SetActive(true);
            timeIconSelectedMob.transform.position = Input.mousePosition;
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.CompareTag("Plane"))
                {
                    _hitObj = hits[i].collider.gameObject;
                    int x = (int) ((_hitObj.transform.position.x + sizePlaneX / 2) / sizePlaneX - 1);
                    int z = (int) ((_hitObj.transform.position.z + sizePlaneZ / 2) / sizePlaneZ - 1);
                    return;
                }

            }

            _hitObj = null;
        }
    }
    public void IsTakePotion(bool @is)
    {
        if (!emeraldController.isBuyPanelBackground[_idSelectedMob])
            return;
        _isTakePotion = @is;
        
        if (_isTakePotion)
        {
            timeIconSelectedMob.sprite = potionSprite;
            return;
        }
        
        int x = (int) ((_hitObj.transform.position.x + sizePlaneX / 2) / sizePlaneX - 1);
        int z = (int) ((_hitObj.transform.position.z + sizePlaneZ / 2) / sizePlaneZ - 1); 
        timeIconSelectedMob.gameObject.SetActive(false);

        if (_hitObj != null && _tryArrayGameObjects[x, z]!= null&& emeraldController.TryBuyMob(10))
        {
            _tryArrayGameObjects[x, z].Heal();
        }
    }
    public void DestroyMob(bool @is)
    {
        _isDestroyMob = @is;
        if (_isDestroyMob)
        {
            timeIconSelectedMob.sprite = mobSprites[10];
        }
        else if (!_isDestroyMob && _hitObj != null)
        {
            int x = (int) ((_hitObj.transform.position.x + sizePlaneX / 2) / sizePlaneX - 1);
            int z = (int) ((_hitObj.transform.position.z + sizePlaneZ / 2) / sizePlaneZ - 1);
            _tryArray[x, z] = false;
            Destroy(_tryArrayGameObjects[x, z]);
            _tryArrayGameObjects[x, z] = null;
            //Destroy(_hitObj.GetComponent<Collider>().gameObject);
        }

        timeIconSelectedMob.gameObject.SetActive(false);
    }

    public void TakeMobId(int id)
    {
        _idSelectedMob = id;
        if (menuController.GetLastLevel() + 2 > id && emeraldController.isBuyPanelBackground[id])
        {
            timeIconSelectedMob.sprite = mobSprites[id];
            //timeIconSelectedMob.gameObject.SetActive(true);
        }
    }

    public void TakeMob(bool info)
    {
        if (menuController.GetLastLevel() + 2 <= _idSelectedMob )
            return;
        if (!emeraldController.TrySpawn(_idSelectedMob))
            return; 
        if (!emeraldController.isBuyPanelBackground[_idSelectedMob])
            return;
        isTakeMob = info;
        if (isTakeMob)
        {
            _timeGameObject = Instantiate(mobs[_idSelectedMob], transform) as GameObject;
            _timeGameObject.transform.position = new Vector3(0, _timeGameObject.transform.position.y, 0);
            //_timeGameObject.SetActive(false);
            return;
        }
        else if(_hitObj != null)
        {
            int x = (int) ((_hitObj.transform.position.x + sizePlaneX / 2) / sizePlaneX - 1);
            int z = (int) ((_hitObj.transform.position.z + sizePlaneZ / 2) / sizePlaneZ - 1);
            if (_tryArray[x, z] == false)
            {
                if (_timeGameObject !=null && _timeGameObject.activeSelf && emeraldController.TryBuyMob(_idSelectedMob))
                {
                    if (_timeGameObject.name != "Bee(Clone)") //если не пчелка
                    {
                        //Debug.Log(_timeGameObject.name);
                    
                        _tryArray[x, z] = true;
                        _tryArrayGameObjects[x, z] = _timeGameObject.GetComponent<Mob>();
                        Mob mob = _timeGameObject.GetComponent<Mob>();
                        mob.SetPosition(x, z);
                        mob.SetActivity(true);
                        _timeGameObject.layer = 0;
                        for (int i = 0; i < _timeGameObject.transform.childCount; i++)
                        {
                            Transform child = _timeGameObject.transform.GetChild(i);
                            child.gameObject.layer = 0;
                            for (int j = 0; j < child.childCount; j++)
                            {
                                child.GetChild(j).gameObject.layer = 0;
                            }
                        }
                    }

                    if (_timeGameObject.name == "Bee(Clone)")
                    {
                        _timeGameObject.layer = 0;
                        _timeGameObject.transform.GetChild(0).gameObject.layer = 0;
                        for (int i = 0; i < _timeGameObject.transform.GetChild(0).childCount; i++)
                        {
                            _timeGameObject.transform.GetChild(0).GetChild(i).gameObject.layer = 0;
                        }

                        _timeGameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("Start", true);
                    }
                    else
                    {
                        _timeGameObject.GetComponent<Animator>().SetBool("Start", true);
                        _timeGameObject.GetComponent<Collider>().enabled = true;
                    }
                }
                else
                {
                    Destroy(_timeGameObject);
                }
            }
        }
        timeIconSelectedMob.gameObject.SetActive(false);
    }

    public void deleteMobOnArray(int x, int z)
    {
        _tryArray[x, z] = false;
    }
}

