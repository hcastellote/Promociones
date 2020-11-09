using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IRepositoryBase<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Get(Guid id);
        T Get(string id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        bool Exists(int id);
        bool Exists(Guid id);
        bool Exists(string id);
        int CollectionSize();

        IEnumerable<T> GetListadoPromocionesVigentes();
        IEnumerable<T> GetListadoPromocionesVigentesFecha(DateTime pFecha);
        IEnumerable<T> GetListadoPromocionesVigentesVenta(string pMedioPago, string pBanco, IEnumerable<string> pCategoriaProducto);

        void ModificaVigenciaPromocion(Guid pId, DateTime pFechaInicio, DateTime pFechaFin);

        bool ValidarSolapamientoPromociones(Guid pId, IEnumerable<string> pMedioPago, IEnumerable<string> pBanco, IEnumerable<string> pCategoriaProducto, DateTime pFechaInicio, DateTime pFechaFin);

    }
}
