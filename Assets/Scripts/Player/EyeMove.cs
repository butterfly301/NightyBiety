using UnityEngine;

public class EyeMove : MonoBehaviour
{
    [SerializeField] private float radius = 0.1f; // 眼球移动半径
    
    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        
        Vector2 direction = (mouseWorldPos - transform.parent.position).normalized;
        
        // 限制在圆形区域内
        transform.localPosition = direction * radius;
    }
}