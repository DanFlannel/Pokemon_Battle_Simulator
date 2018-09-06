using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AnalyticLogger : MonoBehaviour
{
    private static AnalyticLogger instance;
    public static AnalyticLogger Instance { get { return instance; } }

    private static int taskID;

    [Header("Options")]
    public bool canLog = true;

    public bool localBackup = true;
    public bool isThreadActive = false;

    [Header("Information")]
    public string host;

    public string database;
    public string tableName;
    public string userName;
    public string password;

    public ThreadState postingState;
    public System.Data.ConnectionState connectionState;
    public List<AnalyticData> analytics = new List<AnalyticData>();

    private Task postTask = new Task(() => { });
    private MySqlConnection connection;

    //.. Unity Methods

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(instance);
                instance = this;
            }
        }
    }

    private void Start()
    {
        //OpenConnection();
    }

    private void Update()
    {
        updateStates();
        checkCache();
    }

    private void updateStates()
    {
        if (connection != null)
        {
            connectionState = connection.State;
        }
    }

    private void checkCache()
    {
        for (int i = analytics.Count - 1; i >= 0; i--)
        {
            if (analytics[i].sucess && !analytics[i].isPosting)
            {
                analytics.Remove(analytics[i]);
                continue;
            }

            if (!isThreadActive && !analytics[i].isPosting)
            {
                isThreadActive = true;
                analytics[i].isPosting = true;
                int id = i;

                postTask = new Task(() =>
                {
                    tryPushAnlalytic(id);
                    Task.Delay(5000);
                    analytics.FindByID(id).isPosting = false;
                    isThreadActive = false;
                });
                postTask.Start();
            }
        }
    }

    //.. Logger Calls

    public static void Log(string query, string eventName, string eventValue)
    {
        instance.analytics.Add(new AnalyticData(taskID, query, eventName, eventValue));
        taskID++;
    }

    private async void tryPushAnlalytic(int id)
    {
        AnalyticData data = analytics.FindByID(id);

        bool connected = await OpenConnection();

        //Debug.Log("Sucessfully checked connection");
        if (canLog && connected)
        {
            data.sucess = PushCallToDb(data);
            //Debug.Log("tried to post");
        }

        if (localBackup && !data.backedUp)
        {
            data.backedUp = true;
            string post = string.Format("Database,{0},{1}", data.eventValue, data.eventName);
            CSVSaver.postLocally(post);
        }
        analytics.replaceByID(id, data);
    }

    public bool PushCallToDb(AnalyticData data)
    {
        if (InternetReachabilityVerifier.Instance.status != InternetReachabilityVerifier.Status.NetVerified) { return false; }
        MySqlCommand cmd;
        try
        {
            string sql = string.Format("INSERT INTO {0}{1}", tableName, data.eventQuery);
            //Debug.Log(sql);
            cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("?eventName", data.eventName);
            cmd.Parameters.AddWithValue("?eventValue", data.eventValue);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (MySqlException ex)
        {
            Debug.Log("Problem pushing analytics: " + ex);
            return false;
        }
    }

    //.. Connection Handlers

    public async Task<bool> OpenConnection()
    {
        if (InternetReachabilityVerifier.Instance.status != InternetReachabilityVerifier.Status.NetVerified) { return false; }
        if (connection != null)
        {
            if (connection.State != System.Data.ConnectionState.Open && connection.State != System.Data.ConnectionState.Connecting)
            {
                CloseConnection();
                bool conn = await TryConnect();
                return conn;
            }
            else
            {
                return true;
            }
        }
        else
        {
            bool conn = await TryConnect();
            return conn;
        }
    }

    public async Task<bool> TryConnect()
    {
        try
        {
            string connectionString = "SERVER=" + host + ";" + "DATABASE=" + database + ";" + "UID=" + userName + ";" + "PASSWORD=" + password + ";";
            //string connectionString = string.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};", host, tableName, userName, password);
            connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();
            return connection.State == System.Data.ConnectionState.Open;
        }
        catch (Exception ex)
        {
            Debug.Log(string.Format("Error opening SQL Connection: {0}", ex.Message));
            return false;
        }
    }

    public void CloseConnection()
    {
        if (connection.State != System.Data.ConnectionState.Closed)
        {
            connection.Close();
        }
    }

    private void OnApplicationQuit()
    {
        CloseConnection();
    }
}