using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class InternalAnalytics : MonoBehaviour
{
    private static InternalAnalytics instance;
    public static InternalAnalytics Instance { get { return instance; } }

    private static int taskID;

    [Header("Options")]
    public bool canLog = true;

    public bool localBackup = true;
    public bool isThreadActive = false;

    [Header("Information")]
    public string host = "dfc.c460kovmobkz.us-east-2.rds.amazonaws.com";

    public int port = 3306;
    public string database = "Internal";
    public string tableName = "Default";
    public string userName = "dfc";
    public string password = "Re2M&ykkiO8YK6";

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
        SecurityCertificates.AssignTrue();
        SecurityCertificates.EnablePolicies();

        OpenConnection();
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

    private void tryPushAnlalytic(int id)
    {
        AnalyticData data = analytics.FindByID(id);

        OpenConnection();
        //Debug.Log("Sucessfully checked connection");
        if (canLog)
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

    public void OpenConnection()
    {
        if (connection != null)
        {
            if (connection.State != System.Data.ConnectionState.Open && connection.State != System.Data.ConnectionState.Connecting)
            {
                CloseConnection();
                OpenConnection();
            }
            else
            {
                return;
            }
        }
        Task t = new Task(() =>
        {
            string connectionString = "SERVER=" + host + ";" + "PORT=" + port + ";" + "DATABASE=" + database + ";" + "UID=" + userName + ";" + "PASSWORD=" + password + ";" + "SslMode=None;";
            try
            {
                //string connectionString = string.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};", host, tableName, userName, password);
                connection = new MySqlConnection(connectionString);
                connection.Open();
            }
            catch (Exception ex)
            {
                Debug.LogWarning(string.Format("Error opening Internal SQL Connection: {0}", ex.Message));
                if (ex.InnerException != null)
                {
                    Debug.LogWarning(ex.InnerException.Message);
                    Debug.LogWarning(ex.InnerException.InnerException.Message);
                }
            }
        });
        t.Start();
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