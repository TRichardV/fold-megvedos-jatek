using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SecretMeteorLauncherScript : MonoBehaviour {

    readonly int defPrepTimeSec = 5; // 5 sec
    readonly int defWaveTimeSec = 5; // 5 sec

    public GameObject meteor;
    Transform LauncherP;

    int waveNumber = 0;

    int meteorNumberMax = 50;

    int preparationTimeMax;
    int preparationTimeCounter = 0;

    int waveTimeMax;
    int waveTimeCounter = 0;

    List<bool[]> meteorSpawns = new List<bool[]>();
    List<int> spawnTimes = new List<int>();
    int spawnTimeIndex = 0;

    readonly float maxX = 2.2f;
    readonly int colNumber = 8;
    int rowNumber;

    void Start() {

        LauncherP = this.GetComponent<Transform>();

        preparationTimeMax = (int)(1 / Time.fixedDeltaTime * defPrepTimeSec);
        waveTimeMax = (int)(1 / Time.fixedDeltaTime * defWaveTimeSec);

        rowNumber = 7 * defWaveTimeSec;

    }
    void createMeteor (float desX, float desY, float posX, float posY) {

        GameObject obj = Instantiate(meteor);

        obj.transform.parent = this.gameObject.transform;

        obj.transform.position = new Vector2(posX, posY);

        obj.GetComponent<MeteorScript>().desX = desX;
        obj.GetComponent<MeteorScript>().desY = desY;

    }

    void FixedUpdate() {

        GameObject.FindGameObjectsWithTag("c.wavecounter")[0].gameObject.GetComponent<TextMeshProUGUI>().text = "Wave: " + waveNumber + "\n" + waveTimeCounter + "\n" + preparationTimeCounter;

        if (preparationTimeCounter == -1) { // SPAWN METEORS

            if (spawnTimeIndex < rowNumber && waveTimeCounter == spawnTimes[spawnTimeIndex]) {

                Debug.Log(waveTimeCounter + " asd " + spawnTimes[spawnTimeIndex] + " asd " + spawnTimeIndex);
                for (int i = 0; i < colNumber; i++) {

                    if (meteorSpawns[spawnTimeIndex][i] == true) {

                        createMeteor((maxX * 2 / (colNumber - 1)) * i - maxX, 0, (maxX * 2 / (colNumber - 1)) * i - maxX, LauncherP.position.y);

                    }


                }

                spawnTimeIndex++;

            }

            waveTimeCounter++;

            if (waveTimeCounter > waveTimeMax) {

                preparationTimeCounter++;
                waveTimeCounter = 0;

                waveNumber++;

            }

        }
        else { // PREP PHASE

            if (preparationTimeCounter == 0) {

                spawnTimeIndex = 0;
                meteorSpawns.Clear();

                //meteorNumberMax = (int) Math.Round(meteorNumberMax * 1.5f);
                //waveTimeMax = (int) Math.Round(waveTimeMax * 1.3f);

                for (int i = 0; i < rowNumber; i++) {

                    bool[] asd = new bool[colNumber];
                    meteorSpawns.Add(asd);

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

            }

        }

    }

}