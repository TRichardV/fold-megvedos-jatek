using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    //---v?ltoz?k amiket el kell menteni (csak l?trehoz?s, ?rt?k megad?s n?lk?l)---
    public int minta;
    public float[] volumePct;
    public float highscore;

    //--------------------------------------------------------------------
    
    public UserData (User user)
    {

        //---------------v?ltoz?k amiket el kell menteni----------------
        volumePct = user.volumePct;
        highscore = user.highscore;


        //--------------------------------------------------------------
    }
}
