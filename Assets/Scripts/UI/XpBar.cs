using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XpBar : MonoBehaviour
{
    [SerializeField] TMP_Text xpLabel;
    [SerializeField] TMP_Text levelLabel;

    private Player player;

    private void Start() {
        player = Player.Instance;

        player.playerGotXP.AddListener(updateXP);
    }

    private void updateXP() {
        xpLabel.text = player.CurrentXP + " xp";
        levelLabel.text = "Level " + player.CurrentLevel;
    }
}
