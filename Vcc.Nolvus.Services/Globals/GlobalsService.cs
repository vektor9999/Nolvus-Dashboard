﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Vcc.Nolvus.Core.Interfaces;
using Vcc.Nolvus.Core.Services;

namespace Vcc.Nolvus.Services.Globals
{
    public class GlobalsService : IGlobalsService
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(
             string deviceName, int modeNum, ref DEVMODE devMode);
        const int ENUM_CURRENT_SETTINGS = -1;

        const int ENUM_REGISTRY_SETTINGS = -2;

        [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public ScreenOrientation dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        public const string NolvusSection = "Nolvus";
        public const string UserName = "UserName";
        public const string Password = "Password";
        public const string Version = "Version";
        public const string NolvusApi = "https://www.nolvus.net/rest/";
        public const string NexusSection = "Nexus";
        public const string ApiKey = "ApiKey";
        public const string UserAgent = "UserAgent";        
                

        public string ApiVersion
        {
            get
            {
                return ServiceSingleton.Settings.GetIniValue(NolvusSection, Version);
            }
        }

        public string ApiUrl
        {
            get
            {
                return NolvusApi;
            }
        }

        public string NolvusUserName
        {
            get
            {
                return ServiceSingleton.Settings.GetIniValue(NolvusSection, UserName);
            }
        }

        public string NolvusPassword
        {
            get
            {
                return ServiceSingleton.Settings.GetIniValue(NolvusSection, Password);
            }
        }

        public string NexusApiKey
        {
            get
            {
                return ServiceSingleton.Settings.GetIniValue(NexusSection, ApiKey);
            }
        }

        public string NexusUserAgent
        {
            get
            {
                return ServiceSingleton.Settings.GetIniValue(NexusSection, UserAgent);
            }
        }

        public List<string> WindowsResolutions
        {
            get
            {
                List<string> Result = new List<string>();

                DEVMODE vDevMode = new DEVMODE();
                int i = 0;
                while (EnumDisplaySettings(null, i, ref vDevMode))
                {
                    string Reso = string.Format("{0}x{1}", vDevMode.dmPelsWidth, vDevMode.dmPelsHeight);

                    if (!Result.Contains(Reso))
                    {
                        Result.Add(Reso);
                    }
                    i++;
                }

                return Result;
            }
           
        }
    }
}