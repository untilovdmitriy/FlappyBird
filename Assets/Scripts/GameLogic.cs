using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;

public enum windows // перечисление для определения окон, в котором находится игрок в данный момент
{
    main = 1,
    game = 0,
    pause = -1,

    mainScores = 11,
    mainAbout = 12,
    mainExit = 13,

    pauseExit = -11
}

public class GameLogic : MonoBehaviour {

    private static bool paused = false; // пауза
    private static windows window = windows.main; // главное меню
    private static bool soundOn = true; // звук вкл

    private static bool gameOver = false; // конец игры
    private static bool gameStarted = false; // игра началась

    private static float score = 0; //очки
    
    public static bool Paused
    {
        get
        {
            return paused;
        }
        set 
        {
            paused = value;
        }
    }

    public static bool GameOver
    {
        get
        {
            return gameOver;
        }
        set
        {
            gameOver = value;
        }
    }

    public static bool GameStarted
    {
        get
        {
            return gameStarted;
        }
        set
        {
            gameStarted = value;
        }
    }

    public static bool SoundOn
    {
        get
        {
            return soundOn;
        }
        set
        {
            soundOn = value;
        }
    }
    
    public static windows Window
    {
        get
        {
            return window;
        }
        set
        {
            window = value;
        }
    }

    public static float Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

    /// <summary>
    /// сохранение очков
    /// </summary>
    public static void SaveScores() 
    {
        try
        {
            using (StreamWriter sw = new StreamWriter("scores.txt", true))
            {
                string datePatt = @"dd/MM/yyyy hh:mm:ss";
                string dtString = System.DateTime.Now.ToString(datePatt);
                dtString += " " + score.ToString();
                sw.WriteLine(dtString);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("Error!");
            Debug.Log(e.Message);
        }
    }

    /// <summary>
    /// чтение статистики из файла "scores.txt"
    /// </summary>
    /// <returns>статистика</returns>
    public static string GetScores()
    {
        string stat = "";
        bool flag = true;
        int i = 1;

        try
        {
            using (StreamReader sr = new StreamReader("scores.txt"))
            {
                System.Collections.Generic.Dictionary<string, double> dict = new System.Collections.Generic.Dictionary<string, double>();

                while (flag)
                {
                    string line = sr.ReadLine();
                    if (line == null)
                    {
                        flag = false;
                    }
                    else
                    {
                        string[] s = line.Split(' ');
                        if (!dict.ContainsKey(s[0] + " " + s[1])) dict.Add(s[0] + " " + s[1], System.Convert.ToDouble(s[2]));
                    }
                }

                var items = from pair in dict
                            orderby pair.Value descending
                            select pair;

                foreach (System.Collections.Generic.KeyValuePair<string, double> KV in items)
                {
                    stat += i++ + ". " + KV.Key + " - " + KV.Value + "\n";
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("File is empty or not exist!");
            Debug.Log(e.Message);
        }

        return stat;
    }
}
