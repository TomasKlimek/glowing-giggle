
namespace Quantum
{
	internal unsafe sealed class HealthSystem : SystemSignalsOnly, ISignalOnComponentAdded<Health>
	{
		void ISignalOnComponentAdded<Health>.OnAdded(Frame frame, EntityRef entity, Health* component)
		{
			component->Revive();
		}
	}
}
