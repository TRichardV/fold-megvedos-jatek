using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

public class User : MonoBehaviour
{
    //----v?ltoz?k amiket el kell menteni (?rt?kkel vagy n?lk?l)----
    public float[] volumePct = { 1, 1 };

    public float highscore = 0;


    //--------------------------------------------------------------

    private void Awake()
    {
        if (!File.Exists(Application.persistentDataPath + "/user.fun"))
        {
            using (File.Create(Application.persistentDataPath + "/user.fun"))
            SaveData();
        }

        LoadData();
    }

    public void SaveData()
    {
        SaveSystem.SaveUser(this);
    }

    public void LoadData()
    {
        UserData data = SaveSystem.LoadUser();

        //---------------v?ltoz?k amiket el kell menteni----------------
        volumePct = data.volumePct;

        highscore = data.highscore;

        //--------------------------------------------------------------
    }
}
