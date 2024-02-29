using Applications.Enums;
using Applications.Interfaces;
using Applications.Models;
using Infraestructure.Requests;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Infraestructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DockingController(IDockingService dockingService, IMessagePublisher messagePublisher) : ControllerBase
    {
        private readonly IDockingService dockingService = dockingService;
        private readonly IMessagePublisher messagePublisher = messagePublisher;

        [HttpPost("authorize-docking")]
        public async Task<IActionResult> Authorize([FromBody] AuthorizeDockingRequest request)
        {
            var docking = new Docking()
            {
                Status = DockingStatus.AUTHORIZED,
                Muelle = request.Muelle,
                FechaHora = request.FechaHoraDateTime,
                Buque = new Buque()
                {
                    Id = request.IdBuque,
                    Nombre = request.Nombre,
                    Empresa = request.Empresa
                }
            };
            var result = await dockingService.AuthorizeDocking(docking);
            if (result)
            {
                var response = new AuthorizationDockingResponse() 
                {
                    IdDocking = docking.Id,
                    Status  = docking.Status.ToString(),
                    FechaHora = docking.FechaHora,
                    Muelle = docking.Muelle,
                    Empresa = docking.Buque.Empresa,
                    IdBuque = docking.Buque.Id,
                    NombreBuque = docking.Buque.Nombre
                };

                var eventName = "docking-authorized";
                var message = JsonConvert.SerializeObject(response);
                messagePublisher.PublishMessage(eventName, message);
            }

            return Ok(result);
        }


        [HttpPut("cancel-docking")]
        public async Task<IActionResult> Cancel([FromBody] CancelDockingRequest request)
        {
            var result = await dockingService.CancelDocking(request.DockingId);
            if (!result)
                return NotFound($"No se ha encontrado el docking con ID: {request.DockingId}");


            var eventName = "docking-cancelled";
            var message = JsonConvert.SerializeObject(new { request.DockingId, FechaHora = DateTime.Now });
            messagePublisher.PublishMessage(eventName, message);

            return Ok(result);
        }


    }
}
