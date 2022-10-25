using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.Audio;

public class MenuSettingsWindow : MenuWindow
{
    public TMP_Dropdown resolutionsDropdown;

    public AudioMixer masterMixer;
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup sfxGroup;

    Resolution[] resolutions;
    int resolutionIndex;

    bool fullscreen;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionsDropdown.ClearOptions();

        int it=0;
        List<string> resolutionList = resolutions.Select( (res) =>
        {
            bool isCurrent = Screen.width == res.width && Screen.height == res.height;
            if (isCurrent) resolutionIndex = it;
            it++;
            return res.width + " x " + res.height;
        }
        ).ToList();

        resolutionsDropdown.AddOptions(resolutionList);
        resolutionsDropdown.value = resolutionIndex;
        resolutionsDropdown.RefreshShownValue();
    }

    void OnGUI()
    {
        string[] names = QualitySettings.names;
        GUILayout.BeginVertical();
        for (int i = 0; i < names.Length; i++)
        {
            if (GUILayout.Button(names[i]))
            {
                QualitySettings.SetQualityLevel(i, true);
            }
        }
        GUILayout.EndVertical();
    }

    #region Screen Settings Methods
    public void SetQualityLevel(int index)
    {
        QualitySettings.SetQualityLevel(index, false);
    }

    public void SetResolution(int index)
    {
        resolutionIndex = index;
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, fullscreen);
    }

    public void SetFullScreenMode(bool enable)
    {
        fullscreen = enable;
        Screen.fullScreen = enable;
    }
#endregion

    #region Audio Settings Methods
    public void SetVolume(float volume)
    {
        masterMixer.SetFloat("volume", volume);
    }

    public void MuteMusic(bool mute)
    {
        if(mute)
        {
            masterMixer.SetFloat("music", -80);
        }
        else
        {
            masterMixer.SetFloat("music", 0);
        }
    }

    public void MuteSFX(bool mute)
    {
        if (mute)
        {
            masterMixer.SetFloat("sfx", -80);
        }
        else
        {
            masterMixer.SetFloat("sfx", 0);
        }
    }
    #endregion
}
