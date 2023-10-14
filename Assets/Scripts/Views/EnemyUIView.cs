using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIView : MonoBehaviour
{
    public Text CharacterName;

    public Text BloodText;
    public Text AttackText;//攻击力
    public Text DefenceText;//防御力
    public Text DodgeText;//闪避率
    public Text PrecisionText;//精准度
    public Text CriticalHitText;//暴击率
    public Text  SpeedText;
    
    public List<Button> skill = new List<Button>();
}
