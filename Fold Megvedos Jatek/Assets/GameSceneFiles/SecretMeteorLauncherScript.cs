using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SecretMeteorLauncherScript : MonoBehaviour {

    readonly int defPrepTimeSec = 5; // 5 sec
    readonly float defWaveTimeSec = 0.1f; // 1 unit row
    readonly int defTableDownSec = 3; // 4 sec

    readonly int defTableDis = 300;

    public GameObject meteor;
    Transform LauncherP;

    public GameObject wicPanel;
    public GameObject wicText;

    public GameObject ptSlider;

    int waveNumber = 1;

    int meteorNumberMax = 10;
    public int meteorNumber;

    int preparationTimeMax;
    int preparationTimeCounter = 0;

    int waveTimeMax;
    int waveTimeCounter = 0;

    int tableDownMax;
    int tableDownCounter = -1;
    int tableState = -1;

    List<int> meteors = new List<int>();

    readonly float maxX = 2.2f;
    readonly int colNumber = 8;
    int chanceOfSpawn = 10;
    readonly int secondChance = 1;

    int[] spawnablePlaces;

    void Start() {

        LauncherP = this.GetComponent<Transform>();

        preparationTimeMax = (int)(1 / Time.fixedDeltaTime * defPrepTimeSec);
        waveTimeMax = (int)(1 / Time.fixedDeltaTime * defWaveTimeSec);
        tableDownMax = (int)(1 / Time.fixedDeltaTime * defTableDownSec);

        meteorNumber = meteorNumberMax;

        spawnablePlaces = new int[colNumber];
        
        for (int i = 0; i < spawnablePlaces.Length; i++) {

            spawnablePlaces[i] = -1;

        }

        Debug.Log(waveTimeMax);
        

    }
    void createMeteor (float desX, float desY, float posX, float posY) {

        GameObject obj = Instantiate(meteor);

        obj.transform.parent = this.gameObject.transform;

        obj.transform.position = new Vector2(posX, posY);

        obj.GetComponent<MeteorScript>().desX = desX;
        obj.GetComponent<MeteorScript>().desY = desY;

    }

    public bool haveChance() {

        int num = Random.Range(0, 10000);

        return num < chanceOfSpawn*secondChance;

    }

    void FixedUpdate() {

        GameObject.FindGameObjectsWithTag("c.wavecounter")[0].gameObject.GetComponent<TextMeshProUGUI>().text = "Meteors: " + meteorNumber;

        if (preparationTimeCounter == -1) { // SPAWN METEORS

            // COUNTER REFRESH

            if (waveTimeCounter > 1 && waveTimeCounter < 5) {

                meteorNumber = meteorNumberMax;

            }

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

                    if (meteors.Count > 0 && spawnablePlaces[i] == -1 && haveChance()) {

                        createMeteor(i * ((maxX * 2) / (colNumber - 1)) - maxX, 0, i * ((maxX * 2) / (colNumber - 1)) - maxX, this.gameObject.transform.position.y);
                        meteors.RemoveAt(0);
                        spawnablePlaces[i] = 0;

                    }

                }

            }

            // USW

            waveTimeCounter++;

            if (waveTimeCounter > waveTimeMax && meteors.Count == 0) {

                preparationTimeCounter++;
                ptSlider.SetActive(true);
                waveTimeCounter = 0;

                waveNumber++;

            }
            else if (waveTimeCounter > waveTimeMax) {

                waveTimeCounter = 1;

            }

        }
        else { // PREP PHASE

            double sliderScaleUnit = 1d / preparationTimeMax;
            double value = sliderScaleUnit * preparationTimeCounter;
            ptSlider.GetComponent<Slider>().value = (float)value;

            if (preparationTimeCounter == 0) {

                tableDownCounter = 0;
                tableState = 0;
                wicText.GetComponent<TextMeshProUGUI>().text = waveNumber + ". wave is coming!";

                for (int i = 0; i < meteorNumberMax; i++) {

                    meteors.Add(0);

                }

                for (int i = 0; i < spawnablePlaces.Length; i++) {

                    spawnablePlaces[i] = -1;

                }

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

                if (tableDownCounter == (int)Math.Round((double)(tableDownMax/4))) { // SET TABLE IS DOWN

                    tableState = 1;

                }
                else if (tableDownCounter == (int)Math.Round((double)(tableDownMax / 4)*3)) { // SET TABLE GO UP

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