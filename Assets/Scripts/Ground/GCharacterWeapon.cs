using UnityEngine;
using UnityEngine.EventSystems;

public class GCharacterWeapon : MonoBehaviour,IPointerClickHandler
{
    public int WeaponNumber;
    public GameObject Choose;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GManger.Instance.nowWeapon != WeaponNumber)
        {
            GManger.Instance.nowWeapon = WeaponNumber;
            //Choose.SetActive(true);
        }
        else
        {
            GManger.Instance.nowWeapon = -1;
            //Choose.SetActive(false);
        }
    }

    private void Update()
    {
        if (GManger.Instance.nowWeapon != WeaponNumber)
        {
            Choose.SetActive(false);
        }
        else
        {
            Choose.SetActive(true);
        }
    }
}
