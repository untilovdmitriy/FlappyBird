using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Menu : MonoBehaviour {

    public GUISkin MenuSkin; // вид меню
    public GUIContent soundButtonOn; // текстура кнопки включенного звука
    public GUIContent soundButtonOff; // текстура кнопки выключенного звука

    Vector2 scrollPosition; // позиция полосы прокрутки
    Touch touch; // касание пальцем экрана

	void Start () {
        Screen.orientation = ScreenOrientation.Portrait; 
        if (Screen.width < 1400) Screen.fullScreen = true; 
        else Screen.SetResolution(1280, 720, false);        
        GameLogic.Window = windows.main;      
	}

    void Update()
    {
        if (Input.touchCount > 0) // если было касание экрана
        {
            touch = Input.touches[0];
            if (touch.phase == TouchPhase.Moved) // и палец перетаскивали по экрану
            {
                scrollPosition.y += touch.deltaPosition.y; // смещаем позицию скролла
            }
        }
    }

    void OnGUI()
    {
        GUI.skin = MenuSkin;

        if (GameLogic.SoundOn && GameLogic.Window == windows.main) // выключаем звук
        {
            if (GUI.Button(new Rect(Screen.width / 3, 0, 60, 60), soundButtonOn)) 
            {
                GameLogic.SoundOn = false;
            }
        }
        if (!GameLogic.SoundOn && GameLogic.Window == windows.main) // включаем звук
        {
            if (GUI.Button(new Rect(Screen.width / 3, 0, 60, 60), soundButtonOff)) 
            {
                GameLogic.SoundOn = true;
            }
        }
        
        GUI.BeginGroup(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 1.5f));
        if (GameLogic.Window == windows.main)
        {         
            if (GUI.Button(new Rect(0, 0, Screen.width / 2, Screen.height / 2 / 4), "Play!"))
            {
                // идем в игру
                Application.LoadLevel(1);
            }
            if (GUI.Button(new Rect(0, Screen.height / 2 / 4 + 10, Screen.width / 2, Screen.height / 2 / 4), "Scores"))
            {
                // в окно статистики
                GameLogic.Window = windows.mainScores;
            }
            if (GUI.Button(new Rect(0, 2 * Screen.height / 2 / 4 + 20, Screen.width / 2, Screen.height / 2 / 4), "About"))
            {
                // в окно "Об игре"
                GameLogic.Window = windows.mainAbout;
            }
            if (GUI.Button(new Rect(0, 3 * Screen.height / 2 / 4 + 30, Screen.width / 2, Screen.height / 2 / 4), "Exit"))
            {
                // на выход
                GameLogic.Window = windows.mainExit;
            }
        }
        if (GameLogic.Window == windows.mainAbout)
        {
            GUI.Label(new Rect(0, 0, Screen.width / 2, Screen.height / 2 / 6), "About");
            GUI.Box(new Rect(0, Screen.height / 2 / 6 + 10, Screen.width / 2, Screen.height / 2 / 1.5f), "This game was developed by student of PZ-12-u group in FPM DNU of O. Honchar Untilov Dmitriy as a diploma project. (c) 2015");
            if (GUI.Button(new Rect(0, Screen.height / 2 / 6 + Screen.height / 2 / 1.5f + 20, Screen.width / 2, Screen.height / 2 / 4), "Back"))
            {
                // в главне меню из меню "об игре"
                GameLogic.Window = windows.main;
            }
        }
        if (GameLogic.Window == windows.mainExit)
        {
            GUI.Box(new Rect(0, 0, Screen.width / 2, Screen.height / 2 / 5), "Do you want to exit?");
            if (GUI.Button(new Rect(0, Screen.height / 2 / 6, Screen.width / 4, Screen.height / 2 / 6), "Yes"))
            {
                //закрываем игру
                Application.Quit();
            }
            if (GUI.Button(new Rect(Screen.width / 4 + 10, Screen.height / 2 / 6, Screen.width / 4, Screen.height / 2 / 6), "No"))
            {
                // передумали - назад в главное меню
                GameLogic.Window = windows.main;
            }
        }
        if (GameLogic.Window == windows.mainScores)
        {
            //вывод статистики
            GUILayout.Label("Scores");
            
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width / 2), GUILayout.Height(Screen.height / 2 - 20));
                GUILayout.Box(GameLogic.GetScores()); //окно с полосой прокрутки
            GUILayout.EndScrollView();
    
            if (GUI.Button(new Rect(0, Screen.height / 2 + 20, Screen.width / 2, Screen.height / 2 / 4), "Back"))
            {
                //назад в главное меню
                GameLogic.Window = windows.main;
            }
        }
        GUI.EndGroup();
    }
}