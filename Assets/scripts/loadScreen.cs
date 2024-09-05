using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadScreen : MonoBehaviour
{
    // Start is called before the first frame update
    float load=0;
    [SerializeField] Text loadText;
    [SerializeField] Image loadImage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        load += Time.deltaTime/3;
        loadImage.fillAmount = load;
        loadText.text = (int)(load*100)+"%";
        if (load>0.9)
        {
            gameObject.SetActive(false);
        }
    }
}
