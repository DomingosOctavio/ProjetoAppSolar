package crc649a084b6c64066712;


public class Bandeira_Atividade
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onBackPressed:()V:GetOnBackPressedHandler\n" +
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Monitor_Energia_Solar.Bandeira_Atividade, Monitor_Energia_Solar", Bandeira_Atividade.class, __md_methods);
	}


	public Bandeira_Atividade ()
	{
		super ();
		if (getClass () == Bandeira_Atividade.class)
			mono.android.TypeManager.Activate ("Monitor_Energia_Solar.Bandeira_Atividade, Monitor_Energia_Solar", "", this, new java.lang.Object[] {  });
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
