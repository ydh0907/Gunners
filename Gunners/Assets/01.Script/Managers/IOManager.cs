using System;
using System.IO;
using UnityEngine;

public class IOManager : MonoBehaviour
{
    public static IOManager Instance;

    public int Win = 0;
    public int Lose = 0;
    public string path = "Gunners.txt";

    private void Awake()
    {
        Instance = this;

        path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), path);

        if (File.Exists(path))
        {
            using(FileStream fs = File.Open(path, FileMode.Open))
            {
                byte[] buffer = new byte[8];
                fs.Read(buffer, 0, 8);
                Win = BitConverter.ToInt32(buffer, 0);
                Lose = BitConverter.ToInt32(buffer, 4);
            }
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.onGameWin += CountWin;
        GameManager.Instance.onGameLose += CountLose;
    }

    private void OnDisable()
    {
        GameManager.Instance.onGameWin -= CountWin;
        GameManager.Instance.onGameLose -= CountLose;
    }

    private void CountWin() => ++Win;
    
    private void CountLose() => ++Lose;

    private void OnApplicationQuit()
    {
        using (FileStream fs = File.Open(path, FileMode.OpenOrCreate))
        {
            byte[] buffer = new byte[8];
            Buffer.BlockCopy(BitConverter.GetBytes(Win), 0, buffer, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(Lose), 0, buffer, sizeof(int), 4);
            fs.Write(buffer, 0, 8);
        }
    }
}
