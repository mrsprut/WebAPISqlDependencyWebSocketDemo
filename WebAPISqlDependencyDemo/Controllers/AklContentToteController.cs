using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace WebAPISqlDependencyDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AklContentToteController : ControllerBase
    {
        private readonly ILogger<AklContentToteController> _logger;
        private SqlDependency dependency;
        private WebSocket webSocket;

        public AklContentToteController(ILogger<AklContentToteController> logger)
        {
            _logger = logger;
        }
        
        // [DisableCors]
        [HttpGet("/ws")]
        public async Task Get()
        {
            // if (HttpContext.WebSockets.IsWebSocketRequest)
            // {
                using (webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync())
                {
                    _logger.Log(LogLevel.Information, "WebSocket connection established");
                    SubscribeToDbChanges();
                    var buffer = new byte[1024 * 4];
                    WebSocketReceiveResult result;
                    _logger.Log(LogLevel.Information, "Message received from Client");
                    do
                    {
                        result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                        _logger.Log(LogLevel.Information, "Message received from Client");
                    } while (!result.CloseStatus.HasValue);
                    await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription,
                        CancellationToken.None);
                    _logger.Log(LogLevel.Information, "WebSocket connection closed");
                    dependency = null;
                }
            /* }
            else
            {
                HttpContext.Response.StatusCode = 400;
            } */
        }
        
        void SubscribeToDbChanges()
        {
            try
            {
                using SqlConnection connection = new SqlConnection(Appsettings.SQL_SERVER_CONNECTION_STRING);
                SqlDependency.Start(Appsettings.SQL_SERVER_CONNECTION_STRING);
                connection.Open();
                using var command = new SqlCommand(
                    "SELECT [id], [vlenr], [aufnr], [matnr], [charg], [menge_aufnr], [menge_source], [menge_target], [meins], [type_counting], [workstation], [pms_user], [pms_date], [pms_time] FROM [dbo].[AKL_CONTENT_TOTE]",
                    connection
                );
                dependency = new SqlDependency(command);
                dependency.OnChange += OnDependencyChange;
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"id: {reader[0]}; name: {reader[1]}");
                }
                while (dependency != null)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("DB Listening...");
                }
                SqlDependency.Stop(Appsettings.SQL_SERVER_CONNECTION_STRING);
                Console.WriteLine("DB Listening was stopped");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        
        async void OnDependencyChange(object sender, SqlNotificationEventArgs ev)
        {
            // Handle the event (for example, invalidate this cache entry).
            Console.WriteLine($"DB Changed Event Args: {ev.Info} {ev.Source} {ev.Type}");
            if (ev.Info.ToString() == "Insert")
            {
                Fake_SAPContext context = new Fake_SAPContext();
                AklContentTote latestTote =
                    context.AklContentTotes.OrderByDescending(t => t.Id).FirstOrDefault();
                string jsonString = System.Text.Json.JsonSerializer.Serialize(latestTote);
                Console.WriteLine($"jsonString: {jsonString}");
                try
                {
                    await webSocket.SendAsync(
                        new ArraySegment<byte>(Encoding.UTF8.GetBytes(jsonString), 0, jsonString.Length),
                        WebSocketMessageType.Text, true, CancellationToken.None);
                    Console.WriteLine("Insert message was sent");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        
        ~AklContentToteController()
        {
            if (dependency != null)
            {
                SqlDependency.Stop(Appsettings.SQL_SERVER_CONNECTION_STRING);
            }
        }
    }
}