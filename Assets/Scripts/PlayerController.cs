using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float forceMultiplier = 3;
    [SerializeField ]private bool isShoot;

    Rigidbody rb;
    Vector3 startPosition, endPosition;
    public Transform targetPosition;
    float directionX, directionY;

    public GameObject gameOverPanel;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Move();
        GameOver();
    }
    public void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(GameObject.Find("StartButton"));
            startPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;

            directionX = endPosition.x - startPosition.x;
            directionY = endPosition.y - startPosition.y;

            rb.velocity += new Vector3(directionX * moveSpeed * Time.deltaTime, 0, directionY * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (directionY > 50f && directionY < 250)
            {
                Shoot(endPosition - startPosition);
            }
            startPosition = Vector3.zero;
            endPosition = Vector3.zero;
        }
    }
    void Shoot(Vector3 Force)
    {
        if (isShoot)
            return;

        rb.AddTorque(Force);
        rb.AddForce(new Vector3(Force.x, Force.y, Force.y) * forceMultiplier);
        isShoot = true;
    }
    public void GameOver()
    {
        if(transform.position.y < 0)
        {
            gameOverPanel.SetActive(true);
        }
    }
}
        