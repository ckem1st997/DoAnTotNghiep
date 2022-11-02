using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Share.BaseCore.Filters
{
    public class GrpcExceptionInterceptor : Interceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GrpcExceptionInterceptor(IHttpContextAccessor httpContextAccesso)
        {
            _httpContextAccessor = httpContextAccesso;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var metadata = new Metadata();
            string accessToken = "";
            if (_httpContextAccessor.HttpContext != null)
                accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(accessToken) && accessToken.StartsWith("Bearer"))
            {
                metadata.Add("Authorization", accessToken);
            }
            var callOption = context.Options.WithHeaders(metadata);
            context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, callOption);
            return base.AsyncUnaryCall(request, context, continuation);
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
                Log.Error("Error calling via grpc: {Status} - {Message}", e.Status, e.Message);
                return default;
            }
        }
    }
}