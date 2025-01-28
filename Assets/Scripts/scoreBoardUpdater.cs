using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scoreBoardUpdater : MonoBehaviour
{
    public TextMeshProUGUI player1_txt;
    public TextMeshProUGUI player2_txt;
    // Start is called before the first frame update
    void Start()
    {
        player1_txt.text = ScoreManager.get_player_1_score().ToString();
        player2_txt.text = ScoreManager.get_player_2_score().ToString();
    }
}
