﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Comidat.Data.Model;
using Comidat.Diagnostics;
using Comidat.IO;
using UnityEngine;


public class DataContext : MonoBehaviour
{
    public static DataContext Instance;

    //private string _conString = @"Server=sql.lc;Database=ComidatOld;User ID=SA;Password=Umut1996;";
    //private string _conString = @"Server=.;Database=MinePts;User ID=sa;Password=comidat;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;";
    private SqlConnection _dbCon;
    private static Dictionary<string, string> _settings;
    //TODO:Unutma !!!!!! 01-08-2018->636686784000000000 01-08-2100->662563584000000000
    private const long ExpireTime = 636686784000000000;
    void Awake()
    {
        Comidat.Diagnostics.Logger.Archive = Path.Combine(Environment.CurrentDirectory, "Logs");
        Comidat.Diagnostics.Logger.LogFile = Path.Combine(Path.Combine(Environment.CurrentDirectory, "Logs"), "Comidat.Viewer.3D.log");
        ExceptionHandler.InstallExceptionHandler();

        _settings = new Dictionary<string, string>();
        foreach (var setting in new FileReader(Path.Combine(Environment.CurrentDirectory, "settings.ini")))
        {
            var a = setting.Value.Split(new[] { '=' }, 2);
            _settings.Add(a[0].Trim().ToLowerInvariant(), a[1].Trim());
        }

        if ((DateTime.FromBinary(ExpireTime) - DateTime.Now).Days < 0)
            throw new ApplicationException("Application Crash Error Code:010818");

        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        _dbCon = new SqlConnection(_settings["database"]);
    }

    public SqlDataReader Query(string query)
    {
        return Query(new SqlCommand(query));
    }

    public SqlDataReader Query(SqlCommand query)
    {
        if (_dbCon.State != ConnectionState.Open)
            _dbCon.Open();
        SqlDataReader sqr = null;
        try
        {
            query.Connection = _dbCon;
            sqr = query.ExecuteReader();
        }
        catch (SqlException exception)
        {
            Debug.LogWarning(exception.ToString());
        }
        finally
        {
            query.Dispose();
        }
        return sqr;
    }

    public IEnumerable<TagWithLocation> GetTagsLocation()
    {
        var data = Query("SELECT c.TagFirstName, x.XPosition, x.YPosition, x.ZPosition, x.TagId, x.MapId, x.RecordDateTime, c.TagDescription, c.TagFullName, c.TagLastName, c.TagTCNo, c.TagMobilTelephone FROM TBLTags c WITH(NOLOCK) OUTER APPLY(SELECT TOP 1 * FROM TBLPositions r WITH(NOLOCK) WHERE r.TagId = c.Id ORDER BY r.RecordDateTime DESC) x WHERE x.XPosition IS NOT NULL");
        while (data.Read())
        {
            yield return new TagWithLocation
            {
                tag = new TBLTag
                {
                    TagDescription = data.IsDBNull(7) ? "" : data.GetString(7),
                    TagFirstName = data.IsDBNull(0) ? "" : data.GetString(0),
                    TagFullName = data.IsDBNull(8) ? "" : data.GetString(8),
                    TagLastName = data.IsDBNull(9) ? "" : data.GetString(9),
                    TagTCNo = data.IsDBNull(10) ? "" : data.GetString(10),
                    Id = data.GetInt64(4),
                    TagMobilTelephone = data.IsDBNull(11) ? "" : data.GetString(11)
                },
                pos = new TBLPosition { XPosition = data.GetInt32(1), YPosition = data.GetInt32(2), ZPosition = data.GetInt32(3), TagId = data.GetInt64(4), MapId = data.GetInt32(5), RecordDateTime = data.GetDateTime(6) }
            };
        }
        _dbCon.Close();
    }

    public IEnumerable<TBLReader> GetReaders()
    {
        var data = Query("SELECT CONCAT('rdr_',r.MapId,'_',r.local_id) as Name, r.rd_pos_x AS X,r.rd_pos_y AS Y ,r.rd_pos_z AS Z  FROM TBLReaders r WITH(NOLOCK)");
        while (data.Read())
        {
            yield return new TBLReader
            {
                ReaderName = data.GetString(0),
                rd_pos_x = data.GetDouble(1),
                rd_pos_y = data.GetDouble(2),
                rd_pos_z = data.GetDouble(3)
            };
        }
    }

}
