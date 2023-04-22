using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(
    fileName = "Player Progress",
    menuName = "Game Kuis/Player Progress")]
public class PlayerProgress : ScriptableObject
{
    [System.Serializable]
    public struct MainData
    {
        public int koin;
        public Dictionary<string, int> progresLevel;
    }

    [SerializeField]
    private string _filename = "contoh.txt";

    public MainData progresData = new MainData();

    public void SimpanProgres()
    {
        //Sampel Data
        progresData.koin = 200;
        if (progresData.progresLevel == null)
            progresData.progresLevel = new();
        progresData.progresLevel.Add("Level Pack 1", 3);
        progresData.progresLevel.Add("Level Pack 3", 5);

        //informasi penyimpanan data
        var directory = Application.dataPath + "/Temporary";
        var path = directory + "/" + _filename;

        //Membuat Directory Temporary
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory has been created: " + directory);
        }

        //Membuat File Baru
        if (File.Exists(path))
        {
            File.Create(path).Dispose();
            Debug.Log("File Created: " + path);
        }
        
        //menyimpan data ke dalam file menggunakan binari formatter
        var fileStream = File.Open(path, FileMode.Open); 
        //var formatter = new BinaryFormatter();

        fileStream.Flush();
        //formatter.Serialize(fileStream, progresData);

        //menyimpan data ke dalam file menggunakan binary writer
        var writer = new BinaryWriter(fileStream);

        writer.Write(progresData.koin);
        foreach (var i in progresData.progresLevel)
        {
            writer.Write(i.Key);
            writer.Write(i.Value);
        }

        //putuskan aliran memory dengan file
        writer.Dispose();
        fileStream.Dispose(); 

        Debug.Log($"{_filename} Berhasil Disimpan");
    }

    public bool MuatProgres()
    {
        //informasi penyimpanan data
        string directory = Application.dataPath + "/Temporary";
        string path = directory + "/" + _filename;

        var fileStream = File.Open(path, FileMode.OpenOrCreate);

        try
        {
            var reader = new BinaryReader(fileStream);

            try
            {
                progresData.koin = reader.ReadInt32();
                if (progresData.progresLevel == null)
                    progresData.progresLevel = new();
                while (reader.PeekChar() != -1)
                {
                    var namaLevelPack = reader.ReadString();
                    var levelKe = reader.ReadInt32();
                    progresData.progresLevel.Add(namaLevelPack, levelKe);
                    Debug.Log($"{namaLevelPack}:{levelKe}");
                }

                //putuskan aliran memori dengan file
                reader.Dispose();
            }
            catch (System.Exception e)
            {
                Debug.Log($"ERROR: Terjadi kesalahan saat memuat progres binari.\n{ e.Message}");

                    //putuskan aliran memori dengan file
                    reader.Dispose();
                    fileStream.Dispose();

                    return false;
            }

            ////memuat data dari file menggunakan binary formatter 
            //var formatter = new BinaryFormatter();

            //progresData = (MainData)formatter.Deserialize(fileStream);

            //putuskan aliran memori dengan File
            fileStream.Dispose();

            Debug.Log($"{progresData.koin}; {progresData.progresLevel.Count}");

            return true;
        }
        catch (System.Exception e)
        {
            //putuskan aliran memori dengan File
            fileStream.Dispose();

            Debug.Log($"ERROR: Terjadi kesalahan saat memuat progress\n{e.Message}");

            return false;
        }
    }
}
