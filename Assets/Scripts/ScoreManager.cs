using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scores the games score
public static class ScoreManager
{
    public static int player_1_score = 0;
    public static int player_2_score = 0;

    public static void player_1_win()
    {
        player_1_score += 1;
    }

    public static int get_player_1_score()
    {
        return player_1_score;
    }

    public static void player_2_win()
    {
        player_2_score += 1;
    }

    public static int get_player_2_score()
    {
        return player_2_score;
    }
}
