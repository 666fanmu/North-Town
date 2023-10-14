using Characters.Warrier;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;

        

        //战斗中队伍，最多四人
        /// <summary>
        /// 玩家队伍
        /// </summary>
        public List<CharacterBase> CharacterList = new List<CharacterBase>();

        //todo
        //cwq
        //广场招募队伍，可以8选4
        //characterlist
    
        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (CharacterList.Count <= 0)
            {
                //todo
                //游戏结束，失败结算
            }
        }      
        
    }
}
