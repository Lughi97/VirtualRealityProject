using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// This is the save and load class that creates
/// and load the highscore, the total coin collected 
/// and the skin that was use by the player
/// </summary>
public class SaveSystem
{
    static List<List<int>> DataScore = new List<List<int>>();
    static List<coinValue> CoinsCollected = new List<coinValue>();
    static PlayerType currentSavedType;

    public static string directory = "/SaveData/";
    public static string playerFile = "playerData.txt";
    public static string scoreFIle = "HighScores";
    public static string coinFile = "CoinCollected";
    public static PlayerType type;
    public static void SaveScore(ScoringSystem ScoreSystem)
    {
        string dir = Application.persistentDataPath + directory;

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
        Debug.Log("SAVING DATA AT: " + dir);
        BinaryFormatter formatter = new BinaryFormatter();
        //string path = Application.persistentDataPath + "/highscores.json"; // use in the final game 
        // string pathScore = "TempHighScore";

        FileStream stream = new FileStream(dir + scoreFIle, FileMode.Create);

        DataScore.Add(ScoreSystem.HighScoreLevel1);
        DataScore.Add(ScoreSystem.HighScoreLevel2);
        DataScore.Add(ScoreSystem.HighScoreLevel3);
        formatter.Serialize(stream, DataScore);
        stream.Close();

        DataScore.Clear();

        //  string pathCoins = "TempCoinCounter";
        FileStream streamCoin = new FileStream(dir + coinFile, FileMode.Create);
        CoinsCollected = ScoreSystem.listCoins;
        formatter.Serialize(streamCoin, CoinsCollected);
        streamCoin.Close();
        //CoinsCollected.Clear();

    }

    public static void SaveSkin(PlayerType typePlayer)
    {
        string dir = Application.persistentDataPath + directory;

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
        type = typePlayer;
        Debug.Log("SAVING DATA AT: " + dir);
        string json = JsonUtility.ToJson(type);
        File.WriteAllText(dir + playerFile, json);


        //using JSON saving 
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    string pathType= "TempType";

        //    FileStream streamType = new FileStream(pathType, FileMode.Create);
        //   currentSavedType = type;
        //   formatter.Serialize(streamType, currentSavedType);
        //   streamType.Close();

        //currentSavedType = type;
        // string str = JsonUtility.ToJson(currentSavedType);
        //File.WriteAllText(Application.dataPath + "/save.txt", str);

    }

    public static List<List<int>> LoadHighScores()
    {
        string fullPath = Application.persistentDataPath + directory + scoreFIle;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = fileExists(fullPath);
        // string path = "TempHighScore";
        //  if (File.Exists(path))
        //  {
        //       BinaryFormatter formatter = new BinaryFormatter();
        //      FileStream stream = new FileStream(path, FileMode.Open);
        if (stream != null)
        {
            DataScore = formatter.Deserialize(stream) as List<List<int>>;

            stream.Close();
            return DataScore;


        }
        else
        {
            Debug.Log("Save file not found in" + fullPath);
            return null;
        }
    }

    public static List<coinValue> LoadCoinCollected()
    {
        //string pathCoins = "TempCoinCounter";
        string fullPath = Application.persistentDataPath + directory + coinFile;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = fileExists(fullPath);

        if (stream != null)
        {
            CoinsCollected = formatter.Deserialize(stream) as List<coinValue>;
            stream.Close();
            return CoinsCollected;


        }
        else
        {
            Debug.Log("Save file not found in" + fullPath);
            return null;
        }
    }

    public static PlayerType loadType()
    {

        string fullPath = Application.persistentDataPath + directory + playerFile;

        type = ScriptableObject.CreateInstance<PlayerType>();
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            Debug.Log(json);
            JsonUtility.FromJsonOverwrite(json, type);
            return type;
        }
        else
        {
            Debug.Log("Save file does not exists");
            return null;
        }
    }


    public static FileStream fileExists(string path)
    {
        if (File.Exists(path))
        {

            FileStream stream = new FileStream(path, FileMode.Open);
            return stream;
        }
        else return null;
    }
}
