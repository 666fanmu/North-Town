using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using TheGameUI;
namespace Controls
{
    

    public class UIControl : MonoBehaviour
    {
        private UIView _UIView;
        [HideInInspector]
        public GameSenceUIView gameSenceUIView;
        [HideInInspector]
        public PlayerView playerView;
        [HideInInspector]
        public AttackOrderView attackOrderView;

        public static UIControl Instance;

        private void Awake()
        {
            Instance = this;
            _UIView = FindObjectOfType<UIView>();
            gameSenceUIView = FindObjectOfType<GameSenceUIView>();
            attackOrderView = FindObjectOfType<AttackOrderView>();
            playerView = FindObjectOfType<PlayerView>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Escape");
                OpenSettingMenu();
            }
        }

        //打开制作者名单
        public void OpenMakerList()
        {
            _UIView.MakerList.SetActive(true);
        }

        

        ///打开设置菜单
        public void OpenSettingMenu()
        {
            
            _UIView.SettingMenu.SetActive(true);
            
        }


  


        ///打开结算UI
        public void OpenCheckOutUI(bool win)
        {
            if (win==true)
            {
                gameSenceUIView.CheckOutUI.SetActive(true);
                gameSenceUIView.CheckOut.text = "you win!";
                gameSenceUIView.Coins.text = "你获得了"+GameManager.Instance.coins+"枚金币";
            }

            else
            {
                gameSenceUIView.CheckOutUI.SetActive(true);
                gameSenceUIView.CheckOut.text = "you lose";
                gameSenceUIView.Coins.text = "你获得了" + GameManager.Instance.coins + "枚金币";
            }
           
        }


        /// <summary>
        /// 更新最上方行动顺序的UI
        /// </summary>
        public void UpdateActionRoundUI()
        {
           
                if (GameManager.Instance.CurrentActionNum+2<GameManager.Instance.ActionList.Count)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        attackOrderView.AttackOrder[j].sprite = GameManager.Instance.ActionList[GameManager.Instance.CurrentActionNum+j]
                            .attribute
                            .CharacterHeadSprite;
                    }
                }
                
          
        }
        
        //跟新游戏回合数UI
        public void UpdateGameRound()
        {
            playerView.BroadcastText.text="第"+GameManager.Instance.CurrentRound.ToString()+"回合开始";
            gameSenceUIView.GameRound.text = GameManager.Instance.CurrentRound + "/" + GameManager.Instance.GameRound;
            Debug.Log("UPdate GameRound");
        }


    }
}
