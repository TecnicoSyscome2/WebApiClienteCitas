using WebApi.Citas.ClientesApp.DAL;
using WebApi.Citas.ClientesApp.MailBoxService;
using WebApi.Citas.ClientesApp.Modelos;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using Microsoft.AspNetCore.Cors;

namespace WebApi.Citas.ClientesApp.Controllers.cita
{
    [Route("api/Syscome/cita")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class frm_CitasController : ControllerBase
    {
        private readonly CitasDAL _citaService;
        private readonly ServicioEmailBox _mailBox;
        private static readonly Random _random = new Random();
        public frm_CitasController(CitasDAL citaService, ServicioEmailBox mailbox)
        {
            _citaService = citaService;
            _mailBox = mailbox;
        }

        public static string GenerarCodigoTicket()
        {
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringBuilder = new StringBuilder("#");

            for (int i = 0; i < 8; i++)
            {
                int indice = _random.Next(caracteres.Length);
                stringBuilder.Append(caracteres[indice]);
            }

            return stringBuilder.ToString();
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> CrearCita([FromBody] CitasModel nuevaCita)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var cita = await _citaService.CrearCita(nuevaCita);
                string ticketnumber = GenerarCodigoTicket();
                var mensaje = $"Gracias por reservar tu cita. Tu número de ticket es: {ticketnumber}-{cita.Id}.";

                //if (cita.Cliente?.Email != null)
                //{
                //    await _mailBox.EnviarCorreoAsync(cita.Cliente.Email, "Confirmación de Cita", mensaje);
                //}
                if (cita.Correo != null)
                {
                    await _mailBox.EnviarCorreoAsync(cita.Correo, "Confirmación de Cita", mensaje);
                }

                return CreatedAtAction(nameof(ObtenerCitaPorId), new { id = cita.Id }, cita);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }



        //[HttpPost]
        //public async Task<IActionResult> CrearCita([FromBody] CrearCitaRequestModel request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        // Crear la cita principal
        //        var cita = await _citaService.CrearCita(request.Cita, request.DetallesCita);

        //        // Generar número de ticket
        //        string ticketnumber = GenerarCodigoTicket();
        //        var mensaje = $"Gracias por reservar tu cita. Tu número de ticket es: {ticketnumber}-{cita.Id}.";

        //        // Enviar correo si el cliente tiene email registrado
        //        if (cita.Cliente?.Email != null)
        //        {
        //            await _mailBox.EnviarCorreoAsync(cita.Cliente.Email, "Confirmación de Cita", mensaje);
        //        }

        //        return CreatedAtAction(nameof(ObtenerCitaPorId), new { id = cita.Id }, cita);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(new { mensaje = ex.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
        //    }
        //}

        //public class CrearCitaRequestModel
        //{
        //    public CitasModel Cita { get; set; } // Cita principal
        //    public List<CitasDetModel> DetallesCita { get; set; } // Lista de detalles de la cita
        //}



        [HttpGet]
        public async Task<IActionResult> ObtenerCitas()
        {
            var citas = await _citaService.ObtenerCitas();

            if (citas == null || !citas.Any())
            {
                return NotFound(new { mensaje = "No se encontraron citas." });
            }

            return Ok(citas);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCitaPorId(long id)
        {
            var cita = await _citaService.ObtenerCitaPorId(id);

            if (cita == null)
            {
                return NotFound(new { mensaje = $"La cita con ID {id} no existe." });
            }

            return Ok(cita);
        }


        //[HttpPut("{id}")]
        //public async Task<IActionResult> ActualizarCita(long id, [FromBody] CitasModel citaActualizada)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        var cita = await _citaService.ActualizarCita(id, citaActualizada);

        //        if (cita == null)
        //        {
        //            return NotFound(new { mensaje = $"La cita con ID {id} no existe." });
        //        }

        //        return Ok(cita);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(new { mensaje = ex.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
        //    }
        //}


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> EliminarCita(int id)
        //{
        //    try
        //    {
        //        var eliminado = await _citaService.EliminarCita(id);

        //        if (!eliminado)
        //        {
        //            return NotFound(new { mensaje = $"La cita con ID {id} no existe." });
        //        }

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
        //    }
        //}

    }
}
