using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SecretMeteorLauncherScript : MonoBehaviour {

    readonly int defPrepTimeSec = 5; // 5 sec
    readonly float defWaveTimeSec = 0.3f; // 1 unit row
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

    void Start() {

        LauncherP = this.GetComponent<Transform>();

        preparationTimeMax = (int)(1 / Time.fixedDeltaTime * defPrepTimeSec);
        waveTimeMax = (int)(1 / Time.fixedDeltaTime * defWaveTimeSec);
        tableDownMax = (int)(1 / Time.fixedDeltaTime * defTableDownSec);

        meteorNumber = meteorNumberMax;

    }
    void createMeteor (float desX, float desY, float posX, float posY) {

        GameObject obj = Instantiate(meteor);

        obj.transform.parent = this.gameObject.transform;

        obj.transform.position = new Vector2(posX, posY);

        obj.GetComponent<MeteorScript>().desX = desX;
        obj.GetComponent<MeteorScript>().desY = desY;

    }

    void FixedUpdate() {

        GameObject.FindGameObjectsWithTag("c.wavecounter")[0].gameObject.GetComponent<TextMeshProUGUI>().text = "Meteors: " + meteorNumber;

        if (preparationTimeCounter == -1) { // SPAWN METEORS

            // COUNTER REFRESH

            if (waveTimeCounter > 1 && waveTimeCounter < 5) {

                meteorNumber = meteorNumberMax;

            }

            // SPAWN METEORS

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

                for (int i = 0; i < meteorNumberMax; i++) {

                    int y = Random.Range(0, rowNumber);
                    int x = Random.Range(0, colNumber);
                    //float details = (maxX*2/(colNumber-1))*Random.Range(0, colNumber);

                    while (meteorSpawns[y][x] == true) {

                        y = Random.Range(0, rowNumber);
                        x = Random.Range(0, colNumber);

                    }

                    meteorSpawns[y][x] = true;

                }

                for (int i = 0; i < rowNumber; i++) {

                    spawnTimes.Add(i * (waveTimeMax / (rowNumber - 1)));

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