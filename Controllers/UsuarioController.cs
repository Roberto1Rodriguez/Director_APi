using DirectorAPI.Data;
using DirectorAPI.Models;
using DirectorAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace DirectorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        public UsuarioController(Sistem21PrimariaContext context)
        {
            Context = context;
            docenterepository = new Repository<Docente>(context);
            usuariorepository = new Repository<Usuario>(context);
            directorrepository = new Repository<Director>(context);
        }
        public Sistem21PrimariaContext Context { get; }
        private Repository<Usuario> usuariorepository;
        private Repository<Director> directorrepository;
        private Repository<Docente> docenterepository;
        [HttpGet("Usuario")]
        public IActionResult Get()
        {
            var usuario = usuariorepository.GetAll().OrderBy(x => x.Id).ToList();
            return Ok(usuario);
        }
        [HttpGet("Docente")]
        public IActionResult Get()
        {
            var docente = docenterepository.GetAll().OrderBy(x => x.Id).ToList();
            return Ok(docente);
        }
        [HttpGet("Login")]
        public IActionResult Login(Usuario usu)
        {

            var u = usuariorepository.GetAll().FirstOrDefault(x => x.Usuario1 == usu.Usuario1 && x.Contraseña == usu.Contraseña);
            if (u==null||u.Rol!=1)
            {
                return NotFound("Usuario o Contraseña Incorrectos");
            }
            var d = directorrepository.GetAll().FirstOrDefault(x => x.Idusuario == u.Id);
            Director director;
            director = new Director
            {
                Id = d.Id,
                Nombre = d.Nombre,
                Telefono = d.Telefono,
                Direccion = d.Direccion,
                Idusuario = d.Idusuario
            };
            return Ok(d);
        }
        [HttpPost("Usuario")]
        public IActionResult PostUsuario(Usuario u)
        {
            if (u == null)
            {
                return BadRequest();
            }
            if (ValidateUsuario(u, out List<string> errors))
            {
                usuariorepository.Insert(u);
            }

            return Ok();
        }
        private bool ValidateUsuario(Usuario u, out List<string> errors)
        {
            errors = new List<string>();
            if (string.IsNullOrWhiteSpace(u.Usuario1))
            {
                errors.Add("Favor de escribir un usuario.");
            }
            if (string.IsNullOrWhiteSpace(u.Contraseña))
            {
                errors.Add("Favopr de ingresar una contraseña");
            }
            if (u.Rol != 2)
            {
                errors.Add("Favor de ingresar un rol");
            }
            if (usuariorepository.GetAll().Any(x => x.Usuario1 == u.Usuario1))
            {
                errors.Add("Este usuario ya existe");
            }
            return errors.Count == 0;
        }
        [HttpPost("Docente")]
        public IActionResult PostDocente(Docente d)
        {
            
            if (d == null)
            {
                return BadRequest();
            }
            if (ValidateDocente(d, out List<string> errors))
            {
                d.IdUsuario = usuariorepository.GetAll().Max(x => x.Id);
                docenterepository.Insert(d);
            }
            return Ok();
        }
        private bool ValidateDocente(Docente d, out List<string> errors)
        {
            errors = new List<string>();
            if (string.IsNullOrWhiteSpace(d.Nombre))
            {
                errors.Add("Coloque el nombre del docente");
            }
            if (string.IsNullOrWhiteSpace(d.ApellidoPaterno))
            {
                errors.Add("Escriba el apellido paterno");

            }
            if (string.IsNullOrWhiteSpace(d.ApellidoMaterno))
            {
                errors.Add("Escriba el apellido materno");
            }
            if (string.IsNullOrWhiteSpace(d.Telefono))
            {
                errors.Add("Escriba el numero de telefono del docente");
            }
            if (d.Edad == 0)
            {
                errors.Add("Coloque la edad del docente");
            }
            if (string.IsNullOrWhiteSpace(d.Correo))
            {
                errors.Add("Coloque un correo");
            }
            if (d.DocenteGrupo.)
            {

            }
            if (d.TipoDocente == 0 || d.TipoDocente > 2)
            {
                errors.Add("Coloca un tipo de docente");
            }
            return errors.Count == 0;
        }

    }
}
