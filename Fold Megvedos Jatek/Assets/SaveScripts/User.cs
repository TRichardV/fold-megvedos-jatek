using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

public class User : MonoBehaviour
{
    //----változók amiket el kell menteni (értékkel vagy nélkül)----
    public int minta = 0;


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

        //---------------változók amiket el kell menteni----------------
        minta = data.minta;


        //--------------------------------------------------------------
    }
}
