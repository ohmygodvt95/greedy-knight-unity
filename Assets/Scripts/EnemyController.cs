using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [Header("Thiết lập")]
    public float tileSize = 1f;           // Kích thước 1 ô (unit)
    public float moveSpeed = 2f;      // Tốc độ di chuyển
    public int maxSteps = 2;          // Số bước tối đa mỗi lần di chuyển
    public LayerMask obstacleMask;        // Layer của các chướng ngại vật

    [Header("Khoảng thời gian chờ")]
    public float minWait = 0.5f;
    public float maxWait = 2f;

    private Rigidbody2D rb;

    private int dir = 1; // Hướng di chuyển ban đầu (1 = phải, -1 = trái)

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine(RandomWalkRoutine());
    }

    private IEnumerator RandomWalkRoutine()
    {
        // Chạy vô hạn
        while (true)
        {
            Debug.Log("Enemy is moving...");
            // 1. Chọn hướng và số ô
            dir = -dir; // Đảo ngược hướng di chuyển
            int steps = Random.Range(1, maxSteps + 1);
            Vector2 startPos  = rb.position;
            Vector2 targetPos = startPos + Vector2.right * dir * steps * tileSize;

            // 2. Kiểm tra đường đi
            float halfW    = GetComponent<Collider2D>().bounds.extents.x;
            float checkDist = Mathf.Abs(targetPos.x - startPos.x) + halfW;
            var hit = Physics2D.BoxCast(
                startPos,
                GetComponent<Collider2D>().bounds.size,
                0f,
                Vector2.right * dir,
                checkDist,
                obstacleMask
            );

            rb.transform.localScale = new Vector3(dir, 1f, 1f); // Đảo ngược hướng di chuyển

            // 3. Nếu trống thì di chuyển, ngược lại bỏ lượt
            if (hit.collider == null)
                yield return StartCoroutine(MoveTo(targetPos));

            // 4. Chờ rồi lại lặp tiếp
            float waitT = Random.Range(minWait, maxWait);
            yield return new WaitForSeconds(waitT);
        }
    }

    private IEnumerator MoveTo(Vector2 destination)
    {
        float epsilon = 0.01f;  // ngưỡng “gần đủ”
        while (Vector2.Distance(rb.position, destination) > epsilon)
        {
            Vector2 newPos = Vector2.MoveTowards(rb.position, destination, moveSpeed * Time.deltaTime);

            // Nếu không thể di chuyển (kẹt) thì thoát sớm
            if (newPos == rb.position)
            {
                yield break;
            }

            rb.MovePosition(newPos);
            yield return null;
        }
        // Đảm bảo đặt vị trí chính xác ở cuối
        rb.MovePosition(destination);
        yield break;
    }

}
