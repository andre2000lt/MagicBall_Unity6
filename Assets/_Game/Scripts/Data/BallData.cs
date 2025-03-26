using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallData
{
    public string Name;
    public string Material;
    public List<string> Answers;
    public BallType @BallType = BallType.UserBall;


    public BallData()
    {

    }


    public BallData(string name, string material, List<string> answers)
    {
        Name = name;
        Material = material;
        Answers = answers;
    }
    
    public void  SetBallData(string name, string material, List<string> answers)
    {
        Name = name;
        Material = material;
        Answers = answers;
    }   
}


public enum BallType
{
    UserBall,
    DefaultBall
}
