using UnityEngine;

public class JukeBox : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip level1 = null;
    [SerializeField] AudioClip level2;
    [SerializeField] AudioClip level3;
    PlayerController player;
    float level2Threshold = 100f;
    float level3Threshold = 200f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.clip = 
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        if (level1 == null)
            level1 = Resources.Load<AudioClip>("AudioFiles/Music/sfx-MusicEthereal");

        if (level2 == null)
            level2 = Resources.Load<AudioClip>("AudioFiles/Music/sfx-MusicEthereal");

        if (level3 == null)
            level3 = Resources.Load<AudioClip>("AudioFiles/Music/sfx-MusicEthereal");
    }

    // Update is called once per frame
    void Update()
    {
        float score = player.Score;

        if (score < level2Threshold)
        {
            if (audioSource.clip != level1)
            {
                audioSource.clip = level1;
                audioSource.Play();
            }
        }
        else if (score >= level2Threshold && score < level3Threshold)
        {
            if (audioSource.clip != level2)
            {
                audioSource.clip = level2;
                audioSource.Play();
            }
        }
        else if (score >= level3Threshold)
        {
            if (audioSource.clip != level3)
            {
                audioSource.clip = level3;
                audioSource.Play();
            }
        }
    }
}
