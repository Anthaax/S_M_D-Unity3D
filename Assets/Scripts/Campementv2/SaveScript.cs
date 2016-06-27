using UnityEngine;
using System.Collections;
using S_M_D;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveScript : MonoBehaviour {

    public void OnClick()
    {
        Debug.Log( "Save" );
        Save( Start.Gtx );
    }

    public void Save( GameContext Gtx )
    {
        if (!Directory.Exists( @"C:\SauvegardeS_M_D" )) Directory.CreateDirectory( @"C:\SauvegardeS_M_D" );
        string pathString = Path.Combine( @"C:\SauvegardeS_M_D", Gtx.SaveName );
        using (Stream fileStream = new FileStream( pathString, FileMode.Create, FileAccess.Write, FileShare.None ))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize( fileStream, Gtx );
        }
    }
}
