using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    [Header("Sound Effects Clips")]
    public AudioClip[] sfxClips;
    public AudioClip[] sfxPlayer;
    public AudioClip[] sfxCollectables;
    [SerializeField]
    public AudioSource[] audioSources;
    private Dictionary<string, AudioClip> sfxList;
    private Dictionary<string, AudioClip> sfxPlayerList;
    private Dictionary<string, AudioClip> sfxCollectList;


    void Awake()
    {
        if (FindObjectsOfType(typeof(SFXManager)).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        audioSources = gameObject.GetComponentsInChildren<AudioSource>();


        if (audioSources == null)
            Debug.LogError("Audio Source Component not found");

        sfxList = new Dictionary<string, AudioClip>();
        sfxPlayerList = new Dictionary<string, AudioClip>();
        sfxCollectList = new Dictionary<string, AudioClip>();
        for (int i = 0; i < sfxClips.Length; i++)
        {
            sfxList[sfxClips[i].name] = sfxClips[i];
        }
        for (int i = 0; i < sfxPlayer.Length; i++)
        {
            sfxPlayerList[sfxPlayer[i].name] = sfxPlayer[i];
        }
        for (int i = 0; i < sfxCollectables.Length; i++)
        {
            //Debug.Log(sfxCollectables[i].name);
            sfxCollectList[sfxCollectables[i].name] = sfxCollectables[i];
        }
    }
    public void Play(string name, float pitchVariance = 0, bool loop = true)
    {


        if (sfxList.ContainsKey(name))
        {

            if (pitchVariance != 0) audioSources[0].pitch = 1 + Random.Range(-pitchVariance, pitchVariance);
            else audioSources[0].pitch = 1;
            audioSources[0].clip = sfxList[name];
            //audioSource.Play();
            audioSources[0].PlayOneShot(sfxList[name]);
            audioSources[0].loop = loop;

        }
        else
            Debug.LogWarning("No sound of name " + name + " exists");
    }

    public void PlaySoundPlayer(string name, float pitchVariance = 0, bool loop = true)
    {
        //Debug.Log(sfxList.ContainsKey(name)+ " " +name);
        if (sfxPlayerList.ContainsKey(name))
        {

            if (pitchVariance != 0) audioSources[1].pitch = 1 + Random.Range(-pitchVariance, pitchVariance);
            else audioSources[1].pitch = 1;
            audioSources[1].clip = sfxPlayerList[name];
            if (name == "RollingBallMainLoop")
            {
                audioSources[1].volume = 0.5f;
                audioSources[1].playOnAwake = false;
            }
            audioSources[1].Play();
            
        //    audioSources[1].PlayOneShot(sfxPlayerList[name]);
            audioSources[1].loop = loop;

        }
        else
            Debug.LogWarning("No sound of name " + name + " exists");
    }

    public void PlayCollectCollectabels(string name, float pitchVariance = 0, bool loop = true)
    {
        //Debug.Log(sfxCollectList.Keys);

        if (sfxCollectList.ContainsKey(name))
        {

           // if (pitchVariance != 0) audioSources[0].pitch = 1 + Random.Range(-pitchVariance, pitchVariance);
           // else audioSources[2].pitch = 1;
            audioSources[2].clip = sfxList[name];
            //audioSource.Play();
            audioSources[2].PlayOneShot(sfxList[name]);
            audioSources[2].loop = loop;

        }
        else
            Debug.LogWarning("No sound of name " + name + " exists");
    }
        public float mapValue(float mainValue, float inValueMin, float inValueMax, float outValueMin, float outValueMax)
    {
        return (mainValue - inValueMin) * (outValueMax - outValueMin) / (inValueMax - inValueMin) + outValueMin;
    }

    public void changePitchVelocity(float speed, float minValue, float maxValue)
    {
        float pitch = mapValue(speed, minValue, maxValue, 0.75f, 1.3f);
        audioSources[1].pitch = pitch ;
    }

    public void MuteAll()
    {
        for (int i = 0; i < audioSources.Length; i++)
            audioSources[i].mute = true;
    }

    public void UnmuteAll()
    {
        for (int i = 0; i < audioSources.Length; i++)
            audioSources[i].mute = false;
    }

    public void StopAll()
    {
        StopAllCoroutines();
        for (int i = 0; i < audioSources.Length; i++)
            audioSources[i].Stop();
    }
    public void stopSfxPlayer()
    {
        audioSources[1].Stop();
    }
    
 
}

