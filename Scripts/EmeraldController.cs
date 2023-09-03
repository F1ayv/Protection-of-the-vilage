using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;
// ReSharper disable All

public class EmeraldController : MonoBehaviour
{
    public LoadGame loadGame;
    public Sprite emeraldIcon;
    public Sprite lockIcon;
    public GameObject EmeraldPrefabGameObject;
    public Image emeraldPrefabImage;
    public Transform positionEmeraldUI;
    public TextMeshProUGUI EmeraldValueText;
    public TextMeshProUGUI[] priceMobsText;
    private int _emeraldValue;
    private float _reloadSpawnEmerald = 20f, _timer;
    private float _sizePlaneX, _sizePlaneZ;
    private int _sizeX = 9, _sizeZ = 5;
    [SerializeField] private int[] priceMobs;

    public GameObject[] lockPanel;
    //color buy panel
    public Image[] buyPanelBackground;
    public bool[] isBuyPanelBackground;
    public Image[] buyPanelReloadUse;
    public Color[] colorBuyMobUI;
    private bool _reload;
    private bool[] _isReload = new bool[11];
    
    public Image buyPanelBackgroundPotion;
    public bool isBuyPanelBackgroundPotion;
    public Image buyPanelReloadUsePotion;
    void Start()
    {
        _emeraldValue = 50;
        EmeraldValueText.text = _emeraldValue.ToString();
        _sizePlaneX = loadGame.sizePlaneX;
        _sizePlaneZ = loadGame.sizePlaneZ;
        _sizeX = loadGame.sizeX;
        _sizeZ = loadGame.sizeZ;
        for (int i = 0; i < priceMobsText.Length; i++)
        {
            priceMobsText[i].text = priceMobs[i].ToString();
        }
        _reload = true;
    }

    public bool TrySpawn(int idMob)
    {
        if (!_isReload[idMob] && _emeraldValue >=priceMobs[idMob])
            return true;
        return false;
    }

    public bool TryBuyMob(int idMob)
    {
        if (_emeraldValue >=priceMobs[idMob])
        {
            _emeraldValue -= priceMobs[idMob];
            EmeraldValueText.text = _emeraldValue.ToString();
            _reload = true;
            _isReload[idMob] = true;
            StartCoroutine(ReloadUseMob(idMob));
            return true;
        }
        return false;
    }

    public void OpenCloseMobs(int size)
    {
        for (int i = 0; i < size; i++)
        {
            lockPanel[i].SetActive(false);
            //buyPanelReloadUse[i].rectTransform.sizeDelta = new Vector2(110f,0);
        }
        for (int i = size; i < 10; i++)
        {
            lockPanel[i].SetActive(true);
            //buyPanelReloadUse[i].rectTransform.sizeDelta = new Vector2(110f,150);
        }
    }
    IEnumerator ReloadUseMob(int idMob)
    {
        float speed = 20f;
        buyPanelBackground[idMob].color = colorBuyMobUI[1];
        isBuyPanelBackground[idMob] = false;
        if (idMob < 10)
        {
            buyPanelReloadUse[idMob].rectTransform.sizeDelta = new Vector2(110f,150f);
            while (buyPanelReloadUse[idMob].rectTransform.sizeDelta.y >= 0)
            {
                buyPanelReloadUse[idMob].rectTransform.sizeDelta = new Vector2(110f,buyPanelReloadUse[idMob].rectTransform.sizeDelta.y-speed*Time.deltaTime);
                yield return null;
            }
        }
        else
        {
            buyPanelReloadUse[idMob].rectTransform.sizeDelta = new Vector2(130f,130f);
            while (buyPanelReloadUse[idMob].rectTransform.sizeDelta.y >= 0)
            {
                buyPanelReloadUse[idMob].rectTransform.sizeDelta = new Vector2(130f,buyPanelReloadUse[idMob].rectTransform.sizeDelta.y-speed*Time.deltaTime);
                yield return null;
            }
        }
       
        _isReload[idMob] = false;
        if (_emeraldValue>= priceMobs[idMob])
        {
            buyPanelBackground[idMob].color = colorBuyMobUI[0];
            isBuyPanelBackground[idMob] = true;

        }
        else
        {
            buyPanelBackground[idMob].color = colorBuyMobUI[2];
            isBuyPanelBackground[idMob] = false;
        }
        yield return null;
    }
    
    private void FixedUpdate()
    {
        if (_reload)
        {
            _reload = false;
            for (int i = 0; i < 11; i++)
            {
                if (!_isReload[i])
                {
                    if (_emeraldValue>= priceMobs[i])
                    {
                        buyPanelBackground[i].color = colorBuyMobUI[0];
                        isBuyPanelBackground[i] = true;

                    }
                    else
                    {
                        buyPanelBackground[i].color = colorBuyMobUI[2];
                        isBuyPanelBackground[i] = false;
                    }
                }
            }
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer>= _reloadSpawnEmerald)
        {
            _timer = 0;
            int x = Random.Range(0, _sizeX);
            int y = Random.Range(0, _sizeZ);
            GameObject emerald = Instantiate(EmeraldPrefabGameObject, transform);
            emerald.transform.position = new Vector3(_sizeX*_sizePlaneX-_sizePlaneX/2,0,_sizeZ*_sizePlaneZ-_sizePlaneZ/2);
            emerald.transform.localScale = new Vector3(10f, 10f, 10f);
            emerald.GetComponentInChildren<EmeraldGive>().SetStart();
        }
        
        if (Input.GetMouseButton(0) != true)
            return;

        RaycastHit[] hits;
        hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.CompareTag("Emerald"))
            {
                Destroy(hits[i].collider.transform.parent.gameObject);
                Image emerald = Instantiate(emeraldPrefabImage, positionEmeraldUI) as Image;
                emerald.rectTransform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                StartCoroutine(MoveImage(emerald));
                return;
            }
        }
    }
    
    IEnumerator MoveImage(Image emerald)
    {
        bool first = false;
        while (Vector3.Distance(emerald.rectTransform.position,positionEmeraldUI.position)>0.05f)
        {

            if (Vector3.Distance(emerald.rectTransform.position, positionEmeraldUI.position) < 10f && !first)
            {
                first = true;
                _reload = true;
                _emeraldValue+=50;
                EmeraldValueText.text = _emeraldValue.ToString();
            }
            
            emerald.rectTransform.position = Vector2.Lerp(emerald.rectTransform.position,positionEmeraldUI.position, Time.deltaTime*6);
            yield return Time.fixedDeltaTime;
        }
        
        Destroy(emerald.gameObject);
    }
    
   
    
}

