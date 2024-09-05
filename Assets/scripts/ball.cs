using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ball : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audio;
   [SerializeField] AudioClip coin,dead;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        Destroy(gameObject,5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        if (collision.gameObject.tag == "enemy")
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            if (PlayerPrefs.GetInt("SoundEnabled") == 1)
            {
                audio.PlayOneShot(dead);
            }
           
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
            Destroy(collision.gameObject);
            Destroy(gameObject, 1);
        }
        if (collision.gameObject.tag == "wall")
        {

            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "money")
        {
            if (PlayerPrefs.GetInt("SoundEnabled") == 1)
            {
                audio.PlayOneShot(coin);
            }
            PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money", 0) + 1);
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
            Destroy(gameObject, 1);
            Destroy(collision.gameObject);
        }
       
        if (collision.gameObject.tag == "rune")
        {
            if (PlayerPrefs.GetInt("VibeEnabled") == 1)
            {
                Handheld.Vibrate();
            }
            PlayerPrefs.SetFloat("ulta", PlayerPrefs.GetFloat("ulta")+0.34f);
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
            Destroy(collision.gameObject);
            Destroy(gameObject,1);
        }
        
    }
}
