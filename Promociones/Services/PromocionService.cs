using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Repository;
using Models.Enums;

namespace Services
{
    public class PromocionService : IPromocionService
    {
        private readonly IRepositoryBase<Promocion> _PromocionRepository;

        public PromocionService(IRepositoryBase<Promocion> pPromocionRepository)
        {
            this._PromocionRepository = pPromocionRepository;
        }

        public void Delete(Promocion pPromocion)
        {
            try
            {
                this._PromocionRepository.Delete(pPromocion);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Exists(Guid pPromocion)
        {
            try
            {
                return this._PromocionRepository.Exists(pPromocion);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Promocion Get(Guid Id)
        {
            try
            {
                return _PromocionRepository.Get(Id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Promocion> GetAll()
        {
            try
            {
               return _PromocionRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Promocion> GetListadoPromocionesVigentes()
        {
            try
            {
                return _PromocionRepository.GetListadoPromocionesVigentes();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Promocion> GetListadoPromocionesVigentesFecha(DateTime pFecha)
        {
            try
            {
                return _PromocionRepository.GetListadoPromocionesVigentesFecha(pFecha);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Promocion> GetListadoPromocionesVigentesVenta(string pMedioPago, string pBanco, IEnumerable<string> pCategoriaProducto)
        {
            try
            {
                return _PromocionRepository.GetListadoPromocionesVigentesVenta(pMedioPago, pBanco, pCategoriaProducto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ModificaVigenciaPromocion(Guid pId, DateTime pFechaInicio, DateTime pFechaFin)
        {
            try
            {
                 _PromocionRepository.ModificaVigenciaPromocion(pId, pFechaInicio, pFechaFin);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Promocion Save(Promocion pPromocion)
        {
            try
            {
                if (this.validarPromocion(pPromocion))
                { 
                    if (!this.Exists(pPromocion.Id))
                    {
                         pPromocion.Id = Guid.NewGuid();
                        _PromocionRepository.Insert(pPromocion);
                    }
                        else
                    {
                        _PromocionRepository.Update(pPromocion);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return pPromocion;
        }

        private bool validarPromocion(Promocion pPromocion)
        {
            if (pPromocion.PorcentajeDeDescuento != null && pPromocion.MaximaCantidadDeCuotas != null)
                return false;

            if (pPromocion.PorcentajeDeDescuento != null && (pPromocion.PorcentajeDeDescuento < 5 || pPromocion.PorcentajeDeDescuento > 80))
                return false;

            if (_PromocionRepository.ValidarSolapamientoPromociones(pPromocion.Id,pPromocion.MediosDePago, pPromocion.Bancos, pPromocion.CategoriasProductos, (DateTime)pPromocion.FechaInicio, (DateTime)pPromocion.FechaFin))
                return false;

            if (pPromocion.FechaFin < pPromocion.FechaInicio)
                return false;

            if (pPromocion.ValorInteresesCuotas != null && pPromocion.ValorInteresesCuotas > 0 && (pPromocion.MaximaCantidadDeCuotas != null && pPromocion.MaximaCantidadDeCuotas > 0))
                return true;
            else
                return false;


        }
    }
}
