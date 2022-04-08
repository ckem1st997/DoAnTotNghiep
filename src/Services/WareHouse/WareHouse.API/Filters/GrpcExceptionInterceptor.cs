using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WareHouse.API.Filters
{
    public class GrpcExceptionInterceptor : Interceptor
    {
        private readonly ILogger<GrpcExceptionInterceptor> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GrpcExceptionInterceptor(ILogger<GrpcExceptionInterceptor> logger, IHttpContextAccessor httpContextAccesso)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccesso;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var metadata = new Metadata();
            string accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(accessToken) && accessToken.StartsWith("Bearer"))
            {
                metadata.Add("Authorization", accessToken);
            }
            var callOption = context.Options.WithHeaders(metadata);
            //   context.Options.Headers.Add("Authorization", accessToken);

            context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, callOption);

            return base.AsyncUnaryCall(request, context, continuation);
            //    var call = continuation(request, context);

            //    return new AsyncUnaryCall<TResponse>(HandleResponse(call.ResponseAsync), call.ResponseHeadersAsync, call.GetStatus, call.GetTrailers, call.Dispose);
            //}
        }

        private async Task<TResponse> HandleResponse<TResponse>(Task<TResponse> t)
        {
            try
            {
                var response = await t;
                return response;
            }
            catch (RpcException e)
            {
                _logger.LogError("Error calling via grpc: {Status} - {Message}", e.Status, e.Message);
                return default;
            }
        }
    }
}