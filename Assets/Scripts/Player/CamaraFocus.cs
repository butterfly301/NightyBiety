using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFocus : MonoBehaviour
{
    private Transform playerTransform; // 玩家的Transform组件
    
    private Camera mainCamera; // 主摄像机引用
    
    public float maxMouseDistance ; // 鼠标距离玩家的最大距离
    
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // 获取主摄像机
        mainCamera = Camera.main;
    }
    
    void Update()
    {
        if (playerTransform != null)
        {
            // 获取鼠标在世界空间中的位置
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0; // 确保z坐标为0，保持2D
            // 计算鼠标到玩家的向量
            Vector3 mouseToPlayer = mouseWorldPos - playerTransform.position;
            
            // 如果距离超过最大值，则限制距离
            if (mouseToPlayer.magnitude > maxMouseDistance)
            {
                mouseWorldPos = playerTransform.position + mouseToPlayer.normalized * maxMouseDistance;
            }
            
            // 计算中点
            Vector3 midpoint = (playerTransform.position + mouseWorldPos) / 2f;
            
            // 更新空物体的位置
            transform.position = midpoint;
        }
    }
}
