using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
	public string interactionMessage = "Hello, traveler! Press 'E' to interact.";
	public float interactionDistance = 3f;

	private Transform player;
	private bool playerInRange = false;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update()
	{
		// Kiểm tra xem player có ở gần NPC không
		if (player != null && Vector3.Distance(transform.position, player.position) <= interactionDistance)
		{
			playerInRange = true;
			// Hiển thị thông báo tương tác trên màn hình
			// Ví dụ: Debug.Log(interactionMessage);
			// Trong trường hợp này, bạn có thể sử dụng UI hoặc các phương thức khác để hiển thị thông báo trên màn hình.
		}
		else
		{
			playerInRange = false;
		}

		// Xử lý tương tác với player khi nhấn phím 'E'
		if (playerInRange && Input.GetKeyDown(KeyCode.E))
		{
			InteractWithPlayer();
		}
	}

	void InteractWithPlayer()
	{
		// Điều gì sẽ xảy ra khi player tương tác với NPC
		// Ví dụ: Hiển thị hội thoại, kích hoạt sự kiện, v.v.
		Debug.Log("Interacting with player!");
	}
}
