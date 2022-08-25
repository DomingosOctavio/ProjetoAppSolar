using System;
using Android.Content;
using Android.Preferences;



namespace Monitor_Energia_Solar
{
    class Session_Usuario
    {

        private ISharedPreferences nameSharedPrefs;
        private ISharedPreferencesEditor namePrefsEditor; //Declare Context,Prefrences name and Editor name
        private Context mContext;
        private static String PREFERENCE_ACCESS_KEY = "UserId"; //Value Access Key Name
        public static String NAME = "NAME"; //Value Variable Name
        public Session_Usuario(Context context)
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
        private static String PREFERENCE_ACCESS_KEY = "Token"; //Value Access Key Name
        public static String NAME = "Token"; //Value Variable Name
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

    class Session_Conexao
    {

        private ISharedPreferences nameSharedPrefs;
        private ISharedPreferencesEditor namePrefsEditor; //Declare Context,Prefrences name and Editor name
        private Context mContext;
        private static String PREFERENCE_ACCESS_KEY = "Conexao"; //Value Access Key Name
        public static String NAME = "Conexao"; //Value Variable Name
        public Session_Conexao(Context context)
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
