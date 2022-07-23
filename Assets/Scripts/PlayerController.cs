using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jump;
    [SerializeField] private float forceMultiplier = 2;

    Rigidbody rb;
    public Transform targetPosition;

    Vector3 startPosition, endPosition;

    bool shoot;
    float directionX, directionY;

    public GameObject gameOverPanel, gameWinPanel;

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
            if (directionY > 100f && directionY < 300f)
            {
                Shoot();
            }
            else if (directionY < -100f && directionY > -300f)
            {
                Shoot();
            }
            startPosition = Vector3.zero;
            endPosition = Vector3.zero;
        }
    }
    public void Shoot()
    {
        rb.AddForce(new Vector3(0,1,0.5f) * forceMultiplier);
        shoot = true;
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
        else
        {
            shoot = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("TargetArea"))
        {
            if(shoot == true)
            {
                transform.LookAt(targetPosition.parent.position);
                Vector3.MoveTowards(transform.position, targetPosition.position, 100f);
                transform.up = targetPosition.transform.position - transform.position;
                Quaternion.Euler(targetPosition.rotation.x - transform.rotation.x, targetPosition.rotation.y - transform.rotation.y, targetPosition.rotation.z - transform.rotation.z);
            }
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
        