using System;
using System.Collections.Generic;

namespace SignaturePad.ModelServices
{
    /// <summary>
    /// Defines the methods to support the editable data repository implementation.
    /// </summary>
    public interface IEditableDataRepository<TEntity, in TKey> : IDataRepository<TEntity, TKey> where TEntity : class
    {
        #region Methods

        /// <summary>
        /// Create a new entity based on the type of the data repository.
        /// </summary>
        /// <returns>Returns a newly created entity.</returns>
        TEntity Create();

        /// <summary>
        /// Delete one or more entity in the specified collection from the data repository.
        /// </summary>
        /// <param name="entities">A collection of entity to delete.</param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete the specified entity from the data repository.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Insert the specified entity to the data repository.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Reject the changes which have been made to the entity.
        /// </summary>
        /// <param name="entity">The entity of which changes to reject.</param>
        void Reject(TEntity entity);

        /// <summary>
        /// Save all changes made to the data repository since it is first loaded.
        /// </summary>
        /// <param name="onSuccess">The callback to invoke when the save operation succeeded.</param>
        /// <param name="onError">The callback to invoke when the save operation failed.</param>
        void SaveChangesAsync(Action onSuccess = null, Action<Exception> onError = null);

        /// <summary>
        /// Update the specified entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        void Update(TEntity entity);

        #endregion
    }
}