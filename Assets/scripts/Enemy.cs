using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 target;
    private List<Vector3> path;
    private int currentPathIndex;
    Animator animator;
    Transform player;
    private UIcontroller uiController;
    [SerializeField] bool fastEnemy;
    [SerializeField] float speed;
    [SerializeField] float speedMultiplier;
    AudioSource audio;
    [SerializeField] AudioClip walkS, deadS, bitS;

    private void Start()
    {
        path = new List<Vector3>();
        animator = GetComponent<Animator>();
        uiController = GameObject.FindObjectOfType<UIcontroller>();
        audio = gameObject.GetComponent<AudioSource>();
        if (fastEnemy)
        {
            speed *= speedMultiplier;
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player != null)
        {
            target = player.position;
            CalculatePath(transform.position, target);

            if (path.Count > 1)
            {
                MoveAlongPath();
            }

            float distance = Vector3.Distance(transform.position, target);

            // ���������, ��������� �� ���������� � �������� ��������� ���������
            if (distance <= 1.5f)
            {
                animator.SetBool("bliz", true);
            }
            else
            {
                animator.SetBool("bliz", false);
            }
        }
    }

    private void CalculatePath(Vector3 startPosition, Vector3 targetPosition)
    {
        path.Clear();
        // ������� ��������� � �������� ����� ��� ��������
        path.Add(startPosition);
        path.Add(targetPosition);
        currentPathIndex = 1; // �������� �������� �� ������ ����� (targetPosition)
    }

    private void MoveAlongPath()
    {
        if (currentPathIndex < path.Count)
        {
            Vector3 targetPosition = path[currentPathIndex];
            Vector3 direction = (targetPosition - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, targetPosition);

            // ��������� � ��������� ����� ����
            if (distance > 0.1f)
            {
                transform.position += direction * speed * Time.deltaTime;
                transform.rotation = Quaternion.identity; // ������� �� ���� ���� ����� ����� 0
            }
            else
            {
                currentPathIndex++;
            }
        }
        else
        {
            // ���� ��������
            currentPathIndex = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ball")
        {
            //if (PlayerPrefs.GetInt("SoundEnabled") == 1)
            //{
            //    audio.PlayOneShot(deadS);
            //}

            // Destroy(collision.gameObject);
            // Destroy(gameObject, 1f);
        }
    }

    public void bit()
    {
        if (fastEnemy)
        {
            uiController.MinusHP(10);
        }
        else
        {
            uiController.MinusHP(5);
        }
        if (PlayerPrefs.GetInt("SoundEnabled") == 1)
        {
            audio.PlayOneShot(bitS);
        }
    }

    public void WalkSound()
    {
        if (PlayerPrefs.GetInt("SoundEnabled") == 1)
        {
            audio.PlayOneShot(walkS);
        }
    }
}
