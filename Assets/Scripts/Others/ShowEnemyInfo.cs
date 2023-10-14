using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowEnemyInfo : MonoBehaviour, IPointerClickHandler
{
    private Button button;

    void Start()
    {
        // 获取按钮的 Button 组件
        button = GetComponent<Button>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 检查是否是鼠标右键点击
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // 在这里执行右键点击后的操作
            Debug.Log("Right-clicked on button");
            // 这里可以添加你希望执行的右键点击操作
        }
    }
}