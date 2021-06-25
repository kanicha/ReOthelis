using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeSelect : MonoBehaviour
{
    public RectTransform cursor;
    int countUp = 0;
    int countDown = 0;

    // Start is called before the first frame update
    void Start()
    {
        cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-210, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (countDown == 0)
            {
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-210, -101, 0);
                countDown++;
            }
            else if (countDown == 1)
            {
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-210, -202, 0);
                countDown++;
            }
            else if (countDown == 2)
            {
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-210, 0, 0);
                countDown = 0;
            }
        }
    }
}
