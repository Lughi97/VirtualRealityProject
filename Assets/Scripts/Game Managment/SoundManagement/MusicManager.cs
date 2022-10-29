using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Music management, select wich music to play in game 
/// </summary>
public class MusicManager : Singleton<MusicManager>
{
    [SerializeField] private AudioSource[] audioSources;
    //public float[] clipSampleData;
    //public int sampleDataLenght = 1024;
    [SerializeField] private Dictionary<string, AudioSource> musicList;

    void Awake()
    {

        if (FindObjectsOfType(typeof(MusicManager)).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        musicList = new Dictionary<string, AudioSource>();

        audioSources = gameObject.GetComponentsInChildren<AudioSource>();
        for (int i = 0; i < audioSources.Length; i++)
        {
            AudioSource source = audioSources[i];
            musicList[source.gameObject.name] = source;
            // musicList[source.clip.name] = source;
        }
       // clipSampleData = new float[sampleDataLenght];
    }
    
    public void Play(string name, float pitchVariance = 0, bool loop = true)
    {
        if (musicList.ContainsKey(name))
        {
           // musicList[name].clip.GetData(clipSampleData,musicList[name].timeSamples);
            if (pitchVariance != 0) musicList[name].pitch = 1 + Random.Range(-pitchVariance, pitchVariance);

           if (!musicList[name].isPlaying)
            {
                musicList[name].Play();
                musicList[name].loop = loop;
            }
            //    if (!repeated)
           //     { musicList[name].Play(); }
            //    else
            //    {
           //         StartCoroutine(PlayMusicRepeated(name, duration, n_times));
           //     }

        }
        else Debug.LogWarning("No sound of name " + name + " exists");

       
    }
    
   
    /*
    public void PlayMusic(string name, bool repeated = false, float duration = 0, int n_times = 1, float pitchVariance = 0)
    {
        if (musicList.ContainsKey(name))
        {
            if (pitchVariance != 0) musicList[name].pitch = 1 + Random.Range(-pitchVariance, pitchVariance);

            if (!musicList[name].isPlaying)
                if (!repeated)
                { musicList[name].Play(); }
                else
                {
                    StartCoroutine(PlayMusicRepeated(name, duration, n_times));
                }

        }
        else Debug.LogWarning("No sound of name " + name + " exists");
    }
    */
    IEnumerator PlayMusicRepeated(string name, float duration, int n_times)
    {
        for (int i = 0; i < n_times; i++)
        {
            //Debug.Log(n_times);
            musicList[name].Play();
            yield return new WaitForSeconds(duration);
        }

        yield return new WaitForSeconds(0f);

    }
    public void StopAll()
    {
        StopCoroutine("PlayMusicRepeated");
        for (int i = 0; i < audioSources.Length; i++)
            audioSources[i].Stop();
    }
    public void PauseMusic(string name)
    {
        if (musicList.ContainsKey(name))
        {

            musicList[name].Pause();

        }
    }
    public void ResumeMusic(string name, bool loop)
    {
        if (musicList.ContainsKey(name))
        {

            if (!musicList[name].isPlaying)
            {
                musicList[name].Play();
                musicList[name].loop = loop;
            }
        }
    }
}

