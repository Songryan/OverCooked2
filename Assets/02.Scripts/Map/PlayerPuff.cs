using UnityEngine;
using UnityEngine.Pool;

public class PlayerPuff : Singleton<PlayerPuff>
{
    public GameObject puffWalkPrefab;   // 걷는 퍼프 프리팹
    public GameObject puffBurstPrefab;  // 버스트 퍼프 프리팹
    public GameObject switchEfPrefab;
    public GameObject spawnPrefab;

    private IObjectPool<Puff> walkPool; // 걷는 퍼프 풀
    private IObjectPool<Puff> burstPool; // 버스트 퍼프 풀
    private IObjectPool<Puff> switchEfPool;
    private IObjectPool<Puff> spawnPool;
    private new void Awake()
    {
        base.Awake();
        walkPool = new ObjectPool<Puff>(CreateWalk, OnGetPuff, OnReleasePuff, OnDestroyPuff, maxSize: 1000);
        burstPool = new ObjectPool<Puff>(CreateBurst, OnGetPuff, OnReleasePuff, OnDestroyPuff, maxSize: 1000);
        spawnPool = new ObjectPool<Puff>(CreateBurst, OnGetPuff, OnReleasePuff, OnDestroyPuff, maxSize: 1000);
        //switchEfPool = new ObjectPool<Puff>(CreateSwitch, OnGetPuff, OnReleasePuff, OnDestroyPuff, maxSize: 1000);
    }


    // 부스트 퍼프 실행
    public void BoostPuff(Transform puffPosition)
    {
        var burst = burstPool.Get();
        burst.transform.position = puffPosition.position;
        // burst.transform.rotation = puffPosition.rotation;
    }

    // 무브 퍼프 실행
    public void MovePuff(Transform puffPosition)
    {
        var walk = walkPool.Get();
        walk.transform.position = puffPosition.position;
        // walk.transform.rotation = puffPosition.rotation;
    }

    // 스폰 퍼프 실행
    public void SpawnPuff(Transform puffPosition)
    {
        var spawn = spawnPool.Get();
        spawn.transform.position = puffPosition.position;
    }

    public void Switching(Transform Start, Transform End)
    {
        var switcing = switchEfPool.Get();
        switcing.transform.position = Start.position;
        if (Start != null && End != null)
        {

            SwitchParticle Sp = switcing.GetComponent<SwitchParticle>();
            Sp.SwitchPlayer(Start, End);
        }
    }

    // 워크 퍼프 생성 후 풀에 담음
    private Puff CreateWalk()
    {
        return CreatePuff(puffWalkPrefab, walkPool);
    }

    // 버스트 퍼프 생성 후 풀에 담음
    private Puff CreateBurst()
    {
        return CreatePuff(puffBurstPrefab, burstPool);
    }

    private Puff CreateSwitch()
    {
        return CreatePuff(switchEfPrefab, switchEfPool);
    }

    private Puff CrateSpawn()
    {
        return CreatePuff(spawnPrefab, spawnPool);
    }

    // 퍼프를 생성하고 풀에 담음
    private Puff CreatePuff(GameObject prefab, IObjectPool<Puff> pool)
    {
        Puff puff = Instantiate(prefab, transform).GetComponent<Puff>();
        puff.SetManagedPool(pool);
        puff.transform.localScale = Vector3.one * 2;
        return puff;
    }

    // 퍼프를 불러옴
    private void OnGetPuff(Puff puff)
    {
        puff.gameObject.SetActive(true);
    }

    // 퍼프 비활성화
    private void OnReleasePuff(Puff puff)
    {
        puff.gameObject.SetActive(false);
    }

    // 퍼프 삭제
    private void OnDestroyPuff(Puff puff)
    {
        Destroy(puff.gameObject);
    }
}
