using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SecretMeteorLauncherScript : MonoBehaviour {

    readonly int defPrepTimeSec = 5; // 5 sec
    readonly float defWaveTimeSec = 1f; // 1 unit row
    readonly int defTableDownSec = 3; // 4 sec

    readonly int defTableDis = 300;

    public GameObject[] MeteorObjs;
    Transform LauncherP;

    public GameObject wicPanel;
    public GameObject wicText;

    public GameObject ptSlider;

    int waveNumber = 8;

    int meteorNumberMax = 1;
    int meteorNumber;

    int preparationTimeMax;
    int preparationTimeCounter = 0;

    int waveTimeMax;
    int waveTimeCounter = 0;

    int tableDownMax;
    int tableDownCounter = -1;
    int tableState = -1;

    int[,,] waveSettings;

    List<int[]> meteors = new List<int[]>();

    readonly float maxX = 2.2f;
    readonly int colNumber = 8;
    float chanceOfSpawn = 1f;
    float atmChance = 1f;
    float atmChanceIncrease;

    int[] spawnablePlaces;

    void uploadWaveSettings() {

        // 100% chance ==> 1%

        waveSettings = new int[10, 10, 3] {

            { {150, 0, 0 }, { 10, 0, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },
            { {180, 0, 0 }, { 20, 0, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },
            { {210, 0, 0 }, { 35, 0, 0 }, { 10, 1, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },
            { {240, 0, 0 }, { 42, 0, 0 }, { 17, 1, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },
            { {1000, 0, 0 }, { 35, 200, 0 }, { 18, 201, 0 }, { 3, 202, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },
            { {300, 0, 0 }, { 45, 0, 0 }, { 25, 1, 0 }, { 7, 10, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },
            { {340, 0, 0 }, { 50, 0, 0 }, { 30, 1, 0 }, { 15, 10, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },
            { {380, 0, 0 }, { 50, 0, 0 }, { 35, 1, 0 }, { 22, 10, 0 }, { 5, 11, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },
            { {430, 0, 0 }, { 50, 0, 0 }, { 40, 1, 0 }, { 30, 10, 0 }, { 10, 11, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} },

            { {-1, 0, 0 }, { 1, 100, 0 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} }



        };

    }

    void uploadList(int index) {

        meteors.Clear();

        if (waveSettings[index, 0, 0] > 0) {

            chanceOfSpawn = (float)(waveSettings[index, 0, 0]) / 100;
            atmChanceIncrease = (float)(waveSettings[index, 0, 0]) / 1000;
            //Debug.Log("aaaaaaaaaaaaa " + atmChanceIncrease);

        }

        for (int i = 1; i < 10; i++) {

            if (waveSettings[index, i, 0] != 0) {

                for (int e = 0; e < waveSettings[index, i, 0]; e++) {

                    int[] loc = new int[2];

                    loc[0] = waveSettings[index, i, 1];
                    loc[1] = waveSettings[index, i, 2];

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

        LauncherP = this.GetComponent<Transform>();

        preparationTimeMax = (int)(1 / Time.fixedDeltaTime * defPrepTimeSec);
        waveTimeMax = (int)(1 / Time.fixedDeltaTime * defWaveTimeSec);
        tableDownMax = (int)(1 / Time.fixedDeltaTime * defTableDownSec);

        meteorNumber = meteorNumberMax;

        spawnablePlaces = new int[colNumber];
        
        for (int i = 0; i < spawnablePlaces.Length; i++) {

            spawnablePlaces[i] = -1;

        }

    }
    void createMeteor (float desX, float desY, float posX, float posY, int type, int level) {

        GameObject obj = Instantiate(MeteorObjs[Random.Range(0, 5)]);

        //obj.transform.parent = this.gameObject.transform;

        obj.transform.position = new Vector2(posX, posY);

        obj.GetComponent<MeteorScript>().desX = desX;
        obj.GetComponent<MeteorScript>().desY = desY;

        obj.GetComponent<MeteorScript>().type = type;
        obj.GetComponent<MeteorScript>().level = level;

        obj.GetComponent<MeteorScript>().set();
        
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

                    tableDownCounter = 0;
                    tableState = 0;
                    wicText.GetComponent<TextMeshProUGUI>().text = waveNumber + 1 + ". wave is coming!";

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

                if (tableDownCounter > -1) {

                    tableDownCounter++;

                    if (tableDownCounter >= tableDownMax) {

                        tableDownCounter = -1;
                        tableState = -1;

                    }

                    if (tableDownCounter == (int)Math.Round((double)(tableDownMax / 4))) { // SET TABLE IS DOWN

                        tableState = 1;

                    }
                    else if (tableDownCounter == (int)Math.Round((double)(tableDownMax / 4) * 3)) { // SET TABLE GO UP

                        tableState = 2;

                    }

                    float disPerTick = defTableDis / (int)Math.Round((double)(tableDownMax / 4));

                    switch (tableState) {

                        case 0:

                            wicPanel.transform.position = new Vector2(wicPanel.transform.position.x, wicPanel.transform.position.y - disPerTick);

                            break;

                        case 2:

                            wicPanel.transform.position = new Vector2(wicPanel.transform.position.x, wicPanel.transform.position.y + disPerTick);

                            break;

                    }

                }

            }

        }

    }

    void spawnItem() {

        this.gameObject.GetComponent<ItemManagerScript>().spawnItems();

    }

}