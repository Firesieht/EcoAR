using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine.UI;
using JetBrains.Annotations;

public class rotation : MonoBehaviour
{
    // Start is called before the first frame update
    public Text MaxScore;
    void Start()
    {
        MaxScore.text = MemorizeClass.getMemorizeable("score.ini");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, 0, 25f * Time.deltaTime, Space.Self);
    }

    public static class MemorizeClass //класс записи и чтения файлов
    {
        public static void Memorize(string data, string fileName) //функция записи
        {
            File.Delete(Application.persistentDataPath + "/" + fileName);
            FileStream saver = File.OpenWrite(Application.persistentDataPath + "/" + fileName);

            saver.Write(Encoding.ASCII.GetBytes(data), 0, Encoding.ASCII.GetBytes(data).Length);
            saver.Close();
        }

        public static string getMemorizeable(string fileName) //функция чтения данных из файла
        {
            string res = "";

            FileStream reader = File.OpenRead(Application.persistentDataPath + "/" + fileName);
            byte[] array = new byte[reader.Length];
            reader.Read(array, 0, array.Length);
            res = Encoding.Default.GetString(array);

            return res;
        }

    }
}
