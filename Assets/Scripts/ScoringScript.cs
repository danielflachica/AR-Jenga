using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringScript
{
    public int score;
    private string name;

    public ScoringScript()
    {
        score = 0;
    }

    public void Score()
    {
        score += 100;
    }

    public int getScore()
    {
        return score;
    }
    
}
