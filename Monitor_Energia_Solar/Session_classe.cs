using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Preferences; //Add New NameSpace    


namespace Monitor_Energia_Solar
{
    class Session_classe
    {
        private ISharedPreferences nameSharedPrefs;
        private ISharedPreferencesEditor namePrefsEditor; //Declare Context,Prefrences name and Editor name  
        private Context mContext;
        private static String PREFERENCE_ACCESS_KEY = "PREFERENCE_ACCESS_KEY"; //Value Access Key Name  
        public static String NAME = "NAME"; //Value Variable Name  

        public Session_classe(Context context)
        {
            this.mContext = context;
            nameSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            namePrefsEditor = nameSharedPrefs.Edit();
        }
        public void saveAccessKey(string key) // Save data Values  
        {
            namePrefsEditor.PutString(PREFERENCE_ACCESS_KEY, key);
            namePrefsEditor.Commit();
        }
        public string getAccessKey() // Return Get the Value  
        {
            return nameSharedPrefs.GetString(PREFERENCE_ACCESS_KEY, "");
        }
    }
    class Session_Token
    {
        private ISharedPreferences nameSharedPrefs;
        private ISharedPreferencesEditor namePrefsEditor; //Declare Context,Prefrences name and Editor name  
        private Context mContext;
        private static String PREFERENCE_ACCESS_KEY = "PREFERENCE_ACCESS_KEY"; //Value Access Key Name  
        public static String NAME = "NAME"; //Value Variable Name  

        public Session_Token(Context context)
        {
            this.mContext = context;
            nameSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            namePrefsEditor = nameSharedPrefs.Edit();
        }
        public void saveAccessKey(string key) // Save data Values  
        {
            namePrefsEditor.PutString(PREFERENCE_ACCESS_KEY, key);
            namePrefsEditor.Commit();
        }
        public string getAccessKey() // Return Get the Value  
        {
            return nameSharedPrefs.GetString(PREFERENCE_ACCESS_KEY, "");
        }
    }
}