using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class LoadGame : MonoBehaviour {



    public int sceneToStart = 1;                                        //Index number in build settings of scene to load if changeScenes is true
    public bool changeScenes;                                           //If true, load a new scene when Start is pressed, if false, fade out UI and continue in single scene
    public bool changeMusicOnStart;                                     //Choose whether to continue playing menu music or start a new music clip


    [HideInInspector]
    public bool inMainMenu = true;                  //If true, pause button disabled in main menu (Cancel in input manager, default escape key)
    [HideInInspector]
    public Animator animColorFade;                  //Reference to animator which will fade to and from black when starting game.
    [HideInInspector]
    public Animator animMenuAlpha;                  //Reference to animator that will fade out alpha of MenuPanel canvas group
    public AnimationClip fadeColorAnimationClip;        //Animation clip fading to color (black default) when changing scenes
    [HideInInspector]
    public AnimationClip fadeAlphaAnimationClip;        //Animation clip fading out UI elements alpha


    private PlayMusic playMusic;                                        //Reference to PlayMusic script
    private float fastFadeIn = .01f;                                    //Very short fade time (10 milliseconds) to start playing music immediately without a click/glitch
    private ShowPanels showPanels;										//Reference to ShowPanels script on UI GameObject, to show and hide panels

    void Start () {

        //Get a reference to ShowPanels attached to UI object
        showPanels = GetComponent<ShowPanels>();

        //Get a reference to PlayMusic attached to UI object
        playMusic = GetComponent<PlayMusic>();

    }

    public void ChargeSave()
    {
        string[] saveFile = Directory.GetFiles(@"C:\SauvegardeS_M_D", "*", SearchOption.TopDirectoryOnly);
        List<string> file = saveFile.ToList();
        for (int i = 0; i < 4; i++)
        {
            file.Add(null);
            var save = GameObject.Find("SaveGameText" + i.ToString());
            if (file[i] != null)
            {
                FileInfo fileinfo = new FileInfo(file[i]);
                Debug.Log(fileinfo.CreationTime.ToString());
                int partie = i + 1;
                save.GetComponent<Text>().text = "Partie " + partie + " Date : " + fileinfo.CreationTime.ToString();
            }
            else
                save.GetComponent<Text>().text = "Pas de partie";
        }
    }
}
