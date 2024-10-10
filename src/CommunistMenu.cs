using Modding;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Audio;
using UObject = UnityEngine.Object;

namespace CommunistMenu;

[UsedImplicitly]
public class CommunistMenu : Mod
{
    private TextureStrings SpriteDict { get; }

    public CommunistMenu() : base("Communist Menu")
    {
        SpriteDict = new TextureStrings();

        SFCore.MenuStyleHelper.AddMenuStyleHook += AddMenuStyle;
        ModHooks.LanguageGetHook += LanguageGetHook;
    }

    private static string LanguageGetHook(string key, string sheetTitle, string orig)
    {
        return key == "UI_MENU_STYLE_COMMUNISM" ? "Fallen Comrades" : orig;
    }

    private (string languageString, GameObject styleGo, int titleIndex, string unlockKey, string[] achievementKeys, MenuStyles.MenuStyle.CameraCurves cameraCurves, AudioMixerSnapshot musicSnapshot) AddMenuStyle(MenuStyles self)
    {
        GameObject menuStyle = new GameObject();
        SpriteRenderer srC = menuStyle.AddComponent<SpriteRenderer>();
        srC.sprite = SpriteDict.Get(TextureStrings.CommunismKey);
        Color srCColor = srC.color;
        srCColor.r = 0.5f;
        srCColor.g = 0.5f;
        srCColor.b = 0.5f;
        srCColor.a = 1.0f;
        srC.color = srCColor;
        menuStyle.transform.localPosition = new Vector3(14.58f, 8.3f, 0f);

        MenuStyles.MenuStyle.CameraCurves cameraCurves = new MenuStyles.MenuStyle.CameraCurves
        {
            saturation = 1.0f,
            redChannel = new AnimationCurve(),
            greenChannel = new AnimationCurve(),
            blueChannel = new AnimationCurve()
        };
        cameraCurves.redChannel.AddKey(new Keyframe(0f, 0f));
        cameraCurves.redChannel.AddKey(new Keyframe(1f, 1f));
        cameraCurves.greenChannel.AddKey(new Keyframe(0f, 0f));
        cameraCurves.greenChannel.AddKey(new Keyframe(1f, 1f));
        cameraCurves.blueChannel.AddKey(new Keyframe(0f, 0f));
        cameraCurves.blueChannel.AddKey(new Keyframe(1f, 1f));

        //AudioMixerSnapshot audioSnapshot = self.styles[1].musicSnapshot.audioMixer.FindSnapshot("Normal - Gramaphone");
        AudioMixerSnapshot audioSnapshot = self.styles[1].musicSnapshot.audioMixer.FindSnapshot("Normal");
        return ("UI_MENU_STYLE_COMMUNISM", menuStyle, -1, "", null, cameraCurves, audioSnapshot);
    }

    public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
    {
        Log("Initializing");

        Log("Initialized");
    }
}