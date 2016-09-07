using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour {

	public void LoadViewScene() {
		SceneManager.LoadScene("View");
	}

	public void LoadDesigneScene() {
		SceneManager.LoadScene("Designe");
	}
}
