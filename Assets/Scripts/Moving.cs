using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour
{
    public Transform moveable; // обьект
    private Transform first; // 1й клон
    private Transform second; // 2й клон

    private int atStart = 1; // кто в начале? 1й или 2й клон
    public float y = -3.1f;  // высота, на которой они расположены
    public int SpeedScale = 1; // масштаб для скорости - х1
    public float end = 14.0f; //длина обьектов
    private float xf = 0.0f, xs, z = 9.0f; // начальные позиции по х и z

    // Use this for initialization    
    void Start()
    {
        xs = end;
        atStart = 1;

        if (moveable != null) //если задан обьект то клонируем его
        {
            first = (Transform)Instantiate(moveable);
            first.position = new Vector3(xf, y, z);

            second = (Transform)Instantiate(moveable);
            second.position = new Vector3(xs, y, z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            /* если 2й не пуст И в начале стоял 1й И второй полностью вписался 
             * в видимую область экрана то меняем 1й и 2й естами*/
            if (second != null && atStart == 1 && second.position.x <= 0)
            {
                atStart = 2;
                xf = end;
                first.position = new Vector3(xf, y, z);
            }
            /* аналогично наоборот */
            if (first != null && atStart == 2 && first.position.x <= 0)
            {
                atStart = 1;
                xs = end;
                second.position = new Vector3(xs, y, z);
            }

            //задаем позиции 1го и 2го обьектов
            xf -= Time.deltaTime / SpeedScale;
            xs -= Time.deltaTime / SpeedScale;

            //и переставляем
            if (first != null) first.position = new Vector3(xf, y, z);
            if (second != null) second.position = new Vector3(xs, y, z);
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log("null");
            throw e;
        }
    }
}