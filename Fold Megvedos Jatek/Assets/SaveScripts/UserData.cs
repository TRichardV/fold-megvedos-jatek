using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    //---változók amiket el kell menteni (csak létrehozás, érték megadás nélkül)---
    public int minta;
    public float[] volumePct;

    //--------------------------------------------------------------------
    
    public UserData (User user)
    {

        //---------------változók amiket el kell menteni----------------
        volumePct = user.volumePct;


        //--------------------------------------------------------------
    }
}
