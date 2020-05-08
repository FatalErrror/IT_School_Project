using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;

public class Serealizer
{
    const string path = "data";
    const char key = '\u022b';
    public static void SavaData(ISerealizable[] objects)
    {
        List<string> names = new List<string>();
        for (int i = 0; i < objects.Length; i++)
            if (names.Contains(objects[i].getName())) names.Add(objects[i].getName() + i);
            else names.Add(objects[i].getName());


        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        for (int i = 0; i < objects.Length; i++)
        {

            string[] data = objects[i].Serealization();
            for (int j = 0; j < data.Length; j++) data[j] = XOR(key, data[j]);

            File.WriteAllLines(path + "/data" + names[i] + ".data", data);
        }
    }

    private static string XOR(char key, string value)
    {
        var temp = value.ToCharArray();
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = System.Convert.ToChar(temp[i] ^ key);

        }
        return new string(temp);
    }

    public static void LoadData(ISerealizable[] objects)
    {
        List<string> names = new List<string>();
        for (int i = 0; i < objects.Length; i++)
            if (names.Contains(objects[i].getName())) names.Add(objects[i].getName() + i);
            else names.Add(objects[i].getName());
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        for (int i = 0; i < objects.Length; i++)
        {
            string[] data = File.ReadAllLines(path + "/data" + names[i] + ".data");
            for (int j = 0; j < data.Length; j++) data[j] = XOR(key, data[j]);

            objects[i].Deserealization(data);
        }
    }
}

public interface ISerealizable
{
    string getName();
    string[] Serealization();
    void Deserealization(string[] data);

}