package crc649a084b6c64066712;


public class Escolha_BussolaInclinacao
	extends androidx.appcompat.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer,
		com.google.android.material.bottomnavigation.BottomNavigationView.OnNavigationItemSelectedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onBackPressed:()V:GetOnBackPressedHandler\n" +
			"n_onNavigationItemSelected:(Landroid/view/MenuItem;)Z:GetOnNavigationItemSelected_Landroid_view_MenuItem_Handler:Google.Android.Material.BottomNavigation.BottomNavigationView/IOnNavigationItemSelectedListenerInvoker, Xamarin.Google.Android.Material\n" +
			"";
		mono.android.Runtime.register ("Monitor_Energia_Solar.Escolha_BussolaInclinacao, Monitor_Energia_Solar", Escolha_BussolaInclinacao.class, __md_methods);
	}


	public Escolha_BussolaInclinacao ()
	{
		super ();
		if (getClass () == Escolha_BussolaInclinacao.class)
			mono.android.TypeManager.Activate ("Monitor_Energia_Solar.Escolha_BussolaInclinacao, Monitor_Energia_Solar", "", this, new java.lang.Object[] {  });
	}


	public Escolha_BussolaInclinacao (int p0)
	{
		super (p0);
		if (getClass () == Escolha_BussolaInclinacao.class)
			mono.android.TypeManager.Activate ("Monitor_Energia_Solar.Escolha_BussolaInclinacao, Monitor_Energia_Solar", "System.Int32, mscorlib", this, new java.lang.Object[] { p0 });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onBackPressed ()
	{
		n_onBackPressed ();
	}

	private native void n_onBackPressed ();


	public boolean onNavigationItemSelected (android.view.MenuItem p0)
	{
		return n_onNavigationItemSelected (p0);
	}

	private native boolean n_onNavigationItemSelected (android.view.MenuItem p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
