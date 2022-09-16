public interface IInteractable
{
	bool CanBeInteracted { get; }
	void EnableInteraction();
	void DisableInteraction();
	void Interact();
}
