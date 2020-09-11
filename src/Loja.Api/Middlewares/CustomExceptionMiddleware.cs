using Loja.CrossCutting.Dto;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Loja.Api.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly JsonSerializer _serializer;        

        public CustomExceptionMiddleware()
        {
            _serializer = new JsonSerializer
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };        
        }

        public async Task Invoke(HttpContext context)
        {
            var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (ex == null) return;

            using (var writer = new StreamWriter(context.Response.Body))
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var message = !string.IsNullOrEmpty(ex.Message) ? ex.Message : "Erro interno de sistema.";
                
                _serializer.Serialize(writer, ResultDto<bool>.Error(message));
                await writer.FlushAsync().ConfigureAwait(false);
            }
        }
       
    }
}
