using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Pause : MonoBehaviour {

    public GUISkin MenuSkin; // скин для меню (внешний вид, шрифт и т.п.)

    void Update () {
        // Ставим игру на паузу
        if(Input.GetKeyUp(KeyCode.Escape)) // если нажата кнопка паузы (назад/ескейп)
        {     
            if(!GameLogic.Paused) // и игра не на паузе
            {  
                Time.timeScale = 0; // останавливаем течение времени и задаем соотв. зн-я переменным
                GameLogic.Paused = true; 
                GameLogic.Window = windows.pause;
            }
            else
            {  
                //обратные д-я
                Time.timeScale = 1;
                GameLogic.Paused = false;
                GameLogic.Window = windows.game;
            } 
        } 
    } 
    
    void  OnGUI ()
    {
        GUI.skin = MenuSkin; // задали внешний вид
        GUI.BeginGroup(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2)); // группировка для меню
        if (GameLogic.Window == windows.pause) //д-я в меню паузы
        {
            /* если нажата кнопка "вернутся в игру" то 
             * течение времени восстанавливаем и меняем соотв. переменные*/
            if (GUI.Button(new Rect(0, 0, Screen.width / 2, Screen.height / 2 / 4), "Resume")) 
            { // Продолжить
                Time.timeScale = 1;
                GameLogic.Paused = false;
                GameLogic.Window = windows.game; 
            }
            if (GUI.Button(new Rect(0, Screen.height / 2 / 4 + 10, Screen.width / 2, Screen.height / 2 / 4), "To main")) 
            { // уходим в Главное меню 
                Time.timeScale = 1;
                GameLogic.Paused = false;
                GameLogic.Window = windows.main; 
                Application.LoadLevel(0);
            }
            if (GUI.Button(new Rect(0, 2 * Screen.height / 2 / 4 + 20, Screen.width / 2, Screen.height / 2 / 4), "Exit")) 
            { // Выход из игры
                GameLogic.Window = windows.pauseExit;
            } 
        }
        if (GameLogic.Window == windows.pauseExit)
        {
            GUI.Box(new Rect(0, 0, Screen.width / 2, Screen.height / 2 / 5), "Do you want to exit?");
            if (GUI.Button(new Rect(0, Screen.height / 2 / 6, Screen.width / 4, Screen.height / 2 / 6), "Yes"))
            {
                //закрываем игру
                Application.Quit();
            }
            if (GUI.Button(new Rect(Screen.width / 4 + 10, Screen.height / 2 / 6, Screen.width / 4, Screen.height / 2 / 6), "No"))
            {
                //если передумали и ответ "нет", то возврат к игре
                Time.timeScale = 1;
                GameLogic.Paused = false;
                GameLogic.Window = windows.game; 
            }
        }
        GUI.EndGroup();
    }
}