using UnityEngine;

namespace GameManager
{
	public class SfxManager : MonoBehaviour
	{
		public static SfxManager Instance;

		[SerializeField]
		private AudioSource _audioSource = null;
		[SerializeField]
		private AudioClip _explosion = null, _drop = null, _clickButton = null;

		private void Awake()
		{
			if (Instance != null)
				return;

			Instance = this;

			DontDestroyOnLoad(this);
		}

		public void PlayExplosionSfx()
		{
			_audioSource.PlayOneShot(_explosion);
		}

		public void PlayDropSfx()
		{
			_audioSource.PlayOneShot(_drop);
		}

		public void PlayClickButtonSfx()
		{
			_audioSource.PlayOneShot(_clickButton);
		}
	}
}