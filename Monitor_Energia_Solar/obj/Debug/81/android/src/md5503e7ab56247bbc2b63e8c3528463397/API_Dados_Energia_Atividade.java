package md5503e7ab56247bbc2b63e8c3528463397;


public class API_Dados_Energia_Atividade
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onDestroy:()V:GetOnDestroyHandler\n" +
			"";
		mono.android.Runtime.register ("Monitor_Energia_Solar.API_Dados_Energia_Atividade, Monitor_Energia_Solar", API_Dados_Energia_Atividade.class, __md_methods);
	}


	public API_Dados_Energia_Atividade ()
	{
		super ();
		if (getClass () == API_Dados_Energia_Atividade.class)
			mono.android.TypeManager.Activate ("Monitor_Energia_Solar.API_Dados_Energia_Atividade, Monitor_Energia_Solar", "", this, new java.lang.Object[] {  });
	}


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
