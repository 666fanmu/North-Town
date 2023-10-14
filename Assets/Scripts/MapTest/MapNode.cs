using System.Collections;
using System.Collections.Generic;
using Controls;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Managers;


public enum RoomType
{
    NormalRoom,
    RelaxRoom,
    EliteRoom,
    BossRoom
}

public class MapNode : MonoBehaviour
{
    private MapManager _mapManager;
    
    public int value;
    public MapNode left;
    public MapNode right;
    public int level;
    
    //房间是否启用
    public bool isUsed = false;
    
    //房间是否可被选中
    public bool ifCanBeClick=false;
    
    public RoomType roomType;
    public Sprite[] RoomSprite;
    public void RandomRoomType()
    {
        int i = Random.Range(0, 101);
        if (i<=20)
        {
            roomType = RoomType.RelaxRoom;
            this.GetComponent<Button>().image.sprite = RoomSprite[0];
        }
        else if(i>20&&i<70)
        {
            roomType = RoomType.NormalRoom;
            this.GetComponent<Button>().image.sprite = RoomSprite[1];
        }
        else if (i>=70)
        {
            roomType = RoomType.EliteRoom;
            this.GetComponent<Button>().image.sprite = RoomSprite[2];
        }
        
        
    }

    private void Awake()
    {
        _mapManager = FindObjectOfType<MapManager>();
        GetComponent<Button>().onClick.AddListener(UpdatePlace);
        // Debug.Log(gameObject.GetComponent<RectTransform>().localPosition);
    }

    public void UpdatePlace()
    {
        if (_mapManager.currentPlace.activeSelf==false)
        {
            _mapManager.currentPlace.SetActive(true);
        }
        
        if (ifCanBeClick)
        {
            _mapManager.SetCurrentPlace(this.transform.position);
            _mapManager.currentLevel += 1;
            _mapManager.setActiveButtonTrue(this);
        }
        SetRoom();
        
    }

    /// <summary>
    /// 设置房间的怪物等
    /// </summary>
    public void SetRoom()
    {
        List<SetEnemyLineup> canbechosen = new List<SetEnemyLineup>();
        canbechosen.Clear();
        switch (roomType)
        {
            case RoomType.NormalRoom:
                foreach (SetEnemyLineup element in EnemyManager.Instance.SetEnemyList)
                {
                    if (element.setRoomType==RoomType.NormalRoom&&element.setLevel==level+1)
                    {
                        canbechosen.Add(element);
                    }
                }
                break;
            case RoomType.RelaxRoom:
                foreach (SetEnemyLineup element in EnemyManager.Instance.SetEnemyList)
                {
                    if (element.setRoomType==RoomType.RelaxRoom&&element.setLevel==+1)
                    {
                        canbechosen.Add(element);
                    }
                }
                break;
            case RoomType.EliteRoom:
                foreach (SetEnemyLineup element in EnemyManager.Instance.SetEnemyList)
                {
                    if (element.setRoomType==RoomType.EliteRoom&&element.setLevel==+1)
                    {
                        canbechosen.Add(element);
                    }
                }
                break;
            case RoomType.BossRoom:
                foreach (SetEnemyLineup element in EnemyManager.Instance.SetEnemyList)
                {
                    if (element.setRoomType== RoomType.BossRoom&&element.setLevel==+1)
                    {
                        canbechosen.Add(element);
                    }
                }
                if (GameManager.Instance.BossRoom==1)
                {
                    //todo
                    //女神房间
                  
                }
                else if ( GameManager.Instance.BossRoom ==2)
                {
                   //todo
                   //古神房间
                }
                else
                {
                    //todo
                    //普通房间
                }
                break;
        }
        int i = Random.Range(0, canbechosen.Count);
        Debug.Log("canbechosen.count:"+canbechosen.Count);
        for (int a = 0; a < canbechosen[i].Enemylist.Count; a++)
        {
            
            GameObject ccr = Instantiate(canbechosen[i].Enemylist[a].gameObject,EnemyManager.Instance.gameObject.transform);
            EnemyManager.Instance.currentEnemyList.Add(ccr.GetComponent<CharacterBase>());   
        }


        GameManager.Instance.InitSetting();
    }

   
}