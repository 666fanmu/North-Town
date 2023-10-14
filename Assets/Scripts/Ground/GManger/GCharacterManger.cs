using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GCharacterManger : MonoBehaviour
{
   public static GCharacterManger Instance;

   public int test = 0;
   
   public List<CharacterBase> CharacterList = new List<CharacterBase>();
   
   //现在选中的角色信息
   [HideInInspector]
   public CharacterBase nowCharacter;
   public int thisRole;//用于更新角色技能显示时定位角色
   public bool hasCharacter=false;//用于判断现在有没有选中的角色
   public bool[] isInTeam;//用于记录角色是否已经在队伍中
   public int[] TeamMember;//用于记录队伍的成员名单
   
   //现在选中的技能的信息
   //public 

   private void Start()
   {
      Instance = GetComponent<GCharacterManger>();

      for (int i = 0; i < 4; i++)
      {
         TeamMember[i] = -1;
      }
   }

   public void setTeam()
   {
      
   }
   
   
}
