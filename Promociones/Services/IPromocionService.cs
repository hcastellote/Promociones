using Models;
using System;
using System.Collections.Generic;


namespace Services
{
    public interface IPromocionService
    {
        IEnumerable<Promocion> GetAll();
        void Delete(Promocion pPromocion);
        Promocion Get(Guid Id);
        Promocion Save(Promocion pPromocion);
        bool Exists(Guid pPromocion);

        IEnumerable<Promocion> GetListadoPromocionesVigentes();

        IEnumerable<Promocion> GetListadoPromocionesVigentesFecha(DateTime pFecha);


        IEnumerable<Promocion> GetListadoPromocionesVigentesVenta(string pMedioPago, string pBanco, IEnumerable<string> pCategoriaProducto);

        void ModificaVigenciaPromocion(Guid pId, DateTime pFechaInicio, DateTime pFechaFin);
    }
}
