using Calendario.Models;
using Calendario.Repositorio.Interfaces;
using Calendario.Repositorio.Servicios;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;

namespace Calendario.Controllers
{
    public class EventosController : Controller
    {
        private readonly IEventos _eventoRepository;
        private readonly IUsuario _UsuarioRepository;


        public EventosController(IEventos eventoRepository, IUsuario usuarioRepository)
        {
            _eventoRepository = eventoRepository;
            _UsuarioRepository = usuarioRepository;
        }

        public async Task<IActionResult> Index()
        {
            // Obtener el ID del usuario (puede que necesites manejar esto de manera diferente si es asíncrono)
            var usuarioid = _UsuarioRepository.ObtenerUsuarioID();

            // Obtener la lista de eventos de manera asíncrona
            var obtenereventos = await _eventoRepository.GetAllEventosAsync();

            // Pasar la lista de eventos a la vista
            return View(obtenereventos);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Eventos evento)
        {
             evento.UsuarioId = _UsuarioRepository.ObtenerUsuarioID();    
            if (ModelState.IsValid)
            {
                await _eventoRepository.CreateEventoAsync(evento);

                TempData["AlertMessage"] = "Evento creado exitosamente!!!";
                return RedirectToAction("Index");
            }
            return View(evento);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _eventoRepository.GetEventoByIdAsync(id.Value);

            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Eventos evento)
        {
            if (ModelState.IsValid)
            {
                var eventoEncontrado = await _eventoRepository.GetEventoByIdAsync(evento.EventId);
                if (eventoEncontrado == null)
                {
                    return NotFound();
                }

                // Actualizar las propiedades del evento encontrado
                eventoEncontrado.Titulo = evento.Titulo;
                eventoEncontrado.FechaInicio = evento.FechaInicio;
                eventoEncontrado.FechaFin = evento.FechaFin;
                eventoEncontrado.Descripcion = evento.Descripcion;
                eventoEncontrado.Ubicacion = evento.Ubicacion;
                eventoEncontrado.Color = evento.Color;

                // Actualizar el evento en la base de datos
                await _eventoRepository.UpdateEventoAsync(eventoEncontrado);

                TempData["AlertMessage"] = "Evento editado exitosamente!!!";
                return RedirectToAction("Index");
            }

            return View(evento);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var evento = await _eventoRepository.GetEventoByIdAsync(id);

            if (evento == null)
            {
                return NotFound();
            }

            await _eventoRepository.DeleteEventoAsync(id);
            TempData["AlertMessage"] = "Evento eliminado exitosamente!!!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Calendar()
        {
            var eventos = await _eventoRepository.GetAllEventosAsync();
            var items = new List<object>();

            foreach (var evento in eventos)
            {
                var item = new
                {
                    id = evento.EventId,
                    title = evento.Titulo,
                    start = evento.FechaInicio.ToString("yyyy-MM-ddTHH:mm:ss"),
                    end = evento.FechaFin.ToString("yyyy-MM-ddTHH:mm:ss"),
                    ubicacion = evento.Ubicacion,
                    color = evento.Color,
                };
                items.Add(item);
            }

            ViewBag.Eventos = JsonConvert.SerializeObject(items);
            return View();


        }

    }
}
