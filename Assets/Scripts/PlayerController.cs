using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jump;
    [SerializeField] private float forceMultiplier = 2;

    Rigidbody rb;
    public Transform targetPosition;
    public float shootTime;

    Vector3 startPosition, endPosition;

    public bool shoot;
    float directionX, directionY;

    public GameObject gameOverPanel, gameWinPanel;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        DOTween.Init();
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
            startPosition = Input.mousePosition;;
            rb.freezeRotation = false;
            rb.constraints = RigidbodyConstraints.None;
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
            if (directionY > 200f || directionY < -200f)
            {
                Shoot();
            }
            startPosition = Vector3.zero;
            endPosition = Vector3.zero;
            rb.freezeRotation = true;
            rb.constraints = RigidbodyConstraints.FreezePositionX;
        }
    }
    public void Shoot()
    {
        rb.AddForce(new Vector3(0, 1, 0.5f) * forceMultiplier);
        if (shoot == true)
        {
            transform.DOJump(targetPosition.position, 1f, 1, shootTime, false);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
    public void GameOver()
    {
        if(transform.position.y < 0)
        {
            gameOverPanel.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
         if (other.gameObject.CompareTag("Success"))
         {
            gameWinPanel.SetActive(true);
         }
         else if (other.gameObject.CompareTag("TargetArea"))
         {
            shoot = false;
         }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TargetArea"))
        {
            shoot = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.AddForce(Vector3.up * jump);
        }
    }
}
        