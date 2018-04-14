using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager {

    public static int playerAScore = 0;
    public static int playerBScore = 0;

    public static void ResetScore() {
        playerAScore = 0;
        playerBScore = 0;
    }
}
