using Dapper;
using EvaluacionAplicativa2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace EvaluacionAplicativa2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string connection = "Data Source=NBKHR01075\\SQLEXPRESS; Initial Catalog=Tienda; Integrated Security=true";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Cliente> lst = new List<Cliente>();
            using (IDbConnection conexion = new SqlConnection(connection))
            {
                conexion.Open();
                var sql = "select * from Cliente";
                lst = conexion.Query<Cliente>(sql);

            }

            return View(lst);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Cliente model)
        {
            int result = 0;
            using (IDbConnection conexion = new SqlConnection(connection))
            {
                conexion.Open();
                var sql = "insert into Cliente(NroDocumento,Nombre,Direccion,Telefono,Observacion)" +
                    "values(@NroDocumento,@Nombre,@Direccion,@Telefono,@Observacion)";

                result = conexion.Execute(sql, model);
            }

            return RedirectToAction("index");
        }

        public ActionResult Edit(int id)
        {
            IEnumerable<Cliente> lst = new List<Cliente>();
            using (IDbConnection conexion = new SqlConnection(connection))
            {
                conexion.Open();
                var sql = "select * from Cliente where IdCliente =" + id;
                lst = conexion.Query<Cliente>(sql);
            }
            return View(lst);
        }

        [HttpGet]
        public ActionResult Delete(Cliente model, int id)
        {
            int result = 0;
            using (IDbConnection conexion = new SqlConnection(connection))
            {
                conexion.Open();
                var sql = "delete from Cliente where IdCliente =" + id;

                result = conexion.Execute(sql, model);
            }
            return RedirectToAction("index");
        }
        [HttpPost]
        public ActionResult Edit(Cliente model)
        {
            int result = 0;
            using (IDbConnection conexion = new SqlConnection(connection))
            {
                conexion.Open();
                var sql = "Update Cliente set  NroDocumento = @NroDocumento,Nombre =@Nombre,Direccion =@Direccion,Telefono=@Telefono,Observacion=@Observacion" +
                    " where IdCliente = @IdCliente";
                result = conexion.Execute(sql, model);
            }
            return RedirectToAction("index");
        }
    }
}