using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace TheGameUI
{


    public class PlayerView : MonoBehaviour
    {
        #region Informations
        //头像
        public Image CharacterHeadImage;

        public Text CharacterLevel;
        public Text BroadcastText;
        public Text CharacterName;
        public DebugUI.Panel BuffPanel;
        public Text BloodText;
        public Text AttackText; //攻击力
        public Text DefenceText; //防御力
        public Text DodgeText; //闪避率
        public Text PrecisionText; //精准度
        public Text CriticalHitText; //暴击率
        public Text SpeedText;
        public Button Changeplacebutton;


        public List<Button> character = new List<Button>();

        public List<Button> enemy = new List<Button>();

        #endregion

        #region Skills

        public List<Button> skill = new List<Button>();

        #endregion

        #region Equipments


        public List<Button> Equipment = new List<Button>();


        public Image Message;
        
        #endregion

        #region Dice

        public Text DiceText;
        public Text ChanceRollTextA;
        public Text ChanceRollTextB;
        public Text LuckyPoint;
        public Text UNLuckyPoint;
        #endregion

        public List<GameObject> AvailEffectP;
        public List<GameObject> AvailEffectE;
        
    }
}