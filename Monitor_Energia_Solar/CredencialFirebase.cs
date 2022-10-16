using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monitor_Energia_Solar
{
    public class CredencialFirebase
    {
        public static class DboAnahtar
        {
            public static string AuthSecret { get; set; } = "HRy0KVCabpLuTJSYuiiKrULaOlWL26KbyZhI4vjr";
            public static string BasePath { get; set; } = "https://apppainelsolar-496a7-default-rtdb.firebaseio.com/";
        }
    }
}