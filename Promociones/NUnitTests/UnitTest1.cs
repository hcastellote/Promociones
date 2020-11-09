using NUnit.Framework;
using Repository;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Models;
using Models.Enums;
using Repository;
using UnitTest;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using System;
using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Serializers;

namespace NUnitTests
{
    public class Tests
    {

        private ConfigurationModel _configuracion;
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [TestCase(Category = "Promocion")]
        public void CrearPromocion()
        {

            IEnumerable<string> pMedioPago = new string[] { EnumMediosDePago.EFECTIVO.ToString(), EnumMediosDePago.GIFT_CARD.ToString() };
            IEnumerable<string> pBanco = new string[] { EnumBancos.BBVA.ToString(), EnumBancos.Santander_Rio.ToString() };
            IEnumerable<string> pCategoriasProducto = new string[] { EnumCategoriasProductos.Audio.ToString(), EnumCategoriasProductos.Celulares.ToString() };
            var promocion = new Promocion { Id = Guid.NewGuid(), 
                                            Activo = true, 
                                            FechaCreacion = DateTime.Today, 
                                            FechaInicio = DateTime.Today.AddDays(-15),
                                            FechaFin = DateTime.Today.AddDays(15),
                                            PorcentajeDeDescuento = 50,
                                            MaximaCantidadDeCuotas = 12,
                                            ValorInteresesCuotas = 10000,
                                            Bancos = pBanco,
                                            CategoriasProductos = pCategoriasProducto,
                                            MediosDePago = pMedioPago

            };
            //act
            promocion = DependacyInjector.ServiceProvider.GetService<IPromocionService>().Save(promocion);
            var promocionExists = DependacyInjector.ServiceProvider.GetService<IPromocionService>().Exists(promocion.Id);
            //asset
            Assert.IsTrue(promocionExists);
        }

        [TestCase(Category = "Promocion")]
        public void ModificarPromocion()
        {
            var promocion = new Promocion { Id = new Guid("8c1338f6-d3f6-4362-9401-728fe985dc7e") };
            //act


            promocion = DependacyInjector.ServiceProvider.GetService<IPromocionService>().Get((promocion.Id)); 
            bool estadoPromocion = promocion.Activo;
            promocion.Activo = !promocion.Activo;
            promocion = DependacyInjector.ServiceProvider.GetService<IPromocionService>().Save(promocion);

            var promocionModificada = DependacyInjector.ServiceProvider.GetService<IPromocionService>().Get(promocion.Id);

            ////asset
            Assert.IsTrue(estadoPromocion != promocionModificada.Activo);
        }

        [TestCase(Category = "Promocion")]
        public void ModificaVigenciaPromocion()
        {
            var promocion = new Promocion { Id = new Guid("8c1338f6-d3f6-4362-9401-728fe985dc7e") };

            //act
            promocion = DependacyInjector.ServiceProvider.GetService<IPromocionService>().Get((promocion.Id));

            DateTime fechaInicio = (DateTime)promocion.FechaInicio;
            DateTime fechaFin = (DateTime)promocion.FechaFin;

            promocion.FechaInicio = ((DateTime)promocion.FechaInicio).AddDays(30);
            promocion.FechaFin = ((DateTime)promocion.FechaFin).AddDays(60);
            promocion = DependacyInjector.ServiceProvider.GetService<IPromocionService>().Save(promocion);

            var promocionModificada = DependacyInjector.ServiceProvider.GetService<IPromocionService>().Get(promocion.Id);

            ////asset
            Assert.IsTrue(promocion.FechaInicio != fechaInicio && promocion.FechaFin != fechaFin);
        }



        [TestCase(Category = "Promocion")]
        public void GetPromocion()
        {
            //act
            var promocion_new = DependacyInjector.ServiceProvider.GetService<IPromocionService>().Get(new Guid("8c1338f6-d3f6-4362-9401-728fe985dc7e"));

            ////asset
            Assert.IsNotNull(promocion_new);
        }

        [TestCase(Category = "Promocion")]
        public void GetAllPromociones()
        {
            //act
            IEnumerable<Promocion> promociones = DependacyInjector.ServiceProvider.GetService<IPromocionService>().GetAll();

            ////asset
            Assert.IsTrue(promociones.ToList().Count > 0);
        }

        [TestCase(Category = "Promocion")]
        public void GetPromocionesVigentes()
        {
            //act
            IEnumerable<Promocion> promociones = DependacyInjector.ServiceProvider.GetService<IPromocionService>().GetListadoPromocionesVigentes();

            ////asset
            Assert.IsTrue(promociones.ToList().Count > 0);
        }

        [TestCase(Category = "Promocion")]
        public void GetListadoPromocionesVigentesVenta()
        {
            //act
            string pMedioPago = EnumMediosDePago.EFECTIVO.ToString();
            string pBanco = EnumBancos.BBVA.ToString();
            IEnumerable<string> pCategoriasProducto = new string[] { EnumCategoriasProductos.Audio.ToString(), EnumCategoriasProductos.Celulares.ToString() };
            pCategoriasProducto.Append(EnumCategoriasProductos.Audio.ToString());
            IEnumerable<Promocion> promociones = DependacyInjector.ServiceProvider.GetService<IPromocionService>().GetListadoPromocionesVigentesVenta(pMedioPago,pBanco,pCategoriasProducto);

            ////asset
            Assert.IsTrue(promociones.ToList().Count > 0);
        }
    }
}