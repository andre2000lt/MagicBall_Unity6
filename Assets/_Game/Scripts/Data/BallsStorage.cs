using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;



public static class BallsStorage
{
    private const string FILENAME = "userBalls";
    private const string CURRENT_BALL_FILENAME = "currentBallName";

    private static IStorage<List<BallData>> _ballsStorage;
    private static IStorage<CurrentBall> _currentBallStorage;
    

    public static void Init(StorageProviderSO storageProvider)
    {
        _ballsStorage = storageProvider.GetStorage<List<BallData>>(FILENAME);
        _currentBallStorage = storageProvider.GetStorage<CurrentBall>(CURRENT_BALL_FILENAME);


        if (_currentBallStorage.IsStorageEmpty(CURRENT_BALL_FILENAME))
        {
            var allBalls = GetAllBalls();
            var ballName = allBalls[0].Name;
            SetCurrentBallName(ballName);
        }
    }


    public static List<BallData> GetAllBalls() 
    {
        string defaultBallsJSON = Resources.Load<TextAsset>("JSON/defaultBalls").ToString();
        var defaultBalls = JsonConvert.DeserializeObject<List<BallData>>(defaultBallsJSON);
        var userBalls = GetUserBalls();

        // Добавление пользовательских шаров к дефолтным
        foreach ( var item in userBalls )
        {
            defaultBalls.Add(item);
        }

        return defaultBalls;
    }
    
    
    public static List<BallData> GetUserBalls() 
    { 
        return _ballsStorage.Load();
    }


    /// <summary>
    /// Add ball (or Edit if ball exists)
    /// </summary>
    public static void AddBall(BallData ball)
    {
        var userBalls = GetUserBalls();

        if (ContainsName(ball.Name) == false)
        {

            userBalls.Add(ball);
            SaveBalls(userBalls);
        }
        else
        {
            var selectedBall = userBalls.Find(cBall => cBall.Name == ball.Name);
            selectedBall.SetBallData(ball.Name, ball.Material, ball.Answers);
            SaveBalls(userBalls);
        }

        SetCurrentBallName(ball.Name);
    }



    public static void RemoveCurrentBall()
    {
        string ballName = GetCurrentBallName();
        RemoveBall(ballName);
    }


    public static void SetCurrentBallName(string ballName)
    {
        CurrentBall currenBall = new CurrentBall(ballName);
        _currentBallStorage.Save(currenBall);
        GamePlayEventBus.CurrentBallChanged_Event?.Invoke();
    }


    public static BallData GetCurrentBall()
    {
        string currentballName = GetCurrentBallName();
        var allBalls = GetAllBalls();

        return allBalls.Find(ball => ball.Name == currentballName);
    }


    public static List<string> GetAllNames()
    {
        var ballNames = new List<string>();
        var allBalls = GetAllBalls();

        foreach (var ball in allBalls)
        {
            ballNames.Add(ball.Name);
        }

        return ballNames;
    }


    public static bool IsBallAvailable(string ballName)
    {
        foreach (string currentBallName in GetAllNames())
        {
            if (ballName == currentBallName) { return true; }
        }

        return false;
    }


    public static BallData GetBallDataByName(string ballName)
    {
        var allBalls = GetAllBalls();
        return allBalls.Find(ball => ball.Name == ballName);
    }



    // private
    private static void SaveBalls(List<BallData> balls)
    {
        _ballsStorage.Save(balls);
    }


    public static string GetCurrentBallName()
    {
        
        CurrentBall currentBall = _currentBallStorage.Load();

        return currentBall.Name;
    }


    private static void RemoveBall(string name) {
        if (ContainsName(name) == true) 
        {
            var allBalls = GetAllBalls();

            int ballIndex = allBalls.FindIndex(ball => ball.Name == name);
            allBalls.RemoveAt(ballIndex);

            SaveBalls(allBalls); 

            if (name == GetCurrentBallName() )
            {
                SetCurrentBallName(allBalls[0].Name);
            }
        }  
    }
    
    
    private static bool ContainsName(string name) {
        var allBalls = GetAllBalls();
        return allBalls.Exists(ball => ball.Name == name);
    }
}



// Classes
public class CurrentBall
{
    public string Name { get; set; }

    public CurrentBall ()
    {

    }  
    
    
    public CurrentBall (string name)
    {
        Name = name;      
    }
}
