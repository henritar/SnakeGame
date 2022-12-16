
using System;

namespace Data.Types
{
	/// <summary>
	/// TODO: Add comment
	/// </summary>
	public interface IState 
	{
		void InitializeState();
		void EnterState();
		Type UpdateState();
		void ExitState();
		void DestroyState();
	}
}
