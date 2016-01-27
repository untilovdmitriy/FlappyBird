using UnityEngine;
using System.Collections;

public class Flappy : MonoBehaviour {

    public GUISkin MenuSkin; // вид меню
    public Texture2D bird; // птичка в спокойном состоянии
    public Texture2D birdUp; // птичка летит вверх
    public Texture2D birdDown; // падает вниз
    public AudioClip flap; // звук полета
    public AudioClip crash; // звук при столкновении

    public Vector3 upperForce = new Vector3(0, 70, 0);       // сила, с которой птичка подлетает
    private int flying = 0; //0 - ровно, 1 - вверх, -1 - вниз
    private float flapDelay = 0.0f; // задержка при полете

	// Use this for initialization
	void Start () {        
        transform.GetComponent<Renderer>().material.mainTexture = bird; // задаем начальную текстуру птицы
        //сбрасываем зн-я переменных
        flying = 0;
        Time.timeScale = 0;
        GameLogic.GameOver = false; 
        GameLogic.GameStarted = false;
        GameLogic.Score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<AudioSource>().loop = false; // отключаем проигрыш звука по кругу
        if (!GameLogic.Paused) // если игра не на паузе
        {
            flying = 0; 
            if (GameLogic.GameStarted == false || GameLogic.GameOver) Time.timeScale = 0;
            GameLogic.Score += Time.deltaTime; // увеличиваем очки (время)
            if (flapDelay > 0)
            {
                flapDelay -= Time.deltaTime;
            }
            if (Input.GetMouseButton(0)) // было нажатие кнопки полета
            {
                flying = 1; // меняем индикатор
                if (GameLogic.SoundOn && !GetComponent<AudioSource>().isPlaying && flapDelay <= 0)
                {                    
                    GetComponent<AudioSource>().PlayOneShot(flap); // проигрываем звук
                    flapDelay = 0.5f; // обновляем задержку
                }
                if (!GameLogic.GameOver)
                {
                    Time.timeScale = 1;
                    GameLogic.GameStarted = true;
                }
                transform.GetComponent<Rigidbody2D>().AddForce(upperForce); // придаем ускорения вверх
                if (Time.timeScale == 0)
                {
                    Application.LoadLevel(Application.loadedLevel); // если игра закончилась, перезагружаем уровень
                }
            }
            else
            {
                flying = -1; // падаем
            }
            //меняем текстуры
            if (flying == 0) transform.GetComponent<Renderer>().material.mainTexture = bird;
            else if (flying == 1) transform.GetComponent<Renderer>().material.mainTexture = birdUp;
            else if (flying == -1) transform.GetComponent<Renderer>().material.mainTexture = birdDown;
        }         
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        //если было столкновение не с верхом экрана
        if (other.gameObject.tag != "top")
        {
            if (GameLogic.SoundOn) GetComponent<AudioSource>().PlayOneShot(crash); // звук столкновения
            Time.timeScale = 0; // останавливаем течение времени
            GameLogic.GameOver = true; // игра окончена
        }
        GameLogic.SaveScores(); // сохраняем очки
    }

    void OnGUI()
    {
        //выводим соотв. сообщения в начале или в конце игры
        GUI.skin = MenuSkin;
        if (!GameLogic.GameStarted) GUI.Label(new Rect(0, Screen.height / 2, Screen.width, 50), "Please Tap on screen for flying");
        if (GameLogic.GameOver) GUI.Label(new Rect(0, Screen.height / 2, Screen.width, 60), "Game over! Your Score is " + GameLogic.Score + ".\n Tap on screen for try again");
        GUI.Label(new Rect(Screen.width / 2 - 60, Screen.height / 10, 120, 40), GameLogic.Score.ToString());
    }
}
