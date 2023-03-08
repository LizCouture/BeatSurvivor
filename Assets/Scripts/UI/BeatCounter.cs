using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BeatCounter : MonoBehaviour
{
    [SerializeField] float holdNumber = .02f;
    [SerializeField] TMP_Text beatCounterTextField;

    private int lastCountFired = 0;
    private int lastIntShown = 3;

    private string[] numStrings = {
                                    "1  .  .  .",
                                    ".  2  .  .",
                                    ".  .  3  .",
                                    ".  .  .  4"
                                  };

    // Start is called before the first frame update
    void Start()
    {
        beatCounterTextField.text = numStrings[0];
    }

    // Update is called once per frame
    void Update()
    {
        float currentBeat = Conductor.Instance.SongPositionInBeats;
        if (currentBeat >= lastCountFired + 1) {
            lastCountFired++;
            if (lastIntShown > 2) {
                lastIntShown = 0;
            } else lastIntShown++;
            beatCounterTextField.text = numStrings[lastIntShown];
        }
    }
}
