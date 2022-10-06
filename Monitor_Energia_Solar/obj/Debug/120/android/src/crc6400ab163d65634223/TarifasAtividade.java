package crc6400ab163d65634223;


public class TarifasAtividade
	extends androidx.appcompat.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onBackPressed:()V:GetOnBackPressedHandler\n" +
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onDestroy:()V:GetOnDestroyHandler\n" +
			"";
		mono.android.Runtime.register ("Monitor_Energia_Solar.TarifasAtividade, br.net.Monitor_Energia_Solar", TarifasAtividade.class, __md_methods);
	}


	public TarifasAtividade ()
	{
		super ();
		if (getClass () == TarifasAtividade.class)
			mono.android.TypeManager.Activate ("Monitor_Energia_Solar.TarifasAtividade, br.net.Monitor_Energia_Solar", "", this, new java.lang.Object[] {  });
	}


	public TarifasAtividade (int p0)
	{
		super (p0);
		if (getClass () == TarifasAtividade.class)
			mono.android.TypeManager.Activate ("Monitor_Energia_Solar.TarifasAtividade, br.net.Monitor_Energia_Solar", "System.Int32, mscorlib", this, new java.lang.Object[] { p0 });
	}


	public void onBackPressed ()
	{
		n_onBackPressed ();
	}

	private native void n_onBackPressed ();


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onDestroy ()
	{
		n_onDestroy ();
	}

	private native void n_onDestroy ();

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
