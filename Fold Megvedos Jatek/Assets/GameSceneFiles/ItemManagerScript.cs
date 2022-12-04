using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemManagerScript : MonoBehaviour
{
    
    // OBJECTS
    public GameObject launcher;
    public GameObject ironDomeG;
    public GameObject itemChoice;
    

    public bool choiceIsOn = false;

    int[] itemsInPool = { 1, 1, 1, 1, 1, 1, 1, 1 };
    string[] names = { "Iron Dome", "Brimstone", "Artemis", "Warmog", "Iceborn", "Knuts Hammer", "Trisagion", "20/20"};
    

    // AUXILIARY VARIABLES
    int items = 2;
    int item0 = -1;
    int item1 = -1;


    // ITEMS THAT HANDLED THERE
    int haveIronDome = 0;
    int haveBrimstone = 0;
    int haveArtemis = 0;
    int haveWarmog = 0;
    int haveIceborn = 0;


    // ONLY FOR TESTING
    private void Start() {

        //getBrimstoneItem();
        getIcebornItem();
        getKnutsItem();
        //getTrisagionItem();

        getArtemisItem();
        getWarmogItem();

    }

    // SPAWN AN ITEM ON THE SCREEN
    public void spawnItems() {

        choiceIsOn = true;
        itemChoice.active = true;

        item0 = randomItem();
        item1 = randomItem();

        itemChoice.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = names[item0];
        itemChoice.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = names[item1];

    }

    // GET A RANDOM ITEM
    int randomItem() {

        int item = -1;

        item = Random.Range(0, itemsInPool.Length);

        while (itemsInPool[item] == 0 && items > 0) {

            item = Random.Range(0, itemsInPool.Length);

        }

        if (item == -1) {

            Debug.Log("nincs több");

        }

        itemsInPool[item]--;
        items--;
        return item;

    }


    // GET AN ITEM
    public void getItem(int index) {

        int item = -1;

        Debug.Log(index);
        Debug.Log(item0 + " " + item1);

        switch(index) {

            case 0:

                item = item0;
                break;

            case 1:

                item = item1;
                break;

        }

        switch(item) {

            case 0:

                getIronDomeItem();
                break;

            case 1:

                getBrimstoneItem();
                break;

            case 2:

                getArtemisItem();
                break;

            case 3:

                getWarmogItem();
                break;

            case 4:

                getIcebornItem();
                break;

            case 5:

                getKnutsItem();
                break;

            case 6:

                getTrisagionItem();
                break;

            case 7:

                getTwentyItem();
                break;

        }

        choiceIsOn = false;
        itemChoice.active = false;

    }

    void getIronDomeItem() {

        GameObject dome = ironDomeG;

        if (haveIronDome == 0) {

            dome.active = true;

            dome.transform.GetChild(0).GetComponent<SideRocketLauncherScript>().apsPercent = 50;
            dome.transform.GetChild(1).GetComponent<SideRocketLauncherScript>().apsPercent = 50;
            dome.transform.GetChild(0).GetComponent<SideRocketLauncherScript>().damagePercent = 70;
            dome.transform.GetChild(1).GetComponent<SideRocketLauncherScript>().damagePercent = 70;

            dome.transform.GetChild(0).GetComponent<SideRocketLauncherScript>().refreshDatas();
            dome.transform.GetChild(1).GetComponent<SideRocketLauncherScript>().refreshDatas();
            haveIronDome++;

        }
        else if (haveIronDome == 1) {

            dome.transform.GetChild(0).GetComponent<SideRocketLauncherScript>().apsPercent = 80;
            dome.transform.GetChild(1).GetComponent<SideRocketLauncherScript>().apsPercent = 80;
            dome.transform.GetChild(0).GetComponent<SideRocketLauncherScript>().damagePercent = 85;
            dome.transform.GetChild(1).GetComponent<SideRocketLauncherScript>().damagePercent = 85;

            dome.transform.GetChild(0).GetComponent<SideRocketLauncherScript>().refreshDatas();
            dome.transform.GetChild(1).GetComponent<SideRocketLauncherScript>().refreshDatas();
            haveIronDome++;

        }

    }

    void getBrimstoneItem() {

        if (haveBrimstone == 0) {

            launcher.GetComponent<RocketLauncherScript>().bulletBrim = true;
            launcher.GetComponent<RocketLauncherScript>().haveBrimstone++;

            haveBrimstone++;

        }
        else if (haveBrimstone == 1) {

            launcher.GetComponent<RocketLauncherScript>().haveBrimstone++;
            haveBrimstone++;

        }

    }

    void getArtemisItem() {

        if (haveArtemis == 0) {

            GameObject.Find("Earth").GetComponent<EarthScript>().haveTankItem++;
            GameObject.Find("Earth").GetComponent<EarthScript>().getArtemis();
            haveArtemis++;

        }
        else if (haveArtemis == 1) {

            GameObject.Find("Earth").GetComponent<EarthScript>().haveTankItem++;
            GameObject.Find("Earth").GetComponent<EarthScript>().getArtemis();
            haveArtemis++;

        }

    }

    void getWarmogItem() {

        if (haveWarmog == 0) {

            GameObject.Find("Earth").GetComponent<EarthScript>().haveWarmog++;
            GameObject.Find("Earth").GetComponent<EarthScript>().getWarmog();

            haveWarmog++;
        
        }

        else if (haveWarmog == 1) {

            GameObject.Find("Earth").GetComponent<EarthScript>().haveWarmog++;
            GameObject.Find("Earth").GetComponent<EarthScript>().getWarmog();

            haveWarmog++;

        }

    }

    void getIcebornItem() {

        GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().haveIceborn++;

    }

    public void getKnutsItem() {

        GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().haveKnuts++;

    }

    public void getTrisagionItem() {

        GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().haveTrisagion++;

    }

    public void getTwentyItem() {

        GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().haveTwenty++;

    }

}
