using UnityEngine;

public class PlayerHover : MonoBehaviour
{
    [Header("悬停设置")]
    [SerializeField] private float rayLength = 10f; // 最大射线长度
    [SerializeField] private LayerMask wallLayer;    // Wall图层
    [SerializeField] private float hoverForce = 10f; // 悬停力大小
    [SerializeField] private float hoverSmoothness = 5f; // 悬停平滑度
    [SerializeField] private float gravityScale = 1f; // 自然下落时的重力缩放

    private Rigidbody2D rb;
    private float upRayDistance;
    private float downRayDistance;
    private float targetYPosition;
    private bool hasWallAbove;
    private bool hasWallBelow;
    private float originalGravityScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D组件未找到！");
        }
        originalGravityScale = rb.gravityScale;
    }

    private void Update()
    {
        CastRays();
        CalculateTargetPosition();
    }

    private void FixedUpdate()
    {
        ApplyHoverForce();
    }

    // 发射上下射线检测距离
    private void CastRays()
    {
        // 向上发射射线
        RaycastHit2D upHit = Physics2D.Raycast(
            transform.position, 
            Vector2.up, 
            rayLength, 
            wallLayer);
        
        hasWallAbove = upHit.collider != null;
        upRayDistance = hasWallAbove ? upHit.distance : rayLength;
        
        // 向下发射射线
        RaycastHit2D downHit = Physics2D.Raycast(
            transform.position, 
            Vector2.down, 
            rayLength, 
            wallLayer);
        
        hasWallBelow = downHit.collider != null;
        downRayDistance = hasWallBelow ? downHit.distance : rayLength;
        
        // 调试绘制射线
        Debug.DrawRay(transform.position, Vector2.up * upRayDistance, hasWallAbove ? Color.green : Color.yellow);
        Debug.DrawRay(transform.position, Vector2.down * downRayDistance, hasWallBelow ? Color.red : Color.yellow);
    }

    // 计算目标悬停位置
    private void CalculateTargetPosition()
    {
        // 只有当上下都有墙壁时才计算中点
        if (hasWallAbove && hasWallBelow)
        {
            float middlePoint = (upRayDistance - downRayDistance) / 2;
            targetYPosition = transform.position.y + middlePoint;
        }
    }

    // 应用悬停力或自然下落
    private void ApplyHoverForce()
    {
        if (hasWallAbove && hasWallBelow&& !Input.GetKey(KeyCode.W)&& !Input.GetKey(KeyCode.S))
        {
            // 恢复原始重力
            rb.gravityScale = 0;
            
            // 计算当前位置与目标位置的差值
            float positionDifference = targetYPosition - transform.position.y;
            
            // 计算需要的力（使用平滑过渡）
            float force = positionDifference * hoverForce;
            
            // 应用力
            rb.velocity = new Vector2(
                rb.velocity.x, 
                Mathf.Lerp(rb.velocity.y, force, hoverSmoothness * Time.fixedDeltaTime));
        }
        else
        {
            // 没有检测到两面墙时，恢复重力自然下落
            rb.gravityScale = originalGravityScale * gravityScale;
        }
    }
}