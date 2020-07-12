﻿using com.drewchaseproject.MDM.Library.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace com.drewchaseproject.MDM.Library.Data
{
    public class Values
    {
        private static Values _singleton;
        public static Values Singleton => _singleton == null ? _singleton = new Values() : _singleton;
        //{
        //    get
        //    {
        //        if (_singleton == null) _singleton = new Values();
        //        return _singleton;
        //    }
        //}

        public DownloadFile CurrentFileDownloading { get; set; }
        List<DownloadFile> _queue;
        public List<DownloadFile> DownloadQueue { get { if (_queue == null) _queue = new List<DownloadFile>(); return _queue; } set { _queue = value; } }

        public int ConnectionsPerProxy
        {
            get
            {
                if (Configuration.Singleton.manager.GetConfigByKey("Max Connections").ParseInt() > 16)
                {
                    ConnectionsPerProxy = 16;
                    return 16;
                }
                else if (Configuration.Singleton.manager.GetConfigByKey("Max Connections").ParseInt() < 1)
                {
                    ConnectionsPerProxy = 1;
                    return 1;
                }
                return Configuration.Singleton.manager.GetConfigByKey("Max Connections").ParseInt();
            }
            set
            {
                if (value > 16) Configuration.Singleton.manager.GetConfigByKey("Max Connections").Value = 16 + "";
                else if (value < 1) Configuration.Singleton.manager.GetConfigByKey("Max Connections").Value = 1 + "";
                else Configuration.Singleton.manager.GetConfigByKey("Max Connections").Value = value + "";
            }
        }
        public int FileSplitCount
        {
            get
            {
                if (Configuration.Singleton.manager.GetConfigByKey("File Split Count").ParseInt() < 1)
                {
                    FileSplitCount = 1;
                    return 1;
                }
                return Configuration.Singleton.manager.GetConfigByKey("File Split Count").ParseInt();
            }
            set
            {
                if (value < 1) Configuration.Singleton.manager.GetConfigByKey("File Split Count").Value = 1 + "";
                else Configuration.Singleton.manager.GetConfigByKey("File Split Count").Value = value + "";
            }
        }

        public bool PreAllocate { get { return Configuration.Singleton.manager.GetConfigByKey("PreAllocate").ParseBoolean(); } set { Configuration.Singleton.manager.GetConfigByKey("PreAllocate").Value = value + ""; } }
        public string DownloadDirectory { get { return Configuration.Singleton.manager.GetConfigByKey("Download Directory").Value; } set { Configuration.Singleton.manager.GetConfigByKey("Download Directory").Value = value; } }

        string _direct;
        public string DirectDownloadURI
        {
            get
            {
                return _direct;
            }
            set
            {
                if (Uri.IsWellFormedUriString(value, UriKind.Absolute))
                {
                    _direct = value;
                }
                else
                {
                    _direct = string.Empty;
                }
            }
        }

        public string CompanyName => "Chase Labs";
        public string ApplicationName => "Magic Download Manager";
        public string ApplicationRoot
        {
            get
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), CompanyName, ApplicationName);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                return path;
            }
        }
        public string ConfigDirectory
        {
            get
            {
                string path = Path.Combine(ApplicationRoot, "Configuration");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                return path;
            }
        }
        public string ApplicationDirectory
        {
            get
            {
                string path = Path.Combine(ApplicationRoot, "Bin");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                return path;
            }
        }
        public string TempDirectory
        {
            get
            {
                string path = Path.Combine(Environment.GetEnvironmentVariable("temp", EnvironmentVariableTarget.User), CompanyName, ApplicationName);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                return path;
            }
        }

    }
}
