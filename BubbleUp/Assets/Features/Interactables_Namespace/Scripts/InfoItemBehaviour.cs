using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;
public class InfoItemBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform spawn;
        [SerializeField] private Transform target;
        [SerializeField] private float speed;
        [SerializeField] private List<GameObject> infoItemPool;
        [SerializeField][Range(0.01f, 1f)] private float minRespawnTime;
        [SerializeField][Range(0.01f, 1f)] private float maxRespawnTime;
        
        

        private void Start()
        {
            
            StartCoroutine(Spawn());
        }

        private void Instantiate()
        {
            GameObject infoItem = Instantiate(infoItemPool[Random.Range(0, infoItemPool.Count)], transform);
            infoItem.transform.position = spawn.position;
            infoItem.transform.LookAt(target);

            float movementTime = Vector3.Distance(spawn.position, target.position) / speed;
            LeanTween.move(infoItem, target, movementTime).setOnComplete(() =>
            {
                Destroy(infoItem);
            });
        }

        private IEnumerator Spawn()
        {
            Instantiate();
            
            yield return new WaitForSeconds(Random.Range(minRespawnTime, maxRespawnTime));

            StartCoroutine(Spawn());
        }
    }