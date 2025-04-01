using System;
using UnityEngine;
public class Tentacle : MonoBehaviour
{
    public int num;
    public Transform player;
    public float segmentLength = 0.5f;
    public int segmentCount = 8;
    public float maxDistance = 5f;
    public float maxGrabDistance = 5f;
    public LayerMask wallLayer;
    public float stiffness = 100f;
    private float distance;
    
    private GameObject[] segments;
    protected Rigidbody2D endRb;
    private LineRenderer lineRenderer;
    public TentacleState currentState;
    private Camera camera;

    public enum TentacleState
    {
        Walk,
        Grab,
        Eat
    }
    
    void Start()
    {
        camera = Camera.main;
        currentState = TentacleState.Walk;
        CreateTentacle();
        // 忽略触手碰撞体和玩家碰撞体的交互
    }

    void CreateTentacle()
    {
        segments = new GameObject[segmentCount];
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segmentCount;
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.1f;
        
        // 创建触手关节
        for (int i = 0; i < segmentCount; i++)
        {
            segments[i] = new GameObject("Segment_" + i);
            segments[i].layer = LayerMask.NameToLayer("Tentacle");
            //segments[i].transform.position = player.position + Vector3.right * i * segmentLength;
            
            var rb = segments[i].AddComponent<Rigidbody2D>();
            rb.gravityScale = 0.01f;
            rb.useAutoMass = true;
            
            var col = segments[i].AddComponent<CircleCollider2D>();
            col.radius = segmentLength / 2f;
            
            if (i > 0)
            {
                var joint = segments[i].AddComponent<HingeJoint2D>();
                joint.connectedBody = segments[i-1].GetComponent<Rigidbody2D>();
                joint.autoConfigureConnectedAnchor = false;
                joint.anchor = Vector2.left * segmentLength / 2f;
                joint.connectedAnchor = Vector2.right * segmentLength / 2f;
            }
        }
        
        // 第一个段固定在玩家身上
        var playerJoint = segments[0].AddComponent<FixedJoint2D>();
        playerJoint.connectedBody = player.GetComponent<Rigidbody2D>();
        endRb = segments[segmentCount-1].GetComponent<Rigidbody2D>();
        segments[segmentCount - 1].AddComponent<Hook>();
    }

    void Update()
    {
        RenderTentacle();
        AdjustTentacleLength();
    }

    private void FixedUpdate()
    {
        switchState();
    }

    void switchState()
    {
        switch (currentState)
        {
            case TentacleState.Walk:
                Walk();
                break;
            case TentacleState.Grab:
                Grab();
                break;
            case TentacleState.Eat:
                Eat();
                break;
        }
    }

    public void ChangeState(string state)
    {
        switch (state)
        {
            case "Walk":
                currentState = TentacleState.Walk;
                break;
            case "Grab":
                currentState = TentacleState.Grab;
                break;
            case "Eat":
                currentState = TentacleState.Eat;
                break;
        }
    }

    void Walk()
    {
        UpdateEndPoint();
    }

    void Grab()
    {
        UpdateGrabPoint();
    }

    void Eat()
    {
        UpdateEatPoint();
    }
    
    void UpdateEndPoint()
    {
        // 寻找最近的墙面
        Vector2 closestPoint = FindClosestWall();
        
        if (closestPoint != Vector2.zero)
        {
            // 对末端施加力使其靠近墙面
            //Vector2 forceDir = (closestPoint - (Vector2)endRb.position).normalized;
            Vector2 forceDir = (closestPoint - (Vector2)endRb.position).normalized;
            endRb.AddForce(forceDir * stiffness);
        }
    }

    Vector2 FindClosestWall()
    {
        Vector2 closestPoint = Vector2.zero;
        float closestDistance = float.MaxValue;
        
        float angle = num * Mathf.PI * 2 / 16;
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            
        RaycastHit2D hit = Physics2D.Raycast(player.position, direction, maxDistance, wallLayer);
        if (hit.collider != null)
        {
            distance = Vector2.Distance(player.position, hit.point);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPoint = hit.point;
            }
        }
        else
        {
            closestPoint = player.position;
        }
        return closestPoint;
    }

    void UpdateGrabPoint()
    {
        Debug.Log("抓取");
        // 获取鼠标在世界空间中的位置
        Vector3 mouseWorldPos = camera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0; // 确保z坐标为0，保持2D
        distance = Vector2.Distance(player.position, mouseWorldPos);
        //Vector2 forceDir = ((Vector2)mouseWorldPos - endRb.position).normalized;
        Vector2 forceDir = ((Vector2)mouseWorldPos - endRb.position);
        endRb.AddForce(forceDir * stiffness);
        /*if(Vector2.Distance(endRb.position,player.position) <=maxGrabDistance)
            endRb.AddForce(forceDir * stiffness);
        else if( Vector2.Distance(endRb.position,player.position) >maxGrabDistance)
            currentState = TentacleState.Walk;*/
    }

    private Vector3 offset = new Vector3(0, 0.5f,0);
    void UpdateEatPoint()
    {
        Debug.Log("吃吃吃");
        endRb.position = player.position-offset;
        distance = Vector2.Distance(player.position, endRb.position);
    }

    

    void RenderTentacle()
    {
        segments[0].transform.position = player.position;
        for (int i = 0; i < segmentCount; i++)
        {
            lineRenderer.SetPosition(i, segments[i].transform.position);
        }
    }

    void AdjustTentacleLength()
    {
        segmentLength=distance/segmentCount;
    }
}