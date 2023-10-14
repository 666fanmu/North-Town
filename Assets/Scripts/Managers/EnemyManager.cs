using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using TheGameUI;
[System.Serializable]
public class SetEnemyLineup
{
    /// <summary>
    /// 出现在哪层
    /// </summary>
    public int setLevel;

    public RoomType setRoomType;
    public List<CharacterBase> Enemylist = new List<CharacterBase>();

    
}

namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance;
        
        public static PlayerView playerView;
        public GameObject BloodSlider;

        
        /// <summary>
        /// 当前关卡的敌人队伍
        /// </summary>
        public List<CharacterBase> currentEnemyList = new List<CharacterBase>();

        /// <summary>
        /// 所有可能出现的敌人队伍
        /// </summary>
        public List<SetEnemyLineup> SetEnemyList = new List<SetEnemyLineup>();
       
        private void Awake()
        {
            Instance = this;
            playerView = FindObjectOfType<PlayerView>();
        }
        
        private void Start()
        {
            UpdateEnemyTeam();
           // Invoke("InitiateBloodSlider",0.2f);
        }

        public void InitiateBloodSlider()
        {
            for (int i = 0; i < EnemyManager.Instance.currentEnemyList.Count; i++)
            {
                SetBloodSlider(i);
            }
            
        }
        /// <summary>
        /// 当出现人物数量变化时调用
        /// </summary>
        public void UpdateEnemyTeam()
        {
            for (int i = 0; i < currentEnemyList.Count; i++)
            {
                playerView.enemy[i].gameObject.SetActive(true);
                //new Vector2(playerView.enemy[i].gameObject)
                //GameObject ssj=Instantiate(BloodSlider, playerView.enemy[i].gameObject.transform);
            }
        }
        public void SetBloodSlider(int characterID)
        {
            
            playerView.enemy[characterID].GetComponentInChildren<Slider>().value
                = (float)EnemyManager.Instance.currentEnemyList[characterID].Blood/(float)
                  EnemyManager.Instance.currentEnemyList[characterID].attribute.AttributeList
                  [EnemyManager.Instance.currentEnemyList[characterID].attribute.currentLevel - 1].Blood;
        }
    }
}