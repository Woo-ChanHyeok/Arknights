using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemySpawner : MonoBehaviour
{
    PlayableDirector timeLineStarter;
        
    [SerializeField] private GameObject slime_Prefab;
    private GameObject startBox;
    private void Awake()
    {
        TryGetComponent(out timeLineStarter);
        startBox = GameObject.FindGameObjectWithTag("startBox");
    }
    private void Update()
    {
        if (FadeManager.isLoad && timeLineStarter.state == PlayState.Paused)
        {
            timeLineStarter.Play();
        }
        else if (!FadeManager.isLoad && timeLineStarter.state == PlayState.Playing)
        {
            timeLineStarter.Stop();
        }
    }
    public void StartSign()
    {
        timeLineStarter.Play();
    }
    public void Spawn_Slime()
    {
        Instantiate(slime_Prefab, startBox.transform.position, Quaternion.identity);
    }
    public void CheckEnd()
    {
        Map_Information.instance.checkEnd = true;
    }
}
