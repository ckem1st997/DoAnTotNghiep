using Grpc.Core.Interceptors;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Grpc.Core.Interceptors.Interceptor;
using Serilog;

namespace Share.Base.Core.Grpc
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

            try
            {
                var metadata = new Metadata();
                string accessToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"] ?? "";
                if (!string.IsNullOrWhiteSpace(accessToken) && accessToken.StartsWith("Bearer"))
                {
                    metadata.Add("Authorization", accessToken);
                }
                var callOption = context.Options.WithHeaders(metadata);
                context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, callOption);
                return base.AsyncUnaryCall(request, context, continuation);
            }
            catch (RpcException e)
            {
                Log.Error("Error calling via grpc: {Status} - {Message}", e.Status, e.Message);
                throw e;
            }

        }

     
    }
}