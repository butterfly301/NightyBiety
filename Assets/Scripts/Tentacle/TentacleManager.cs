using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TentacleManager : MonoBehaviour
{
    public Transform player;
    private Camera mainCamera;
    public Tentacle[] tentacles;
    // 8方向枚举
    public enum FourDirection
    {
        UpRight,
        DownRight,
        DownLeft,
        UpLeft
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        ChangeLogic();
    }

    void ChangeLogic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (GetCurrentDirection())
            {
                case FourDirection.UpRight:
                    tentacles[GetRandomInt(0, 1, 2)].ChangeState("Grab");
                    /*tentacles[0].ChangeState("Grab");
                    tentacles[1].ChangeState("Grab");
                    tentacles[2].ChangeState("Grab");*/
                    break;
                case FourDirection.DownRight:
                    tentacles[GetRandomInt(0, 6, 7)].ChangeState("Grab");
                    /*tentacles[0].ChangeState("Grab");
                    tentacles[6].ChangeState("Grab");
                    tentacles[7].ChangeState("Grab");*/
                    break;
                case FourDirection.DownLeft:
                    tentacles[GetRandomInt(4, 5, 6)].ChangeState("Grab");
                    /*tentacles[4].ChangeState("Grab");
                    tentacles[5].ChangeState("Grab");
                    tentacles[6].ChangeState("Grab");*/
                    break;
                case FourDirection.UpLeft:
                    tentacles[GetRandomInt(2, 3, 4)].ChangeState("Grab");
                    /*tentacles[2].ChangeState("Grab");
                    tentacles[3].ChangeState("Grab");
                    tentacles[4].ChangeState("Grab");*/
                    break;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            for (int i = 0; i < tentacles.Length; i++)
            {
                tentacles[i].ChangeState("Walk");
            }
        }
    }
    
    FourDirection GetCurrentDirection()
    {
        // 获取鼠标在世界空间中的位置
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0; // 确保z坐标为0，保持2D
        // 计算鼠标到玩家的向量
        Vector2 direction = (mouseWorldPos - player.position).normalized;
        // 获取当前方向区域
        return Get4DiagonalDirection(direction);
    }
    
    public static FourDirection Get4DiagonalDirection(Vector2 direction)
    {
        // 判断x和y的正负
        bool right = direction.x > 0;
        bool up = direction.y > 0;

        // 根据象限确定方向
        if (right)
        {
            return up ? FourDirection.UpRight : FourDirection.DownRight;
        }
        else
        {
            return up ? FourDirection.UpLeft : FourDirection.DownLeft;
        }
    }
    
    public int GetRandomInt(int num1, int num2, int num3)
    {
        int randomIndex = Random.Range(0, 3);
        return randomIndex == 0 ? num1 : (randomIndex == 1 ? num2 : num3);
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                for (int i = 0; i < tentacles.Length; i++)
                {
                    tentacles[i].ChangeState("Eat");
                }
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                for (int i = 0; i < tentacles.Length; i++)
                {
                    tentacles[i].ChangeState("Walk");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Food"))
            for (int i = 0; i < tentacles.Length; i++)
            {
                tentacles[i].ChangeState("Walk");
            }
    }
}
