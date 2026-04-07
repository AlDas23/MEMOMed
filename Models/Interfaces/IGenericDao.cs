namespace MEMOMed.Models.Interfaces;

public interface IGenericDao<T>
{
    // Add
    void InsertEntity(T entity);
    // Read
    T? GrabEntityById(int id);
    // Update
    void UpdateEntity(T newEntity, int oldId);
    // Delete
    void DeleteEntity(int id);
}