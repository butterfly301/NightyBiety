using UnityEngine;

public class Tentacle : MonoBehaviour
{
    [Header("基本设置")]
    public Transform player;          // 玩家Transform
    public float maxDistance = 5f;   // 触手最大长度
    public LayerMask wallLayer;      // 墙面所在的层
    public int segmentCount = 10;    // 触手分段数
    public float smoothSpeed = 5f;   // 吸附平滑速度

    [Header("视觉效果")]
    private LineRenderer lineRenderer; // 触手渲染器
    public float width = 0.1f;       // 触手宽度

    private Vector2 endPoint;        // 触手终点位置
    public int tentacleNum;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segmentCount;
        endPoint = transform.position;
    }

    void Update()
    {
        UpdateEndPoint();
        RenderTentacle();
    }

    void UpdateEndPoint()
    {
        // 从玩家位置向四周发射射线寻找最近的墙面
        Vector2 closestHitPoint = FindClosestWall();
        
        if (closestHitPoint != Vector2.zero)
        {
            // 平滑移动到新的吸附点
            endPoint = Vector2.Lerp(endPoint, closestHitPoint, smoothSpeed * Time.deltaTime);
        }
        else
        {
            // 没有检测到墙面时，终点保持在玩家附近
            endPoint = Vector2.Lerp(endPoint, player.position, smoothSpeed * Time.deltaTime);
        }
    }

    Vector2 FindClosestWall()
    {
        Vector2 closestPoint = Vector2.zero;
        float closestDistance = float.MaxValue;
            float angle = tentacleNum * Mathf.PI * 2 / 16;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            
            RaycastHit2D hit = Physics2D.Raycast(player.position, direction, maxDistance, wallLayer);
            
            if (hit.collider != null)
            {
                float distance = Vector2.Distance(player.position, hit.point);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = hit.point;
                }
        }

        return closestPoint;
    }

    void RenderTentacle()
    {
        // 使用贝塞尔曲线使触手更自然
        Vector2 controlPoint = (player.position + (Vector3)endPoint) / 2f + Vector3.up * 0.5f;

        for (int i = 0; i < segmentCount; i++)
        {
            float t = i / (float)(segmentCount - 1);
            Vector2 point = CalculateBezierPoint(t, player.position, controlPoint, endPoint);
            
            // 添加波动效果
            float waveAmount = Mathf.Sin(t * Mathf.PI * 4 + Time.time * 3f) * 0.05f;
            point += new Vector2(-Mathf.Sin(t * Mathf.PI), 0) * waveAmount;
            
            lineRenderer.SetPosition(i, point);
        }
    }

    Vector2 CalculateBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        
        return uu * p0 + 2 * u * t * p1 + tt * p2;
    }
}