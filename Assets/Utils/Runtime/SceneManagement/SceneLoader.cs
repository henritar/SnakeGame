using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
	/// <summary>
	/// Loads a new <see cref="Scene"/> immediately.
	/// </summary>
	public class SceneLoader : MonoBehaviour
	{
		[SerializeField] private string _sceneName = "";
		[SerializeField] private LoadSceneMode _loadSceneMode = LoadSceneMode.Additive;

		protected void Start()
		{
			SceneManager.LoadScene(_sceneName, _loadSceneMode);
		}
	}
}