using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public const float MAX_SWIPE_TIME = 0.5f;
    public const float MIN_SWIPE_DISTANCE = 0.17f;
    Vector2 startPos;
    float startTime;


    [SerializeField] private float moveSpeed;

    [SerializeField] private Transform ball;
    [SerializeField] private Transform posDribble;
    [SerializeField] private Transform target;

    private bool isBallFlying = false;
    private float timer = 0;

    void Start()
    {
        
    }
    void Update()
    {
        Dribble();
        Move();
        BallFly();
    }
    public void Move()
    {
#if UNITY_EDITOR
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.position += direction * moveSpeed * Time.deltaTime;
#endif
        /*if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                startPos = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width);
                startTime = Time.time;
            }
            if (t.phase == TouchPhase.Ended)
            {
                if (Time.time - startTime > MAX_SWIPE_TIME) // press too long
                    return;

                Vector2 endPos = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width);

                Vector2 swipe = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);

                if (swipe.magnitude < MIN_SWIPE_DISTANCE) // Too short swipe
                    return;

                if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
                { // Horizontal swipe
                    if (swipe.x > 0)
                    {
                        direction = Vector2.right;
                    }
                    else
                    {
                        direction = Vector2.left;
                    }
                }
                else
                { // Vertical swipe
                    if (swipe.y > 0)
                    {
                        direction = Vector2.up;
                    }
                    else
                    {
                        direction = Vector2.down;
                    }
                }
            }
        }*/
    }
    public void Dribble()
    {
        ball.position = posDribble.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * 3));

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isBallFlying = true;
            timer = 0;
        }
    }
    public void BallFly()
    {
        if (isBallFlying)
        {
            timer += Time.deltaTime;
            float duration = 0.5f;
            float lastTime = timer / duration;

            Vector3 startPosition = ball.position;
            Vector3 endPosition = target.position;
            Vector3 pos = Vector3.Lerp(startPosition, endPosition, lastTime);

            Vector3 arc = Vector3.up * 5 * Mathf.Sin(lastTime * 3.14f);
            ball.position = pos + arc;

            if (lastTime >= 1)
            {
            isBallFlying = false;
            ball.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TargetArea"))
        {
        transform.LookAt(target.parent.position);
        }
    }
}
        
