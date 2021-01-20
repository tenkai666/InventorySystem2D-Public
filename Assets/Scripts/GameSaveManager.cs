using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameSaveManager : MonoBehaviour
{
    public Item thisItem1;
    public Item thisItem2;
    public Item thisItem3;

    public Inventory myInventory;

    public void SaveGame()
    {
        Debug.Log(Application.persistentDataPath);//程序路径

        //判断是否有存储文件夹
        if (!Directory.Exists(Application.persistentDataPath + "/game_SaveData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_SaveData");//创建文件夹
        }

        BinaryFormatter formatter = new BinaryFormatter();//二进制转化

        //创建存储文件，扩展名随便
        FileStream file = File.Create(Application.persistentDataPath + "/game_SaveData/inventory.txt");

        var json = JsonUtility.ToJson(myInventory);//转换成Json格式

        formatter.Serialize(file, json);//序列化将json变量的string内容保存在file里面

        file.Close();


        BinaryFormatter formatter2 = new BinaryFormatter();//二进制转化

        FileStream file1 = File.Create(Application.persistentDataPath + "/game_SaveData/inventory2.txt");//1 创建存储文件

        var json1 = JsonUtility.ToJson(thisItem1);//2  能覆写回来
        var json2 = JsonUtility.ToJson(thisItem2);
        var json3 = JsonUtility.ToJson(thisItem3);

        formatter2.Serialize(file1, json1);//(1,2)
        formatter2.Serialize(file1, json2);
        formatter2.Serialize(file1, json3);

        file1.Close();
    }

    public void LoadGame()
    {
        BinaryFormatter bf = new BinaryFormatter();

        if (File.Exists(Application.persistentDataPath + "/game_SaveData/inventory.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath +
                "/game_SaveData/inventory.txt", FileMode.Open);

            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), myInventory);

            file.Close();
        }

        BinaryFormatter bf2 = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/game_SaveData/inventory2.txt"))
        {
            FileStream file2 = File.Open(Application.persistentDataPath + 
                "/game_SaveData/inventory2.txt", FileMode.Open);//打开文件

            JsonUtility.FromJsonOverwrite((string)bf2.Deserialize(file2), thisItem1);//反序列化
            JsonUtility.FromJsonOverwrite((string)bf2.Deserialize(file2), thisItem2);
            JsonUtility.FromJsonOverwrite((string)bf2.Deserialize(file2), thisItem3);

            file2.Close();
        }
    }

    //public void RestartScene()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

    //public void GoToMainMenu()
    //{
    //    SceneManager.LoadScene(0);
    //}

    public void QuitGame()
    {
        Application.Quit();
    }
}
