using UnityEngine;
using System.Collections;
using S_M_D;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;
using System.Linq;

public class SaveScript : MonoBehaviour {

    public void OnClick()
    {
        Save( Start.Gtx );
    }

    public void Save( GameContext Gtx )
    {
        if (!Directory.Exists( @"C:\SauvegardeS_M_D" )) Directory.CreateDirectory( @"C:\SauvegardeS_M_D" );
        string pathString = Path.Combine( @"C:\SauvegardeS_M_D", Gtx.SaveName );
        if (CheckIfTheFileExist(pathString))
        {
            File.Delete( pathString );
        }
        RegulateNumberOfSave();
        using (Stream fileStream = new FileStream( pathString, FileMode.Create, FileAccess.Write, FileShare.None ))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize( fileStream, Gtx );
        }
    }
    private bool CheckIfTheFileExist(string path)
    {
        return File.Exists( path );
        
    }
    private void RegulateNumberOfSave()
    {
        string[] saveFile = Directory.GetFiles( @"C:\SauvegardeS_M_D", "*", SearchOption.TopDirectoryOnly );
        if (saveFile.Length >= 4)
        {
            Debug.Log( saveFile.Length + "first one" );
            string last = TheLastFile( saveFile );
            Debug.Log( last );
            saveFile.ToList().Remove( last );
            Debug.Log( saveFile.Length + "123456789" );
            File.Delete( last.ToString() );
            RegulateNumberOfSave();
        }
    }
    private string TheLastFile(string[] saveFile)
    {
        DateTime last = DateTime.MaxValue;
        string save = null;
        for (int i = 0; i < saveFile.Length; i++)
        {
            FileInfo file = new FileInfo( saveFile[i] );
            DateTime date = file.CreationTime;
            if (last > date)
            {
                last = date;
                save = saveFile[i];
            }
        }
        return save;
    }
}
