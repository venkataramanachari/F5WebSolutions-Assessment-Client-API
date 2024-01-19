using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientAPI;
using ClientAPI.Models;
using System.Net.Mail;
using System.Net;

namespace ClientAPI.Controllers
{
    public class ClientsController : ControllerBase
    {
        private readonly ClientDBContext _context;

        public ClientsController(ClientDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Api/GetClients")]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
          if (_context.Clients == null)
          {
              return NotFound();
          }
            return await _context.Clients.ToListAsync();
        }

        [HttpGet]
        [Route("api/GetClientById")]
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
          if (_context.Clients == null)
          {
              return NotFound();
          }
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        //[HttpPut]
        //[Route("api/UpdateClient")]
        //public async Task<IActionResult> UpdateClient(int id, Client client)
        //{
        //    if (id != client.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(client).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ClientExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        [HttpPost]
        [Route("api/ManageClient")]
        public async Task<int> ManageClient([FromBody]Client client)
        {
          if (_context.Clients == null)
          {
              return 0;
          }

            else
            {
                if (client.id == 0)
                {
                    _context.Clients.Add(client);
                    await _context.SaveChangesAsync();
                } else
                {
                    var data = await _context.Clients.FindAsync(client.id);
                    if (data != null)
                    {
                        data.name = client.name;
                        data.phone = client.phone;
                        data.email = client.email;
                        data.address = client.address;
                        await _context.SaveChangesAsync();
                    }
                }
            }


            return client.id;
        }

        [HttpGet]
        [Route("api/DeleteClient")]
        public async Task<int> DeleteClient(int id)
        {
            if (_context.Clients == null)
            {
                return 0;
            }
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return 0;
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return id;
        }

        [HttpGet]
        [Route("api/SendMail")]
        public async Task<bool> SendEmailAsync(int id)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();

            var client = await _context.Clients.FindAsync(id);

            message.From = new MailAddress("rajuv162@gmail.com");
            message.To.Add(client.email);
            message.Subject = "Accout Information";
            message.IsBodyHtml = true;
            message.Body = "Your Details is Already Saved in Client Demo";

            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("rajuv162@gmail.com", "wjur cmxn sbiu zczs");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtpClient.Send(message);

            return true;
        }
    }
}
