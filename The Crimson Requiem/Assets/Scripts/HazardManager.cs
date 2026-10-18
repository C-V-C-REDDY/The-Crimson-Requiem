using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HazardManager : MonoBehaviour
{
    [SerializeField] List<Transform> points;
    [SerializeField] List<HazardData> hazards;
    [SerializeField] GameObject smokePrefab;
    [SerializeField] float warningTime = 3f;
    [SerializeField] Vector2 gapRange = new(15f, 25f);

    int lastPoint = -1;

    void Start()
    {
        if(points.Count == 0 || hazards.Count == 0)
        {
            Debug.LogError("HazardManager : assign points and Hazards.");
            return;
        }
        StartCoroutine(HazardLoop());
    }

    IEnumerator HazardLoop()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(gapRange.x, gapRange.y));

            Vector3 pos = PickPoint();
            HazardData data = hazards[Random.Range(0, hazards.Count)];

            var smoke = Instantiate(smokePrefab, pos, Quaternion.identity);
            yield return new WaitForSeconds(warningTime);
            Destroy(smoke);

            var zone = Instantiate(data.zonePrefab, pos, Quaternion.identity).GetComponent<HazardZone>();
            zone.Init(data);
            yield return new WaitUntil(() => zone == null);
        }
    }

    Vector3 PickPoint()
    {
        int i;
        do { i = Random.Range(0,points.Count); }
        while ( i == lastPoint && points.Count > 1);
        lastPoint = i;
        return points[i].position; 
    }

    void OawGizmos()
    {
       if(points == null) return;
       Gizmos.color = Color.red;
       foreach (var p in points) if(p) Gizmos.DrawWireSphere(p.position, 1f);       
    }

}
