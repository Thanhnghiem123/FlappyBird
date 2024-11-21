using System.Collections;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab; // Prefab của pipe
    public float spawnInterval = 1f; // Khoảng thời gian giữa mỗi lần sinh pipe
    public float destroyXPosition = -10f; // Vị trí X mà tại đó pipe sẽ bị trả về pool

    private ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        InvokeRepeating("SpawnPipes", 0f, spawnInterval); // Bắt đầu sinh pipe ngay lập tức và lặp lại mỗi spawnInterval giây
    }

    void SpawnPipes()
    {
        float spawnYPosition = Random.Range(0, 2.5f);
        Vector3 spawnPosition = new Vector3(transform.position.x, spawnYPosition, transform.position.z);

        GameObject spawnedPipe = objectPooler.SpawnFromPool(pipePrefab.tag, spawnPosition, Quaternion.identity);
        GameObject backGround = GameObject.FindGameObjectWithTag("BackGround");
        if (spawnedPipe != null)
        {
            spawnedPipe.transform.SetParent(backGround.transform);
            // Tùy chỉnh thêm logic di chuyển hoặc hủy đối tượng tại đây nếu cần
            //StartCoroutine(ReturnPipeToPool(spawnedPipe));
        }
    }

    System.Collections.IEnumerator ReturnPipeToPool(GameObject pipe)
    {
        yield return new WaitForSeconds(5f); // Chờ một khoảng thời gian trước khi trả pipe về pool, thay đổi giá trị này tùy theo gameplay

        if (pipe.transform.position.x <= destroyXPosition)
        {
            objectPooler.ReturnToPool(pipe);
        }
    }
}
