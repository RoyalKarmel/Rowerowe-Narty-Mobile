using UnityEngine;
using Firebase.Database;

public class DatabaseManager : MonoBehaviour
{
    public AuthManager authManager;
    private string deviceID;
    private DatabaseReference dbReference;

    void Awake()
    {
        deviceID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    void Start()
    {
        authManager.IsUserLoggedIn();
    }

    #region Utils
    public DatabaseReference GetDbReference()
    {
        return dbReference;
    }

    public string GetDeviceID()
    {
        return deviceID;
    }
    #endregion
}
