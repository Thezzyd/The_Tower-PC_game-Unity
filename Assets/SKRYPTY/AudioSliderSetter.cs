using UnityEngine;
using UnityEngine.UI;

public class AudioSliderSetter : MonoBehaviour
{
    void Start()
    {
        string tag = gameObject.tag;

        if (tag.Equals("SfxVolume"))
        {
            GetComponent<Slider>().value = OptionsManager.sfxVolume;
        }
        else if (tag.Equals("MusicVolume"))
        {
            GetComponent<Slider>().value = OptionsManager.musicVolume;
        }

    }


}
