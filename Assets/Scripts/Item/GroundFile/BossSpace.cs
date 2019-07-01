using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;
using System.Collections.Generic;

public class BossSpace : MonoBehaviour
{
	[SerializeField]
	private BossStage bossStage;

	[SerializeField]
	private BossStatus bossstatus;

	[SerializeField]
	private int itemCount = 40;                     //	生成するオブジェクトの数
	[SerializeField]
	private float radius;                           //	半径
	[SerializeField]
	private float repeat = 2f;                      //	何周期するか
	[SerializeField]
	private GameObject particle;

	private List<GameObject> myList = new List<GameObject>();

	public bool removeExistingColliders = true;
	// Start is called before the first frame update
	void Start()
    {
		//	オブジェクトを消しておく
		gameObject.SetActive(false);

		//	メッシュを反転
		CreateInvertedMeshCollider();

		//	メッセージを受け取ったら表示する
		bossStage.OnBornBossMessge.Subscribe(_ =>
		{
			gameObject.SetActive(true);

			for (int i = 0; i < itemCount; ++i)
			{
				var oneCycle = 2.0f * Mathf.PI; // sin の周期は 2π

				var point = ((float)i / itemCount) * oneCycle; // 周期の位置 (1.0 = 100% の時 2π となる)
				var repeatPoint = point * repeat; // 繰り返し位置

				var x = Mathf.Cos(repeatPoint) * radius;
				var z = Mathf.Sin(repeatPoint) * radius;

				var position = new Vector3(x, 0, z);

				var obj = Instantiate(particle, position + transform.position, Quaternion.Euler(-90.0f, 0, 0));

				myList.Add(obj);
			}
		});

		//	メッセージを受け取ったらGameObjectを削除する
		bossstatus.OnBossStatusMessge.Subscribe(_ =>
		{
			Destroy(gameObject);

			for (int i = 0; i < itemCount; ++i)
			{
				myList[i].GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);
				Destroy(myList[i], 5.0f);
			}
		});
	}

	void Update()
	{
	}

	public void CreateInvertedMeshCollider()
	{
		if (removeExistingColliders)
			RemoveExistingColliders();

		InvertMesh();

		gameObject.AddComponent<MeshCollider>();
	}

	private void RemoveExistingColliders()
	{

		//	メッシュの削除
		Collider[] colliders = GetComponents<Collider>();
		for (int i = 0; i < colliders.Length; i++)
		{
			DestroyImmediate(colliders[i]);
		}
			
	}

	//	メッシュの反転
	private void InvertMesh()
	{
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.triangles = mesh.triangles.Reverse().ToArray();
	}
}
