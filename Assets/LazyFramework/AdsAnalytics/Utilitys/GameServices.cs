using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
namespace AdsAnalytics
{
    //Set DontDestroyOnload
    [Singleton("GameService" , true)]
    public class GameServices : Singleton<GameServices>
    {
        public FirebaseApp firebaseApp;
        private bool firebaseReady = false;
        public bool FirebaseReady => firebaseReady;
        private void Start()
        {
            InitFireBase();
        }
        void InitFireBase()
        {
            firebaseReady=false;
            FirebaseApp.CheckDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus==DependencyStatus.Available)
                {
                    // Create and hold a reference to your FirebaseApp,
                    // where app is a Firebase.FirebaseApp property of your application class.
                    firebaseApp=Firebase.FirebaseApp.DefaultInstance;
                    firebaseReady=true;
                    // Set a flag here to indicate whether Firebase is ready to use by your app.
                }
                else
                {
                    firebaseReady=false;
                    UnityEngine.Debug.LogError(System.String.Format(
                        "Could not resolve all Firebase dependencies: {0}" , dependencyStatus));
                    // Firebase Unity SDK is not safe to use here.
                }
            });
        }
    }
}

