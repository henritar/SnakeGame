using Architectures.UMVCS.Model.Data;
using UnityEngine;

namespace Architectures.UMVCS.Model
{
	/// <summary>
	/// TODO: Add comment
	/// </summary>
	public class BaseModel : BaseActor
	{
		protected BaseConfigData ConfigData { get { return _configData; } set => _configData = value; }

        public int Index { get => _index; set => _index = value; }

        [SerializeField]
		private BaseConfigData _configData = null;

		private int _index;

		public override void Initialize()
		{
			if (!IsInitialized)
			{
				BaseApp.Instance.Context.ModelLocator.AddModel(this);
			}
			base.Initialize();
		}

		protected override void UnInitialize()
		{
			if (IsInitialized)
			{
				BaseApp.Instance.Context.ModelLocator.RemoveModel(this);
			}
			base.UnInitialize();
		}
	}
}