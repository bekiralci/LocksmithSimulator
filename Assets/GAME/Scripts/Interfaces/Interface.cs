using UnityEngine;

public interface ILeftClickListener
{
    void OnLeftClick();
}

public interface IRightClickListener
{
    void OnRightClick();
}


// Assets/Scripts/Services/IStoreSatisfactionService.cs
namespace MultiQuestSystem.Services
{
    public interface IStoreSatisfactionService
    {
        /// <summary>
        /// Mevcut d�kkan memnuniyet seviyesini al�r.
        /// </summary>
        int CurrentLevel { get; }

        /// <summary>
        /// D�kkan memnuniyetini delta kadar art�r�r veya azalt�r.
        /// </summary>
        void AddSatisfaction(int delta);
    }
}


