package crc649a084b6c64066712;


public class CadastroTokenAtividade
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Monitor_Energia_Solar.CadastroTokenAtividade, Monitor_Energia_Solar", CadastroTokenAtividade.class, __md_methods);
	}


	public CadastroTokenAtividade ()
	{
		super ();
		if (getClass () == CadastroTokenAtividade.class)
			mono.android.TypeManager.Activate ("Monitor_Energia_Solar.CadastroTokenAtividade, Monitor_Energia_Solar", "", this, new java.lang.Object[] {  });
	}


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
