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
    public Sprite[] images;


    public bool choiceIsOn = false;

    int[] itemsInPool = { 1, 1, 1, 1, 1, 1, 1, 1 };
    string[] names = { "Iron Dome", "Brimstone", "Artemis", "Warmog", "Iceborn", "Knuts Hammer", "Trisagion", "20/20", "Damage Up!", "APS Up!"};
    

    // AUXILIARY VARIABLES
    int items = 8;
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

        getIronDomeItem();

    }

    // SPAWN AN ITEM ON THE SCREEN
    public void spawnItems() {

        choiceIsOn = true;
        itemChoice.active = true;

        if (items > 1) {

            item0 = randomItem();
            item1 = randomItem();

        }
        else {

            item0 = 8;
            item1 = 9;

        }



        itemChoice.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = names[item0];
        itemChoice.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = names[item1];
        GameObject.Find("Item0").transform.GetChild(0).GetComponent<Image>().sprite = images[item0];
        GameObject.Find("Item1").transform.GetChild(0).GetComponent<Image>().sprite = images[item1];
        GameObject.Find("Item0").transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 180, 0, 240);
        GameObject.Find("Item0").GetComponent<Image>().color = new Color32(255, 255, 255, 35);
        GameObject.Find("Item1").transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 180, 0, 240);
        GameObject.Find("Item1").GetComponent<Image>().color = new Color32(255, 255, 255, 35);
    }

    // GET A RANDOM ITEM
    int randomItem() {

        int item = -1;

        int item4 = Random.Range(0, itemsInPool.Length);

        while (itemsInPool[item4] < 1 && items > 0) {

            item4 = Random.Range(0, itemsInPool.Length);

        }
        if (itemsInPool[item4] > 0) {

            item = item4;

        }

        if (item == -1) {

            Debug.Log("nincs több");

        }

        itemsInPool[item]--;
        items--;
        //Debug.Log(items + " " + itemsInPool[0] + " " + itemsInPool[1] + " " + itemsInPool[2] + " " + itemsInPool[3] + " " + itemsInPool[4] + " " + itemsInPool[5] + " " + itemsInPool[6] + " " + itemsInPool[7]);
        return item;

    }


    // GET AN ITEM
    public void getItem(int index) {

        int item = -1;

        switch(index) {

            case 0:

                item = item0;
                break;

            case 1:

                item = item1;
                break;

        }

        if (items < 2) {

            getAnItem(item);

        }

        getAnItem(item);

        choiceIsOn = false;
        itemChoice.active = false;

        GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().setStats();

    }

    void getAnItem(int item) {

        switch (item) {

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

            case 8:

                GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().getDamageUp();
                break;

            case 9:

                GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().getAPSUp();
                break;

        }

    }

    void getIronDomeItem() {

        GameObject dome = ironDomeG;

        if (haveIronDome == 0) {

            dome.active = true;

            GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().haveIronDome++;

            GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().getDome();
            GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().ironDomeRefresh();

            haveIronDome++;

        }
        else if (haveIronDome == 1) {

            GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().haveIronDome++;

            GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().getDome();
            GameObject.Find("RocketLauncher").GetComponent<RocketLauncherScript>().ironDomeRefresh();

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
