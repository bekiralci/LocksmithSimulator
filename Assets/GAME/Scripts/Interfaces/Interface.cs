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
        /// Mevcut dükkan memnuniyet seviyesini alýr.
        /// </summary>
        int CurrentLevel { get; }

        /// <summary>
        /// Dükkan memnuniyetini delta kadar artýrýr veya azaltýr.
        /// </summary>
        void AddSatisfaction(int delta);
    }
}


