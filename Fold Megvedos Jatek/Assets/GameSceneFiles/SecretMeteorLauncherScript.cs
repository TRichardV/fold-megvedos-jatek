using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SecretMeteorLauncherScript : MonoBehaviour {

    // GAMEOBJECTS
    public GameObject wicPanel;
    public GameObject wicText;

    public GameObject ptSlider;


    // STATS
    readonly int defPrepTimeSec = 5; // 5 sec
    readonly float defWaveTimeSec = 1f; // 1 unit row
    readonly int defTableDownSec = 3; // 4 sec

    readonly int defTableDis = 300;

    readonly float maxX = 2.2f;
    readonly int colNumber = 8;


    // WAVE NUMBER
    int waveNumber = 9;


    // WAVES + METEORS
    public GameObject[] MeteorObjs;

    int[,,] waveSettings;

    int[] spawnablePlaces;

    List<int[]> meteors = new List<int[]>();


    // COUNTERS AND STATES
    int preparationTimeMax;
    int preparationTimeCounter = 0;

    int waveTimeMax;
    int waveTimeCounter = 0;


    // AUXILIARY VARIABLES
    float chanceOfSpawn = 1f;
    float atmChance = 1f;
    float atmChanceIncrease;

    int meteorNumberMax = 1;
    int meteorNumber;


    void uploadWaveSettings() {

        waveSettings = new int[20, 10, 3] {

            { {140, 0, 0 }, { 10, 0, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },
            { {160, 0, 0 }, { 20, 0, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },
            { {185, 0, 0 }, { 35, 0, 0 }, { 7, 1, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },
            { {210, 0, 0 }, { 42, 0, 0 }, { 15, 1, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },
            { {1000, 0, 0 }, { 35, 200, 0 }, { 18, 201, 0 }, { 3, 202, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },
            { {270, 0, 0 }, { 35, 0, 0 }, { 25, 1, 0 }, { 10, 10, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },
            { {315, 0, 0 }, { 35, 0, 0 }, { 30, 1, 0 }, { 17, 10, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },
            { {350, 0, 0 }, { 30, 0, 0 }, { 30, 1, 0 }, { 24, 10, 0 }, { 5, 11, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },
            { {400, 0, 0 }, { 30, 0, 0 }, { 30, 1, 0 }, { 30, 10, 0 }, { 13, 11, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },

            { {-1, 0, 0 }, { 1, 100, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },


            { {400, 0, 0 }, { 30, 0, 1 }, { 30, 1, 1 }, { 30, 10, 1 }, { 13, 11, 1 }, { 5, 3, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },
            { {430, 0, 0 }, { 30, 0, 1 }, { 30, 1, 1 }, { 30, 10, 1 }, { 13, 11, 1 }, { 10, 3, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },
            { {460, 0, 0 }, { 30, 0, 1 }, { 30, 1, 1 }, { 30, 10, 1 }, { 10, 11, 1 }, { 17, 3, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },
            { {500, 0, 0 }, { 20, 0, 1 }, { 20, 1, 1 }, { 35, 10, 1 }, { 10, 11, 1 }, { 25, 3, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },
            { {1000, 0, 0 }, { 35, 200, 1 }, { 18, 201, 1 }, { 3, 202, 1 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },
            { {515, 0, 0 }, { 20, 0, 1 }, { 20, 1, 1 }, { 35, 10, 1 }, { 10, 11, 1 }, { 25, 3, 0 }, { 5, 5, 0 }, {0,0,0}, {0,0,0}, {0,0,0} },
            { {530, 0, 0 }, { 20, 0, 1 }, { 20, 1, 1 }, { 35, 10, 1 }, { 10, 11, 1 }, { 25, 3, 0 }, { 10, 5, 0 }, {0,0,0}, {0,0,0}, {0,0,0} },
            { {545, 0, 0 }, { 20, 0, 1 }, { 20, 1, 1 }, { 35, 10, 1 }, { 10, 11, 1 }, { 25, 3, 0 }, { 17, 5, 0 }, {0,0,0}, {0,0,0}, {0,0,0} },
            { {560, 0, 0 }, { 20, 0, 1 }, { 20, 1, 1 }, { 35, 10, 1 }, { 10, 11, 1 }, { 25, 3, 0 }, { 25, 5, 0 }, {0,0,0}, {0,0,0}, {0,0,0} },

            { {-1, 0, 0 }, { 1, 100, 1 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },

        };

            // 3
            // 5

            // 2
            // 4
            // 12
            // 303
    }

    void uploadList(int index) {

        meteors.Clear();

        if (waveSettings[index, 0, 0] > 0) {

            chanceOfSpawn = (float)(waveSettings[index, 0, 0]) / 100;
            atmChanceIncrease = (float)(waveSettings[index, 0, 0]) / 1000;

        }

        for (int i = 1; i < 10; i++) {

            if (waveSettings[index, i, 0] != 0) {

                for (int e = 0; e < waveSettings[index, i, 0]; e++) {

                    int[] loc = new int[2];

                    loc[0] = waveSettings[index, i, 1];
                    loc[1] = waveSettings[index, i, 2];
                    Debug.Log("ads: " + loc[1]);
                    meteors.Add(loc);

                }

            }

        }
        shufle(); // KEVERES

    }

    void shufle() {

        List<int[]> meteors2 = new List<int[]>();

        for (int i = 0; i < meteors.Count; i++) {

            meteors2.Add(meteors[i]);

        }

        meteors.Clear();
        List<int> indexes = new List<int>();

        for (int i = 0; i < meteors2.Count; i++) {
            
            int rNum = Random.Range(0, meteors2.Count);

            while (indexes.Contains(rNum)) {

                rNum = Random.Range(0, meteors2.Count);

            }

            meteors.Add(meteors2[rNum]);

            indexes.Add(rNum);

        }

    }

    void Start() {

        uploadWaveSettings();

        preparationTimeMax = (int)(1 / Time.fixedDeltaTime * defPrepTimeSec);
        waveTimeMax = (int)(1 / Time.fixedDeltaTime * defWaveTimeSec);

        meteorNumber = meteorNumberMax;

        spawnablePlaces = new int[colNumber];
        
        for (int i = 0; i < spawnablePlaces.Length; i++) {

            spawnablePlaces[i] = -1;

        }

    }
    void setComponents(GameObject gameobj)
    {
        Rigidbody2D rb2 = gameobj.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        //BoxCollider2D bc2 = gameobj.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        MeteorScript ms = gameobj.AddComponent(typeof(MeteorScript)) as MeteorScript;

        rb2.gravityScale = 0;
        //bc2.isTrigger = true;

        gameobj.tag = "meteor";
    }

    public GameObject createMeteor (float desX, float desY, float posX, float posY, int type, int level) {

        GameObject obj = null;
        switch (type)
        {
            case 0: // NORMAL METEOR
            case 1: // NORMAL METEOR t2
            case 2: // NORMAL METEOR t3
            case 3: // A MOVING METEOR (TO RIGHT AND LEFT)
            case 4: // BIG METEOR
            case 5: // METEOR WAVE TO THE RIGHT (OR LEFT)

                obj = Instantiate(MeteorObjs[Random.Range(0, 5)]);
                break;

            case 10: // REGULAR SPACESHIP

                obj = Instantiate(MeteorObjs[5]);
                break;

            case 11: // SHOOTING SPACESHIP

                obj = Instantiate(MeteorObjs[7]);
                break;

            case 12: // MOVING AND SHOOTING SPACESHIP

                obj = Instantiate(MeteorObjs[6]);
                break;

            case 110: // BOSS'S SPACESHIP

                obj = Instantiate(MeteorObjs[7]);
                break;

            case 90: // SPACESHIP BULLET 1

                obj = Instantiate(MeteorObjs[17]);
                break;

            case 100: // BOSS 1

                obj = Instantiate(MeteorObjs[Random.Range(13, 17)]);
                break;

            case 120: // BOSS'S BULLET

                obj = Instantiate(MeteorObjs[18]);
                break;

            case 200: // COIN t1

                obj = Instantiate(MeteorObjs[19]);
                break;

            case 201: // COIN t2

                obj = Instantiate(MeteorObjs[20]);
                break;

            case 202: // COIN t3

                obj = Instantiate(MeteorObjs[21]);
                break;

            case 300: // SATELITE

                obj = Instantiate(MeteorObjs[22]);
                break;
        }

        setComponents(obj);

        //obj.transform.parent = this.gameObject.transform;

        obj.transform.position = new Vector2(posX, posY);

        obj.GetComponent<MeteorScript>().desX = desX;
        obj.GetComponent<MeteorScript>().desY = desY;

        obj.GetComponent<MeteorScript>().type = type;
        obj.GetComponent<MeteorScript>().level = level;

        obj.GetComponent<MeteorScript>().set();

        return obj;
        
    }

    public bool haveChance() {

        int num = Random.Range(0, 10000);

        //Debug.Log(atmChance * chanceOfSpawn);
        if (num < chanceOfSpawn*atmChance) {

            atmChance = 1f;
            return true;

        }
        else {

            atmChance = atmChance + (atmChance * (atmChanceIncrease/100));
            return false;

        }

    }

    public void minusMeteor() {

        meteorNumber--;

    }

    void FixedUpdate() {

        GameObject.FindGameObjectsWithTag("c.wavecounter")[0].gameObject.GetComponent<TextMeshProUGUI>().text = "Meteors: " + meteorNumber;

        if (preparationTimeCounter == -1) { // SPAWN METEORS

            // SPAWN METEORS

            for (int i = 0; i < colNumber; i++) {

                if (spawnablePlaces[i] == waveTimeMax) {

                    spawnablePlaces[i] = -1;

                }

                if (spawnablePlaces[i] > -1 && spawnablePlaces[i] < waveTimeMax) {

                    spawnablePlaces[i]++;

                }

            }

            if (meteors.Count > 0) {

                for (int i = 0; i < colNumber; i++) {

                    if (meteors.Count > 0 && waveSettings[waveNumber, 0, 0] == -1) {

                        createMeteor(0, -50f, 0, this.gameObject.transform.position.y, meteors[0][0], meteors[0][1]);
                        meteors.RemoveAt(0); // actually useless

                    }

                    else if (meteors.Count > 0 && spawnablePlaces[i] == -1 && haveChance()) {

                        createMeteor(i * ((maxX * 2) / (colNumber - 1)) - maxX, -50f, i * ((maxX * 2) / (colNumber - 1)) - maxX, this.gameObject.transform.position.y, meteors[0][0], meteors[0][1]);
                        
                        meteors.RemoveAt(0);
                        spawnablePlaces[i] = 0;

                    }

                }

            }

            // USW

            waveTimeCounter++;

            if (waveTimeCounter > waveTimeMax && meteors.Count == 0 && meteorNumber <= 0) {

                preparationTimeCounter++;
                ptSlider.SetActive(true);
                waveTimeCounter = 0;

                waveNumber++;
                if (waveNumber % 10 == 0) {

                    spawnItem();

                }

            }
            else if (waveTimeCounter > waveTimeMax) {

                waveTimeCounter = 1;

            }

        }
        else { // PREP PHASE

            if (this.gameObject.GetComponent<ItemManagerScript>().choiceIsOn == false) {

                double sliderScaleUnit = 1d / preparationTimeMax;
                double value = sliderScaleUnit * preparationTimeCounter;
                ptSlider.GetComponent<Slider>().value = (float)value;

                if (preparationTimeCounter == 0) {

                    wicText.GetComponent<TextMeshProUGUI>().text = waveNumber + 1 + ". Wave is coming!";
                    GameObject.Find("WaveIsComingPanel").GetComponent<Animator>().Play("WaweIsComing", 0, 0);

                    for (int i = 0; i < spawnablePlaces.Length; i++) {

                        spawnablePlaces[i] = -1;

                    }

                    uploadList(waveNumber);

                    meteorNumberMax = meteors.Count;
                    meteorNumber = meteorNumberMax;

                }

                preparationTimeCounter++;

                if (preparationTimeCounter >= preparationTimeMax) {

                    preparationTimeCounter = -1;

                    ptSlider.SetActive(false);

                }

            }

        }

    }

    void spawnItem() {

        this.gameObject.GetComponent<ItemManagerScript>().spawnItems();

    }

}