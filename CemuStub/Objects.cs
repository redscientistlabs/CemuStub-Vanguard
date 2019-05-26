using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.IO;
using RTCV.CorruptCore;

namespace CemuStub
{

    public class CemuGameInfo
    {
        public FileInfo gameRpxFileInfo = null;
        public FileInfo cemuExeFile = null;
        public FileInfo[] updateCodeFiles = null;
        public DirectoryInfo gameSaveFolder = null;
        public string rpxFile = null;
        public string gameRpxPath = null;
        public string updateRpxPath = null;
        public string updateCodePath = null;
        public string updateMetaPath = null;
        public string updateRpxLocation = null;
        public string updateRpxCompressed = null;
        public string updateRpxBackup = null;
        public string FirstID = null;
        public string SecondID = null;
        public string fileInterfaceTargetId = null;
        public string gameName = "Autodetect";
        public string updateRpxUncompressedToken = null;

        public override string ToString()
        {
            return gameName;
        }
    }

    enum CemuState
    {
        UNFOUND,
        RUNNING,
        GAMELOADED,
        PREPARING,
        READY
    }

}
