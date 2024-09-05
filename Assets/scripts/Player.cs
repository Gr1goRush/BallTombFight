using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject marker; // Метка
    GameObject bulletPrefab; // Префаб шара
    public GameObject additionalPrefab; // Префаб дополнительного объекта
    public Transform shootPoint; // Точка выстрела
    public float markerDistance = 2f; // Расстояние метки от игрока
    public Joystick joystick; // Ссылка на джойстик
    public Button fireButton; // Ссылка на UI кнопку для выстрела
    public Button multiFireButton; // Ссылка на UI кнопку для мультивыстрела
    public float shootCooldown = 5f; // Время перезарядки
    private float timerShoot;
    [SerializeField] GameObject[] Balls;

    void Start()
    {
        // Привязываем метод Shoot к событию нажатия кнопки fireButton
        fireButton.onClick.AddListener(Shoot);

        // Привязываем метод MultiShoot к событию нажатия кнопки multiFireButton
        multiFireButton.onClick.AddListener(MultiShoot);

        timerShoot = -1; // Инициализация для возможности стрельбы сразу        
    }

    void Update()
    {
        // Получаем направление от джойстика
        Vector2 direction = new Vector2(joystick.Horizontal, joystick.Vertical);

        // Если есть ввод с джойстика
        if (direction.magnitude > 0.1f)
        {
            // Нормализуем направление для расчета угла
            direction.Normalize();

            // Вычисляем угол поворота
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Вычисляем новую позицию метки
            Vector2 markerPosition = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad) * markerDistance,
                Mathf.Sin(angle * Mathf.Deg2Rad) * markerDistance
            );

            // Устанавливаем позицию метки относительно игрока
            marker.transform.localPosition = markerPosition;

            // Устанавливаем поворот метки
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

                // Вычисляем направление выстрела
                Vector2 shootDirection = (marker.transform.position - transform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity, transform);
            bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * 10f;

            // Создаем дополнительный объект на позиции маркера и с таким же поворотом, добавив +130 к Z повороту
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
            // Создаем 8 шаров, равномерно распределенных по кругу
            for (int i = 0; i < 8; i++)
            {
                // Вычисляем угол для текущего выстрела
                float angle = i * (360f / 8);

                // Вычисляем направление выстрела
                Vector2 shootDirection = new Vector2(
                    Mathf.Cos(angle * Mathf.Deg2Rad),
                    Mathf.Sin(angle * Mathf.Deg2Rad)
                ).normalized;

                // Смещаем точку спауна, чтобы шары не пересекались
                Vector3 spawnPosition = shootPoint.position + new Vector3(shootDirection.x, shootDirection.y, 0) * 0.5f;

                // Создаем шар и задаем ему направление
                GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * 10f; // Скорость шара

                // Создаем дополнительный объект на позиции маркера и с таким же поворотом, добавив +130 к Z повороту
                Quaternion additionalRotation = marker.transform.rotation * Quaternion.Euler(0, 0, 130);

            }
        }

      


    }
    
}