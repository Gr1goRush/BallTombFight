using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject marker; // �����
    GameObject bulletPrefab; // ������ ����
    public GameObject additionalPrefab; // ������ ��������������� �������
    public Transform shootPoint; // ����� ��������
    public float markerDistance = 2f; // ���������� ����� �� ������
    public Joystick joystick; // ������ �� ��������
    public Button fireButton; // ������ �� UI ������ ��� ��������
    public Button multiFireButton; // ������ �� UI ������ ��� ��������������
    public float shootCooldown = 5f; // ����� �����������
    private float timerShoot;
    [SerializeField] GameObject[] Balls;

    void Start()
    {
        // ����������� ����� Shoot � ������� ������� ������ fireButton
        fireButton.onClick.AddListener(Shoot);

        // ����������� ����� MultiShoot � ������� ������� ������ multiFireButton
        multiFireButton.onClick.AddListener(MultiShoot);

        timerShoot = -1; // ������������� ��� ����������� �������� �����        
    }

    void Update()
    {
        // �������� ����������� �� ���������
        Vector2 direction = new Vector2(joystick.Horizontal, joystick.Vertical);

        // ���� ���� ���� � ���������
        if (direction.magnitude > 0.1f)
        {
            // ����������� ����������� ��� ������� ����
            direction.Normalize();

            // ��������� ���� ��������
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // ��������� ����� ������� �����
            Vector2 markerPosition = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad) * markerDistance,
                Mathf.Sin(angle * Mathf.Deg2Rad) * markerDistance
            );

            // ������������� ������� ����� ������������ ������
            marker.transform.localPosition = markerPosition;

            // ������������� ������� �����
            marker.transform.rotation = Quaternion.Euler(0, 0, angle + 130);
        }
        timerShoot += Time.deltaTime;
        if (timerShoot < shootCooldown)
        {
            fireButton.interactable = false;
        }
        else
        {
            fireButton.interactable = true;
        }
    }

    void Shoot()
    {
       
            switch (PlayerPrefs.GetInt("ActiveCharacter"))
            {
                case 0:
                    bulletPrefab = Balls[0];
                    break;
                case 1:
                    bulletPrefab = Balls[1];
                    break;
                case 2:
                    bulletPrefab = Balls[2];
                    break;
                case 3:
                    bulletPrefab = Balls[3];
                    break;
                default:
                    break;
            }
            if (timerShoot > shootCooldown)
            {
                timerShoot = 0;

                // ��������� ����������� ��������
                Vector2 shootDirection = (marker.transform.position - transform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity, transform);
            bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * 10f;

            // ������� �������������� ������ �� ������� ������� � � ����� �� ���������, ������� +130 � Z ��������
            Quaternion additionalRotation = marker.transform.rotation * Quaternion.Euler(0, 0, 130);
                Instantiate(additionalPrefab, marker.transform.position, additionalRotation);
            }

        
        

    }

    void MultiShoot()
    {
        switch (PlayerPrefs.GetInt("ActiveCharacter"))
        {
            case 0:
                bulletPrefab = Balls[0];
                break;
            case 1:
                bulletPrefab = Balls[1];
                break;
            case 2:
                bulletPrefab = Balls[2];
                break;
            case 3:
                bulletPrefab = Balls[3];
                break;
            default:
                break;
        }
        if (PlayerPrefs.GetFloat("ulta") > 1)
        {
            if (PlayerPrefs.GetInt("VibeEnabled") == 1)
            {
                Handheld.Vibrate();
            }
            PlayerPrefs.SetFloat("ulta", 0);
            // ������� 8 �����, ���������� �������������� �� �����
            for (int i = 0; i < 8; i++)
            {
                // ��������� ���� ��� �������� ��������
                float angle = i * (360f / 8);

                // ��������� ����������� ��������
                Vector2 shootDirection = new Vector2(
                    Mathf.Cos(angle * Mathf.Deg2Rad),
                    Mathf.Sin(angle * Mathf.Deg2Rad)
                ).normalized;

                // ������� ����� ������, ����� ���� �� ������������
                Vector3 spawnPosition = shootPoint.position + new Vector3(shootDirection.x, shootDirection.y, 0) * 0.5f;

                // ������� ��� � ������ ��� �����������
                GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * 10f; // �������� ����

                // ������� �������������� ������ �� ������� ������� � � ����� �� ���������, ������� +130 � Z ��������
                Quaternion additionalRotation = marker.transform.rotation * Quaternion.Euler(0, 0, 130);

            }
        }

      


    }
    
}