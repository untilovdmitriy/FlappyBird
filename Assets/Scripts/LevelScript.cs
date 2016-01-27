using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour {

    public Transform obstacle; // препядствие
    private const int n = 5; // кол-во препядствий
    private float xstart = 10, x = 0, step = 4, y = 13; // начальные позиции
    private Transform[] obstacles; // массив препядствий

	// Use this for initialization
	void Start () {
        x = xstart;
        obstacles = new Transform[n]; // заполняем массив препядствий клонами обьекта obstacle и расставляем их с шагом step
        for (int i = 0; i < n; i++)
        {
            obstacles[i] = (Transform)Instantiate(obstacle);
            y = Random.Range(10.5f, 14.5f);
            obstacles[i].transform.position = new Vector3(x, y, 0);
            x += step;
        }
        x = xstart + 1.7f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        //двигаем одновременно все препядствия из массива
        foreach (Transform t in obstacles)
        {
            t.Translate(Vector2.left * Time.deltaTime * 4);
            // если последнее препядствие зашло за левый край экрана, перемещаем его на самое последнее место справа
            if (t.position.x < -8)
            {
                y = Random.Range(10.5f, 14.5f);     // двигаем препядствие по вертикале на случайную высоту
                {
                    t.position = new Vector3(x, y, 0);                                   
                }
            }        
        }
	}
}