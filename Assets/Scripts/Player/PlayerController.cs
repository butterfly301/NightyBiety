using TwoBitMachines.FlareEngine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce ;
    [SerializeField] private float recoilForce;
    
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    
    private Rigidbody2D rb;
    private Firearm firearm;
    private bool isGrounded;
    private float moveInput;
    private bool facingRight = true;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        firearm = GetComponentInChildren<Firearm>();
    }

    private void Update()
    {
        /*Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // 保持z坐标为0
        transform.position = mousePosition;   */
        // 获取水平输入(A/D或左右箭头)
        moveInput = Input.GetAxisRaw("Horizontal");
        

        RecoilWhenShoot();
        // 跳跃检测
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        
        
    }

    private void FixedUpdate()
    {
        // 地面检测
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        // 水平移动
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
    }

    private void CheckDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // 确定目标朝向
        facingRight = mousePosition.x < transform.position.x;
        if (!facingRight)
        {
            Flip();
        }
        else if (facingRight)
        {
            Flip();
        }
        // 翻转角色朝向
        /*if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }*/
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        if(facingRight)
            firearm.rotate.fixedDirection = new Vector2(1, 0);
        else if(!facingRight)
            firearm.rotate.fixedDirection = new Vector2(-1, 0);
    }

    // 可选：在编辑器中可视化地面检测范围
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
    
    

    void RecoilWhenShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (facingRight)
            {
                rb.AddForce(Vector2.left * recoilForce, ForceMode2D.Force);
            }
            else if (!facingRight)
                rb.AddForce(Vector2.right * recoilForce, ForceMode2D.Force);
        }
    }
}