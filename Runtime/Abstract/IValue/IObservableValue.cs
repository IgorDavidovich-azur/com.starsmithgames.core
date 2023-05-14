using System;

public interface IObservableValue
{
	event Action onChanged;
}