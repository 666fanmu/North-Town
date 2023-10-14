using UnityEngine;
using UnityEngine.EventSystems;

public class GWeapon : MonoBehaviour,IPointerClickHandler
{
    public WeaponBase weapen;
    public bool Choosed;
    
    // Start is called before the first frame update
    void Start()
    {
        Choosed = false;
    }

    // Update is called once per frame
   

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GManger.Instance.nowWeapon != -1)
        {
            if (!Choosed)
            {
                switch (GManger.Instance.nowWeapon)
                {
                    case 1:
                        //GCharacterManger.Instance.nowCharacter.Weapon = weapen;
                        GameObject a = Instantiate(weapen.gameObject, transform.position, Quaternion.identity);
                        a.transform.parent = GCharacterManger.Instance.nowCharacter.transform;
                        GCharacterManger.Instance.nowCharacter.Weapon = a.GetComponent<WeaponBase>();
                        break;
                    case 2:
                        //GCharacterManger.Instance.nowCharacter.Armor = weapen;
                        GameObject b = Instantiate(weapen.gameObject, transform.position, Quaternion.identity);
                        b.transform.parent = GCharacterManger.Instance.nowCharacter.transform;
                        GCharacterManger.Instance.nowCharacter.Armor = b.GetComponent<WeaponBase>();
                        break;
                    case 3:
                        //GCharacterManger.Instance.nowCharacter.Ring = weapen;
                        GameObject c = Instantiate(weapen.gameObject, transform.position, Quaternion.identity);
                        c.transform.parent = GCharacterManger.Instance.nowCharacter.transform;
                        GCharacterManger.Instance.nowCharacter.Ring = c.GetComponent<WeaponBase>();
                        break;
                    case 4:
                        //GCharacterManger.Instance.nowCharacter.NeckLace = weapen;
                        GameObject d = Instantiate(weapen.gameObject, transform.position, Quaternion.identity);
                        d.transform.parent = GCharacterManger.Instance.nowCharacter.transform;
                        GCharacterManger.Instance.nowCharacter.NeckLace = d.GetComponent<WeaponBase>();
                        break;
                }

                Choosed = true;
            }
            else
            {
                
            }

            GCharacterManger.Instance.nowCharacter.UpdateWeapon();
            GroundUIControl.Instance.upDateHeroInterface();
        }
    }
}
