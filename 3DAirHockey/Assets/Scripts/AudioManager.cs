using UnityEngine;

/* Author: 
 * Last change date: 
 * Checked by: Malin Ejdbo
 * Date of check: 2018-03-13
 * Comment: Documentation.
*/

public class AudioManager : MonoBehaviour
{
    public AudioClip ClubCollision;
    public AudioClip PuckCollision;
    public AudioClip Goal;
    public AudioClip LostGame;
    public AudioClip WonGame;
    public float timertime;

    private AudioSource audioSource;

    float timer = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPuckCollision()
    {
        if(timer>timertime)
        {
            audioSource.PlayOneShot(PuckCollision);
        }

        timer = 0;
    }

    public void PlayClubCollision()
    {
        if (timer > timertime)
        {
            audioSource.PlayOneShot(PuckCollision);
        }

        timer = 0;
    }

    public void PlayGoal()
    {
        audioSource.PlayOneShot(Goal);
    }

    public void PlayLostGame()
    {
        audioSource.PlayOneShot(LostGame);
    }

    public void PlayWonGame()
    {
        audioSource.PlayOneShot(WonGame);
    }

    private void Update()
    {
        timer += Time.deltaTime;

    }
}